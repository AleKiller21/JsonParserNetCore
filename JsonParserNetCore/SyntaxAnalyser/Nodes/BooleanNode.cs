using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class BooleanNode : ValueNode
    {
        private readonly bool _booleanValue;

        public BooleanNode(bool value)
        {
            _booleanValue = value;
        }

        internal override dynamic GetValue()
        {
            return _booleanValue;
        }
    }
}
