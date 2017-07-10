using System;
using JsonParserNetCore.SyntaxAnalyser;

namespace JsonParserNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = @"{
	""firstName"": ""name1"",
	""lastName"": ""name2"",
	""age"": 22,
	""Address"": ""address"",
	""City"": ""city"",
	""married"": false,
	""home number"": -1,
	""degrees"": [""Computer Science""],
	""department"": ""IT"",
	""phoneNumber"": -1,
	""Skills"": [""C#"", ""C++"", ""Java"", ""JavaScript"", ""SQL"", ""Git"", ""VideoEditing""],
	""projects"": [{
			""name"": ""DBCLI"",
			""description"": ""A database manager""
		},
		{
			""name"": ""Automata Visualizer"",
			""description"": -1,
			""another object"": {
				""negative"": -25.3
			}
		}
	],
	""permanent"": true
}

";

            var parser = new Parser(json);
            var value = parser.Parse();

            //Console.WriteLine(value["City"]);

            //Console.WriteLine(value["projects"][1]["another object"]["negative"]);

            //Console.WriteLine(value.IsEmpty());

            //foreach (var key in value.GetKeys())
            //{
            //    Console.WriteLine(value[key]);
            //}

            foreach (var skill in value["Skills"])
            {
                Console.WriteLine(skill.GetValue());
            }

            //for (var i = 0; i < value["projects"].Count; i++)
            //{
            //    Console.WriteLine(value["projects"][i]["description"]);
            //}
        }
    }
}
