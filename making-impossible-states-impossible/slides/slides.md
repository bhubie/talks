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

TODO - Description of Talk


<!--
The last comment block of each slide will be treated as slide notes. It will be visible and editable in Presenter Mode along with the slide. [Read more in the docs](https://sli.dev/guide/syntax.html#notes)
-->

---
transition: fade-out
---

# About me Slide

TODO

---
transition: slide-up
---

# Talk Overview slide
TODO


---
transition: slide-up
---

# Requirements

1. Send an image to a service
   - The service will:
     - See if there is a license plate in the image
     - Send back information about the license plate, including the a composite image of the plate
1. Display the returned license plate info on the screen
1. Display an error message on the screen if an error is returned
1. Retry logic if any errors returned from the service


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
        @_licensePlateResult.State
    </p>
}

@if(_error) {
    <h3>Error Getting License Plate</h3>
    <button>Retry</button>
}

@code {
    private LicensePlateResult? _licensePlateResult;
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
TODO - Split up, add image of what it would look like rendered..
-->

---
transition: slide-up
---

# Bug
If an error occurs, and you retry and get a "Success" error message isnt cleared

<!--
Ship off to QA, and we get our first bug repot.
If an error occurs, then we get back a license plate. the error message isnt cleared.
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
            _loading = true;
            _error = false;
            _licensePlateResult = await _licesePlateService.GetLicensePlateFromImage(pathToSomeImage);
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
-->

## Better fix 
- Creating an enumeration

<!--
This:
1. Removes booleans and in my opinion simplifies the logic and makes it easier to read
-->


---
transition: slide-up
---

# New requirements

License Plate Recoginition Service will now send back differnt types of error codes, if an error happens and the UI will need to display the code.

Error codes:
- License Plate Not found
- Image Path not found
- Error (Catch all error)

<!--
Okay.. so we got that working now. Few months later, product management comes back with some requirements for this feature.
-->

---
transition: slide-up
---

# Updated Code

---
transition: slide-up
---

# New Bug

---
transition: slide-up
---

# Problems with current code
- Getting into states that should be impossible 
- When going into a new state, have to remember to "clear" out data not associated with the new state

---
transition: slide-up
---
# There has to be a better way

---
transition: slide-up
---
# Discrimitive unions/Algebaric Data types


---
transition: slide-up
---
# Lets use it in C#
- :( not nativly supported....yet
- Proposel has been announced though
  - TODO link proposal

---
transition: slide-up
---
# Libraries 
- One OF
- DU

---
transition: slide-up
---
# Updating current code to use One Of





