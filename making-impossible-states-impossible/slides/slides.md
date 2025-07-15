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

TODO - Hunter slide with hiring link
---
layout: section
transition: slide-up
---


# Making Impossible States Impossible?


<!--
- a lot of you are probably wondering what I a am meaning when I say Making impossible states impossible
-->

---
layout: section
---

# States your application should never be in.

<v-clicks>

1. Displaying Loading indicator while displaying a result.
1. Displaying a Success message while displaying an error message.
1. Not resetting a field when others are cleared.

</v-clicks>


<!--
  In applications there are a lot of states that the application should never been in in the first place. 
  Like - displaying an error messsage while simulatensoutly displaying a result.
  
  
  Thes are simple things that should never happen.  Impossible states  based on the requiremen - yet they do.

  (NEXT SLIDE)
  
-->

---


## Most likely occurr as a by-product of how you are storing the state in the application.

<!--
These types of bug are usually by-products of how you are storing the state in the application.

  What if we could just make those weird states impossible to get into in the first place, by modelling our state in a differnt way?

  (NEXT SLIDE)

-->

---

TODO - Goals of this talk
1. new way of thinking about modeling application state

<!--
That is the goal of this talk.  Hopefully you all walk away wth new ways of how you can model application state. which will hopefuly avoid these types of bugs alltogether.

To demonstrate this.  I am going to walk you through a feature I worked on at Hunter Engineering and how it evolved as requirements changed.
Note - The code didn't end up exactly in this final state I am proposing - but it is similar. 

(Next Slide)

-->

--- 

# Requirements
## Test License Plate Recognition Feature
1. Receive "live" image bytes streamed from a camera
   - If error connecting to camera - display error message.
   - Otherwise - display the image on the screen.
1. "Test License Plate Recognition" button on screen that when clicked will:
   - Send current image bytes to a service
        - The service will:
            - See if there is a license plate in the image
            - Send back information about the license plate, including the a composite image of the plate
1. Display the returned license plate info on the screen in a "Test Results" section
1. If no plate is found, display a message saying "No plate found"
1. Display an error message on the screen if an error is returned
1. User should be able to retry this test if an error is returned by clicking the "Test License Plate Recognition" button again

<!--
- Okay - so we get our user story for a new feature and look over the requirements.
- Before I go into detail of the requirements - this is centered around a feature in one of our desktop application about License Plate Recognition. We do a lot of LPR stuff at hunter so we can try and eventually do a VIN lookup on a vehicle so we know the aligment speficiations.  
   - So in order to get the VIN -  we attempt to recognize the license plate of the vehicle so we can then call a third party service that will do the VIN lookup based on the licesne plate characaters.

- Anways - this feature is a sort of calibration/test feature used when settinup up our equipment. Just so we know the camera is aimed correctly.


-->

---

# Lets Build it
TODO - some gif


---
layout: section
---

# Building the View Model


<!--
 - What we will be focussing on building here is the ViewModel that would be used in the view,
 Exposing the necessary properties and methods to the view.  
-->

---
layout: section
transition: view-transition
---

# 1st Requirement - Receiving live image bytes {.inline-block.view-transition-title}

---

# 1st Requirement - Receiving live image bytes {.inline-block.view-transition-title}

```csharp {all|9-13|11|12|15-21|17|18|19|}
public class LicensePlateTestViewModel(ImageService imageService)
{
    private ImageService _imageService = imageService;

    public byte[] ImageBytes = [];
    public bool IsLoadingImage;
    public bool ErrorFetchingImage;

    public void Init()
    {
        IsLoadingImage = true;
        _imageService.ImageReceived += OnImageReceived;
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        IsLoadingImage = false;
        ImageBytes = e.ImageBytes;
        ErrorFetchingImage = e.ErrorRetrievingImage;
    }
}
```

<!--
- Note this code isnt the exact code we wrote. 
- Code is pretty self explanatory.
- we have an init method.
- we set loading to true
- we subscribe to an event from am image service we are provided that is providing the "live" image every so often as image bytes.
- It emits the "live" image every so often as image bytes.
- once we receive an image we set loading false, and set the image bytes.
- if we have an error - we set another error flag.
- All pretty straightforward.

(Next Slide)
-->

---
layout: section
transition: view-transition
---

# 2nd Requirement - "LPR Test" {.inline-block.view-transition-title}

---

# 2nd Requirement - "LPR Test" {.inline-block.view-transition-title}

```csharp {all|10-12|19-33|24-26|28-32|}{maxHeight:'75vh'}
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public byte[] ImageBytes = [];
    public bool IsLoadingImage;
    public bool ErrorFetchingImage;
    
    public string? LicensePlateText;
    public bool IsLoadingLpr;
    public bool IsLprError;

    public void Init()
    {
        IsLoadingImage = true;
        _imageService.ImageReceived += OnImageReceived;
    }
    
    public async Task GetLicensePlate() 
    {
        try
        { 
            IsLoadingLpr = true;
            LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
            IsLoadingLpr = false;
        }
        catch (Exception ex)
        {
            IsLoadingLpr = false;
            IsLprError = true;
        }
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        IsLoadingImage = false;
        ImageBytes = e.ImageBytes;
        ErrorFetchingImage = e.ErrorRetrievingImage;
    }
}
```

<!--
Now that we got that written - lets implement the calls to for actaully Testing the LPR.

- we have some more properties on the view model related to the LPR test.
- we are exposing a new method for executing the LPR test
- In it are are setting loading and results when we get them
- then setting some more flags if we get an error.

- Now perfect - feature is done regarding the View Model. 
- we write the view, some tests - then ship it off to QA for testing.
-->

---
layout: section
transition: view-transition
---

# 🪲 Bug

<v-click>

## If an error occurs when attempting to get the place, and the user clicks  "retry" and get a a plate back, the error message isn't cleared

</v-click>

<!--
QA - looks at it and we get our first bug on it.

If an error occurs, then we get back a license plate. the error message isnt cleared.
We are getting into a state that should be impossible.

-->

---
layout: center
---

```csharp
public async Task GetLicensePlate() 
{
    try
    { 
        IsLoadingLpr = true;
        LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
        IsLoadingLpr = false;

    }
    catch (Exception ex)
    {
        IsLoadingLpr = false;
        IsLprError = true;
    }
}
```

<!--
Okay - so we fire up the debugger to dive into the code. 
Looking at our Model - must be a senenario some how where we are not resetting the IsLprError boolean.

- Simple enough to write an automated test for. So we write a failing test. Now lets make the fix.

-->

---
transition: slide-up
---

# Fix

## Simple
- Always set **IsLprError** boolean to false before firing off the call to the LPR service.

<v-click>

```csharp {monaco-diff}
public async Task GetLicensePlate() 
{
    try
    { 
        IsLoadingLpr = true;
        LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
        IsLoadingLpr = false;
    }
    catch (Exception ex)
    {
        IsLoadingLpr = false;
        IsLprError = true;
    }
}
~~~
public async Task GetLicensePlate() 
{
    try
    { 
        IsLprError = false;
        IsLoadingLpr = true;
        LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
        IsLoadingLpr = false;
    }
    catch (Exception ex)
    {
        IsLoadingLpr = false;
        IsLprError = true;
    }
}
```
</v-click>

<!--

- Simple enough fix - Just set the IsLprError boolean to false always before getting the license plate. Easy enough
-->

---
layout: section
---

# Is there a better fix?
<v-click>

## Yes - Swap the booleans for an <span class="bg-yellow-500 p-1">enumeration</span>

</v-click>

<!--

Is there a better fix?
Could we have avoided this bug alltogether?

IMO - Yes - Swap the booleans for an enumeration instead.

With our current implentation we are starting to get into boolean hell, and this gets us out of it.
  
Often times when people are coding on the front end - they go for booleans first.
I would like to encourage you all to thinkg about representing  state in an enumration rather than a boolean even for simple stuff.

-->

---

# Better Fix - Code example

````md magic-move
```csharp
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public byte[] ImageBytes = [];
    public bool IsLoadingImage;
    public bool ErrorFetchingImage;
    
    public string? LicensePlateText;
    public bool IsLoadingLpr;
    public bool IsLprError;

    public void Init()
    {
        IsLoadingImage = true;
        _imageService.ImageReceived += OnImageReceived;
    }
    
    public async Task GetLicensePlate() 
    {
        try
        { 
            IsLoadingLpr = true;
            LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
            IsLoadingLpr = false;

        }
        catch (Exception ex)
        {
            IsLoadingLpr = false;
            IsLprError = true;
        }
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        IsLoadingImage = false;
        ImageBytes = e.ImageBytes;
        ErrorFetchingImage = e.ErrorRetrievingImage;
    }
}
```
```csharp 
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public byte[] ImageBytes = [];
    public ImageState imageState;
    
    public string? LicensePlateText;
    public LprState lprState = LprState.Received;
    
    public void Init()
    {
        imageState = ImageState.Loading;
        _imageService.ImageReceived += OnImageReceived;
    }
    
    public async Task GetLicensePlate() 
    {
        try
        {
            lprState = LprState.Loading;
            LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
            lprState = LprState.Received;
        }
        catch (Exception ex)
        {
            lprState = LprState.Error;
        }
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        imageState = e.ErrorRetrievingImage ? ImageState.Error : ImageState.Received;
        ImageBytes = e.ImageBytes;
    }
    
    public enum ImageState
    {
        Loading,
        Error,
        Received
    }

    public enum LprState
    {
        Loading,
        Error,
        Received
    }
}
```
````

<!--
 - Here is what the code handler for getting the licens eplate looks like before the fix.
 - Here is what the handler looks like now.
 - Simply declaring an Enum at the bottom and now we can get rid of all of the boolean toggling we were doing in the first place and this prevents the original bug from happening

 TODO - Code highlighting and explaining.
-->

---
layout: section
transition: slide-up
---

# New requirements

<v-click>

## License Plate Recoginition Service will now send back error codes, if an error happens and the UI will need to display the error message.

Error codes:
- Not Licensed
- No Credits
- Generic (Catch all error)

</v-click>

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
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public byte[] ImageBytes = [];
    public ImageState imageState;
    
    public string? LicensePlateText;
    public LprState lprState = LprState.Received;
    
    public void Init()
    {
        imageState = ImageState.Loading;
        _imageService.ImageReceived += OnImageReceived;
    }
    
    public async Task GetLicensePlate() 
    {
        try
        {
            lprState = LprState.Loading;
            LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
            lprState = LprState.Received;
        }
        catch (Exception ex)
        {
            lprState = LprState.Error;
        }
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        imageState = e.ErrorRetrievingImage ? ImageState.Error : ImageState.Received;
        ImageBytes = e.ImageBytes;
    }
}
```
```csharp
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public byte[] ImageBytes = [];
    public ImageState imageState;
    
    public string? LicensePlateText;
    public LprState lprState = LprState.Received;
    public LicensePlateError? LprErrorReason;

    public void Init()
    {
        imageState = ImageState.Loading;
        _imageService.ImageReceived += OnImageReceived;
    }
    
    public async Task GetLicensePlate() 
    {
        try
        {
            lprState = LprState.Loading;
            LicensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
            lprState = LprState.Received;
        }
        catch (LicensePlateException ex)
        {
            lprState = LprState.Error;
            LprErrorReason = ex.ErrorReason;
        }
        catch (Exception ex)
        {
            lprState = LprState.Error;
            LprErrorReason = LicensePlateError.Generic;
        }
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        imageState = e.ErrorRetrievingImage ? ImageState.Error : ImageState.Received;
        ImageBytes = e.ImageBytes;
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
layout: section
transition: view-transition
---

# Problems with current code {.inline-block.view-transition-title}


<!--
Now - this code does look a lot better, but as the requiremnt change - cracks are starting to grow with how everything is implemented.
-->

---

# Problems with current code {.inline-block.view-transition-title}

1. You can still get into states that should be impossible 

```csharp
public ImageState imageState;
public LprState lprState = LprState.Received;
public LicensePlateError? LprErrorReason;
```

<!--
In our view model - we have 3 different fields related to state.
Though it is unlikely - it is possible to still get in a state where we have a LPR error message and a License plate text.




-->

---

# Problems with current code {.inline-block.view-transition-title}

2. States can accidently access data it shouldnt know about 
   - When going into a new state, have to remember to "clear" out data not associated with the new state
   - A state should only know about the data it needs to know about. 

<!--
- Not all states should have access to the LicensePlate Text or even the method to make the call to Get the license plate data, yet how it is currently coded - we allow for it.
- We are having to remember to clear differnt error states when fetching new data.
-->

---

# Problems with current code {.inline-block.view-transition-title}


3. No Exhaustive pattern matching on states

<!--
If you added a new enumeration - it is up to you to remember to touch the UI to handle it wherever it is being used. We cant easily enforice to not compile if we forget to handle it.
-->

---

# Problems with current code {.inline-block.view-transition-title}

4. As state becomes more complex, it becomes harder to reason about.

<!--
- We are already starting to see this now.  it is not that obvious to tell what fields go with which state.
-->

---
layout: center
transition: slide-up
---

# What I am looking for is a way to decribe a type as being one of a set number of things while not leaking the the data of the type to the others.

## A way to describe a type as "this, or that, or this other thing"

---

# What can we do about it?

TODO - some gif...

---
layout: section
transition: slide-up
---

# Discriminated unions / 
# Tagged unions / 
# Algebaric Data types / 
# Sum types

<!--
 Differnt langues calls this differnt things and they each have their differnet meaning
 But the they important thing is that this is a native feature in those languages.

 - Show of hands - has anyone ever heard of these terms before?
-->

---

A data structure used to hold a value that could take on several different, but __fixed__, types. Only __one__ of the types can be in use at any one time. 
Each type can optinally cary its own data.

<!--
 Based on the language you are using - This feature looks differnt a simple definitionn of it is

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

TODO - Benefits of discriminated unions

---
layout: section
transition: slide-up
---

# Lets use it in C#

<!--
- Great - now that we hve the structure we want to use, lets implement it in c#.
-->

---
layout: section
---

# 😔 not nativly supported<v-click>...yet</v-click>

<v-click>

## Proposel has been announced 🎉
  
</v-click>

<!--
- Well - Not natively supported yet...
- As of about a year ago - they have announced a proposel of how they would like to implment this in the languague. No release date yet. but they are working on it.
   - I am going to be going a bit more detail into this spec later so you see how it may be implemented. 
- If you have ever lurked online C# communities this always seems to be the feature that others are waiting for to be implemented in the language,

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

# OneOf

> This library provides F# style discriminated unions for C#, using a custom type OneOf<T0, ... Tn>. 
> An instance of this type holds a single value, which is one of the types in its generic argument list.

---

# OneOf

```csharp
record Circle(double Radius);
record Rectangle(double Length, double Width);
record Triangle(double Base, double Height);

....
    
public static double Area(OneOf<Circle, Rectangle, Triangle> shape) {
    return shape.Match(
        circle => 3.14 * circle.Radius * circle.Radius,
        rectangle => rectangle.Length * rectangle.Width,
        triangle => triangle.Base * triangle.Height / 2
    );
}
... 

OneOf<Circle, Rectangle, Triangle> shape = new Circle(10);

var area = Area(shape);
Console.WriteLine(area); // "12"

```

---
layout: section
transition: view-transition
---

# Updating current code to use OneOf  {.inline-block.view-transition-title}

---

# Updating current code to use OneOf {.inline-block.view-transition-title}

```csharp {all|6|14-24|18|22|27|28|30-50|33-49|32|37-39|43,47|52-55|all}{maxHeight:'75vh'}
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public OneOf<LoadingImage, ErrorRetrievingImage, DisplayingImage>  LicensePlateTestState;
    
    public void Init()
    {
        LicensePlateTestState = new LoadingImage();
        _imageService.ImageReceived += OnImageReceived;
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        if (e.ErrorRetrievingImage)
        {
            LicensePlateTestState = new ErrorRetrievingImage();
        }
        else
        {
            LicensePlateTestState = new DisplayingImage(e.ImageBytes,  _licensePlateService);
        }
    }
}

public record LoadingImage;
public record ErrorRetrievingImage;

public record DisplayingImage(byte[] ImageBytes, LicensePlateService _licensePlateService)
{
    public  OneOf<LprUnknown, LprLoading, LprReceived, LprError> LprState = new LprUnknown();
    public async Task GetLicensePlate()
    {
        try
        {
            LprState = new LprLoading();
            var licensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
            LprState = new LprReceived(licensePlateText);
        }
        catch (LicensePlateException ex)
        {
            LprState = new LprError(ex.ErrorReason);
        }
        catch (Exception ex)
        {
            LprState = new LprUnknown();
        }
    }
};

public record LprUnknown;
public record LprLoading;
public record LprReceived(string licensePlateText);
public record LprError(LicensePlateError errorReason);
```

<!--
- Here is a take at the update code using OneOf.
- Quite a a bit change, in comparting the old model to the new.
- First big thing is the One of declaration here.
    - I am saying this type can be one of three things. LoadingImage, ErrorRetrievingImage or DisplayingImage

- Those types there are just records I have declared and I will go over them in a moment.
- If we  look at the ImageReceived event handlder - we essentially just set the state to the appropriate type.
- Now going over each of the types.
- The DisplayingImage type is a bit more complex. 
   - I moved the logic for actually getting the LPR text into it, as that logic is the only the concern for this state.
   - That LPR state is its one One of Type with the differnt states the LPR recognition an be in.
   - Then we expose the GetLicensePlate method and set the Result like we did before but just with the new One of syntax.

- We now in my opionin - have a much cleaner view model.  We are 
   - not leaking data to other states, this preventing getting into states that could be impossible.
   - The model also seems to be easier to reason about. 
-->

---

# UI Using OneOf

```csharp {all|3-7|4|5|6|18-3|22-27|23|24|25|26|28|all}{maxHeight:'75vh'}
@inject LicensePlateTestViewModel LicensePlateTestViewModel

@LicensePlateTestViewModel.LicensePlateTestState.Match(
    loadingImage => RenderLoading,
    errorRetrievingImage => RenderErrorRetrievingImage,
    displayingImage => RenderImage(displayingImage)
);

@code {

    protected override void OnInitialized()
    {
        LicensePlateTestViewModel.Init();
    }

    private RenderFragment RenderLoading => @<div>Loading...</div>;
    private RenderFragment RenderErrorRetrievingImage => @<div>Error Loading Image</div>;
    private RenderFragment RenderImage(DisplayingImage image) => 
        @<div>
            <img src=@BytesToBase64(image.ImageBytes) />
            <div>
                @image.LprState.Match(
                    unknown => RenderLprNotSent,
                    loading => RenderLprLoading,
                    received => RenderLprReceived(received.licensePlateText),
                    error => RenderLprError(error.errorReason)
                );
                <button onclick="@image.GetLicensePlate()">Test Lpr</button>
            </div>
        </div>;

    private RenderFragment RenderLprNotSent => @<span></span>;
    private RenderFragment RenderLprLoading => @<span>Loading...</span>;
    private RenderFragment RenderLprReceived(string licensePlateText) => @<span>@licensePlateText</span>;
    private RenderFragment RenderLprError(LicensePlateError error) => @<span>@error</span>;
    
    private string BytesToBase64(byte[] bytes)
    {
        return string.Empty;
    }
}
```

<!--
Up until now, I have not shown any UI code, but now that we are using Discriminated Unions, I waant to demonstrate one of the best benefits of it in my Opinion
and that is the exhaustive pattern matching with the Match method.

Here I have a razor component. Pretty stratight forwward. I am injecting in the View Model.
- On the view model, I am using the Match Method which wl match on the current objects type and execute the code you want to.
   - Which in this case I am returning a Blazor Render Fragment that will be rendered in the UI..
   - So Going over this, it is saying in the case of loadingImage - execute the RenderLoading method.
   - if the state is errorRetrievingImage - execute the RenderErrorRetrievingImage method.
   - if the state is displayingImage - execute the RenderImage method.
   - The RenderImage method is a bit more complex as that has another Oneof we are matching on for the LPR state.

- The best thing about this Match method, Is its exhaustiveness.
   - If I were to add another type into the OneOf declleration, I immendeitlay get a syntax error where it is being used and my code woudldnt compile.

   - Gong to attempt to show this now in my editor
   (Demo)

 (Pause for questions befor next slide )
-->

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
layout: section
transition: view-transition
---

# Updating current code to use Dunet  {.inline-block.view-transition-title}

---

# Updating current code to use Dunet {.inline-block.view-transition-title}

```csharp
namespace examples.Components;

public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;
    
    public LicensePlateTestState LicensePlateTestState;
    
    public void Init()
    {
        LicensePlateTestState = new LicensePlateTestState.LoadingImage();
        _imageService.ImageReceived += OnImageReceived;
    }
    
    private void OnImageReceived(object? sender, ImageEventArgs e)
    {
        if (e.ErrorRetrievingImage)
        {
            LicensePlateTestState = new LicensePlateTestState.ErrorRetrievingImage();
        }
        else
        {
            LicensePlateTestState = new LicensePlateTestState.DisplayingImage(e.ImageBytes,  _licensePlateService);
        }
    }
}

[Union]
public partial record LicensePlateTestState
{
    partial record LoadingImage;
    partial record ErrorRetrievingImage;

    public partial record DisplayingImage(byte[] ImageBytes, LicensePlateService _licensePlateService)
    {
        public  LicensePlateRecognitionState LprState = new LicensePlateRecognitionState.Unknown();
        public async Task GetLicensePlate()
        {
            try
            {
                LprState = new LicensePlateRecognitionState.Loading();
                var licensePlateText = await _licensePlateService.GetLicensePlateFromImage(ImageBytes);
                LprState = new LicensePlateRecognitionState.Received(licensePlateText);
            }
            catch (LicensePlateException ex)
            {
                LprState = new LicensePlateRecognitionState.Error(ex.ErrorReason);
            }
            catch (Exception ex)
            {
                LprState = new LicensePlateRecognitionState.Unknown();
            }
        }
    };
}

[Union]
public partial record LicensePlateRecognitionState
{
    partial record Unknown;
    partial record Loading;
    partial record Received(string licensePlateText);
    partial record Error(LicensePlateError errorReason);

```

<!--
Here is what our model looks like using Dunet.
- The code honestly looks pretty much the same as the OneOf example - just updated to use the Dunet syntax.
- we expose a type of LicensePlateTestState which is a arecord type
  - We updaate the ImageReceived event handler setting hte state appropriately.
- here is what the LicensePlateTestState looks like.
  - declaring the differnt states our view can be in as Partial records.
- The displaying image one is pretty much the same as well. just updating with a differnt type for LicensePlateRecognitionState.
- and here are the differnt states for it.


- Any questions on this?

-->

---

# UI Using Dunet

```csharp

```

<!--
UI example is exactly the same as the OneOf example. we are relying on the Match method to display the UI we want based on the sate.
-->

---
layout: section
---

# Which library should you use?

<!--
- Honestly up to you. I would say give them both a try to see which syntax you like better.
- In my opinon - for any new work I would probably choose Dunet.  It is a bit more feature rich giving us an async match methods as well as json serialization support,
  and the syntax looks closer to the syntax Microsoft is propsing for the native implementation.
-->

---

# Comparing OneOf and Dunet

<table>
  <thead>
    <tr>
      <th>Feature / Aspect</th>
      <th><strong>Dunet</strong></th>
      <th><strong>OneOf</strong></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><strong>Approach</strong></td>
      <td>Source generator: generates union types and pattern matching at compile time</td>
      <td>Runtime generic type: <code>OneOf&lt;T0, T1, ... Tn&gt;</code> holds a value of one of the given types</td>
    </tr>
    <tr>
      <td><strong>Syntax</strong></td>
      <td>Declarative: <code>[Union]</code> partial record with nested records for each case</td>
      <td>Imperative: Inherit from <code>OneOfBase&lt;T0, T1, ... Tn&gt;</code> or use <code>OneOf&lt;T0, T1, ... Tn&gt;</code></td>
    </tr>
    <tr>
      <td><strong>Pattern Matching</strong></td>
      <td>Generates <code>Match</code> methods for exhaustive and specific matching</td>
      <td><code>Match</code> method requires all cases to be handled (compile-time enforced, no fallback)</td>
    </tr>
    <tr>
      <td><strong>Exhaustiveness</strong></td>
      <td>Enforced at compile time (compiler error if not all cases handled)</td>
      <td>Enforced at compile time (compiler error if not all cases handled in <code>Match</code>)</td>
    </tr>
    <tr>
      <td><strong>Async Pattern Matching</strong></td>
      <td>Yes: <code>MatchAsync</code> for <code>Task</code>/<code>ValueTask</code> return types</td>
      <td>No built-in async match; must use regular <code>Match</code> and handle async logic manually</td>
    </tr>
    <tr>
      <td><strong>Specific Match</strong></td>
      <td>Generates specific match methods for each variant (e.g., <code>MatchCircle</code>)</td>
      <td>No specific match methods; only general <code>Match</code></td>
    </tr>
    <tr>
      <td><strong>Custom Data per Case</strong></td>
      <td>Each case can have its own fields and types (like F# DUs)</td>
      <td>Each case is a separate type, but no custom fields per case (just the type itself)</td>
    </tr>
    <tr>
      <td><strong>Serialization</strong></td>
      <td>Supports System.Text.Json with <code>JsonDerivedType</code> attributes</td>
      <td>Needs custom converters or manual handling</td>
    </tr>
    <tr>
      <td><strong>.NET Version</strong></td>
      <td>.NET Standard 2.0+</td>
      <td>.NET Standard 2.0+</td>
    </tr>
  </tbody>
</table>

--- 
layout: section
---

# Will we alway have to use a library to get this in C#?

<!--
  - The anser to that is no, As I mentioned about almost a year know Microsoft anounced their intentions to finally bring DU support to C# proper
  - They havn't commited to what version this will be in, but they have given examples on how they think it might look.
-->

---

<img src="./reddit-discriminated-unions-comment.png" />

<!--

Its always been a joke - but it does look like we will infact get GTA6 before native DU support.

-->

---

# Current Discriminated Union Proposal


```csharp
union struct U 
{
    A(int x, string y);
    B(int z);
    C;
}

U u = new A(10, "ten");

var x = u switch { 
    A a => a.x,
    B b => b.z,
    C c => 0
};

```

<img src="./proposal-link.png" />


<!--
 - Here is the current syntax they are proposing for this. 
 - This is just in the propose stage - so highly subject to change.
 - I do suggest you take some time to read the proposeal linked, as it goes way into why.
-->


---

# Summary
- booleans or Enums
- union types
- two list or one list with multiple fields

---

TODO - Advanced version of the code with more states and more complex logic.

---

# Questions?


---




