---
# You can also start simply with 'default'
theme: seriph
# random image from a curated Unsplash collection by Anthony
# like them? see https://unsplash.com/collections/94734566/slidev
background: https://cover.sli.dev
# some information about your slides (markdown enabled)
title: Welcome to Slidev
info: |
  ## Slidev Starter Template
  Presentation slides for developers.

  Learn more at [Sli.dev](https://sli.dev)
# apply unocss classes to the current slide
class: text-center
# https://sli.dev/features/drawing
drawings:
  persist: false
# slide transition: https://sli.dev/guide/animations.html#slide-transitions
transition: slide-left
# enable MDC Syntax: https://sli.dev/features/mdc
mdc: true
---

# Making Impossible States Impossible in C#


---
transition: fade-out
---

# About me Slide

TODO

---
transition: slide-up
---

# Making Impossible States Impossible?
## WTF does that even mean?

<!--
- a lot of you are probably wondering what I a am meaning when I say Making impossible states impossible
-->

----

TODO - What I mean by impossible states

<!--
  In front end applications there are a lot of states that that components should never been in in the first place. 
  Like - displaying an error messsage while simulatensoutly displaying a result.
  Sometthing that should never happen.  It is an impossible state based on the requirements, but it does happen. And now us as developers have to track down why - and fix it.

  TODO - find web page example of this actually happening. 

  What if we could just make those weird states impossible to get into in the first place, by modelling our state in a differnt way?
  
-->

---

TODO - Goals of this talk

<!--
That is the goal of this talk.  Hopefully you all walk away wth a new way of thinking about modeling application state.

-->

--- 

TODO - real world example slide

<!--
To demonstrate this.  I am going to walk you through a real world example of a piece of code I worked on at Hunter Engeering and how it evolved as requirements changed.
-->

---

# Requirements

1. Send an image to a service
   - The service will:
     - See if there is a license plate in the image
     - Send back information about the license plate, including the a composite image of the plate
1. Display the returned license plate info on the screen
1. Display an error message on the screen if an error is returned
1. Retry logic if any errors returned from the service

<!--
- Okay - so we get our user story for a new feature and look over the requirements.
- Before I go into the requriemtns - this is centered around a feature in one of our desktop application about License Plate Recognition. We do a lot of LPR stuff at hunter so we can try and eventually do a VIN lookup ona a vehicle so we know the aligment speficiations.  
   - to get the VIN so we can do the spec look up we attempt to get the license plate of the vehicle so we can then call a third parryservice that will do the VIN lookup based on the licesne plate characaters.

- Anways - this feature is a sort of calibratioon/test features so we can see on screen that the plate recognition is working based on the image.

-->

---
transition: slide-up
---

# First Pass
```csharp

@if(_loading) {
  <h3>Loading...</h3>
}

@if(_licensePlateResult is not null) {
    <p>
        @_licensePlateResult.Text
    </p>
}

@if(_error) {
    <h3>Error Getting License Plate</h3>
    <button onclick="GetLicensePlate()">Retry</button>
}

@code {
    private LicensePlateResult _licensePlateResult;
    private bool _loading;
    private bool _error;

    private async Task GetLicensePlate() 
    {
        try
        {
            _loading = true;
            _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
            _loading = false;

        }
        catch (Exception ex)
        {
            _loading = false;
            _error = true;
        }
    }
}
```

<!--
- Note this code isnt the exact code we wrote. 
TODO - Split up, add image of what it would look like rendered..

Decent job - code does what it is supposed to do. we shi it off to QA.

(Next Slide)
-->

---
transition: slide-up
---

# Bug
If an error occurs, and you retry and get a "Success" error message isnt cleared

<!--
QA - looks at it and we get our first bug on it.

If an error occurs, then we get back a license plate. the error message isnt cleared.

Alright - simple enough fix. 
-->

---
transition: slide-up
---

# Fix

## Simple
- just set **_error** boolean to false before retrying.

```csharp {monaco-diff}
 private async Task GetLicensePlate() 
    {
        try
        {
            _loading = true;
            _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
            _loading = false;
        }
        catch (Exception ex)
        {
            _error = true;
        }
    }
~~~
 private async Task GetLicensePlate() 
    {
        try
        {
            _error = false;
            _loading = true;
            _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
            _loading = false;
        }
        catch (Exception ex)
        {
            _loading = false;
            _error = true;
        }
    }
```

<!--
TODO - Add in animation of code now working??

- Simple enough fix - Just set the _error boolean to false. Easy enough
-->

---

# Better fix 
- Swap the booleans for an enumeration

<!--

However - at this point there is a just as easy fix we can do - removing the booleans and using an enumartion instead.
This:
I feel like makes the code easer to reason about what state it is in getting rid of the booleans 

Often times when people are coding on the front end - they go for booleans first.
I would like to encourage to go towards representing the state in an enumration rather than a boolean even for simple stuff.
-->

---

# Better Fix - Code example

````md magic-move
```csharp 
 private async Task GetLicensePlate() 
    {
        try
        {
            _error = false;
            _loading = true;
            _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
            _loading = false;
        }
        catch (Exception ex)
        {
            _loading = false;
            _error = true;
        }
    }
```
```csharp
  private async Task GetLicensePlate() 
    {
        try
        {
            _state = State.Loading;
            _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
            _state = State.Initialized;

        }
        catch (Exception ex)
        {
            _state = State.Error;
        }
    }

    enum State
    {
        Loading,
        Error,
        Initialized
    };
```
````

<!--
 - Here is what the code handler for getting the licens eplate looks like before the fix.
 - Here is what the handler looks like now.
 - Simply declaring an Enum at the bottom and now we can get rid of all of the boolean toggling we were doing in the first place and this prevents the original bug from happening
-->


---

# Better Fix - Code example

````md magic-move
```csharp 
@if(_loading) {
    <h3>Loading...</h3>
}

@if(_licensePlateResult is not null) {
    <p>
        @_licensePlateResult.Text
    </p>
}

@if(_error) {
    <h3>Error Getting License Plate</h3>
    <button onclick="@GetLicensePlate()">Retry</button>
}
```
```csharp
@switch (_state)
{
    case State.Loading:
        <h3>Loading...</h3>
        break;
    case State.Initialized:
        <p>
            @if (_licensePlateResult is not null)
            {
                @_licensePlateResult.Text
            }
            else
            {
                <span>No Plate Found</span>
            }
        </p>
        break;
    case State.Error:
        <h3>Error Getting License Plate</h3>
        <button onclick="@GetLicensePlate()">Retry</button>
        break;
}
```
````

<!--
- Here is also a before and after of the actual Html
- now I can easilyl just do a switch on the state and render what is needed.
-->

---
transition: slide-up
---

# New requirements

License Plate Recoginition Service will now send back differnt types of error codes, if an error happens and the UI will need to display the code.

Error codes:
- Not Licensed
- Image Path not found
- Error (Catch all error)

<!--
- Okay.. so we got that working now and I feel like we ar ein a much better state.
- Few months later, product management comes back with some requirements for this feature.
-->

---
transition: slide-up
---

# Updated Code

````md magic-move
```csharp 
private State _state = State.Loading;

private async Task GetLicensePlate() 
{
    try
    {
        _state = State.Loading;
        _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
        _state = State.Initialized;

    }
    catch (Exception ex)
    {
        _state = State.Error;
    }
}
```
```csharp
private State _state = State.Loading;
private LicensePlateError? _error;

private async Task GetLicensePlate() 
{
    try
    {
        _state = State.Loading;
        _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
        _state = State.Initialized;

    }
    catch (LicensePlateException ex)
    {
        _state = State.Error;
        _error = ex.ErrorReason;
    }
    catch (Exception ex)
    {
        _state = State.Error;
        _error = LicensePlateError.Generic;
    }
}
```
````


<!--
  - New requiments seem simple enough.  
  - This license plate service has a new exception it throws that returns the error code.
  - we now have a reference to a new enum representing the error code
  - we are catching the special exception setting the erroc code from it
  - then just updating the generic exception just incase and handling the error code appropriately
-->

---

#TODO - UI example

<!--
 Here is what the UI looks like now.
  - Overall - Doesn't look too bad. Easy enough to reason about from top to bottom.

-->

---
transition: slide-up
---

# New Bug

---
transition: slide-up
---

# Problems with current code

- still Getting into states that should be impossible 
- States can accidently access data it shouldnt know about 
  - When going into a new state, have to remember to "clear" out data not associated with the new state
  - A state should only know about the data it needs to know about. 
- No Exhaustive pattern matching on states
- As state becomes more complex, it becomes harder to reason about.

---
transition: slide-up
---

# What can we do about it?

TODO - some gif...

---
transition: slide-up
---

# Discriminated unions / Tagged unions / Algebaric Data types / Sum types

<!--
 Differnt langues calls this differnt things and they each have their differnet meaning
 But the they important thing is that this is a native feature in those languages.

 - Show of hands - has anyone ever heard of these terms before?
-->

---

A data structure used to hold a value that could take on several different, but __fixed__, types. Only __one__ of the types can be in use at any one time

<!--
 This feature looks a bit differnt in the languages, a simple definitoin of it is

 A data structure used to hold a value that could take on several different, but __fixed__, types. Only __one__ of the types can be in use at any one time

 This gives us my wish of being able to define a type of this or that.
-->

---
# Breaking down the name of "Discriminated Unions"

A discriminated union is called that because:
- Union: It is a type that can hold (or "be") one of several different, but fixed, types—like a union of possibilities.
- Discriminated: Each possible case (variant) is "tagged" or "labeled" with a unique identifier (the discriminant), which allows you to distinguish (or "discriminate") which variant the value currently holds.
   - This "tag" enables the compiler and the programmer to safely determine which data is present and how to handle it.

<!--
  - Union: It is a type that can hold (or "be") one of several different, but fixed, types—like a union of possibilities.
  - Discriminated: Each possible case (variant) is "tagged" or "labeled" with a unique identifier (the discriminant), which allows you to distinguish (or "discriminate") which variant the value currently holds.
-->




---

---

# Examples in other languages

<div class="grid grid-cols-3 gap-3">
<div>
## TypeScript
<span>Called "Union Types" in TypeScript</span>
```typescript
type Shape =
  | { kind: "circle"; radius: number }
  | { kind: "rectangle"; width: number; height: number }
  | { kind: "square"; side: number };

// Example usage:
function area(shape: Shape): number {
  switch (shape.kind) {
    case "circle":
      return Math.PI * shape.radius * shape.radius;
    case "rectangle":
      return shape.width * shape.height;
    case "square":
      return shape.side * shape.side;
  }
}

```
</div>

<div>
## F#
<span>Called "Discriminated Unions" in F#</span>
```fsharp
type Shape =
  | Circle of radius: float
  | Rectangle of width: float * height: float
  | Square of side: float

// Example usage:
let area shape =
  match shape with
  | Circle r -> System.Math.PI * r * r
  | Rectangle (w, h) -> w * h
  | Square s -> s * s

```
</div>

<div>
## Rust
<span>Called "Enums" in Rust</span>
```rust
enum Shape {
    Circle { radius: f64 },
    Rectangle { width: f64, height: f64 },
    Square { side: f64 },
}

fn area(shape: &Shape) -> f64 {
    match shape {
        Shape::Circle { radius } => std::f64::consts::PI * radius * radius,
        Shape::Rectangle { width, height } => width * height,
        Shape::Square { side } => side * side,
    }
}
```
</div>

</div>


<!--
- Here is what this looks like in a few other languages. 
- Not going to spend to much time on this, but want to give you an idea of what it looks like in other languages
- All of these examples are defining the same type in the end which is a shape.
- and a function that takes in the shape - matching on it to calcualte the area.

- Notices - that the on the typescript example it is a bit more verbose. 
as unions in it are "Untagged" you cannot discriciminate the type when matching unless you tag a property on the object

-->

---
transition: slide-up
---
# Lets use it in C#
- 😔 not nativly supported....yet
- Proposel has been announced though
  - TODO link proposal

<!--
- Great - now that we hve the structure we want to use, lets implement it in c#.
- Well - Not natively supported yet...
- As of about a year ago - they have announced a proposel of how they would like to implment this in the languague. No release date yet. but they are working on it.
   - I am going to be going a bit more detail into this spec later so you see how it may be implemented. 
- If you have ever lurked online C# communiteies this always seems to be the feature that others are waiting for to be implemented in the language,

-->

---
transition: slide-up
---
# Libraries 
- OneOf
- Dunet

<!--
There are a few libraries that implement a take on Discriminated Unions in C#

- OneOf and Du

- Has anyone ever heard of these or actually used them?
- OneOf is one we are actually using at my work now to solve this problem
- Du is another one that I found while doing research for this talk

- Going to be go over each of them here shortly.

-->

---

#OneOf

> This library provides F# style discriminated unions for C#, using a custom type OneOf<T0, ... Tn>. 
> An instance of this type holds a single value, which is one of the types in its generic argument list.

```csharp
record Circle(double Radius);
record Rectangle(double Length, double Width);
record Triangle(double Base, double Height);

....

public class Shape : OneOfBase<Circle, Rectangle, Triangle>
{
    Shape(<Circle, Rectangle, Triangle> _) : base(_) { }
}

... 

public static double Area(Shape shape) {
  return shape.Match(
    circle => 3.14 * circle.Radius * circle.Radius,
    rectangle => rectangle.Length * rectangle.Width,
    triangle => triangle.Base * triangle.Height / 2
  );
}

var shape = new Shape(new Circle(10));

var area = 
Console.WriteLine(area); // "12"

```

---
transition: slide-up
---
# Updating current code to use One Of


---

# Dunet

> Dunet is a simple source generator for discriminated unions in C#.

```csharp
[Union]
partial record Shape
{
    partial record Circle(double Radius);
    partial record Rectangle(double Length, double Width);
    partial record Triangle(double Base, double Height);
}

....

public static double Area(Shape shape) {
  return shape.Match(
    circle => 3.14 * circle.Radius * circle.Radius,
    rectangle => rectangle.Length * rectangle.Width,
    triangle => triangle.Base * triangle.Height / 2
  );
}

var shape = new Shape.Rectangle(3, 4);
var area = Area(shape);

Console.WriteLine(area); // "12"

```

---

# Updating current code to use Dunet

---



---

# Diving into current discriminated C# proposal


---





