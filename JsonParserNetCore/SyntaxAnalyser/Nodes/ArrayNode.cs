using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class ArrayNode : ValueNode
    {
        public List<dynamic> ValueList;

        public ArrayNode()
        {
            ValueList = new List<dynamic>();
        }

        public override dynamic GetValue()
        {
            return ValueList;
        }
    }
}
