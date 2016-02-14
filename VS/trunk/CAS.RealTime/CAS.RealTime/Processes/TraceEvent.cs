//<summary>
//  Title   : TraceEvent in RTLib 
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  History :
//    20090715 - mzbrzezny - TraceError and Helper to get whole message from exception are added (GetMessageWithExceptionNameFromExceptionIncludingInnerException)
//    20071009 - mzbrzezny - during TraceEventClose we need to catch exception in case the listener is disposed
//    2007 - mpostol - created      
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Class responsible for tracing inside RTLib
  /// </summary>
  public class TraceEvent
  {
    #region private
    private TraceSource m_tracesource;

    private void PrivateTrace( TraceEventType type, int id, string message )
    {
      try
      {
        m_tracesource.TraceEvent( type, id, message );
        m_tracesource.Flush();
      }
      catch ( IOException ) { }
    }
    #endregion
    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="TraceEvent"/> class.
    /// </summary>
    /// <param name="sourcename">The source name.</param>
    public TraceEvent( string sourcename )
    {
      m_tracesource = new TraceSource( sourcename );
    }
    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations before the
    /// <see cref="TraceEvent"/> is reclaimed by garbage collection.
    /// </summary>
    ~TraceEvent()
    {
      TraceEventClose();
    }
    #endregion
    #region public
    /// <summary>
    ///Close the local <see cref="TraceSource"/>
    /// </summary>
    public void TraceEventClose()
    {
      try
      {
        m_tracesource.Flush();
        m_tracesource.Close();
      }
      catch ( ObjectDisposedException )
      {
      }
    }
    /// <summary>
    /// regular trace message
    /// </summary>
    /// <param name="type">type of message, e.g. Verbose, Error, etc.. please see <see cref="System.Diagnostics.TraceEventType"/></param>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public void Trace( TraceEventType type, int id, string source, string message )
    {
      PrivateTrace( type, id, source + ": " + message );
    }
    /// <summary>
    /// verbose trace message
    /// </summary>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public void TraceVerbose( int id, string source, string message )
    {
      Trace( TraceEventType.Verbose, id, source, message );
    }
    /// <summary>
    /// information trace message
    /// </summary>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public void TraceInformation( int id, string source, string message )
    {
      Trace( TraceEventType.Information, id, source, message );
    }
    /// <summary>
    /// warning trace message
    /// </summary>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public void TraceWarning( int id, string source, string message )
    {
      Trace( TraceEventType.Warning, id, source, message );
    }
    /// <summary>
    /// error trace message
    /// </summary>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public void TraceError( int id, string source, string message )
    {
      Trace( TraceEventType.Error, id, source, message );
    }
    /// <summary>
    /// Gets the message with exception name from exception including inner exception.
    /// </summary>
    /// <param name="ex">The ex.</param>
    /// <returns>the whole message</returns>
    public static string GetMessageWithExceptionNameFromExceptionIncludingInnerException( Exception ex )
    {
      StringBuilder sb = new StringBuilder( ex.GetType().ToString() );
      sb.Append( ":" );
      sb.Append( ex.Message );
      Exception InnerEx = ex.InnerException;
      while ( InnerEx != null )
      {
        sb.Append( "; " );
        sb.Append( InnerEx.GetType().ToString() );
        sb.Append( ":" );
        sb.Append( InnerEx.Message );
        InnerEx = InnerEx.InnerException;
      }
      return sb.ToString();
    }
    #endregion
  }
}