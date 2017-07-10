using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class ArrayNode : ValueNode
    {
        private readonly ValueList _values;

        public ArrayNode(ValueList valueList)
        {
            _values = valueList;
        }

        public override dynamic GetValue()
        {
            return _values;
        }
    }
}
