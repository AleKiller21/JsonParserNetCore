using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class ObjectNode : ValueNode
    {
        private readonly Dictionary<string, ValueNode> _members;

        public ObjectNode(Dictionary<string, ValueNode> members)
        {
            _members = members;
        }

        public dynamic this[string key] => _members[key].GetValue();

        internal override dynamic GetValue()
        {
            return this;
        }

        public Dictionary<string, ValueNode>.KeyCollection GetKeys()
        {
            return _members.Keys;
        }

        public bool IsEmpty()
        {
            return _members.Count == 0;
        }
    }
}
