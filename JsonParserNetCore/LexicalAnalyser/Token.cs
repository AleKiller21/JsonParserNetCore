using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.LexicalAnalyser
{
    public class Token
    {
        public string Lexeme;
        public TokenType Type;
        public int Row;
        public int Col;

        public Token(string lexeme, TokenType type, int row, int col)
        {
            Lexeme = lexeme;
            Type = type;
            Row = row;
            Col = col;
        }
    }
}
