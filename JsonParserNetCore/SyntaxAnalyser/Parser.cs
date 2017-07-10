using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JsonParser;
using JsonParser.LexicalAnalyser;
using JsonParser.SyntaxAnalyser.Exceptions;
using JsonParserNetCore.SyntaxAnalyser.Nodes;

namespace JsonParserNetCore.SyntaxAnalyser
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

        public ObjectNode Parse()
        {
            var objectNode = Object();
            if(!CheckTokenType(TokenType.Eof))
                throw new SyntaxException($"End of json expected at row {GetTokenRow()} column {GetTokenColumn()}.");

            return objectNode;
        }

        private ObjectNode Object()
        {
            if (!CheckTokenType(TokenType.CurlyBraceOpen))
                throw new CurlyBraceOpenExpectedException(GetTokenRow(), GetTokenColumn());

            NexToken();
            var members = new Dictionary<string, ValueNode>();
            MemberList(members);
            if(!CheckTokenType(TokenType.CurlyBraceClose))
                throw new CurlyBraceCloseExpectedException(GetTokenRow(), GetTokenColumn());

            NexToken();
            return new ObjectNode(members);
        }

        private void MemberList(Dictionary<string, ValueNode> members)
        {
            if (CheckTokenType(TokenType.LiteralString))
            {
                var key = Key();
                if (!CheckTokenType(TokenType.Colon))
                    throw new ColonExpectedException(GetTokenRow(), GetTokenColumn());

                NexToken();
                var value = Value();
                members.Add(key, value);
                MemberListPrime(members);
            }

            else
            {
                //Epsilon
            }
        }

        private void MemberListPrime(Dictionary<string, ValueNode> members)
        {
            if (CheckTokenType(TokenType.Comma))
            {
                NexToken();
                MemberListPrimePrime(members);
            }
            else
            {
                //Epsilon
            }
        }

        private void MemberListPrimePrime(Dictionary<string, ValueNode> members)
        {
            var key = Key();
            if(!CheckTokenType(TokenType.Colon))
                throw new ColonExpectedException(GetTokenRow(), GetTokenColumn());

            NexToken();
            var value = Value();
            members.Add(key, value);
            MemberListPrime(members);
        }

        private string Key()
        {
            if(!CheckTokenType(TokenType.LiteralString))
                throw new LiteralStringExpectedException(GetTokenRow(), GetTokenColumn());

            var key = _currentToken.Lexeme;
            NexToken();
            return key.Replace("\"", string.Empty);
        }

        private ValueNode Value()
        {
            if (CheckTokenType(TokenType.LiteralInt) || CheckTokenType(TokenType.LiteralFloat))
                return LiteralNum();

            if (CheckTokenType(TokenType.LiteralString))
            {
                var stringValue = _currentToken.Lexeme;
                NexToken();
                return new StringNode(stringValue.Replace("\"", string.Empty));
            }

            if(CheckTokenType(TokenType.CurlyBraceOpen)) return Object();
            if (CheckTokenType(TokenType.SquareBracketOpen)) return Array();
            if (CheckTokenType(TokenType.RwTrue) || CheckTokenType(TokenType.RwFalse)) return BooleanValue();

            throw new SyntaxException($"Numeric literal, string, object or array token expected as json value at " +
                                           $"row {GetTokenRow()} colum {GetTokenColumn()}");
        }

        private BooleanNode BooleanValue()
        {
            if (CheckTokenType(TokenType.RwTrue) || CheckTokenType(TokenType.RwFalse))
            {
                var value = _currentToken.Lexeme;
                NexToken();
                return new BooleanNode(bool.Parse(value));
            }

            throw new SyntaxException($"true/false keywords expected at row {GetTokenRow()} column {GetTokenColumn()}.");
        }

        private NumberNode LiteralNum()
        {
            if (CheckTokenType(TokenType.LiteralInt) || CheckTokenType(TokenType.LiteralFloat))
            {
                var numValue = double.Parse(_currentToken.Lexeme);
                NexToken();

                return new NumberNode(numValue);
            }

            throw new SyntaxException($"Numeric literal expected at row {GetTokenRow()} column {GetTokenColumn()}.");
        }

        private ArrayNode Array()
        {
            if(!CheckTokenType(TokenType.SquareBracketOpen))
                throw new SyntaxException($"'[' token expected at row {GetTokenRow()} column {GetTokenColumn()}.");

            NexToken();
            var arrayValue = ValueList();
            
            if(!CheckTokenType(TokenType.SquareBracketClose))
                throw new SyntaxException($"']' token expected at row {GetTokenRow()} column {GetTokenColumn()}.");

            NexToken();

            return new ArrayNode(arrayValue);
        }

        private ValueList ValueList()
        {
            if (IsValue())
            {
                var valueNode = Value();
                var valueList = ValueListPrime();

                valueList.Insert(0, valueNode);
                return valueList;
            }

            return new ValueList();
        }

        private ValueList ValueListPrime()
        {
            if (CheckTokenType(TokenType.Comma))
            {
                NexToken();
                var valueNode = Value();
                var valueList = ValueListPrime();

                valueList.Insert(0, valueNode);
                return valueList;
            }

            return new ValueList();
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
