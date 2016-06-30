# Walkthrough of Using the VersionOne Lifecycle REST APIs

This hands-on walkthrough shows you the basics for how to perform four types of VersionOne Lifecycle REST Data API requests:
* Querying existing assets
* Creating new assets
* Updating assets
* Executing operations on assets

## Exercises

You'll step through seven brief exercises to complete this walkthrough. Each one builds upon knowledge gained in the previous one.

* Exercise 1: Understand the REST Data API fundamentals
* Exercise 2: Find a Scope (Project) OID Token in the UI
* Exercise 3: Query a Scope for specific attributes
* Exercise 4: Create your own Story (Backlog Item) within a Scope
* Exercise 5: Query your Story
* Exercise 6: Update your Story
* Exercise 7: Close your Story!

## How to try this against our public test instance

You'll need just two things:

1. [cURL](https://curl.haxx.se) -- a popular command-line HTTP client
2. This VersionOne Lifecycle Access Token in your clipboard: `1.aBg7sVXSZeEsf3cwvQFEdkkt384=` -- a credential to authenticate with the REST Data API

### Setup

This walkthrough assumes you are on a system that can run the popular **cURL** command-line HTTP client. cURL is available for a huge variety of operating systems. [Download it here](https://curl.haxx.se/download.html) if you don't already have it.

Once you have cURL installed, you can try it against a test URL, like Google, by simply typing `curl 'http://www.google.com'` and pressing enter. 

You should then see a result like this:

![cURL Google](https://cloud.githubusercontent.com/assets/1863005/16272737/f3fd2c50-386c-11e6-864f-408abbe67a86.png)

Unlike the Google's public web site, the VersionOne Lifecycle APIs require authentication/authorization. So, you'll need to use an Access Token. The value of the token is `1.aBg7sVXSZeEsf3cwvQFEdkkt384=` for our example instance. Using this with cURL is easy. Here is the format:

```shell
curl 'http://domain/path' -H 'Authorization:Bearer <access token>'
```

For example, to get the details of the Admin Member from our VersionOne Lifecycle examples instance, run this cURL command:

```shell
curl 'https://www16.v1host.com/api-examples/rest-1.v1/Data/Member/20' -H 'Authorization:Bearer 1.aBg7sVXSZeEsf3cwvQFEdkkt384='
```
You should then see a result like this:

![cURL VersionOne Lifecycle Admin Member](https://cloud.githubusercontent.com/assets/1863005/16494319/e8b463cc-3eb6-11e6-9993-22afc846b8ad.png)

### Review

Don't worry if you don't understand much about the URLs or the results you've seen yet. You'll learn all about that starting in _Exercise 1: Understand the REST Data API fundamentals_.

## How to try this against your own VersionOne Lifecycle instance

If you'd like to adapt this walkthrough to run against your own instance of VersionOne Lifecycle, you'll need to [generate an Access Token as described in the VersionOne Community site](https://community.versionone.com/Help-Center/Lifecycle_System_Asset_Diagram_and_Descriptions/Managing_Your_Member_Account_Details/Authorizing_Application_Access). Once you've generated the token, simply use that token and your own instance URL in place of the token and URL that the exercises contain.

## Exercise 1: Understand the REST Data API fundamentals

Before you get started, let's take a moment to understand some fundamentals about VersionOne Lifecycle's REST Data API.

### What you'll learn

* How to construct the base Data API URL for any VersionOne Lifecycle instance
* How to deconstruct an asset's OID Token and use it to contruct the exact Data API URL for that asset
* Understand the two supported output formats for Data API responses

### Step 1: Construct the base Data API URL for our examples instance

Assuming you are following along with our public examples instance, then the instance URL is:

`https://www16.v1host.com/api-examples`

This URL breaks down into three parts: `protocol://host/instance` (`https`, `www16.v1host.com`, and `api-examples`)

Given this starting point, the REST Data API endpoint is simple to construct by adding `/rest-1.v1/Data` to the end. Doing this to the examples instance URL produces this:

`https://www16.v1host.com/api-examples/rest-1.v1/Data`

### Step 2: Construct the base Data API URL for your own instance

If, however, you are using your own VersionOne Lifecycle instance, then you may have a different host and will definitely have a different instance name.

If, for example, your instance URL is `https://www7.v1host.com/acme`, then the REST Data API endpoint for that instance will be:

`https://www7.v1host.com/acme/rest-1.v1/Data`

### Step 3: Construct the Data API URL for an asset

As you probably already know from using VersionOne Lifecycle, the business objects within the system are called [assets](https://community.versionone.com/Developers/Developer-Library/Platform_Concepts/Asset). All assets in a particular instance are identified by the combination of their [asset type](https://community.versionone.com/Developers/Developer-Library/Platform_Concepts/Asset_Type) and a unique integer collectively called an [OID Token](https://community.versionone.com/Developers/Developer-Library/Platform_Concepts/OID_Token). 

For example, `Member:20` is an OID Token that uniquely identifies a Member asset within an instance. It just so happens that this particular combination **always** identifies the Admin user within an instance because of the way the database gets initialized.

While you are used to viewing assets within the UI, you may not know that all assets **automatically** have their own REST Data API URLs as well.

The URL for the default API representation of an asset always follows the following simple form:

`https://www16.v1host.com/api-examples/rest-1.v1/Data/<asset type>/<asset id>`

Thus, to construct the address for the `Member:20` asset, you start by breaking it apart on the `:`, producing an asset type of `Member`, and an asset id of `20`. Then, you just plug them into the form above, producing:

`https://www16.v1host.com/api-examples/rest-1.v1/Data/Member/20`

### Step 4: Understand the XML and JSON Data API response formats

TODO

## Exercise 2: Find a Scope (Project) OID Token in the UI

Now that you understand what an OID Token is and how to construct a Data API URL for an asset with it, you can start using the API!

But, how do you _actually find_ an OID Token from within VersionOne LifeCycle's UI?

This exercise shows you how to do exactly that for a Scope asset (known as **Project** to the UI) and to query the REST Data API for it.

While this exercise may seem basic, it provides a solid foundation for you to learn  more sophisticated and powerful query parameters and options.

### What you'll learn

* How to find the [OID Token](https://community.versionone.com/Developers/Developer-Library/Platform_Concepts/OID_Token) of a Scope within the VersionOne Lifecycle UI
* How to use the numeric porition of an OID Token to get the default API representation of a Scope using cURL

### Step 1: View the Projects page in Lifecycle

To get started, [navigate to the **Projects** page in our Lifecycle example instance](https://www16.v1host.com/api-examples/Default.aspx?menu=ProjectAdminPage&feat-nav=a1). Use the username `admin` and password `admin` when prompted.

You should see a page like this after authenticating:

![Projects page](https://cloud.githubusercontent.com/assets/1863005/16241266/ec88673a-37ba-11e6-9fdf-a7a4f500195d.png)

### Step 2: View the VersionOne Lifecycle REST APIs 101 Project

Click the project named `VersionOne Lifecycle REST APIs 101` to open it in a dialog window. Once it pops open click the full-screen option from the top right, highlighted here:

![Project dialog](https://cloud.githubusercontent.com/assets/1863005/16241937/0db32aaa-37be-11e6-833d-db14e9df21d4.png)

Clicking that will open the Project in a fullscreen window or tab, like below:

![Project fullscreen](https://cloud.githubusercontent.com/assets/1863005/16242154/137459cc-37bf-11e6-9774-a803562f9062.png)

Now:

* Copy the text after the **=** sign in the highlighted `oidToken` query string parameter to your clipboard, **but note this:** You may see **oidToken=Scope%3A1005** instead of **oidToken=Scope:1005**. This is because of URL encoding. Just know that **%3A** is the encoded value for the **:** character.
* Paste this value (should be **Scope:1005** or **Scope%3A1005**) into a text editor and then:
  * Replace the **:** (or **%3A** characters) with a single **/** character, producing **Scope/1005**
  * Finally, prefix the whole thing with another single **/** character, producing **/Scope/1005**

Still with us? Keep your **/Scope/1005** text handy, and head to Step 3!

### Step 3: Execute query via cURL

Now, recall from Exercise 1 that the form for constructing the URL for an asset's Data API URL is as follows:

`https://www16.v1host.com/api-examples/rest-1.v1/Data/<asset type>/<asset id>`

Combining the base Data API URL, `https://www16.v1host.com/api-examples/rest-1.v1/Data` with what's in your clipboard, **/Scope/1005** produces the following URL:

`https://www16.v1host.com/api-examples/rest-1.v1/Data/Scope/1005`

Now that you have the asset's Data API URL, you can fetch it with cURL using this command:

```curl
curl 'https://www16.v1host.com/api-examples/rest-1.v1/Data/Scope/1005' -H "Authorization:Bearer 1.aBg7sVXSZeEsf3cwvQFEdkkt384="
```

#### Expected result

You should get back a result that looks like this:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<Asset href="/api-examples/rest-1.v1/Data/Scope/1005" id="Scope:1005">
  <Attribute name="AssetType">Scope</Attribute>
  <Relation name="Schedule" />
  <Relation name="Parent">
    <Asset href="/api-examples/rest-1.v1/Data/Scope/0" idref="Scope:0" />
  </Relation>
  <Relation name="Owner" />
  <Attribute name="AssetState">64</Attribute>
  <Attribute name="BeginDate">2016-06-19</Attribute>
  <Attribute name="Description">Learn how to use the VersionOne Lifecycle REST APIs with 
  hands-on exercises.</Attribute>
  <Attribute name="Name">VersionOne Lifecycle REST APIs 101</Attribute>
  <Attribute name="EndDate" />
  <Relation name="Status" />
  <Relation name="TestSuite" />
  <Relation name="SecurityScope">
    <Asset href="/api-examples/rest-1.v1/Data/Scope/1005" idref="Scope:1005" />
  </Relation>
  <Relation name="Scheme">
    <Asset href="/api-examples/rest-1.v1/Data/Scheme/1001" idref="Scheme:1001" />
  </Relation>
  <Attribute name="Reference" />
  <Attribute name="TargetEstimate" />
  <Attribute name="TargetSwag" />
  <Relation name="PlanningLevel" />
  <Attribute name="Schedule.Name" />
  <Attribute name="Parent.Name">System (All Projects)</Attribute>
  <Attribute name="Owner.Name" />
  <Attribute name="Owner.Nickname" />
  <Attribute name="Status.Name" />
  <Attribute name="TestSuite.Name" />
  <Attribute name="SecurityScope.Name">VersionOne Lifecycle REST APIs 101</Attribute>
  <Attribute name="Scheme.Name">Default Scheme</Attribute>
  <Attribute name="PlanningLevel.Name" />
  <Relation name="BuildProjects" />
  <Attribute name="BuildProjects.Name" />
  <Attribute name="Ideas" />
</Asset>
```

To see the result as JSON, run this cURL command:

```curl
curl 'https://www16.v1host.com/api-examples/rest-1.v1/Data/Scope/1005' -H "Authorization:Bearer 1.aBg7sVXSZeEsf3cwvQFEdkkt384=" -H "Accept:application/json"
```

#### Expected result

```json
{
  "_type": "Asset",
  "href": "\/api-examples\/rest-1.v1\/Data\/Scope\/1005",
  "id": "Scope:1005",
  "Attributes": {
    "AssetType": {
      "_type": "Attribute",
      "name": "AssetType",
      "value": "Scope"
    },
    "Schedule": {
      "_type": "Relation",
      "name": "Schedule",
      "value": null
    },
    "Parent": {
      "_type": "Relation",
      "name": "Parent",
      "value": {
        "_type": "Asset",
        "href": "\/api-examples\/rest-1.v1\/Data\/Scope\/0",
        "idref": "Scope:0"
      }
    },
    "Owner": {
      "_type": "Relation",
      "name": "Owner",
      "value": null
    },
    "AssetState": {
      "_type": "Attribute",
      "name": "AssetState",
      "value": 64
    },
    "BeginDate": {
      "_type": "Attribute",
      "name": "BeginDate",
      "value": "2016-06-19"
    },
    "Description": {
      "_type": "Attribute",
      "name": "Description",
      "value": "Learn how to use the VersionOne Lifecycle REST APIs with hands-on exercises."
    },
    "Name": {
      "_type": "Attribute",
      "name": "Name",
      "value": "VersionOne Lifecycle REST APIs 101"
    },
    "EndDate": {
      "_type": "Attribute",
      "name": "EndDate",
      "value": null
    },
    "Status": {
      "_type": "Relation",
      "name": "Status",
      "value": null
    },
    "TestSuite": {
      "_type": "Relation",
      "name": "TestSuite",
      "value": null
    },
    "SecurityScope": {
      "_type": "Relation",
      "name": "SecurityScope",
      "value": {
        "_type": "Asset",
        "href": "\/api-examples\/rest-1.v1\/Data\/Scope\/1005",
        "idref": "Scope:1005"
      }
    },
    "Scheme": {
      "_type": "Relation",
      "name": "Scheme",
      "value": {
        "_type": "Asset",
        "href": "\/api-examples\/rest-1.v1\/Data\/Scheme\/1001",
        "idref": "Scheme:1001"
      }
    },
    "Reference": {
      "_type": "Attribute",
      "name": "Reference",
      "value": null
    },
    "TargetEstimate": {
      "_type": "Attribute",
      "name": "TargetEstimate",
      "value": null
    },
    "TargetSwag": {
      "_type": "Attribute",
      "name": "TargetSwag",
      "value": null
    },
    "PlanningLevel": {
      "_type": "Relation",
      "name": "PlanningLevel",
      "value": null
    },
    "Schedule.Name": {
      "_type": "Attribute",
      "name": "Schedule.Name",
      "value": null
    },
    "Parent.Name": {
      "_type": "Attribute",
      "name": "Parent.Name",
      "value": "System (All Projects)"
    },
    "Owner.Name": {
      "_type": "Attribute",
      "name": "Owner.Name",
      "value": null
    },
    "Owner.Nickname": {
      "_type": "Attribute",
      "name": "Owner.Nickname",
      "value": null
    },
    "Status.Name": {
      "_type": "Attribute",
      "name": "Status.Name",
      "value": null
    },
    "TestSuite.Name": {
      "_type": "Attribute",
      "name": "TestSuite.Name",
      "value": null
    },
    "SecurityScope.Name": {
      "_type": "Attribute",
      "name": "SecurityScope.Name",
      "value": "VersionOne Lifecycle REST APIs 101"
    },
    "Scheme.Name": {
      "_type": "Attribute",
      "name": "Scheme.Name",
      "value": "Default Scheme"
    },
    "PlanningLevel.Name": {
      "_type": "Attribute",
      "name": "PlanningLevel.Name",
      "value": null
    },
    "BuildProjects": {
      "_type": "Relation",
      "name": "BuildProjects",
      "value": [
        
      ]
    },
    "BuildProjects.Name": {
      "_type": "Attribute",
      "name": "BuildProjects.Name",
      "value": [
        
      ]
    },
    "Ideas": {
      "_type": "Attribute",
      "name": "Ideas",
      "value": [
        
      ]
    }
  }
}
```

### Review
At this point, you should know how to do the two tasks described in the beginning. Also note:
* You can find the `oidToken` for almost all asset types in the same way you just did for Scope
* The XML and JSON output you see above is the **default** representation of the asset, but there are many more parameters and options for customizing query results. Continue to _Exercise 2: Query a Scope for specific attributes_ to learn about the `sel` parameter.

# NOTES:

* a subset of its attributes (`Name`, `Description`, `Members`, `CreateDate`)
* Exercise 3: Create a Story (also known as **Backlog Item** or **Requirement** to the UI) that belongs to this Scope (by referencing its [OID Token](https://community.versionone.com/Developers/Developer-Library/Platform_Concepts/OID_Token))
* Exercise 4: Query your new Story for a subset of its attributes (`Name`, `Description`, `Estimate`, `Status`, and `Owners`)
* Exercise 5: Update your new Story to modify its `Description`, `Status`, and `Owners` attributes
* Exercise 6: Execute the **Close** operation on your Story



## Example: Query a Story from VersionOne Lifecycle
* Use cURL example to query for Attributes, Single Value Relations, and Multi-Value Relations on the same Story in the screenshot
* Shows output in both current XML, current JSON + PROPOSED BETTER JSON (A|B split test)
* Shows screenshot again but mapped to the XML and JSON representations of the information

## Example: Update a Story in VersionOne Lifecycle 
* cURL example how to update the Story, modifying Attributes, Single Value Relations, and Multi-Value relations (addition and deletion in one single example)
* Shows output in both current XML, current JSON + PROPOSED BETTER JSON (A|B split test)
* Shows screenshot again but with the changes mapped to the XML and JSON inputs we just sent to the API

## Example: Close a Story in VersionOne Lifecycle
* cURL example again
* Shows how to close a story
* Shows output in both current XML, current JSON + PROPOSED BETTER JSON (A|B split test)
* Shows screenshot with the changed status in the UI


## TODOs

* DONE: Create an instance and populate a sample story with mixture of all relevant scalars, relationships, and aggregate data.
  * URL: https://www16.v1host.com/api-examples/Account.mvc/LogIn?destination=%2Fapi-examples%2F

## NOTES

* Should utilize Access Tokens
* Should utilize common tools (cURL)
* Should demonstrate a vertical slice of API functionality
* Should be hands-on
* Should minimize explanation -- let's wait to put it front of a few internal people and ask what questions they have that would make the narrative explanation most valuable
