﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JsonParserNetCore.SyntaxAnalyser.Nodes
{
    public abstract class ValueNode
    {
        internal abstract dynamic GetValue();
    }
}