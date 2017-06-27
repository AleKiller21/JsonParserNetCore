using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.LexicalAnalyser
{
    public enum TokenType
    {
        CurlyBraceOpen,
        CurlyBraceClose,
        SquareBracketOpen,
        SquareBracketClose,
        Comma,
        Colon,
        LiteralString,
        LiteralInt,
        LiteralFloat,
        RwTrue,
        RwFalse,
        Eof
    }
}
