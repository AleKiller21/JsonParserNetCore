using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.LexicalAnalyser
{
    public class LexicalException : Exception
    {
        public LexicalException(int row, int col) : base($"Invalid token found at row {row} column {col}.")
        {
            
        }

        public LexicalException(string message) : base(message)
        {
            
        }
    }
}
