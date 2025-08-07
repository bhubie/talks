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

<style>
.slidev-vclick-target {

  opacity: 0;
  transition: opactiy 0.3s ease;
}

.slidev-vclick-target.slidev-vclick-current,
.slidev-vclick-target.slidev-vclick-prior {
  opacity: 1;
}

</style>

# Making Impossible States Impossible in C#

<div class="absolute bottom-4 right-4">

## Brett Huber

</div>

---

# About Me

<br />

<div class="grid grid-cols-2 gap-4">

<div class="flex flex-col  gap-4 text-xl">

Working in the software industy for 13 years in various differnt roles

- Product Owner
- Developer
- Solutions Architect

</div>

<div class="flex flex-col items-center gap-4">

<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" data-supported-dps="24x24" fill="#0a66c2" class="mercado-match" width="96" height="96" focusable="false">
  <path d="M20.5 2h-17A1.5 1.5 0 002 3.5v17A1.5 1.5 0 003.5 22h17a1.5 1.5 0 001.5-1.5v-17A1.5 1.5 0 0020.5 2zM8 19H5v-9h3zM6.5 8.25A1.75 1.75 0 118.3 6.5a1.78 1.78 0 01-1.8 1.75zM19 19h-3v-4.74c0-1.42-.6-1.93-1.38-1.93A1.74 1.74 0 0013 14.19a.66.66 0 000 .14V19h-3v-9h2.9v1.3a3.11 3.11 0 012.7-1.4c1.55 0 3.36.86 3.36 3.66z"></path>
</svg>
<img src="./linked-in.png" width="200" />

</div>

</div>
<!--
Been working in the software industy for 13 years now in various differnt roles

- Product Ownder
- Developer
- Solutions Architect

Feel free to connect with me on Linked in

-->

---
layout: view-transition
---

<div class="grid grid-cols-1 grid-rows-1">
  <img src="./hunter-aligner.png" class="col-start-1 row-start-1 w-full h-full object-cover p-8" />
  <img src="./hunter-logo-white-red.webp" class="col-start-1 row-start-1 justify-self-end self-start z-10" width="182px" /> {.view-transition-title}
</div>

<!--

People from hunter I see a few, but can you all raise your hands?

Fun fact - I believe we sent about X people here to this conference. so probablt about X% of people here are fron hunter.
If you havnt hear about hunter - we are a hidden gem in st louis.  
I have been tehre for 5 and a half years now.

We sell equipment to auto shops and dealershipts. Lift racks, tire balancers, tire changers, aligners.  

(NEXT SLIDE)

-->

---

<img src="./hunter-logo-white-red.webp" /> {.view-transition-title}


<div class="flex flex-col justify-center items-center mt-10 mb-10">

# Hiring - Scrum Master, Software Engineer I-III roles

</div>

<div class="flex flex-row justify-around">

<img src="./hunter-finalist.png" height="250" width="250" />

<img src="./hunter-hiring-qr-code.png" height="250" width="250" />

</div>

<!--

- and we are hiring.
- have some postions open for Scrum master, Software Engineers 1 through 3.  
I would encourage you all to apply. Scan the QR code to see the hiring page.

-->

---
layout: section
---


# Making Impossible States Impossible?


<!--

- Making Impossible States Impossible? A lot of you are probably wondering what I am meaning by this statement

- what sort of clickbait title is this??

-->

---
layout: section
transition: view-transition
---

# States your application should never be in. {.inline-block.view-transition-title}

<!--
Simply put - States your application should never be in in the first place.

(NEXT SLIDE)
-->

---
layout: header-single-col
---

# States your application should never be in. {.inline-block.view-transition-title}


::content::

<div class="text-3xl flex flex-col items-center ">

Displaying a Loading indicator while also displaying a result.

<div class="text-6xl mt-3">
⏳ 📝
</div>

</div>

---
layout: header-single-col
---

# States your application should never be in. {.inline-block.view-transition-title}


::content::

<div class="text-3xl flex flex-col items-center">

Displaying a Success message while also displaying an error message.


<div class="text-6xl mt-3">
✅ ❌
</div>


</div>

<!--
  These are just some simple examples, but
  I am sure we have all been there before. The Front end of your application being in some weird state that should never happen.

  (CLICK)

  
  Thes are simple things that should never happen.  Impossible states  based on the requirements - yet they do.

  (NEXT SLIDE)
  
-->

---
layout: section
---


# Typically occur as a by-product of how you are managing the state in the application.

<!--
 These types of bug are usually by-products of how you are managing the state in the application.

  (NEXT SLIDE)

-->

---
layout: section
---

# What if we could make these weird states be impossible to get in?

---
layout: section
---


# New way of thinking about modeling application state

<!--
Goal of this talk is to hopefully - have you all walk away wth new ways of how you can model application state. which will hopefuly avoid these types of bugs alltogether.

- This talk is focused around C# but the concepts we talk about here should apply to other langues.


(Next Slide)

-->

---
layout: section
---

# Lets Build something!

<!--
To demonstrate this.  - lets build something.

(NEXT SLIDE)
-->

---
layout: section
---
    
# License Plate Recognition (LPR) Testing feature.

## See how the state evolved as the requirements changed.

<!--
What we are going to be building is a feature I worked on at Hunter Engineering.  we are going to see how the state evolved as the requirements changed.

Feature has to do with visual testing of License Plate recognition.  Displaying the returned Plate characters from a live image feed.  This was a feature we used in one of our desktop applications. Serving as a sort of calibration thing so users know we are capturing plate characters correctly. 

Note - The code didn't end up exactly in this final state I am proposing - but it is similar. 

(NEXT SLIDE)

-->

--- 

# Feature Requirements

## Requirement 1:

<br />


<v-click>

### Receive "live" image bytes streamed from a camera

</v-click>
   

<br />

<v-click>

- ### If error connecting to camera - display error message.
- ### Otherwise - display the image on the screen.

</v-click>


<!--
- Okay - so we get our user story for a new feature and look over the requirements.- 

-->

---

# Feature Requirements

## Requirement 2:

<br />

<v-click>

### Send Image to LPR Service for plate recognition

</v-click>

<br />

<v-clicks>

1. ### Call service via a "Test License Plate Recognition" button on screen
   - ### Service will return the plate characters it finds
1. ### Dislay the returned characters on the screen
1. ### If no plate is found, display a message saying "No plate found"
1. ### Display an error message on the screen if an error is returned
1. ### User should be able to retry this test if an error is returned by clicking the "Test License Plate Recognition" button

</v-clicks>

---
layout: section
---

# Lets Build it

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

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>

# 1st Requirement - Receiving live image bytes {.inline-block.view-transition-title}

```csharp {all|9-13|11|12,15-21|17|18|19|}{maxHeight:'500px'}
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
- [click] we have an init method.
- [click] we set loading to true
- [click] we subscribe to an event from am image service we are provided that is providing the "live" image every so often as image bytes.
-  It emits the "live" image every so often as image bytes.
- [click] once we receive an image we set loading false.
- [click] then we set the image bytes to the image bytes we received.
- [click] if we have an error - we set another error flag.
- All pretty straightforward.

(Next Slide)
-->

---
layout: section
transition: view-transition
---

# 2nd Requirement - "LPR Test" {.inline-block.view-transition-title}

---

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>

# 2nd Requirement - "LPR Test" {.inline-block.view-transition-title}

```csharp {all|10-12|19-33|24-26|28-32|}{maxHeight:'500px'}
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

- [click] we have some more properties on the view model related to the LPR test.
- [click] we are exposing a new method for executing the LPR test
- [click] In it are are setting loading and results when we get them
- [click] then setting some more flags if we get an error.

- [click] Now perfect - feature is done regarding the View Model. 
- we write the view, some tests - then ship it off to QA for testing.
-->

---
layout: section
transition: view-transition
---

# 🪲 Bug

<v-click>

## If an error occurs when attempting to get the license plate, and the user clicks  "retry" and a plate is returned, the error message isn't cleared

</v-click>

<!--
QA - looks at it and we get our first bug on it.

[click] If an error occurs, then we get back a license plate. the error message isnt cleared.
We are getting into a state that should be impossible.

-->

---
layout: center
---

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>

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
Looking at our Model - must be a scenario some how where we are not resetting the IsLprError boolean.

(NEXT SLIDE)


-->

---
layout: section
---

# We write a failing test

<v-click>

## A test that should never have had to be written in the first place.

</v-click>

<!--
- Simple enough to write an automated test for. So we write a failing test.

(CLICK)

  - [click] Honestly - a test we should never have to write in the first place. and we make the fix

(NEXT SLIDE)

-->

---

# Fix

<div class="flex justify-center items-center text-2xl">

Always set **IsLprError** boolean to false before firing off the call to the LPR service.

</div>




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

<!--

- Simple enough fix - Just set the IsLprError boolean to false always before getting the license plate. Easy enough
-->

---
layout: section
transition: view-transition
---

# Is there a better fix?

<v-click>

## Yes - Swap the booleans for an <span class="bg-yellow-500 p-1">enumeration</span> {.inline-block.view-transition-title}

</v-click>

<!--

Is there a better fix?
Could we have avoided this bug alltogether?

[click] IMO - Yes - Swap the booleans for an enumeration instead.

With our current implentation we are starting to get into boolean hell, and this gets us out of it.
  
Often times when people are coding on the front end - they go for booleans first.
I would like to encourage you all to thinkg about representing  state in an enumration rather than a boolean even for simple stuff.

-->

---

# Enumeration - Code example {.inline-block.view-transition-title}

<!-- <div style="max-height: 500px; overflow-y: auto;"> -->

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>



```csharp {all|38-50|7|10|14|22,24,28|34|}{maxHeight:'500px'}
public class LicensePlateTestViewModel(ImageService imageService, LicensePlateService licensePlateService)
{
    private ImageService _imageService = imageService;
    private LicensePlateService _licensePlateService = licensePlateService;

    public byte[] ImageBytes = [];
    public ImageState imageState;

    public string LicensePlateText;
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


<!--
 - Here is what the code handler for getting the license plate looks like before the fix.
 - Here is what the handler looks like now.
 - [click] we declare two enums for the image and lpr states
 - [click] then throughout the code we just set the state to the appropriate enum value
 
 Simply declaring an Enum at the bottom and now we can get rid of all of the boolean toggling we were doing in the first place and this prevents the original bug from happening

 TODO - Code highlighting and explaining.
-->

---
layout: section
---

# New requirements

<v-click>

## License Plate Recoginition Service will now send back error codes, if an error happens and the UI will need to display the error message.

</v-click>

<!--
- Okay.. so we got that working now and I feel like we ar ein a much better state.
- Few months later, product management comes back with some requirements for this feature.

Here are the requirements

-->

---

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>


# Updated Code

```csharp{all|11|27-31|32-36|}{maxHeight:'500px'}
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

<!--
  - New requiments seem simple enough.  
 - [click] we expose a new property on the view model that would hold the error cde
  - [click] This license plate service has a new exception it throws that returns the error code.
  - we are catching the special exception setting the erroc code from it
  - [click] then just updating the generic exception just incase and handling the error code appropriately
-->

---
layout: section
transition: view-transition
---

# Problems with current code {.inline-block.view-transition-title}


<!--
Now - this code does look a lot better, but as the requiremnt change - cracks are starting to grow with how everything is implemented.

(NEXT SLIDE)
-->

---
layout: header-single-col
---

<style scoped>
    pre {
        font-size: 1.50rem !important;
    }
</style>


# Problem 1

::content::

<div class="flex flex-col gap-4 text-2xl">

## You can still get into states that should be impossible 


```csharp
public ImageState imageState;
public LprState lprState = LprState.Received;
public LicensePlateError? LprErrorReason;
```


</div>

<!--
In our view model - we have 3 different fields related to state.

States for the Image
States for the License Plate Recognition
Error states for the License Plate Recognition

The view model currently allows us to be in a state where we have an Error loading the image while displaying license plate text.

-->

---
layout: header-single-col
---

# Problem 2 

::content::

<div class="flex flex-col items-center"> 

<div class="text-3xl">

States can accidently access data it shouldnt know about 

</div>

<div class="flex flex-col gap-4 text-2xl items-center justify-center">
  
  - A state should only know about the data it needs to know about. 

  - When going into a new state, have to remember to "clear" out data not associated with the new state

</div>

</div>

<!--
- Not all states should have access to the LicensePlate Text or even the method to make the call to Get the license plate data, yet how it is currently coded - we allow for it.

- With the current implementation - we  have to remember to clear out the data for one state - just so it may not show up in another state.
-->

---
layout: header-single-col
---

<style scoped>
    pre {
        font-size: 1.25rem !important;
    }
</style>

# Problem 3

::content::



<div class="flex flex-col gap-4 text-2xl">

## As state becomes more complex, it becomes harder to reason about.


```csharp
    public byte[] ImageBytes = [];
    public ImageState imageState;
    
    public string? LicensePlateText;
    public LprState lprState = LprState.Received;
    public LicensePlateError? LprErrorReason;
```

</div>

<!--
- We are already starting to see this now.  it is not that obvious to tell what fields go with which state.
-->

---
layout: header-single-col
---


# Problem 4

::content::


<div class="flex flex-col items-center">

<div class="text-3xl"> 

No Exhaustive pattern matching on states

</div>

</div>

<!--
If you added a new enumeration - it is up to you to remember to touch the UI to handle it wherever it is being used. We cant easily enforce our code not to compile if we forget to handle a case.

Having a tool that helps forces your case analysis to be exhaustive would be incredibly useful.
-->

---
layout: center
---

# What I am looking for is a way to describe a type as being one of a set number of things while not leaking the the data of the type to the others.

<v-click>

## A way to define a type as "this, that, or this other thing"

</v-click>


<!--
- What I am looking for is a way to describe a type as being one of a set number of things while not leaking the the data of the type to the others.

- [click] A way to define a type as "this, that, or this other thing"

-->

---
layout: section
---

# What can we do about it?


---
layout: section
transition: view-transition
---

# Discriminated unions  / {.inline-block.view-transition-title}
# Tagged unions / 
# Algebaric Data types / 
# Sum types

<!--
 Differnt langues calls this differnt things and they each have their differnet meaning
 But the they important thing is that this is a native feature in those languages.

 - Show of hands - has anyone ever heard of these terms before?
-->

---
layout: quote
transition: view-transition
---

# Discriminated Unions {.inline-block.view-transition-title}

<div class="text-2xl">


A data structure used to hold a value that could take on several different, but __fixed__, types. Only __one__ of the types can be in use at any one time. 


<v-click>

Each type can optinally carry its own data.

</v-click>

</div>

<!--
 Based on the language you are using - This feature looks differnt a simple definitionn of it is

 A data structure used to hold a value that could take on several different, but __fixed__, types. Only __one__ of the types can be in use at any one time

 This gives us my wish of being able to define a type of this or that.
-->

---

# Discriminated Unions {.inline-block.view-transition-title}

<div class="text-2xl flex flex-col gap-4">


<div v-click="1">Paradigm originating from functional programming.</div>
<div v-click="1">Concept Dates back to the 1970s </div>


<div class="flex flex-col" v-click="2">
<span class="underline decoration-yellow-500 decoration-2 underline-offset-4">Union:</span> 
A type that can hold (or "be") one of several different, but fixed, types—like a union of possibilities.
</div>



<div class="flex flex-col" v-click="3">
<span class="underline decoration-yellow-500 decoration-2 underline-offset-4">Discriminated:</span> 
Each possible case (variant) is "tagged" or "labeled" with a unique identifier (the discriminant), which allows you to distinguish (or "discriminate") which variant the value currently holds.
</div>

   

<div v-click="4">This "tag" enables the compiler and the programmer to safely determine which data is present and how to handle it.</div>



</div>

<!--
  - Union: A type that can hold (or "be") one of several different, but fixed, types—like a union of possibilities.
  - Discriminated: Each possible case (variant) is "tagged" or "labeled" with a unique identifier (the discriminant), which allows you to distinguish (or "discriminate") which variant the value currently holds.
-->


---
layout: section
transition: view-transition
---

# Examples in other languages {.inline-block.view-transition-title}

<!--
 - Next - I am going to show you some examples of this in other languages 
- Not going to spend to much time on this, but want to give you an idea of what it looks like in other languages to give you an idea
- All of these examples are defining the same type in the end which is a shape.
- and a function that takes in the shape - matching on it to calcualte the area.
-->


---

<style scoped>
    pre {
        font-size: 1.25rem !important;
    }
</style>

# Examples in other languages {.inline-block.view-transition-title}

## F# - "Discriminated Unions"

```fsharp{all|1-4|7-11|8-11|}
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

<!--
Here is what this looks like in F#.  
This is probably the most foreign looking one of the all due to the F# syntax.

[click] We have a Shape type with our 3 differny kinds, Circle, Rectangle, and Square. 
- Each kind has its own parameters. 

- [click] @e then have an area function. that Takes in a Shape.
- [click] The match keyword is then being used matching on the type and calcuating the area differnly based on it.
- This is exhaustive - so you have to handle all the cases otherwise it would not compile.

-->
---

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>

# Examples in other languages {.inline-block.view-transition-title}
## Rust - "Enums"

```rust {all|7-11|}
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

<!--
- Here it is in Rust this might look a bit cleaner if you are not used to F# syntax

- [click] The match function here is exhaustive as well, so you have to handle all the cases otherwise it would not compile.

(Pause a bit)
-->

---

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>

# Examples in other languages {.inline-block.view-transition-title}

## TypeScript - "Union Types"

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

<!--
And here it is in typescript.

- Notices - that ttis example, it is a bit more vervose than the others
as unions in it are "Untagged" you cannot discriciminate the type when matching unless you tag a property on the object- 

Also - This it non-exhaustive in this language.
-->

---
layout: section
---

# Lets use it in C#

<!--
- Great - now that we hve the structure we want to use, lets use it in C#.
-->

---
layout: section
---

# 😔 not natively supported<span class="text-black dark:text-white" v-click="1">...yet</span>

<span v-click="2">

## Proposal has been announced 🎉
  
</span>

<!--
- Well - Not natively supported 

[click] yet...

- [click] As of about a year ago - they have announced a proposel of how they would like to implment this in the languague. No release date yet. but they are working on it.
   - I am going to be going a bit more detail into this spec later so you see how it may be implemented. 
- If you have ever lurked online C# communities this always seems to be the feature that others are waiting for to be implemented in the language,

We are not doomed though. - There are some libraries we can use to get something similar.

(NEXT SLIDE)

-->

---
layout: two-cols-header
transition: view-transition
---
# Libraries 

::left::

<div class="flex flex-col justify-center items-center w-full text-5xl">

  OneOf {.inline-block.view-transition-title}

</div>

::right::

<div class="flex flex-col justify-center items-center w-full text-5xl">

  Dunet

</div>


<!--
There are a few libraries that implement a take on Discriminated Unions in C#

- OneOf and Dunet

- Has anyone ever heard of these or actually used them?
- OneOf is one we are actually using at my work now to solve this problem
- Dunet is another one that I found while doing research for this talk

- Going to be go over each of them here shortly and how they can be used in the application

-->

---

<style scoped>
    blockquote {
        font-size: 2.0rem !important;
        line-height: 2.0rem !important;
    }
</style>

# OneOf {.inline-block.view-transition-title}

<div class="flex flex-col h-full justify-evenly">

<div >

> This library provides F# style discriminated unions for C#, using a custom type OneOf<T0, ... Tn>. 
>
> An instance of this type holds a single value, which is one of the types in its generic argument list.

</div>

<div class="flex justify-around items-center">


https://github.com/mcintyre321/OneOf

<img src="./oneof-github-link.png" height="150" width="150" />

</div>

</div>


<!--
First up OneOF
From their github they say,
-->

---

<style scoped>
    pre {
        font-size: 1.00rem !important;
    }
</style>

# OneOf

```csharp {all|1-3|7|8-12|16|18-19|}{maxHeight:'500px'}
record Circle(double Radius);
record Rectangle(double Length, double Width);
record Square(double Side);

....
    
public static double Area(OneOf<Circle, Rectangle, Triangle> shape) {
    return shape.Match(
        circle => 3.14 * circle.Radius * circle.Radius,
        rectangle => rectangle.Length * rectangle.Width,
        square => square.Side * square.Side
    );
}
... 

OneOf<Circle, Rectangle, Square> shape = new Rectangle(3, 4);

var area = Area(shape);
Console.WriteLine(area); // "12"

```

---

<style scoped>
    blockquote {
        font-size: 2.0rem !important;
        line-height: 2.0rem !important;
    }
</style>

# Dunet


<div class="flex flex-col h-full justify-evenly">

<div >


> Dunet is a simple source generator for discriminated unions in C#.

</div>

<div class="flex justify-around items-center">


  https://github.com/domn1995/dunet

  <img src="./dunet-github-link.png" height="150" width="150" />

</div>

</div>




---

<style scoped>
    pre {
        font-size: 1.0rem !important;
    }
</style>

# Dunet


```csharp {all|3-9|14-18|21-24|}{maxHeight:'500px'}
....

[Union]
partial record Shape
{
    partial record Circle(double Radius);
    partial record Rectangle(double Length, double Width);
    partial record Square(double Side);
}

....

public static double Area(Shape shape) {
  return shape.Match(
    circle => 3.14 * circle.Radius * circle.Radius,
    rectangle => rectangle.Length * rectangle.Width,
    square => square.Side * square.Side
  );
}

var shape = new Shape.Rectangle(3, 4);
var area = Area(shape);

Console.WriteLine(area); // "12"
```

<!--
- Big change between Dunet and One of is how you declare your union types.
- [click] Dunet you tag your types with the [Union] attribute and then declare your types as partial records. Source generated then do a lot of magic behind the scense.
- Other than the decleartion - everything else is pretty similary.
- [click] you also have your match method matching on the types.
- [click] then here is how you declare and use it
-->

---
layout: section
transition: view-transition
---

# Updating current code to use OneOf  {.inline-block.view-transition-title}

---

# Updating current code to use OneOf {.inline-block.view-transition-title}

```csharp {all|6|14-24|18|22|27|28|30-50|33-49|32|37-39|43,47|52-55|all}{maxHeight:'500px'}
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
    public  OneOf<LprNotSent, LprLoading, LprReceived, LprError> LprState = new LprNotSent();
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
            LprState = new LprError(LicensePlateError.Generic);
        }
    }
};

public record LprNotSent;
public record LprLoading;
public record LprReceived(string licensePlateText);
public record LprError(LicensePlateError errorReason);
```

<!--
- Here is a take at the update code using OneOf.
- Quite a a bit change, in comparting the old model to the new.
- [click] First big thing is the One of declaration here.
    - I am saying this type can be one of three things. LoadingImage, ErrorRetrievingImage or DisplayingImage

- Those types there are just records I have declared and I will go over them in a moment.
- [click] If we  look at the ImageReceived event handlder - we essentially just set the state to the appropriate type.
- [click] If there is an error we set it to ErrorRetrievingImage.
- [click] If we have an image, we set it to DisplayingImage passing in the image bytes.

-Now going over each of the types.
- [click] Loading image is just a record with no data.
- [click] ErrorRetrievingImage is the same.
- [click] The DisplayingImage type is a bit more complex. 
   - [click] I moved the logic for actually getting the LPR text into it, as that logic is the only the concern for this state.
   - [click] That LPR state is its one One of Type with the differnt states the LPR recognition an be in.
   - [click] Then we expose the GetLicensePlate method and set the Result like we did before but just with the new One of syntax.
   - [click] Same thing with handling the exceptions
   - [click] Here are the record types for the LPR states.

- We now in my opionin - have a much cleaner view model.  We are 
   - not leaking data to other states, this preventing getting into states that could be impossible.
   - The model also seems to be easier to reason about. 
-->

---

# UI Using OneOf

```csharp {all|3-7|4,16|5,17|6,18|18-30|22-27|23,32|24,33|25,34|26,35|28,36|all}{maxHeight:'500px'}
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
                    notSent => RenderLprNotSent,
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
- [click] On the view model, I am using the Match Method which will match on the current objects type and execute the code you want to.
   - Which in this case I am returning a Blazor Render Fragment that will be rendered in the UI..
   - [click] So Going over this, it is saying in the case of loadingImage - execute the RenderLoading method.
   - [click] if the state is errorRetrievingImage - execute the RenderErrorRetrievingImage method.
   - [click] if the state is displayingImage - execute the RenderImage method.
   - [click] The RenderImage method is a bit more complex as that has another Oneof we are matching on for the LPR state.
   - [click] I am matching again on the lpr stae
   - [click] When notSent - render the lpr not sent function
   - [click] When loading - render the lpr loading function
   - [click] When received - render the lpr received function passing in the license plate text
   - [click] When error - render the lpr error function
- [click] then when the button is clicked we get the license plate

- [click] The best thing about this Match method, Is its exhaustiveness.
   - If I were to add another type into the OneOf declleration, I immendeitlay get a syntax error where it is being used and my code woudldnt compile.


 (Pause for questions befor next slide )
-->

---
layout: section
transition: view-transition
---

# Updating current code to use Dunet  {.inline-block.view-transition-title}

---

# Updating current code to use Dunet {.inline-block.view-transition-title}

```csharp {*|8|16-26|}{maxHeight:'500px'}
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
        public  LicensePlateRecognitionState LprState = new LicensePlateRecognitionState.NotSent();
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
                LprState = new LicensePlateRecognitionState.Error(LicensePlateError.Generic);
            }
        }
    };
}

[Union]
public partial record LicensePlateRecognitionState
{
    partial record NotSent;
    partial record Loading;
    partial record Received(string licensePlateText);
    partial record Error(LicensePlateError errorReason);
}
```

<!--
Here is what our model looks like using Dunet.
- The code honestly looks pretty much the same as the OneOf example - just updated to use the Dunet syntax.
- [click] we expose a type of LicensePlateTestState which is a record type
- [click] We update the ImageReceived event handler setting the state appropriately.
- [click] here is what the LicensePlateTestState looks like.
  - declaring the different states our view can be in as Partial records.
- [click] The displaying image one is pretty much the same as well. just updating with a different type for LicensePlateRecognitionState.
- and here are the different states for it.


- Any questions on this?

-->

---

# UI Using Dunet

```csharp {all}{maxHeight:'500px'}
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
    private RenderFragment RenderImage(LicensePlateTestState.DisplayingImage image) =>
        @<div>
            <img src=@BytesToBase64(image.ImageBytes) />
            <div>
                @image.LprState.Match(
                    notSent => RenderLprNotSent,
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
      <td><strong>Exhaustive Pattern Matching</strong></td>
      <td>✅</td>
      <td>✅</td>
    </tr>
    <tr>
      <td><strong>Async Pattern Matching</strong></td>
      <td>✅</td>
      <td>❌</td>
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

# Current Discriminated Union Proposal


<div class="">

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

<img class="absolute bottom-0 right-0 p-4" height="200px" width="200px" src="./proposal-link.png" />


</div>


<!--
 - Here is the current syntax they are proposing for this. 
 - This is just in the propose stage - so highly subject to change.
 - I do suggest you take some time to read the proposeal linked, as it goes way into why.
-->

---
layout: section
transition: view-transition
---

# Summary {.view-transition-title}


---
layout: header-single-col
---


# Summary {.view-transition-title}

::content::

<div class="text-5xl">

Do we want booleans or Enums

</div>






<!--
-  do we want to use booleans or enums to represent something.  When we have one boolean everything is fine -  but when we need to add another boolean related to the thing we are representing - instead use a enumeration

-->

---
layout: header-single-col
---


# Summary {.view-transition-title}

::content::

<div class="text-5xl">

Consider union types

</div>

<!--

- rather than enums - consider using union type instead to represent it.  This can make your state easier to reason about, exposing the data only to the state - while also giving us exhaustive pattern matching.

-->

---

# Questions?

---

TODO - Advanced version of the code with more states and more complex logic.

---



