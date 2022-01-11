using System;
using BL.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.EventArgsTests
{
    [TestClass]
    public class CompletionStateEventArgsTests
    {
        [TestMethod]
        public void CompletionStateEventArgsCreatingWithValidParameterTest()
        {
            var fileName = "TestName";

            var eventArgs = new CompletionStateEventArgs(CompletionState.Completed, fileName);

            Assert.IsTrue(Equals(fileName, eventArgs.FileName)
                          && Equals(CompletionState.Completed, eventArgs.CompletionState));
        }

        [TestMethod]
        public void CompletionStateEventArgsCreatingWithInvalidParameterTest()
        {
            string fileName = null;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new CompletionStateEventArgs(CompletionState.Completed, fileName));
        }
    }
}