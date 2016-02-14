//<summary>
//  Title   : Static instance of Trace Event for the locale use
//  System  : Microsoft Visual C# .NET 2005
//  History :
//    $LastChangedDate$
//    $Rev$
//    $LastChangedBy$
//    $URL$
//    $Id$
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http:\\www.cas.eu
//</summary>

using System;
using System.Diagnostics;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Static instance of Trace Event for the locale use
  /// </summary>
  [Obsolete( "Use CAS.Lib.RTLib.Processes.TraceEvent instead" )]
  public static class AssemblyTraceEvet
  {
    private const string Name = "CAS.Lib.RTLib";
    private static TraceEvent m_TraceEvent = new TraceEvent( Name );
    #region public static
    /// <summary>
    /// regular trace message
    /// </summary>
    /// <param name="type">type of message, e.g. Verbose, Error, etc.. please see <see cref="System.Diagnostics.TraceEventType"/></param>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public static void Trace( TraceEventType type, int id, string source, string message )
    {
      m_TraceEvent.Trace( type, id, source, message );
    }
    /// <summary>
    /// Traces <see cref="TraceEventType.Verbose"/> type message  
    /// </summary>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public static void TraceVerbose( int id, string source, string message )
    {
      m_TraceEvent.Trace( TraceEventType.Verbose, id, source, message );
    }
    #endregion
  }
}
