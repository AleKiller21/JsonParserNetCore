using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.LexicalAnalyser
{
    public class Symbol
    {
        public char symbol;
        public int Row;
        public int Col;

        public Symbol(char symbol, int row, int col)
        {
            this.symbol = symbol;
            Row = row;
            Col = col;
        }
    }
}
