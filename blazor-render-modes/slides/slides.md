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
# open graph
# seoMeta:
#  ogImage: https://cover.sli.dev
---

# Mastering Blazor Rendering Modes

---


# About Me

---

# Blazor Experience 

---

# History of Blazor

<Timeline /> 

---

# Initial Two Modes

Had to choose up front
TODO - pictues of each mode

---


# Blazor Server

<div>
  <div>
    Server
    <div>
    </div>
  </div>

  <div>
    Browser
    <div>
      Dom
    </div>
    <div>
      Blazor.web.js
    </div>
  </div>
</div>

---

# Blazor WebAssembly
TODO - graph how how it works


--- 

# Pros and cons of each?

---

# Fast forward to dotnet 8

TODO - Display all modes making marking new ones 
TODO - Cross out old names and show new names

<!--
In dotnet 8, Microsofot made the bigged update to Blazor todate, introduing new rendor modes
and completely overhauling how they can be used,

The New Modes are: TODI - name modes
I will be going through each of thse in detail, but First I want to explained the other Changes introduced with them
-->

--- 

# Major changes with new modes
1. No longer hace to choose up Server or WASM up front.
   - Blazor is now a swiss army knive - letting you mix and match where you want components to be rendered.
1. Performance improvements with pre-rendering on the server where it can.

<!--
  - Make Some componenst be server rendered, 
-->

---

# Setting up a new project

--- 

# Applying Render Modes across your application

## Component Defintion

```csharp
@page "..."
@rendermode InteractiveServer
```

## Component Instance

```csharp
<Counter @rendermode="InteractiveServer" />
```

<!--
  - Component Defintion
  - Component Instance
-->

---

Note - Component Authors should avoid coupling the definitino to a specific render mode.
Components should be designed to support any render mode.


<!--
 Ex: Say uou are creating a re-usable button.  Do not define a render mode in the component definition.  Let the consumers of the component decide, by setting it in the component instance.
-->

--- 

# Static Server Side Rendering (static SSR)

- Renders on the server - sending Html to the client
TODO - Some sort of animation
<!--
  - 
  - Demo Counter Rendered static that it wont click
-->

---

# static SSR With Forms

```csharp
@page "/static-form"

.......

<p>Your Name Is: @Name</p>

<form class="d-inline" data-enhance method="post" @formname="form" @onsubmit="SubmitForm">
    <AntiforgeryToken />
    <input type="text" @bind-value="@Name" name="Name" />

    <button class="btn btn-primary" type="submit">Submit</button>
</form>

@code {
    
    [SupplyParameterFromForm(FormName = "form")]
    public string Name { get; set; }

    private void SubmitForm()
    {
        Console.WriteLine($"Name: {Name}");
    }
}

```

<!--
  - Good news is - buttons can still work with static SSR in the context of form submission
  - The code here shows a plain old Html form.  
  - When I click it it will submit the value in the input form. Callback will then print the value from the input.
  - Demo Form Submission
  - Demo Stream rendering blocking
    - I am going to show another example of static rendering that simulated a slow API call - just rendering a list of weather from the database.
    - You can see here when I click the page - nothing happens at first until all of the data is ready.  
      - Not an ideal user experience
-->

---

# Stream rendering
TODO - graph of stream rendering
<!--
  - This is where stream rendering comes in.
  - With streaming, the Server will generating what Html it can - sending it to the client
  - Then when data becomes ready - it will stream the Html down to the client
  - you can kind of see a graph of what I am talking about here
-->

---

# Stream rendering example

```csharp {|2|}
@page "/stream-rendering"
@attribute [StreamRendering]

....

@code {
    private List<WeatherForecast> forecasts = [];

    protected override async Task OnInitializedAsync()
    {

        await Task.Delay(1000);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        await foreach (var weather in GenerateForecastsAsync(startDate, summaries, 15))
        {
            forecasts.Add(weather);
            await Task.Delay(100); // Optional throttling to prevent overwhelming UI
            StateHasChanged();
        }
    }

    private async IAsyncEnumerable<WeatherForecast> GenerateForecastsAsync(DateOnly startDate, string[] summaries, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WeatherForecast
                {
                    Date = startDate.AddDays(i),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                };

        }
    }
}


```
<!--
  - Here is a code example of how to get implement stream rendering
  - (Highlight the stream rendering attribute click mose)
     - The most impporant thing being the stream rendering attribute
  - Then i just have an Async call here yielding data every .5 seconds
  - (Demo Stream Rendering Page)
  - And now if we look at what this looks like on the demo page, you can see Initial data is rendered
    - Then as the data becomes ready - streamed down to the client
    - Looking at the network tab you can see no other calls being make, the server is just returning html down
-->

---

# Interactive Server

<!--
  - Let know web socket  used to keep connection open
  - Demo interactive server page
    - let know when navigation away - web socket closed
    - TODO - When TO USE
-->

---

# Interactive WebAssembly

---

# Interactive AutoRendering

<!--
  This new mode is a combination of InteractiveServer and InteractiveWebAssembly.
   - On the initial render of the component, it renders on the server using InteractiveServer, keeping a web socket connection open.
   - In the background it is also seding the necessary WASM code to the client.
   - On Subsequent visits to the page, it will render Client Side via Web Assembly.
   - Demo AutoRender,

   - As you can see here on the network tab - intial render is Ineractive Auto. 
     - On th network tab you can see web socket open.
     - If I naviate away, web socket is cloced. When I come back. render mode is web assembly.
     - On Any refresh of the page, - It still renders In Web Assemblhy.
-->

---

# Persisting State between auto modes

```csharp
@page "/persisted-state"

@rendermode InteractiveWebAssembly
@inject PersistentComponentState ApplicationState

....

@code {
    private List<WeatherForecast> forecasts = [];
    private PersistingComponentStateSubscription persistingSubscription;

    protected override async Task OnInitializedAsync()
    {

         if (!ApplicationState.TryTakeFromJson<List<WeatherForecast>>(
            nameof(forecasts), out var resotoredForcast))
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            await foreach (var weather in GenerateForecastsAsync(startDate, summaries, 15))
            {
                forecasts.Add(weather);
                StateHasChanged();
            }

            await Task.Delay(100); 
        }
        else
        {
            forecasts = resotoredForcast!;
        }

     
        // Call at the end to avoid a potential race condition at app shutdown
        persistingSubscription = ApplicationState.RegisterOnPersisting(PersistForecasts);
    }
```

<!--
  - If I go back to this mode that is rendering statically on the server during pre-rendering, but when it hydrates on the Client in InteractiveWebAssembly. You may notice that the weather data changed.
  - This is because the component is assentially remounting on the client, causing the full lifecycle to run again.
  - To get around the this problem, Microsoft introduced the PersistentComponentState service.
  - You can think of it like a cache in a way. 
  - Here is some code that shows how to use it.
  - TODO - More info from one video on this
  - Demo Persisted StatePage
-->

--- 

# Performance 

--- 

# Render mode propagation and rules

<!--
  - In the next few slides I am going to go over some of the rules on how Render modes can be propagated down the component tree.
-->

---
layout: two-cols-header
---

# A componnent will inheriret the render mode of its parent unless it is overriden.

::left::

```csharp
@page "/some-page"

<SomeComponent />
```

::right::

```csharp
@page "/some-page"
@rendermode InteractiveServer

<SomeComponent />
```


<!--
  A componnent will inheriret the render mode of its parent unless it is overriden.
  - So in these code examples.  On the left - Some component is rendered statically.
    - While on the right it would be rendered interactively, since the page specifies it.
-->

---

# <b>Interactive</b> renderd compoents children must share the same interactive mode


```csharp
@page "/some-page"
@rendermode InteractiveServer

<SomeComponent @rendermode="InteractiveWebAssembly" />
```


<div>
❌ Error:
Cannot create a component of type 'BlazorSample.Components.SomeComponent' because its render mode 'Microsoft.AspNetCore.Components.Web.InteractiveWebAssemblyRenderMode' is not supported by Interactive Server rendering.
</div>

<!-- 
  - This means An Interactive Server component can not be a child of a Web Assembly component and Vise Versa.
  - In this code example here, I have a component being rendered interactively on the server.
    - The Child component specified with Web Assembly.
    - When I try and build - we get this error.
-->

---
layout: center
---

```csharp
@page "/some-page"

<SomeComponent @rendermode="InteractiveServer" />
<SomeComponent @rendermode="InteractiveWebAssembly" />
```

<!--
  Note - that that only applies to Interactive Modes.
  - Since the default render mode of a page is static.. that means that this example is allowed.
    - One component rendering as Server, while the other rendering in Web Assembly.
-->

---

# Parameters passed to an interactive child component from a Static parent must be JSON serializable. 

```csharp
@page "/render-mode-9"

<SomeComponent @rendermode="InteractiveServer">
    Child content
</SomeComponent>
```

<div>
❌ Error:

System.InvalidOperationException: Cannot pass the parameter 'ChildContent' to component 'SomeComponent' with rendermode 'InteractiveServerRenderMode'. This is because the parameter is of the delegate type 'Microsoft.AspNetCore.Components.RenderFragment', which is arbitrary code and cannot be serialized.
</div>

<!--
This means that you can't pass render fragments or child content from a Static parent component to an interactive child component.This means that you can't pass render fragments or child content from a Static parent component to an interactive child component.
- So if we tried to render this code here - we would get this error.

- There is a way around this though, by wrapping the component with child content in a wrapper compoment
  (Go to next slide)
-->

---

<div>
### WrapperComponent.razor:

```csharp
<SomeComponent>
    Child content
</SomeComponent>
```
</div>

<div>
```csharp
@page "/some-page"

<WrapperComponent @rendermode="InteractiveServer" />
```
</div>

<!--
  - The example here shows what I mean
-->

---

# Prerendering
- The server outputs the HTML UI of the page as soon as possible in response to the initial request, which makes the app feel more responsive to users.
- Prerendering is enabled by default for interactive components (InteractiveServer, InteractiveWebAssembly)
- Which means you can opt out of it on certain components

```csharp
@rendermode @(new InteractiveServerRenderMode(prerender: false))

```

<!--
  - With the latest version - we also got performance improvements with prerendering
  - The server will output Html as quickly as possible, increassing the First Contentful Paint (FCP)
-->

---

TODO - Slide comparing speeds on prerendering

---

# Downside of prerendering

```csharp
<h2>Render Mode: @RendererInfo.Name</h2>


<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount" disabled="@(!RendererInfo.IsInteractive)">
    Click me
</button>
```
TODO - Demo pages

<!--
  - One potential downside of prerendering is the page might not be fully interative yet, meaning wasm is still initializing.
  - This can lead to a  poor user experience. 
  - Demo page "pre-rendering-downside"
    - Turn on 3g throttling
  - I have an example here where you can see here on the left on page load, the button looks ready to be clicked.
    - However as I spam click it, it isn't working.
  - To get around this issue, starting with dotNet 9 - the framework gave us som privitives we can use to see what the current rendering mode is.
    - Based on that we can then conditionally set attributes on the page based on the interatctivity. 
  - Here in my code example you an see based on the interactiviy I am disabling the button.
    - I am also displayin the current render mode
  - Back to my demos - the here on this page you can see it in action
-->

---

# [ExcludeFromInteractiveRouting] attribute

```csharp
@page "/some-page-i-want-to-render-statically"
@attribute [ExcludeFromInteractiveRouting]
```
<!--
  - Not going to go to much into detail with this.  
  - But - Say if you set your app globally to run in interactive mode.
  - There maybe a few pages though you want to render complegtely statically.
  - Adding this attribute will then get it to render statically.
-->

---

# When to use which mode

---

# Slide on example of how you can swap render data on server, then swap component to render WASM to sort it