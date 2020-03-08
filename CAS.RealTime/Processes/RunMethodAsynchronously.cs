//<summary>
//  Title   : Processes - running method Asynchronously 
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    mzbrzezny 20070903 - created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>

using System;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Class that allows running method asynchronously
  /// </summary>
  public class RunMethodAsynchronously
  {

    private AsyncOperation m_asyncoperation_internal;

    /// <summary>
    /// Delegate for method that will be called asynchronously
    /// </summary>
    public delegate void AsyncOperation(object[] parameters);
    /// <summary>
    /// This method is called after asynchronous call
    /// </summary>
    /// <param name="ar">The ar.</param>
    private void MyAsyncCallback(IAsyncResult ar)
    {
      m_asyncoperation_internal.EndInvoke( ar );
    }
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
      AsyncCallback callBack = new AsyncCallback( MyAsyncCallback );
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
