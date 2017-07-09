using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class ObjectNode : ValueNode
    {
        public Dictionary<string, ValueNode> Members;

        public ObjectNode(Dictionary<string, ValueNode> members)
        {
            Members = members;
        }

        public dynamic this[string key] => Members[key].GetValue();

        internal override dynamic GetValue()
        {
            return this;
        }

        public Dictionary<string, ValueNode>.KeyCollection GetKeys()
        {
            return Members.Keys;
        }
    }
}
