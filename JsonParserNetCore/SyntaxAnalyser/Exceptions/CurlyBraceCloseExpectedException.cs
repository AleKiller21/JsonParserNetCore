using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.SyntaxAnalyser.Exceptions
{
    public class CurlyBraceCloseExpectedException : SyntaxException
    {
        public CurlyBraceCloseExpectedException(int row, int col) : base("'}' token expected at row " + row + " column " + col + ".")
        {
        }
    }
}
