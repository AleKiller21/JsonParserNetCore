using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class ArrayNode : ValueNode
    {
        public ValueList values;

        public ArrayNode(ValueList valueList)
        {
            values = valueList;
        }

        internal override dynamic GetValue()
        {
            return values;
        }
    }
}
