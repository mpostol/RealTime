//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using UAOOI.ProcessObserver.RealTime.Processes;

namespace CAS.RealTime.UnitTests
{
    [TestClass]
    public class RunMethodAsynchronouslyUnitTest
    {
        [TestMethod]
        public void RunMethodAsynchronouslyTestMethod()
        {
            RunMethodAsynchronously runasync = new RunMethodAsynchronously(delegate (object[] param) { WaitAndCheckConsistency((int)param[0], (int)param[1]); });
            runasync.RunAsync(new object[] { 5, 5 });
            bool _res = false;
            lock (this)
                _res = m_MEthodFinished.Wait(this, 1200);
            Assert.IsTrue(_res, "Calling asynchronous method filed.");
        }

        [TestMethod]
        public void RunMethodAsynchronouslyTimeOutTestMethod()
        {
            RunMethodAsynchronously runasync = new RunMethodAsynchronously(delegate (object[] param) { WaitAndCheckConsistency((int)param[0], (int)param[1]); });
            runasync.RunAsync(new object[] { 5, 5 });
            bool _res = false;
            lock (this)
                _res = m_MEthodFinished.Wait(this, 500);
            Assert.IsFalse(_res, "Calling asynchronous method filed.");
        }

        #region instrumentation

        private Condition m_MEthodFinished = new Condition();

        private void WaitAndCheckConsistency(int par1, int par2)
        {
            Thread.Sleep(1000);
            m_MEthodFinished.Notify();
        }

        #endregion instrumentation
    }
}