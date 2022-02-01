//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;

namespace UAOOI.ProcessObserver.RealTime.Processes
{
    /// <summary>
    /// Class that allows running method asynchronously
    /// </summary>
    public class RunMethodAsynchronously
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunMethodAsynchronously"/> class.
        /// </summary>
        /// <param name="asyncoper">Delegate to method that will be called asynchronously</param>
        public RunMethodAsynchronously(AsyncOperation asyncoper)
        {
            AsyncOperationField = asyncoper;
        }

        /// <summary>
        /// Delegate for method that will be called asynchronously
        /// </summary>
        public delegate void AsyncOperation(object[] parameters);

        /// <summary>
        /// Runs the method asynchronously. Return immediately.
        /// </summary>
        /// <param name="parameters">parameters for the method (delegate)  </param>
        public void RunAsync(object[] parameters)
        {
            AsyncCallback callBack = new AsyncCallback(MyAsyncCallback);
            AsyncOperationField.BeginInvoke(parameters, null, null);
        }

        /// <summary>
        /// Runs the asynchronously method without any additional parameters
        /// </summary>
        public void RunAsync()
        {
            RunAsync(null);
        }

        #region private

        private readonly AsyncOperation AsyncOperationField;

        /// <summary>
        /// This method is called after asynchronous call
        /// </summary>
        /// <param name="asyncResult">The result captured as the <see cref="IAsyncResult"/> instance.</param>
        private void MyAsyncCallback(IAsyncResult asyncResult)
        {
            AsyncOperationField.EndInvoke(asyncResult);
        }

        #endregion private
    }
}