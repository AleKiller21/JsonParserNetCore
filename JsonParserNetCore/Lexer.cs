using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParser.LexicalAnalyser;

namespace JsonParser
{
    public class Lexer
    {
        private InputStream _input;
        private Symbol _currentSymbol;
        private Dictionary<string, TokenType> _reservedWords;
        private Dictionary<char, TokenType> _punctuationSymbols;

        public Lexer(string json)
        {
            _input = new InputStream(json);
            _currentSymbol = _input.GetNextSymbol();
            InitReservedWordsDict();
            InitPunctuationSymbols();
        }

        public List<Token> GetTokens()
        {
            var tokens = new List<Token>();
            var currentToken = GetNextToken();
            while (currentToken.Type != TokenType.Eof)
            {
                tokens.Add(currentToken);
                currentToken = GetNextToken();
            }

            return tokens;
        }

        public Token GetNextToken()
        {
            while(char.IsWhiteSpace(_currentSymbol.symbol)) NextSymbol();

            if(_currentSymbol.symbol == '$') return new Token(_currentSymbol + "", TokenType.Eof, _currentSymbol.Row, _currentSymbol.Col);
            if (_currentSymbol.symbol == '"') return GetStringToken();
            if (char.IsDigit(_currentSymbol.symbol) || _currentSymbol.symbol == '-') return GetNumToken();
            if (_punctuationSymbols.ContainsKey(_currentSymbol.symbol))
            {
                var row = _currentSymbol.Row;
                var col = _currentSymbol.Col;
                var lexeme = _currentSymbol.symbol;
                NextSymbol();
                return new Token(lexeme + "", _punctuationSymbols[lexeme], row, col);
            }

            throw new LexicalException($"Unrecognized token found at row {_currentSymbol.Row} column {_currentSymbol.Col}.");
        }

        private Token GetNumToken()
        {
            var lexeme = new StringBuilder(_currentSymbol.symbol + "");
            var row = _currentSymbol.Row;
            var col = _currentSymbol.Col;

            NextSymbol();
            if (lexeme.ToString() == "-")
            {
                if(!char.IsDigit(_currentSymbol.symbol))
                    throw new LexicalException($"Unrecognized token found at row {_currentSymbol.Row} column {_currentSymbol.Col}.");

                lexeme.Append(_currentSymbol.symbol);
                NextSymbol();
            }

            if (_currentSymbol.symbol == '.') return GetFloatToken(lexeme, row, col);
            while (char.IsDigit(_currentSymbol.symbol))
            {
                lexeme.Append(_currentSymbol.symbol);
                NextSymbol();
                if (_currentSymbol.symbol == '.') return GetFloatToken(lexeme, row, col);
            }

            return new Token(lexeme.ToString(), TokenType.LiteralInt, row, col);
        }

        private Token GetFloatToken(StringBuilder lexeme, int row, int col)
        {
            lexeme.Append(_currentSymbol.symbol);
            NextSymbol();
            if(!char.IsDigit(_currentSymbol.symbol)) throw new LexicalException(_currentSymbol.Row, _currentSymbol.Col);

            while (char.IsDigit(_currentSymbol.symbol))
            {
                lexeme.Append(_currentSymbol.symbol);
                NextSymbol();
            }

            return new Token(lexeme.ToString(), TokenType.LiteralFloat, row, col);
        }

        private Token GetStringToken()
        {
            var lexeme = new StringBuilder(_currentSymbol.symbol + "");
            var row = _currentSymbol.Row;
            var col = _currentSymbol.Col;

            NextSymbol();
            while (_currentSymbol.symbol != '"')
            {
                if(_currentSymbol.symbol == '\n' || _currentSymbol.symbol == '\t')
                    throw new LexicalException(_currentSymbol.Row, _currentSymbol.Col);

                lexeme.Append(_currentSymbol.symbol);
                NextSymbol();
            }

            lexeme.Append(_currentSymbol.symbol);
            NextSymbol();

            if (_reservedWords.ContainsKey(lexeme.ToString()))
                return new Token(lexeme.ToString(), _reservedWords[lexeme.ToString()], row, col);

            return new Token(lexeme.ToString(), TokenType.LiteralString, row, col);
        }

        private void NextSymbol()
        {
            _currentSymbol = _input.GetNextSymbol();
        }

        private void InitReservedWordsDict()
        {
            _reservedWords = new Dictionary<string, TokenType>();
            _reservedWords["true"] = TokenType.RwTrue;
            _reservedWords["false"] = TokenType.RwFalse;
        }

        private void InitPunctuationSymbols()
        {
            _punctuationSymbols = new Dictionary<char, TokenType>();
            _punctuationSymbols['{'] = TokenType.CurlyBraceOpen;
            _punctuationSymbols['}'] = TokenType.CurlyBraceClose;
            _punctuationSymbols['['] = TokenType.SquareBracketOpen;
            _punctuationSymbols[']'] = TokenType.SquareBracketClose;
            _punctuationSymbols[','] = TokenType.Comma;
            _punctuationSymbols[':'] = TokenType.Colon;
        }
    }
}
