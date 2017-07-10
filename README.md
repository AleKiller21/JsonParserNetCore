# JsonParserNetCore
A C# JSON parser made in .Net Core

## Usage
Below is the JSON we'll be using for the examples.

```json
{
	"firstName": "name1",
	"lastName": "name2",
	"age": 22,
	"Address": "address",
	"City": "city",
	"married": false,
	"home number": -1,
	"degrees": ["Computer Science"],
	"department": "IT",
	"phoneNumber": -1,
	"Skills": ["C#", "C++", "Java", "JavaScript", "SQL", "Git", "VideoEditing"],
	"projects": [{
			"name": "DBCLI",
			"description": "A database manager"
		},
		{
			"name": "Automata Visualizer",
			"description": -1,
			"another object": {
				"negative": -25.3
			}
		}
	],
	"permanent": true
}
```
In order to get the object representation of this JSON, just instantiate the Parser class, with the JSON as argument, and call the Parse method.
```C#
var parser = new Parser(json);
var value = parser.Parse();
```
Use square brackets notation along with the json key in order to access its corresponding value.

```C#
value["City"]
//city

value["projects"][1]["another object"]["negative"]
//-25.3
```
In order to iterate over all the members of the current object, make sure to call the `GetKeys` method which will return all the keys of the json object.

```C#
foreach (var key in value.GetKeys())
{
    Console.WriteLine(value[key]);
}

/*
  name1
  name2
  22
  address
  city
  False
  -1
  IT
  -1
  True
*/
```

To iterate over the array elements, you are presented with two choices:

1. Use a classic for loop.

```C#
for (var i = 0; i < value["projects"].Count; i++)
{
    Console.WriteLine(value["projects"][i]["description"]);
}
/*
  A database manager
  -1
*/
```

2. Use a foreach loop. In this case, you will need to call the `GetValue` method for each element to actually get the element stored in the array.

```C#
foreach (var skill in value["Skills"])
{
    Console.WriteLine(skill.GetValue());
}

/*
  C#
  C++
  Java
  JavaScript
  SQL
  Git
  VideoEditing
*/
```

That's all. All the examples above are included in the Program.cs so you can test them by yourself.
