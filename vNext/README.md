# API Powerups Improvements Roadmap

To become ready for Marketplace, we need to improve our APIs. The following outline provides a roughly sequential, but also iterative, plan for doing so. This is based on many prototypes and feedback from a diverse group of V1ers as well as support customers and Services team members.

## Lifecycle external

* [API Documentation Improvements (Walkthrough exercises)](https://www7.v1host.com/V1Production/Epic.mvc/Summary?oidToken=Epic%3A845027) by V1 -- power up end users, support team, broader dev team, services team
* MetaTrain into builds by V1, Community -- power up everyone who writes queries against Lifecycle, seriously everyone, and even those who don't write them
 * Blog about it, and 
 * Add wish list for improvements to GitHub issues list, and encourage active community participation and pull-requests with rewards
* Meta-Free Fluent Query support in .NET API Client by V1 -- power up .NET users using our SDK (and power down the extra keystrokes, and extra support tickets dealing with the verbose nature of meta-based `IAttributeDefinition`, `FilterTerm`, `Services`, and family)
* Fluent Query support in JS Client by V1 -- same benefits as above, but also introduce CONSISTENCY across our clients
* Update Walkthrough exercises to link to .NET and JavaScript client samples to parallel the `cURL` samples
* Fluent Query support in Java by Community -- seek community contribution for this with consistency with the .NET and JS clients
* Fluenty Query support in Python by Community -- seek community contribution for this with consistency with .NET and JS clients

## Lifecycle internal

* Module for batch support in API at `/api/Assets` (or whatever) by V1, Community -- provide YAML/JSON asset creation for a complete tree of assets plus single-level deep query-based update and execute operation support. (Piggy back on query.v1 for fetching the assets, but still implement in C#)
 * This module should itself be open-source, taking a dependency upon nothing more than `VersionOne.Contract`
 * Add wish list in GitHub for supporting multiple-level-deep query based update and execution support. See if anyone bites.
   * If nobody bites and sends a pull-request, we can of course implement this ourselves, but it should **not** come before iterating through the remainder of this list. Instead it should be a later iteration.
* Continuous deployment build with Troy/Q's help on Azure or Skytap for our new and improved APIs -- we can point our "early adopter", elite group at this instance to get feedback without disrupting the main Lifecycle process
 * Partners can start using this build to prototype their own enhancements based upon the new batch API at `/api/Assets`!
   * Furthermore: partners could start craft their own endpoints like `/api/alm-connect` and potentially their own visible features like just `/alm-connect`, under which they would be free to craft their own API endpoints however they like.
 * Services team members like Jess can use whatever code library or no library they damn well please to take advantage of this new API (he doesn't use our SDKs, just in case you were wondering he prefers strings)
 * Invite power coders from support tickets to try things out or go visit them in Atlanta and observe them
 * At the same time, be creating the **Success Stories** and **Case Studies** that demonstrate the value we are creating with these early pioneers
  * Once we've delighted these early users and address issues, and have a growing body of GitHub contributions, **only then is it time to roll out the new endpoint for mass consumption** with the complete body of success stories, how-tos, and what's coming already churning
 * Note: consider using hot-deployment of the `VersionOne.Module.AssetsApi` or other new features
   * Related: Partners could also just get a build from us and install locally, but with a Hot-deployment capability, this could allow them to start crafting their own `api/partner-integration` endpoint without needing any access to our proprietary code base
* WebHooks (probably based on ActivityStream)

## Marketplace

TODO

![image](https://cloud.githubusercontent.com/assets/1863005/18800840/49766168-81ad-11e6-919a-014c5f80786d.png)

# Ideas about Market Place / Extensibility
* Allow reusable UI components from Lifecycle to be easily used inside of an add-on to run within Lifecycle.
  * Data to populate components can be sourced from Lifecycle data APIs
  * Or, data can come from their own data APIs

# Current ideas and spikes for API improvements and new APIs

## Replace .NET API Client with new lightweight client

* Support query.v1 fluenty query builder. (See WIP here: https://github.com/JogoShugh/VersionOneRestSharpClient/blob/master/VersionOneRestSharpClient/Example/Example.cs#L44)
* Support meta-free query builder with a variety of Where/Filter free-standing functions utilizing C# 6.0 `using static` goodness (WIP: https://github.com/JogoShugh/VersionOneRestSharpClient/blob/master/VersionOneRestSharpClient/Example/Example.cs#L228)
* Support simplified `Create`, `Update` methods with anonymous C# objects to simplifiy updating both Scalar attributes and Relationship based lists of OID Token references. (WIP: https://github.com/JogoShugh/VersionOneRestSharpClient/blob/master/VersionOneRestSharpClient/Example/Example.cs#L228)
* Support `dynamic` keyword of C# for simple code (WIP: https://github.com/JogoShugh/VersionOneRestSharpClient/blob/master/VersionOneRestSharpClient/Example/Example.cs#L228)

## Batch support for updating via `query.v1` or appropriately named endpoint.

See WIP at https://github.com/versionone/api-examples/blob/master/rest/write.v1.md and https://github.com/versionone/api-examples/blob/master/rest/yaml-batch-children.md 

* Support updating multiple types of assets in one payload
* Once completed, update the .NET client above to ease batch operations.

## Support posting a graph of assets in a single payload, with inferred relationships based on ReciprocalRelation

* Example: Be able to create a new Scope and one or more Workitems (ex: Story) and one or more Children on each Workitem (ex: Task or Tests)
* See WIP here: https://github.com/JogoShugh/VersionOneRestSharpClient/blob/master/VersionOneRestSharpClient/Example/Example.cs#L178

## Support lightweight interface for utilizing `Services` and `Query2` via Arena Plugins in Lifecycle

Assuming interfaces named `IDynamicAsset` and `IServicesClient` that live in `VersionOne.Arena.Contract`:

* `IDyanamicAsset` would inherit from .NET's DynamicObject to support the `dynamic` keyword and a meta-free experience for coding. See example from http://github.com/JogoShugh/VersionOneRestSharpClient/blob/master/VersionOneRestSharpClient/Client/Asset.cs
* `IServicesClient` would be similar to what's in the spike at https://github.com/JogoShugh/VersionOneRestSharpClient/, having simple `Create`, `Update`, and `Query` methods (but also able to post batches and Asset Graphs as outlined in the previous item) 
  * Inside of `VersionOne.DLL`, there would be a concreate implementation of `IServicesClient` marked for `[Export]` via MEF. Thus, Arena modules that are compiled against `VersionOne.Arena.Contract` would acquire this at runtime via an `[Import]` and would never directly depend on the proprietary `VersionOne.DLL` assembly.

# Incubator Ideas storm

Add one initials-prefixed bullet per idea, no matter how immediately practical or not. Use sub-bullets for fleshing them out a bit.

* JG/WS: Add formal CORS support for VersionOne instances -- allow to be toggled in System Administration such that the admin can provide a proper `Access-Control-Allow-Origin` and related headers. See prior branch experiments here:  https://github.com/versionone/VersionOne.SDK.Experimental/tree/master/ApiInputTranslatorPlugins
* JG: What are the documentation hurdles that our own teammates in V1 Dev teams, Support, and Services face when trying to code against the API or support customers?
* JG: Within our existing Java, .NET, Python, and JavaScript API client, what's a quick win we can take to make a large number of people happier?
* AndrewS: GraphQL based support for querying v1
* MI/JG: Clear documentation examples included for all relationship based filters like SubsMeAndDown, etc
  * API documentation similar to Twitter, Google, Heroku with cURL examples embedded inline
  * JG: Runnable code samples on line -- See https://community.versionone.com/Developers/DekiScript_Testing and the corresponding GitHub code samples repo here: https://github.com/versionone/CommunitySite.CodeSamples/tree/master/CommunitySite.CodeSamples/rest-1.v1
* JG/MI: Query builder that generates URI for plugging into any SDK or curl
* JG/MI: Generate clients in all languages based on code generation like t4 templates maybe
* MI: Generate a client with intellisense from Meta query -- Could this be intelligently populated at runtime inside VS? See F# Type Providers for real-world examples: https://msdn.microsoft.com/en-us/library/hh156509.aspx
* JG: LINQ on top of meta?
* JG: oData support for meta (great for excel, the number one agile tool on Earth) http://www.odata.org/
* CW: JSON data schema for API results and queries http://jsonapi.org/
