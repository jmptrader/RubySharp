﻿namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public Expression ParseExpression()
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return null;

            return new Expression(int.Parse(token.Value));
        }
    }
}
