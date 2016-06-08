# Walkthrough of Using the VersionOne Lifecycle REST APIs

## NOTES

* Should utilize Access Tokens
* Should utilize common tools (cURL)
* Should demonstrate a vertical slice of API functionality
* Should be hands-on
* Should minimize explanation -- let's wait to put it front of a few internal people and ask what questions they have that would make the narrative explanation most valuable

## What you need to try this against our public test instance
* Link to where to download cURL
* Show screenshot of a Story in public Lifecycle instance

## What you need to do to repeat this in your own instance
* Provide instructions for generating an Access Token in their own instance

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

* Create an instance and populate a sample story with mixture of all relevant scalars, relationships, and aggregate data.
