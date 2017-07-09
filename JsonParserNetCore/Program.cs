using System;
using JsonParser.SyntaxAnalyser;

namespace JsonParserNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
//            var json = @"{
//	""inge"": ""carlos"",
//	""array"":[
//		{},{},{},{},{},{},{},[[[[[]]]]]
//	],
//	""jack"": 25,
//	""aku"": 55,
//	""dbz"": [""goku"", ""trunks"", ""gohan""],
//	""vegueta"": [{},{},{},{},{},""Pikoro""],
//	""madness!!"":
//	{
//		""\n"": 25
//	}
//}";

            var json = @"{
	""person"": {
		""name"": ""Alejandro"",
		""age"": 22
	}
}";
            var parser = new Parser(json);
            parser.Parse();
            Console.WriteLine("PASE!!");
        }
    }
}
