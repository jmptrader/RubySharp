﻿namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class DynamicClass : DynamicObject
    {
        private static IFunction newfunction = new NewFunction();

        private string name;
        private DynamicClass superclass;
        private IDictionary<string, IFunction> methods = new Dictionary<string, IFunction>();

        public DynamicClass(string name, DynamicClass superclass = null)
            : this(null, name, superclass)
        {
        }

        public DynamicClass(DynamicClass @class, string name, DynamicClass superclass = null)
            : base(@class)
        {
            this.name = name;
            this.superclass = superclass;
            this.SetInstanceMethod("class", new LambdaFunction(GetClass));
        }

        public string Name { get { return this.name; } internal set { this.name = value; } }

        public DynamicClass SuperClass { get { return this.superclass; } }

        public void SetInstanceMethod(string name, IFunction method)
        {
            this.methods[name] = method;
        }

        public IFunction GetInstanceMethod(string name)
        {
            if (this.methods.ContainsKey(name))
                return this.methods[name];

            if (this.superclass != null)
                return this.superclass.GetInstanceMethod(name);

            return null;
        }

        public virtual DynamicObject CreateInstance()
        {
            return new DynamicObject(this);
        }

        public override IFunction GetMethod(string name)
        {
            if (name == "new")
                return newfunction;

            return base.GetMethod(name);
        }

        public override string ToString()
        {
            return this.Name;
        }

        private static object GetClass(DynamicObject obj, IList<object> values)
        {
            return obj.Class;
        }
    }
}
