﻿namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AddExpression : BinaryExpression
    {
        public AddExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue is string || rightvalue is string)
                return leftvalue.ToString() + rightvalue.ToString();

            if (leftvalue is int)
                if (rightvalue is int)
                    return (int)leftvalue + (int)rightvalue;
                else
                    return (int)leftvalue + (double)rightvalue;
            else if (rightvalue is int)
                return (double)leftvalue + (int)rightvalue;
            else
                return (double)leftvalue + (double)rightvalue;
        }
    }
}
