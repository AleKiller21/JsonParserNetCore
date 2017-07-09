using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParser.LexicalAnalyser;
using JsonParser.SyntaxAnalyser.Exceptions;

namespace JsonParser.SyntaxAnalyser
{
    public class Parser
    {
        private readonly Lexer _lex;
        private Token _currentToken;

        public Parser(string json)
        {
            _lex = new Lexer(json);
            _currentToken = _lex.GetNextToken();
        }

        public void Parse()
        {
            Object();
            if(!CheckTokenType(TokenType.Eof))
                throw new SyntaxException($"End of json expected at row {GetTokenRow()} column {GetTokenColumn()}.");
        }

        private void Object()
        {
            if (!CheckTokenType(TokenType.CurlyBraceOpen))
                throw new CurlyBraceOpenExpectedException(GetTokenRow(), GetTokenColumn());

            NexToken();
            MemberList();
            if(!CheckTokenType(TokenType.CurlyBraceClose))
                throw new CurlyBraceCloseExpectedException(GetTokenRow(), GetTokenColumn());

            NexToken();
        }

        private void MemberList()
        {
            if (CheckTokenType(TokenType.LiteralString))
            {
                Key();
                if (!CheckTokenType(TokenType.Colon))
                    throw new ColonExpectedException(GetTokenRow(), GetTokenColumn());

                NexToken();
                Value();
                MemberListPrime();
            }

            else
            {
                //Epsilon
            }
        }

        private void MemberListPrime()
        {
            if (CheckTokenType(TokenType.Comma))
            {
                NexToken();
                MemberListPrimePrime();
            }
            else
            {
                //Epsilon
            }
        }

        private void MemberListPrimePrime()
        {
            Key();
            if(!CheckTokenType(TokenType.Colon))
                throw new ColonExpectedException(GetTokenRow(), GetTokenColumn());

            NexToken();
            Value();
            MemberListPrime();
        }

        private void Key()
        {
            if(!CheckTokenType(TokenType.LiteralString))
                throw new LiteralStringExpectedException(GetTokenRow(), GetTokenColumn());

            NexToken();
        }

        private void Value()
        {
            if (CheckTokenType(TokenType.LiteralInt) || CheckTokenType(TokenType.LiteralFloat))
                LiteralNum();

            else if(CheckTokenType(TokenType.LiteralString)) NexToken();
            else if(CheckTokenType(TokenType.CurlyBraceOpen)) Object();
            else if (CheckTokenType(TokenType.SquareBracketOpen)) Array();
            else throw new SyntaxException($"Numeric literal, string, object or array token expected as json value at " +
                                           $"row {GetTokenRow()} colum {GetTokenColumn()}");
        }

        private void LiteralNum()
        {
            if(CheckTokenType(TokenType.LiteralInt) || CheckTokenType(TokenType.LiteralFloat)) NexToken();
            else
            {
                throw new SyntaxException($"Numeric literal expected at row {GetTokenRow()} column {GetTokenColumn()}.");
            }
        }

        private void Array()
        {
            if(!CheckTokenType(TokenType.SquareBracketOpen))
                throw new SyntaxException($"'[' token expected at row {GetTokenRow()} column {GetTokenColumn()}.");

            NexToken();
            ValueList();
            
            if(!CheckTokenType(TokenType.SquareBracketClose))
                throw new SyntaxException($"']' token expected at row {GetTokenRow()} column {GetTokenColumn()}.");

            NexToken();
        }

        private void ValueList()
        {
            if (IsValue())
            {
                Value();
                ValueListPrime();
            }
            else
            {
                //Epsilon
            }
        }

        private void ValueListPrime()
        {
            if (CheckTokenType(TokenType.Comma))
            {
                NexToken();
                Value();
                ValueListPrime();
            }
            else
            {
                //Epsilon
            }
        }

        private void NexToken()
        {
            _currentToken = _lex.GetNextToken();
        }

        private bool CheckTokenType(TokenType type)
        {
            return _currentToken.Type == type;
        }

        private int GetTokenRow()
        {
            return _currentToken.Row;
        }

        private int GetTokenColumn()
        {
            return _currentToken.Col;
        }

        private bool IsValue()
        {
            return CheckTokenType(TokenType.LiteralInt) || CheckTokenType(TokenType.LiteralFloat) ||
                   CheckTokenType(TokenType.LiteralString) || CheckTokenType(TokenType.CurlyBraceOpen) ||
                   CheckTokenType(TokenType.SquareBracketOpen);
        }
    }
}
