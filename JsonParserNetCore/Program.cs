using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParser.SyntaxAnalyser;

namespace JsonParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = @"{
	""inge"": ""carlos"",
	""array"":[
		{},{},{},{},{},{},{},[[[[[]]]]]
	],
	""jack"": 25,
	""aku"": 55,
	""dbz"": [""goku"", ""trunks"", ""gohan""],
	""vegueta"": [{},{},{},{},{},""Pikoro""],
	""madness!!"":
	{
		""\n"": 25
	}
}";
            var parser = new Parser(json);
            parser.Parse();
            Console.WriteLine("PASE!!");
        }
    }
}
