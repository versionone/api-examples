# Background

The [query.v1](https://community.versionone.com/Developers/Developer-Library/Sample_Code/Tour_of_query.v1) endpoint has been around for a while and provides powerful support and simple SQL-like syntax for reading Assets from VersionOne Lifecycle. Customers trying to use this endpoint find it useful, though they do sometimes have trouble constructing YAML strings inside of a language like Java that does not support multi-line strings.

There are a number of things we can do to improve this, all while retaining the power and flexibility of the `query.v1` endpoint.

# Idea 1: Add a fluent interface for building YAML queries

This document is primarily about `writing`, not querying, but first a quick detour. Here's an example from a real support ticket:

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

# Idea 2: Extend capabilities of the YAML syntax to support writing

Leaving aside the question of whether we introduce a new `write.v1` or just enhance the existing `query.v1` endpoint, here are some ideas:

## Support POSTing new Assets and updates

### Create new

```yaml
asset: Story
attributes:
 Name: My name
 Description: My description
 Owners: Member:20
```

### Update existing

```yaml
asset: Story:12235
attributes:
 Name: My updated name
 Description: My updated description
 Owners:
  replace: # replaces Member:20 with Member:50
   - Member:20
   - Member:50
```
Alternatively

```yaml
asset: Story:1234
attributes:
 Name: My updated name
 Description: My updated description
 Owners:
  remove:
  - Member:20
  add:
  - Member:50
```

## Support POSTing batching of new Assets and updates in a single payload

Because YAML makes it easy to separate multiple documents, we can easily support batch requests:

```yaml
asset: Scope
attributes:
 Name: New Scope
 Owner: Member:20
---
asset: Story
attributes:
 Name: My new Story
 Description: My description
 Scope: Scope:12345
---
asset: Story
attributes:
 Name: My second new Story
 Description: My second description
 Scope: Scope:12345
```

Now, you may ask yourself, "What if I want the two new stories to be workitems WITHIN the new Scope instead of having to manually specify `Scope:12345`, something that already exists?

### Creating a new Scope with new Workitems, each of which itself has new Children

Good question, here's a proposed syntax:

```
asset: Scope
attributes:
 Name: New Scope with Workitems
 Owner: Member:20
 Workitems:
 - asset:Story
   attributes:
    Name: New Story under new scope
    Description: New desc
    Children:
    - asset: Task
      attributes:
       Name: Task 1
    - asset: Test
      attributes:
       Name: Test 1
 - asset:Story 
   attributes:
    Name: Second story
    Description: A desc
    Children:
    - asset: Task
      attributes:
       Name: Another task
    - asset: Test
      attributes:
       Name: Another test
```

In this case, we would parse the top-level document and create it with its scalar attributes, and then look at the attributes that match what normally are read-only relationships and then attempt to create new Asset items based on their definitions, creating them in the correct way with the appropriate `Scope` reference when we make them. Obviously, it might take some mapping and creative distpatch tabling in the endpoint to know which Attributes are actuall Relationships and so forth.

#### Doing the above in C#

What might creating this tree of Assets look like via C# or Java to avoid forcing people to build up strings when that's not convenient? (Think OpsHub, Tasktop, other big partners that are mapping and syncing, not just ad-hoc querying). Here's an idea:

Below is in C#, it would look the cleanest, because of anonymous types, but in Java we could use hash maps and other convience classes or a lightweight, generic `Asset` type:

``c#

client.Create("Scope",
new {
 Name = "New Scope with Workitems",
 Owner = Relation("Member:20"),
 Workitems = Assets(
    Asset("Story", new {
        Name = "Story under new Scope",
        Description = "New desc",
        Children = Assets(
            Asset("Task", new {
                Name = "Task 1"
            }),
            Asset("Test", new {
                Name = "Test 1"
            })
        )
    }),
    Asset("Story", new {
        Name = "Second story",
        Description = "A desc",
        Children = Assets(
            Asset("Task", new {
                Name = "Task 1"
            }),
            Asset("Test", new {
                Name = "Test 1"
            })
        )
     )  
    }
 )
});
```






