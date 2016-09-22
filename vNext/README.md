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
















