using System;
using System.Collections.Generic;
using JsonParser.SyntaxAnalyser;
using JsonParserNetCore.SyntaxAnalyser;

namespace JsonParserNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = @"{
	""firstName"": ""Alejandro"",
	""lastName"": ""Ferrera"",
	""age"": 22,
	""Address"": -1,
	""City"": ""San Pedro Sula"",
	""married"": false,
	""home number"": -1,
	""degrees"": [""Computer Science""],
	""department"": ""IT"",
	""phoneNumber"": -1,
	""Skills"": [""C#"", ""C++"", ""Java"", ""JavaScript"", ""SQL"", ""Git"", ""VideoEditing""],
	""projects"": [{
			""name"": ""DBCLI"",
			""description"": ""A database manager for a file based database""
		},
		{
			""name"": ""Automata Visualizer"",
			""description"": -1
		}
	],
	""permanent"": true
}";

            var parser = new Parser(json);
            var value = parser.Parse();
            Console.WriteLine(value["department"]);
            //Console.WriteLine(value["dbz"][2]);
            foreach (var key in value.GetKeys())
            {
                Console.WriteLine(value[key]);
            }
            //for (var i = 0; i < value["projects"].Count; i++)
            //{
            //    Console.WriteLine(value["projects"][i]["description"]);
            //}
        }
    }
}
