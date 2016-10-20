//_______________________________________________________________
//  Title   : TraceEvent 
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________


using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Class responsible for tracing. Created
  /// </summary>
  public class TraceEvent
  {
    #region private
    private TraceSource m_traceSource;

    private void PrivateTrace( TraceEventType type, int id, string message )
    {
      try
      {
        m_traceSource.TraceEvent( type, id, message );
        m_traceSource.Flush();
      }
      catch ( IOException ) { }
    }
    #endregion

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="TraceEvent"/> class.
    /// </summary>
    /// <param name="sourceName">The source name.</param>
    public TraceEvent( string sourceName )
    {
      m_traceSource = new TraceSource( sourceName );
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
        m_traceSource.Flush();
        m_traceSource.Close();
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