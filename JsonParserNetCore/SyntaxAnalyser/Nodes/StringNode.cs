using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class StringNode : ValueNode
    {
        private readonly string _stringValue;

        public StringNode(string value)
        {
            _stringValue = value;
        }

        internal override dynamic GetValue()
        {
            return _stringValue;
        }
    }
}
