# Ideas about Market Place / Extensibility
* Allow reusable UI components from Lifecycle to be easily used inside of an add-on to run within Lifecycle.
  * Data to populate components can be sourced from Lifecycle data APIs
  * Or, data can come from their own data APIs

# General API Needs

## Batch Support
* JSON / YAML based (forget XML)
* Batch attribute update based on query results
 * Allow nested updates
* Batch operation execution based on query results
 * Allow nested execution
* Batch create parent-child / asset graphs with implicit relationships automatically wired up
* WebHooks (probably based on ActivityStream)

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
