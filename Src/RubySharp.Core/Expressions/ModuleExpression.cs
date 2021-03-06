﻿namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class ModuleExpression : BaseExpression
    {
        private static int hashcode = typeof(ClassExpression).GetHashCode();

        private string name;
        private IExpression expression;

        public ModuleExpression(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public override object Evaluate(Context context)
        {
            object value = null;

            if (context.Module != null)
            {
                if (context.Module.Constants.HasLocalValue(this.name))
                    value = context.Module.Constants.GetLocalValue(this.name);
            }
            else if (context.HasLocalValue(this.name))
                value = context.GetLocalValue(this.name);

            DynamicClass module;

            if (value == null || !(value is DynamicClass))
            {
                DynamicClass modclass = (DynamicClass)context.RootContext.GetLocalValue("Module");
                var superclass = (DynamicClass)context.RootContext.GetLocalValue("Object");
                module = new DynamicClass(modclass, this.name, superclass, context.Module);

                if (context.Module != null)
                    context.Module.Constants.SetLocalValue(this.name, module);
                else
                    context.RootContext.SetLocalValue(this.name, module);
            }
            else
                module = (DynamicClass)value;

            Context newcontext = new Context(module, context.RootContext);
            return this.expression.Evaluate(newcontext);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ModuleExpression)
            {
                var expr = (ModuleExpression)obj;

                return this.name == expr.name && this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode() + this.expression.GetHashCode() + hashcode;
        }
    }
}
