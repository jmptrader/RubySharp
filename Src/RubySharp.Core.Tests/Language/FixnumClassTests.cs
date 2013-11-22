﻿namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class FixnumClassTests
    {
        [TestMethod]
        public void FixnumClassInstance()
        {
            Assert.IsNotNull(FixnumClass.Instance);
            Assert.AreEqual("Fixnum", FixnumClass.Instance.Name);
        }

        [TestMethod]
        public void GetClassInstanceMethod()
        {
            Assert.IsNotNull(FixnumClass.Instance.GetInstanceMethod("class"));
        }

        [TestMethod]
        public void GetUnknownInstanceMethod()
        {
            Assert.IsNull(FixnumClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
