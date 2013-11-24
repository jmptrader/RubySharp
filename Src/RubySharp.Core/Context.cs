﻿namespace RubySharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class Context
    {
        private Context parent;
        private IDictionary<string, object> values = new Dictionary<string, object>();
        private DynamicObject self;
        private ModuleObject module;

        public Context()
            : this(null)
        {
        }

        public Context(Context parent)
        {
            this.parent = parent;
        }

        public Context(ModuleObject module, Context parent)
        {
            this.module = module;
            this.parent = parent;
        }

        public Context(DynamicObject self, Context parent)
        {
            this.self = self;
            this.parent = parent;
        }

        public DynamicObject Self { get { return this.self; } internal set { this.self = value; } }

        public ModuleObject Module { get { return this.module; } }

        public Context RootContext
        {
            get
            {
                if (this.parent == null)
                    return this;

                return this.parent.RootContext;
            }
        }

        public void SetLocalValue(string name, object value)
        {
            this.values[name] = value;
        }

        public bool HasLocalValue(string name)
        {
            return this.values.ContainsKey(name);
        }

        public bool HasValue(string name)
        {
            if (this.HasLocalValue(name))
                return true;

            if (this.parent != null)
                return this.parent.HasValue(name);

            return false;
        }

        public object GetLocalValue(string name)
        {
            return this.values[name];
        }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            if (this.parent != null)
                return this.parent.GetValue(name);

            return null;
        }

        public IList<string> GetLocalNames()
        {
            return this.values.Keys.ToList();
        }
    }
}
