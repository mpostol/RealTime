//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Class that allows running method asynchronously
  /// </summary>
  public class RunMethodAsynchronously
  {
    private AsyncOperation m_asyncoperation_internal;

    /// <summary>
    /// This method is called after asynchronous call
    /// </summary>
    /// <param name="asyncResult">The result captured as the <see cref="IAsyncResult"/> instance.</param>
    private void MyAsyncCallback(IAsyncResult asyncResult)
    {
      m_asyncoperation_internal.EndInvoke(asyncResult);
    }

    /// <summary>
    /// Delegate for method that will be called asynchronously
    /// </summary>
    public delegate void AsyncOperation(object[] parameters);

    /// <summary>
    /// Initializes a new instance of the <see cref="RunMethodAsynchronously"/> class.
    /// </summary>
    /// <param name="asyncoper">Delegate to method that will be called asynchronously</param>
    public RunMethodAsynchronously(AsyncOperation asyncoper)
    {
      m_asyncoperation_internal = asyncoper;
    }

    /// <summary>
    /// Runs the method asynchronously. Return immediately.
    /// </summary>
    /// <param name="parameters">parameters for the method (delegate)  </param>
    public void RunAsync(object[] parameters)
    {
      AsyncCallback callBack = new AsyncCallback(MyAsyncCallback);
      m_asyncoperation_internal.BeginInvoke(parameters, null, null);
    }

    /// <summary>
    /// Runs the asynchronously method without any additional parameters
    /// </summary>
    public void RunAsync()
    {
      RunAsync(null);
    }
  }
}