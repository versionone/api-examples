# Walkthrough of Using the VersionOne Lifecycle REST APIs

This hands-on walkthrough shows you the basics for how to perform three types of VersionOne Lifecycle REST API requests:
* Querying existing assets 
* Adding new assets
* Updateng assets
* Executing operations on assets

## TODO: finish outlining the concrete steps at a high level
* Query a Project (Scope) for its `Name` and `Description`
  * Add a Backlog Item that belongs to this Scope (by referencing its Oid Token as "Scope" Relation)
* Query -- retrieve the `Name`, `Description`, `Estimate`, `Status`, and `Owners` attributes from an existing **Backlog Item** (also called **Story**)
* Update -- modify the Description an existing Backlog Item
* Execute Operation -- trigger behavior on an existing Backlog Item to change its status within Lifecycle
* 
* suse the popular cURL HTTP client to query information about an existing Backlog Item (also called Story) from the VersionOne Lifecycle REST APIs


## NOTES

* Should utilize Access Tokens
* Should utilize common tools (cURL)
* Should demonstrate a vertical slice of API functionality
* Should be hands-on
* Should minimize explanation -- let's wait to put it front of a few internal people and ask what questions they have that would make the narrative explanation most valuable


## What you need to try this against our public test instance

This walkthrough assumes you are on a system that can run the popular **cURL** command-line HTTP client. cURL is available for a huge variety of operating systems. [Download it here](https://curl.haxx.se/download.html) if you don't already have it.

Once you have cURL installed, you can try it against a test URL, like Google, by simply typing `curl 'http://www.google.com'` and pressing enter. You should get back a result like this:

![cURL Google](https://cloud.githubusercontent.com/assets/1863005/16272737/f3fd2c50-386c-11e6-864f-408abbe67a86.png)

Unlike the Google's public web site, the VersionOne Lifecycle APIs require authentication/authorization. So, you'll need 

## What you need to do to repeat this in your own instance

If you'd like to adapt this tutorial to run against 
* Provide instructions for generating an Access Token in their own instance

## Query a Project (Scope) by ID

The simplest thing you can do with the VersionOne Lifecycle REST APIs is to query for information about the Projects and other workitems in your instance. 

### Step 1: View the Projects page in Lifecycle

To get started, [navigate to the **Projects** page in our Lifecycle example instance](https://www16.v1host.com/api-examples/Default.aspx?menu=ProjectAdminPage&feat-nav=a1). Use the username `admin` and password `admin` when prompted.

You should see a page like this after authenticating:

![Projects page](https://cloud.githubusercontent.com/assets/1863005/16241266/ec88673a-37ba-11e6-9fdf-a7a4f500195d.png)

### Step 2: View the VersionOne Lifecycle REST APIs 101 Project

Click the project named `VersionOne Lifecycle REST APIs 101` to open it in a dialog window. Once it pops open click the full-screen option from the top right, highlighted here:

![Project dialog](https://cloud.githubusercontent.com/assets/1863005/16241937/0db32aaa-37be-11e6-833d-db14e9df21d4.png)

Clicking that will open the Project in a fullscreen window or tab, like below. **Copy the numeric porition in the value of the highlighted `oidToken` query string parameter value**. You will need this to query the **Scope** asset via the API in the next step. Note that **Scope** is the system term for what appears in the user interface as **Project**.

![Project fullscreen](https://cloud.githubusercontent.com/assets/1863005/16242154/137459cc-37bf-11e6-9774-a803562f9062.png)

### Step 3: Execute query via cURL

cURL command:

```curl
curl 'https://www16.v1host.com/api-examples/rest-1.v1/Data/Scope/1005' -H "Authorization:Bearer 1.aBg7sVXSZeEsf3cwvQFEdkkt384="
```

Result:

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
