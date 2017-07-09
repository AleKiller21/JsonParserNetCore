using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public class ValueList : List<ValueNode>
    {
        public new dynamic this[int index] => base[index].GetValue();
    }
}
