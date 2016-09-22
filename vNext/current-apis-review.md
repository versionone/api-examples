# Action Ideas

* Let's create a new NuGet package called `VersionOne.SDK.NET.APIClient.VNext` or `VersionOne.SDK.NET.APIClient.Edge` or `VersionOne.SDK.NET.APIClient.Experimental` with newer, execiting features that we **want feedback on before we decide to integrate into the main build**. We should make it clear that the "support" mechanism for this library is the Gitter.IM channel for the repo and we welcome collaboration, but that the library **IS NOT** supported through ZenDesk -- Some of the features may make it to the main build if-and-only-if we get the right kind of positive feedback we want to see. (What is our concrete metric for making that decision?)
  * Consider doing similar things with the other languages as well to spur more external collaboration, but in a zero-risk way.
  * When external contributors demonstrate proven value and track record, allow them more responsibility for the `VNext` repo itself.

# Background

![Pigs or Chicken](https://www.braintrustgroup.com/img/chicken-pigs-cartoon.png)
http://www.implementingscrum.com/2006/09/11/the-classic-story-of-the-pig-and-chicken/

Historically, we have supported the [.NET](https://community.versionone.com/Developers/Developer-Library/Documentation/.NET_SDK) and [Java SDKs](https://community.versionone.com/Developers/Developer-Library/Documentation/Java_SDK) for programming against the VersionOne Lifecycle [rest-1](https://community.versionone.com/Developers/Developer-Library/Platform_Concepts/Endpoints/rest-1.v1%2F%2FData) and [query.v1](https://community.versionone.com/Developers/Developer-Library/Sample_Code/Tour_of_query.v1) API endpoints. These SDKs are showing their age in a dynamic world, and Python and JavaScript have caught up to the developer mindshare of the world.

We have a [Python SDK](https://github.com/versionone/VersionOne.SDK.Python) and a [JavaScript SDK](https://github.com/versionone/VersionOne.SDK.JavaScript) that are much more user-friendly, but they need some formal love.

# Problem / Opportunity

We cannot leave our customers or ourselves in the dark by failing to support Python and JavaScript formally. We need to rectify this situation.

We are not a company founded to make our personal programming language preferences of C# and Java safe and cushy for us as employees.

Instead, we are a company that reacts to our users and the market at large. Python and JavaScript are powerful forces at large and within our company, and we **ignore them at our company's peril**

# Python Motivations

* The Python SDK has become very popular in terms of both usage and external contributions
  * 12 contributors (including the original author Joe Koberg who is no longer listed in the contributors): https://github.com/versionone/VersionOne.SDK.Python/graphs/contributors?from=2012-05-06&to=2016-06-09&type=a
  * **7 of these people** are not V1 employees
* Python is the foundation of our Continuum product
* Python is the 2nd and 4th most popular programming language by two leading indexes
  * From [PYPL PopularitY of Programming Language](http://pypl.github.io/PYPL.html)
  ```
  Worldwide, Jun 2016 compared to a year ago:

  Rank	Language    Share    Trend
  1     Java        23.9 %    -0.5 %
  2     Python      12.8 %    +2.1 %
  3     PHP         10.5 %    -0.8 %
  4     C#           8.8 %    -0.5 %
  5     Javascript   7.7 %    +0.6 %
  ```
  * From TIOBE Index:
  ```
  Jun 2016     Jun 2015     Change  Programming Language    Ratings     Change
  1            1                    Java                    20.794%     +2.97%
  2            2                    C                       12.376%     -4.41%
  3            3                    C++                     6.199%      -1.56%
  4            6            up      Python                  3.900%      -0.10%
  5            4            down    C#                      3.786%      -1.27%
  ```

# JavaScript Motivations

The v1sdk, or JavaScript SDK, is a [published module](https://www.npmjs.com/package/v1sdk) in the NPM registry. It is written in emerging technologies and can be consumed in both the browser and in Node.js applications. Due to being written in the next version of JavaScript, ES2015, and the fact it utilizes modern testing practices, the SDK is not only forward thinking, but likely to attract the “right” kind of developers. However, its most compelling motivation to garner VersionOne’s official support is its increasing usage in our Core product.

The v1sdk is officially being used to power portions of budgets, timesheet, and most recently roadmapping; with its usage increasing each release cycle. With this utilization has come inevitable required changes in how it works, as well as bug fixes. These currently funnel through only a handful of individuals within VersionOne; creating a bottleneck that hinders our own feature work’s progression and quality. Officially supporting v1sdk will not only empower our developers to submit improvements, but send a message to the community; one that showcases VersionOne’s talents and usage of the technologies that current developers seek.

# Comparison of Python SDK and JavaScript SDK code to .NET and Java SDKs for the same operation

Ask yourself:

* Which of the following code samples are easiest to understand?
* Which would you like to write?
* Which could you explain to others?
* Which do you feel most comfortable maintaining?
* **Which would motivate you as a developer out in the wild to say: _VersionOne is keeping up with the times, and I'd love to work with that kind of code!_**


## Java SDK

[Querying with Filtering on Multiple Attributes](https://community.versionone.com/Developers/Developer-Library/Documentation/Java_SDK/Querying_Assets)

```java
V1Connector connector = V1Connector
    .withInstanceUrl("https://www14.v1host.com/v1sdktesting")
    .withUserAgentHeader("AppName", "1.0")
    .withAccessToken("1.rWM8lKLk+PnyFxkEWVX5Kl2u6Jk=")
    .build();
   
IServices services = new Services(connector);
Oid projectOid = services.getOid("Scope:0");
IAssetType assetType = services.getMeta().getAssetType("Defect");

Query query = new Query(assetType);
IAttributeDefinition projectAttribute = assetType.getAttributeDefinition("Scope");
IAttributeDefinition todoAttribute = assetType.getAttributeDefinition("ToDo");
query.getSelection().add(projectAttribute);
query.getSelection().add(todoAttribute);

FilterTerm projectTerm = new FilterTerm(projectAttribute);
projectTerm.equal(projectOid);
FilterTerm todoTerm = new FilterTerm(todoAttribute);
todoTerm.equal(0);

GroupFilterTerm groupFilter = new AndFilterTerm(projectTerm, todoTerm);
query.setFilter(groupFilter);

QueryResult result = services.retrieve(query); 
for (Asset task : result.getAssets()) {
    System.out.println(task.getOid().getToken());
    System.out.println(task.getAttribute(projectAttribute).getValue());
    System.out.println(task.getAttribute(todoAttribute).getValue());
    System.out.println();
}

/***** OUTPUT *****
Defect:37396
Scope:0
0.0

Defect:39675
Scope:0
0.0
******************/
```

## .NET SDK

[Querying with Filtering](https://community.versionone.com/Developers/Developer-Library/Documentation/.NET_SDK/Querying_Assets)

```csharp
V1Connector connector = V1Connector
    .WithInstanceUrl("https://www14.v1host.com/v1sdktesting")
    .WithUserAgentHeader("AppName", "1.0")
    .WithAccessToken("1.rWM8lKLk+PnyFxkEWVX5Kl2u6Jk=")
    .Build();
   
IServices services = new Services(connector);

IAssetType taskType = services.Meta.GetAssetType("Task");
Query query = new Query(taskType);

IAttributeDefinition nameAttribute = taskType.GetAttributeDefinition("Name");
IAttributeDefinition todoAttribute = taskType.GetAttributeDefinition("ToDo");
query.Selection.Add(nameAttribute);
query.Selection.Add(todoAttribute);
   
FilterTerm term = new FilterTerm(todoAttribute);
term.Equal(0);
query.Filter = term;
QueryResult result = services.Retrieve(query);
 
foreach (Asset task in result.Assets)
{
    Console.WriteLine(task.Oid.Token);
    Console.WriteLine(task.GetAttribute(nameAttribute).Value);
    Console.WriteLine(task.GetAttribute(todoAttribute).Value);
    Console.WriteLine();
}

/***** OUTPUT *****
Task:1153
Code Review
0
 
Task:1154
Design Component
0 ...
******************/
```

## Python SDK

```python
from v1pysdk import V1Meta

with V1Meta(
  instance_url = 'http://localhost/VersionOne',
  username = 'admin',
  password = 'admin'
) as v1:
    for s in (v1.Defect
      .filter("ToDo>'0'")
      .where(Scope='Scope:1003')
      .select('Name', 'ToDo', 'Description', 'Scope')
      ):
        output = """
Defect Name: {s.Name}
Description: {s.Description}
Todo: {s.ToDo}
Scope Name: {s.Scope.Name}
Scope: {s.Scope.Key}
        """
        print output.format(**locals())
```

## JavaScript SDK
```javascript
// **NOTE:** This assumes you have jQuery as a node module and is written in ES2015 syntax.
const jquery = require('jquery');
const v1sdk = require('./../dist/v1sdk');
const v1 = new v1sdk.V1Meta({
    "your.hosteddomain.com", // hostname
    "VersionOne", // instance name
    "80", // port
    "http", // protocol: http|https
    "admin", // basic auth username
    "admin", // basic auth password
    post: (url, data, headerObj) => $.ajax({
        url,
        method: 'POST',
        data,
        headers: headerObj,
        dataType: 'json'
    })
});

// Create an Actual
v1.create('Actual', { Value: 5.4, Date: new Date() })
    .then(console.log)
    .catch(console.log);

// Query for a Story
v1.query({
    from: 'Story',
    select: ['ID', 'Estimate'],
    where: 'ID=Story:1115'
})
// and update Story’s estimate by 1
.then(story => v1.update(story.ID, 'Story', { Estimate: story.Estimate + 1 })
.catch(console.log);
```

# Python SDK Ranked Current Challenges

* Mistmatch between relative community participation (high) with internal company understanding or participation (low)
* Lack of sufficient Integration Test suite, similar to what [Java](https://github.com/versionone/VersionOne.SDK.Java.APIClient/tree/master/src/test/java/com/versionone/sdk/integration/tests) and [.NET](https://github.com/versionone/VersionOne.SDK.NET.APIClient/blob/D-10588_support_pre-loading_meta_fix_FSpec/APIClient.IntegrationTests/IntegrationTests.cs) have
  * **IMPORTANT NOTE:** These are Business-facing, Outside-In tests that exercise the client functionality end-to-end against a real deployed VersionOne instance and thus provide the broadest and most reliable measure of the code's business value and correctness. Do not confuse these with the notion of a "unit test" that has become popular in current test-driven-development trends, wherein there is a dogmatic whiff / stench of endless preaching that a "unit test" should never communicate with outside systems, and thus all external dependencies must be "mock objects". Historically, the sharp distinction between a unit test and integration test was not intended by Kent Beck and other TDD luminaries, so do not let developers act religious about this. See this presentation by Ian Cooper for background: [TDD: Where Did It All Go Wrong?](https://www.infoq.com/presentations/tdd-original). For a good summary of the business value of integrations tests and the developer value of unit tests (in the current limited definition) see this: http://stackoverflow.com/questions/10752/what-is-the-difference-between-integration-and-unit-tests
* Lack of Community Site documentation
* Lack of internal company awareness
* Apparent lack of internal company concern :panda_face:, despite having and entire product now based on Python in Continuum
* Lack of unit tests with mocked objects
* In November 2014, it was announced that Python 2.7 would be supported until 2020, and reaffirmed that there would be no 2.8 release as users were expected to move to Python 3.4+ as soon as possible.
https://en.wikipedia.org/wiki/History_of_Python (more info here: https://hg.python.org/peps/rev/76d43e52d978)

# JavaScript SDK Ranked Current Challenges

* Socialization and V1 developer empowerment to support the SDK as needs change over time

## Evolving Java and .NET SDKs

We have prototyped simplified .NET querying as well. See this issue for details: https://github.com/versionone/VersionOne.SDK.NET.APIClient/issues/15

Note that Java also has lambda support now and we can use this to fluentize the client. See [Lambda Expression](https://docs.oracle.com/javase/tutorial/java/javaOO/lambdaexpressions.html) for details.

As an example in the .NET SDK spike:

```csharp
V1Connector
.WithInstanceUrl(BaseUrl)
.WithUserAgentHeader("Sample", "0.0.0")
.WithUsernameAndPassword(UserName, Password)
.Query("TeamRoom")
.Select("Team.Name", "Name")
.Success(assets => {
	foreach (dynamic asset in assets)
	{
		var team = asset["Team.Name"] + ":" + asset.Name;
		Console.WriteLine(team);
	}
})
.Execute();
```

Versus:

```csharp
V1Connector connector = V1Connector
            .WithInstanceUrl("https://www.MyV1INstance")
            .WithUserAgentHeader("HappyApp", "0.1")
            .WithUsernameAndPassword("login", "pwd")
            .Build();


        IServices services = new Services(connector);

        IAssetType trType = services.Meta.GetAssetType("TeamRoom");
        Query query = new Query(trType);
        IAttributeDefinition teamAttribute = trType.GetAttributeDefinition("Team.Name");
        IAttributeDefinition nameAttribute = trType.GetAttributeDefinition("Name");
        query.Selection.Add(teamAttribute);
        query.Selection.Add(nameAttribute);


        QueryResult result = services.Retrieve(query);
        Asset teamRooms = result.Assets[0];

        foreach (Asset story in result.Assets)
        {
            Console.WriteLine(story.Oid.Token);
            Console.WriteLine(story.GetAttribute(teamAttribute).Value);
            Console.WriteLine(story.GetAttribute(nameAttribute).Value);
            Console.WriteLine();
        }
```

### FluentQuery Examples in .NET

Here are more examples in the .NET spike. See them all in [this branch](https://github.com/versionone/VersionOne.SDK.NET.APIClient/blob/S-58164_FluentQuery/Example/GettingStarted/src/Program.cs#L39-L97).

```csharp
        public void GetTeamRoomsWithLambdas()
        {
            V1Connector
            .WithInstanceUrl(BaseUrl)
            .WithUserAgentHeader("Sample", "0.0.0")
            .WithUsernameAndPassword(UserName, Password)
            .Query("TeamRoom")
            .Select("Team.Name", "Name")
            .Where(
                Equal("Team.Name", "Brainstorm Team")
            )
            .Success(assets =>
            {
                foreach (Asset asset in assets)
                {
                    var team = asset["Team.Name"] + ":" + asset["Name"];
                    Console.WriteLine(team);
                }
            })
            .Execute();
            //.WhenEmpty(() => Console.WriteLine("No results found..."))
        }

        public void GetTeamRoomsWithRetrieve()
        {
            var queryResult = V1Connector
            .WithInstanceUrl(BaseUrl)
            .WithUserAgentHeader("Sample", "0.0.0")
            .WithUsernameAndPassword(UserName, Password)
            .Query("TeamRoom")
            .Select("Team.Name", "Name")
            .Retrieve();
            System.Console.WriteLine(queryResult);
        }

        public void ErrorExampleWithExecute()
        {
            V1Connector
            .WithInstanceUrl(BaseUrl)
            .WithUserAgentHeader("Sample", "0.0.0")
            .WithUsernameAndPassword(UserName, Password)
            .Query("JiraRoom")
            .Select("Team.Name", "Name")
            .Error(exception => Console.WriteLine("Exception! " + exception.Message))
            .Success(assets => Console.WriteLine("Assets count: " + assets))
            .Execute();
        }

        public void ErrorExampleWithRetrieve()
        {
            var queryResult = V1Connector
            .WithInstanceUrl(BaseUrl)
            .WithUserAgentHeader("Sample", "0.0.0")
            .WithUsernameAndPassword(UserName, Password)
            .Query("JiraRoom")
            .Select("Team.Name", "Name")
            .Error(exception => Console.WriteLine("Exception! " + exception.Message))
            .Retrieve();
        }
```

### Large Customer Support Ticket Before and After

Here is a real snip of current customer code, from a very large customer, from a real openAgile support ticket. The first snippet is the actual code, while the second is what it would look like if we support the FluentQuery approach.

#### Current .NET SDK code

This is the code our customer had to write with the state of the current .NET SDK:

```csharp
var connector = V1Connector
  .WithInstanceUrl(BaseUrl)
  .WithUserAgentHeader("Sample", "0.0.0")
  .WithUsernameAndPassword(UserName, Password);

var services = new Services(connector);

var StoryAssetType = MetaModel.GetAssetType("Story");
var StoryNameAttribute = MetaModel.GetAttributeDefinition("Story.Name");
var StoryReferenceAttribute = MetaModel.GetAttributeDefinition("Story.Reference");
var StoryNumberAttribute = MetaModel.GetAttributeDefinition("Story.Number");
var StoryStatusAttribute = MetaModel.GetAttributeDefinition("Story.Status");
var StoryPriorityAttribute = MetaModel.GetAttributeDefinition("Story.Priority");
var StoryScopeAttribute = MetaModel.GetAttributeDefinition("Story.Scope");
var StoryOwnersAttriute = MetaModel.GetAttributeDefinition("Story.Owners");
var StoryDescriptionAttribute = MetaModel.GetAttributeDefinition("Story.Description");
var StorySprintAttribute = MetaModel.GetAttributeDefinition("Story.Timebox");
var StoryEpicAttribute = MetaModel.GetAttributeDefinition("Story.Super");
var StoryStateAttribute = MetaModel.GetAttributeDefinition("Story.AssetState");
var StoryProjectAttribute = MetaModel.GetAttributeDefinition("Story.Project");

Query AssetQuery = new Query(StoryAssetType);
AssetQuery.Selection.Add(StoryNameAttribute);
AssetQuery.Selection.Add(StoryReferenceAttribute);
AssetQuery.Selection.Add(StoryNumberAttribute);
AssetQuery.Selection.Add(StoryStatusAttribute);
AssetQuery.Selection.Add(StoryPriorityAttribute);
AssetQuery.Selection.Add(StoryScopeAttribute);
AssetQuery.Selection.Add(StoryOwnersAttriute);
AssetQuery.Selection.Add(StoryDescriptionAttribute);
AssetQuery.Selection.Add(StorySprintAttribute);
AssetQuery.Selection.Add(StoryEpicAttribute);
AssetQuery.Selection.Add(StoryStateAttribute);
AssetQuery.Selection.Add(StoryProjectAttribute);

FilterTerm Filter = new FilterTerm(StoryReferenceAttribute);
Filter.Equal(reference);

AssetQuery.Filter = Filter;

var queryResult = services.Retrieve(AssetQuery);
```

#### Customer Code Refactored (Not Much Better)

Because the SDK is so cumbersome, customers often write code, like above, that is even more verbose than it needs to be. However, this is not entirely without reason. As you can see, they created variable names that speak to the human terms like `Sprint`, and `Project` instead of machine terms like `Timebox` and `Scope`. 

At best, we could condense their code to this in absence of FluentQuery:

```csharp
var connector = V1Connector
  .WithInstanceUrl(BaseUrl)
  .WithUserAgentHeader("Sample", "0.0.0")
  .WithUsernameAndPassword(UserName, Password);

var services = new Services(connector);

var assetType = MetaModel.GetAssetType("Story");
Query AssetQuery = new Query(assetType);
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Name"));
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Reference"));
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Number"));
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Status"));
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Priority")););
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Scope"));
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Owners")););
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Description")););
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Timebox")););
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Epic")););
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("AssetState")););
AssetQuery.Selection.Add(assetType.GetAttributeDefinition("Scope")););

FilterTerm Filter = new FilterTerm(assetType.GetAttributeDefinition("Reference"));
Filter.Equal(reference);

AssetQuery.Filter = Filter;

var queryResult = services.Retrieve(AssetQuery);
```

#### With .NET FluentQuery spike

And here's what it would look like with FluentQuery support:

```csharp
var reference = "Some reference value...";
var queryResult = V1Connector
  .WithInstanceUrl(BaseUrl)
  .WithUserAgentHeader("Sample", "0.0.0")
  .WithUsernameAndPassword(UserName, Password)
  .Query("Story")
  .Select("Name", "Reference", "Number", "Status", 
        "Priority", "Scope", "Owners", "Description", 
        "Timebox", "Super", "AssetState")
  .Where(
    Equal("Reference", reference)
  )
  .Retrieve();
```

# The choice is ours; the choice is yours

:pig: or :chicken:?
