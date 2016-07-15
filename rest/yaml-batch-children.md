Given this C# code (as seen in this [experimental VersionOne REST Client](https://github.com/JogoShugh/VersionOneRestSharpClient/blob/master/VersionOneRestSharpClient/Example/Example.cs#L178))

```csharp
  dynamic scope = Asset("Scope");
  scope.Name = "My Project";
  scope.Owner = "Member:20";
  scope.Parent = "Scope:0";
  scope.BeginDate = DateTime.Now;

  for (var i = 0; i < 5; i++)
  {
      dynamic story = Asset("Story");
      story.Name = $"Story {i}";
      story.Description = $"Description {i}";
      for (var j = 0; j < 3; j++)
      {
          story.CreateRelatedAsset("Children", Asset("Task", new {
              Name = $"Task {i}{j}",
              Description = $"Description for Task {i}{j}"
          }));
          story.CreateRelatedAsset("Children", Asset("Test", new {
              Name = $"Test {i}{j}",
              Description = $"Description for Test {i}{j}"
          }));
      }
      scope.CreateRelatedAsset("Workitems", story);
  }

  string yaml = scope.GetYamlPayload();
```

I want the following YAML produced (note though that this actually has TWO documents, pasted twice from above and slightly modified):

```YAML
asset: Scope
attributes:
  Name: My Project
  Owner: Member:20
  Parent: Scope:0
  BeginDate: 7/15/2016 3:13:54 PM
  Workitems:
  - asset: Story
    attributes:
      Name: Story 1
      Description: Description 1
      Children:
      - asset: Test
        attributes:
          Name: Test 10
          Description: Description for Test 10
      - asset: Task
        attributes:
          Name: Task 11
          Description: Description for Task 11
      - asset: Test
        attributes:
          Name: Test 11
          Description: Description for Test 11
      - asset: Task
        attributes:
          Name: Task 12
          Description: Description for Task 12
      - asset: Test
        attributes:
          Name: Test 12
          Description: Description for Test 12
  - asset: Story
    attributes:
      Name: Story 2
      Description: Description 2
      Children:
      - asset: Test
        attributes:
          Name: Test 20
          Description: Description for Test 20
      - asset: Task
        attributes:
          Name: Task 21
          Description: Description for Task 21
      - asset: Test
        attributes:
          Name: Test 21
          Description: Description for Test 21
      - asset: Task
        attributes:
          Name: Task 22
          Description: Description for Task 22
      - asset: Test
        attributes:
          Name: Test 22
          Description: Description for Test 22
  - asset: Story
    attributes:
      Name: Story 3
      Description: Description 3
      Children:
      - asset: Test
        attributes:
          Name: Test 30
          Description: Description for Test 30
      - asset: Task
        attributes:
          Name: Task 31
          Description: Description for Task 31
      - asset: Test
        attributes:
          Name: Test 31
          Description: Description for Test 31
      - asset: Task
        attributes:
          Name: Task 32
          Description: Description for Task 32
      - asset: Test
        attributes:
          Name: Test 32
          Description: Description for Test 32
  - asset: Story
    attributes:
      Name: Story 4
      Description: Description 4
      Children:
      - asset: Test
        attributes:
          Name: Test 40
          Description: Description for Test 40
      - asset: Task
        attributes:
          Name: Task 41
          Description: Description for Task 41
      - asset: Test
        attributes:
          Name: Test 41
          Description: Description for Test 41
      - asset: Task
        attributes:
          Name: Task 42
          Description: Description for Task 42
      - asset: Test
        attributes:
          Name: Test 42
          Description: Description for Test 42
---
asset: Scope
attributes:
  Name: Second Project
  Owner: Member:20
  Parent: Scope:0
  BeginDate: 7/15/2016 3:13:54 PM
  Workitems:
  - asset: Story
    attributes:
      Name: Story 1
      Description: Description 1
      Children:
      - asset: Test
        attributes:
          Name: Test 10
          Description: Description for Test 10
      - asset: Task
        attributes:
          Name: Task 11
          Description: Description for Task 11
      - asset: Test
        attributes:
          Name: Test 11
          Description: Description for Test 11
      - asset: Task
        attributes:
          Name: Task 12
          Description: Description for Task 12
      - asset: Test
        attributes:
          Name: Test 12
          Description: Description for Test 12
  - asset: Story
    attributes:
      Name: Story 2
      Description: Description 2
      Children:
      - asset: Test
        attributes:
          Name: Test 20
          Description: Description for Test 20
      - asset: Task
        attributes:
          Name: Task 21
          Description: Description for Task 21
      - asset: Test
        attributes:
          Name: Test 21
          Description: Description for Test 21
      - asset: Task
        attributes:
          Name: Task 22
          Description: Description for Task 22
      - asset: Test
        attributes:
          Name: Test 22
          Description: Description for Test 22
  - asset: Story
    attributes:
      Name: Story 3
      Description: Description 3
      Children:
      - asset: Test
        attributes:
          Name: Test 30
          Description: Description for Test 30
      - asset: Task
        attributes:
          Name: Task 31
          Description: Description for Task 31
      - asset: Test
        attributes:
          Name: Test 31
          Description: Description for Test 31
      - asset: Task
        attributes:
          Name: Task 32
          Description: Description for Task 32
      - asset: Test
        attributes:
          Name: Test 32
          Description: Description for Test 32
  - asset: Story
    attributes:
      Name: Story 4
      Description: Description 4
      Children:
      - asset: Test
        attributes:
          Name: Test 40
          Description: Description for Test 40
      - asset: Task
        attributes:
          Name: Task 41
          Description: Description for Task 41
      - asset: Test
        attributes:
          Name: Test 41
          Description: Description for Test 41
      - asset: Task
        attributes:
          Name: Task 42
          Description: Description for Task 42
      - asset: Test
        attributes:
          Name: Test 42
          Description: Description for Test 42
```

And, when I POST that to `api/write` (or whatever...), then I expect to it to create the two top-level Scope assets, and each child in the `Workitems` attribute with the Scope as its containing Scope relation. For each of those, I expect the assets under the `Children` attribute to be created with the proper `Parent` relation set.

Finally, I expect that when I execute this `query.v1` query:

```yaml
from: Scope
where: 
 ID: Scope:1176
select:
- from: Workitems:Story
  select:
  - from: Children
    select:
    - Name
    - Description
---
from: Scope
where: 
 ID: Scope:1201
select:
- from: Workitems:Story
  select:
  - from: Children
    select:
    - Name
    - Description
```

Then, I get back the full resulting tree of assets:

```json
[
  [
    {
      "_oid": "Scope:1176",
      "Workitems:Story": [
        {
          "_oid": "Story:1177",
          "Children": [
            {
              "_oid": "Test:1178",
              "Name": "Test 10",
              "Description": "Description for Test 10"
            },
            {
              "_oid": "Task:1179",
              "Name": "Task 11",
              "Description": "Description for Task 11"
            },
            {
              "_oid": "Test:1180",
              "Name": "Test 11",
              "Description": "Description for Test 11"
            },
            {
              "_oid": "Task:1181",
              "Name": "Task 12",
              "Description": "Description for Task 12"
            },
            {
              "_oid": "Test:1182",
              "Name": "Test 12",
              "Description": "Description for Test 12"
            }
          ]
        },
        {
          "_oid": "Story:1183",
          "Children": [
            {
              "_oid": "Test:1184",
              "Name": "Test 20",
              "Description": "Description for Test 20"
            },
            {
              "_oid": "Task:1185",
              "Name": "Task 21",
              "Description": "Description for Task 21"
            },
            {
              "_oid": "Test:1186",
              "Name": "Test 21",
              "Description": "Description for Test 21"
            },
            {
              "_oid": "Task:1187",
              "Name": "Task 22",
              "Description": "Description for Task 22"
            },
            {
              "_oid": "Test:1188",
              "Name": "Test 22",
              "Description": "Description for Test 22"
            }
          ]
        },
        {
          "_oid": "Story:1189",
          "Children": [
            {
              "_oid": "Test:1190",
              "Name": "Test 30",
              "Description": "Description for Test 30"
            },
            {
              "_oid": "Task:1191",
              "Name": "Task 31",
              "Description": "Description for Task 31"
            },
            {
              "_oid": "Test:1192",
              "Name": "Test 31",
              "Description": "Description for Test 31"
            },
            {
              "_oid": "Task:1193",
              "Name": "Task 32",
              "Description": "Description for Task 32"
            },
            {
              "_oid": "Test:1194",
              "Name": "Test 32",
              "Description": "Description for Test 32"
            }
          ]
        },
        {
          "_oid": "Story:1195",
          "Children": [
            {
              "_oid": "Test:1196",
              "Name": "Test 40",
              "Description": "Description for Test 40"
            },
            {
              "_oid": "Task:1197",
              "Name": "Task 41",
              "Description": "Description for Task 41"
            },
            {
              "_oid": "Test:1198",
              "Name": "Test 41",
              "Description": "Description for Test 41"
            },
            {
              "_oid": "Task:1199",
              "Name": "Task 42",
              "Description": "Description for Task 42"
            },
            {
              "_oid": "Test:1200",
              "Name": "Test 42",
              "Description": "Description for Test 42"
            }
          ]
        }
      ]
    }
  ],
  [
    {
      "_oid": "Scope:1201",
      "Workitems:Story": [
        {
          "_oid": "Story:1202",
          "Children": [
            {
              "_oid": "Test:1203",
              "Name": "Test 10",
              "Description": "Description for Test 10"
            },
            {
              "_oid": "Task:1204",
              "Name": "Task 11",
              "Description": "Description for Task 11"
            },
            {
              "_oid": "Test:1205",
              "Name": "Test 11",
              "Description": "Description for Test 11"
            },
            {
              "_oid": "Task:1206",
              "Name": "Task 12",
              "Description": "Description for Task 12"
            },
            {
              "_oid": "Test:1207",
              "Name": "Test 12",
              "Description": "Description for Test 12"
            }
          ]
        },
        {
          "_oid": "Story:1208",
          "Children": [
            {
              "_oid": "Test:1209",
              "Name": "Test 20",
              "Description": "Description for Test 20"
            },
            {
              "_oid": "Task:1210",
              "Name": "Task 21",
              "Description": "Description for Task 21"
            },
            {
              "_oid": "Test:1211",
              "Name": "Test 21",
              "Description": "Description for Test 21"
            },
            {
              "_oid": "Task:1212",
              "Name": "Task 22",
              "Description": "Description for Task 22"
            },
            {
              "_oid": "Test:1213",
              "Name": "Test 22",
              "Description": "Description for Test 22"
            }
          ]
        },
        {
          "_oid": "Story:1214",
          "Children": [
            {
              "_oid": "Test:1215",
              "Name": "Test 30",
              "Description": "Description for Test 30"
            },
            {
              "_oid": "Task:1216",
              "Name": "Task 31",
              "Description": "Description for Task 31"
            },
            {
              "_oid": "Test:1217",
              "Name": "Test 31",
              "Description": "Description for Test 31"
            },
            {
              "_oid": "Task:1218",
              "Name": "Task 32",
              "Description": "Description for Task 32"
            },
            {
              "_oid": "Test:1219",
              "Name": "Test 32",
              "Description": "Description for Test 32"
            }
          ]
        },
        {
          "_oid": "Story:1220",
          "Children": [
            {
              "_oid": "Test:1221",
              "Name": "Test 40",
              "Description": "Description for Test 40"
            },
            {
              "_oid": "Task:1222",
              "Name": "Task 41",
              "Description": "Description for Task 41"
            },
            {
              "_oid": "Test:1223",
              "Name": "Test 41",
              "Description": "Description for Test 41"
            },
            {
              "_oid": "Task:1224",
              "Name": "Task 42",
              "Description": "Description for Task 42"
            },
            {
              "_oid": "Test:1225",
              "Name": "Test 42",
              "Description": "Description for Test 42"
            }
          ]
        }
      ]
    }
  ]
]
```

The C# code to produce this is in this private Gist:

https://gist.github.com/JogoShugh/e51ff6d1c6aa3e0227cfe8444faad944
