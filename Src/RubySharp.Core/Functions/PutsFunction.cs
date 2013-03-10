﻿namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class PutsFunction : IFunction
    {
        private TextWriter writer;

        public PutsFunction(TextWriter writer)
        {
            this.writer = writer;
        }

        public object Apply(IList<object> values)
        {
            foreach (var value in values)
                this.writer.WriteLine(value);

            return null;
        }
    }
}
