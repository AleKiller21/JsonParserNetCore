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
            	""inge"": ""-carlos"",
            	""array"":[
            		{},{},{},{},{},{},{},[[[[[]]]]]
            	],
            	""jack"": -1,
            	""aku"": 55,
            	""dbz"": [""goku"", ""trunks"", ""gohan""],
            	""vegueta"": [{},{},{},{},{},""Pikoro""],
            	""madness!!"":
            	{
            		""yo"": 25
            	}
            }";

            //            var json = @"{
            //	""person"": {
            //		""name"": ""Alejandro"",
            //		""age"": 22
            //	}
            //}";
            var parser = new Parser(json);
            var value = parser.Parse();
            Console.WriteLine(value["jack"]);
            //foreach (var val in value["dbz"])
            //{
            //    Console.WriteLine(val);
            //}
            //for (var i = 0; i < value["dbz"].Count; i++)
            //{
            //    Console.WriteLine(value["dbz"][i]);
            //}
        }
    }
}
