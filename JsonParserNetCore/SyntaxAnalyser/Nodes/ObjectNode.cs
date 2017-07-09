using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class ObjectNode : ValueNode
    {
        public Dictionary<string, ValueNode> Members;

        public ObjectNode()
        {
            Members = new Dictionary<string, ValueNode>();
        }

        public ValueNode this[string key] => Members[key].GetValue();

        public override dynamic GetValue()
        {
            return this;
        }
    }
}
