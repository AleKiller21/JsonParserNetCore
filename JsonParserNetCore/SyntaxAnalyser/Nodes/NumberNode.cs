using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class NumberNode : ValueNode
    {
        private readonly dynamic _numValue;

        public NumberNode(dynamic num)
        {
            _numValue = num;
        }

        internal override dynamic GetValue()
        {
            return _numValue;
        }
    }
}
