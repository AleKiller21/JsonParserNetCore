using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.LexicalAnalyser
{
    public class InputStream
    {
        private string _json;
        private int _row;
        private int _col;
        private int _iterator;

        public InputStream(string json)
        {
            _json = json;
            _row = 1;
            _col = 1;
            _iterator = 0;
        }

        public Symbol GetNextSymbol()
        {
            if(_iterator == _json.Length)
                return new Symbol('$', _row, _col);

            if (_json[_iterator] == '\n')
            {
                var column = _col;
                _col = 1;
                return new Symbol(_json[_iterator++], _row++, column);
            }

            return new Symbol(_json[_iterator++], _row, _col++);
        }
    }
}
