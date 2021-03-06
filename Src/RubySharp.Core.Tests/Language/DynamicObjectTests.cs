﻿namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class DynamicObjectTests
    {
        private DynamicClass @class;
        private IFunction foo;

        [TestInitialize]
        public void Setup()
        {
            Machine machine = new Machine();
            this.@class = new DynamicClass((DynamicClass)machine.RootContext.GetLocalValue("Class"), "Dog", (DynamicClass)machine.RootContext.GetLocalValue("Object"));
            this.foo = new DefinedFunction(null, null, null);
            this.@class.SetInstanceMethod("foo", this.foo);
        }

        [TestMethod]
        public void CreateObject()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            Assert.AreSame(this.@class, obj.Class);
        }

        [TestMethod]
        public void GetSingletonClass()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            var singleton = obj.SingletonClass;

            Assert.IsNotNull(singleton);
            Assert.AreSame(obj.Class, singleton.SuperClass);
            Assert.AreEqual(string.Format("#<Class:{0}>", obj.ToString()), singleton.Name);
            Assert.IsNotNull(singleton.Class);
            Assert.AreEqual("Class", singleton.Class.Name);
        }

        [TestMethod]
        public void ObjectToString()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            var result = obj.ToString();

            Assert.IsTrue(result.StartsWith("#<Dog:0x"));
            Assert.IsTrue(result.EndsWith(">"));
        }

        [TestMethod]
        public void GetUndefinedValue()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            Assert.IsNull(obj.GetValue("name"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            obj.SetValue("name", "Nero");

            Assert.AreEqual("Nero", obj.GetValue("name"));
        }

        [TestMethod]
        public void GetMethodFromClass()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            Assert.AreSame(this.foo, obj.GetMethod("foo"));
        }

        [TestMethod]
        public void GetInitialMethods()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            Assert.IsNotNull(obj.GetMethod("class"));
            Assert.IsNotNull(obj.GetMethod("methods"));
        }

        [TestMethod]
        public void InvokeClassMethod()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            Assert.AreSame(this.@class, obj.GetMethod("class").Apply(obj, null, null));
        }

        [TestMethod]
        public void InvokeMethodsMethod()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            var result = obj.GetMethod("methods").Apply(obj, null, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicArray));

            DynamicArray names = (DynamicArray)result;

            Assert.IsTrue(names.Contains(new Symbol("foo")));
            Assert.IsTrue(names.Contains(new Symbol("class")));
            Assert.IsTrue(names.Contains(new Symbol("methods")));
        }

        [TestMethod]
        public void InvokeSingletonMethodsMethod()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            var result = obj.GetMethod("singleton_methods").Apply(obj, null, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicArray));

            DynamicArray names = (DynamicArray)result;

            Assert.AreEqual(0, names.Count);
        }

        [TestMethod]
        public void GetMethodFromSingletonClass()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            var newfoo = new DefinedFunction(null, null, null);
            obj.SingletonClass.SetInstanceMethod("foo", newfoo);
            Assert.AreSame(newfoo, obj.GetMethod("foo"));
        }
    }
}
