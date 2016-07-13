# Background

The [query.v1](https://community.versionone.com/Developers/Developer-Library/Sample_Code/Tour_of_query.v1) endpoint has been around for a while and provides powerful support and simple SQL-like syntax for reading Assets from VersionOne Lifecycle. Customers trying to use this endpoint find it useful, though they do sometimes have trouble constructing YAML strings inside of a language like Java that does not support multi-line strings.

There are a number of things we can do to improve this, all while retaining the power and flexibility of the `query.v1` endpoint.

# Add a fluent interface for building YAML queries

Here's an example from a real support ticket:

## Customer code:

```java
String rnQuery = "@\" from: Defect select: - Name - Number - Description - Resolution - from: Children:Task select: - Name - Description where: Name: Release Notes where: Scope.Name: "+ v1ProjectName + "@\" Timebox.Name: " + v1Sprint + "\"";

rnQueryResult = services.executePassThroughQuery(rnQuery);
```

## Actual Java code that works

We helped them correct this with this code:

```java
String rnQuery = "from: Defect select: \n" +
" - Name\n" +
" - Number\n" + 
" - Description\n" + 
" - Resolution\n" + 
" - ResolutionReason.Name\n" + 
" - from: Children:Task\n" + 
" select:\n" + 
" - Name\n" + 
" - Description\n" + 
" where:\n" + 
" Name: Release Notes\n" + 
"where:\n" + 
" Scope.Name: " + v1ProjectName + "\n" +
" Timebox.Name: " + v1Sprint; 
```

## Spike of fluent interface to build query.v1 queries

We've spiked out a way to build the YAML via a fluent interface, which makes the experience much easier and less error-prone, removing the need for manual string construction. So far, we've done this in C#, but because [Java supports](http://docs.oracle.com/javase/1.5.0/docs/guide/language/static-import.html) `import static` (and has long before C# 6 added `using static`) the interface can look very clean in both languages.

### C# Fluent builder spike

Remember, this same syntax can work in Java. For that matter, we could do the same in JavaScript and Python.

```c#
var v1ProjectName = "System (All Projects)";
var v1Sprint = "Iteration 1";
var query =
    From("Defect")
    .Select(
        "Name",
        "Number",
        "Description",
        "Resolution",
        "ResolutionReason.Name",
        From("Children:Task")
            .Select(
                "Name",
                "Description"
            )
            .Where(
                Equal("Name", "Release Notes")
            )
    )
    .Where(
        Equal("Scope.Name", v1ProjectName),
        Equal("Timebox.Name", v1Sprint)
    );
```

Resulting YAML:

```yaml
from: Defect
select:
- Name
- Number
- Description
- Resolution
- ResolutionReason.Name
- from: Children:Task
  select:
  - Name
  - Description
  where:
    Name: Release Notes
where:
  Scope.Name: System (All Projects)
  Timebox.Name: Iteration 1
```







