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
