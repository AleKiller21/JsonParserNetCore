using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.SyntaxAnalyser.Exceptions
{
    public class LiteralStringExpectedException : SyntaxException
    {
        public LiteralStringExpectedException(int row, int col) : base($"String literal expected as json key at row {row} column {col}.")
        {
        }
    }
}
