﻿namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DivideExpression : BinaryExpression
    {
        public DivideExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            return (int)leftvalue / (int)rightvalue;
        }
    }
}
