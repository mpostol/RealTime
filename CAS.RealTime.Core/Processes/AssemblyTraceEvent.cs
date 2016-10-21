//<summary>
//  Title   : Static TraceSource to be used by this assembly.
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

using CAS.Lib.RTLib.Properties;
using System;
using System.Diagnostics;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Static instance of <see cref="TraceSource"/> 
  /// </summary>
  public static class AssemblyTraceEvent
  {

    #region public static
    /// <summary>
    /// Gets the assembly trace source of name defined by the <c>TraceName</c> defined in the the <see cref="Settings"/>.
    /// </summary>
    /// <value>The assembly trace source.</value>
    public static TraceSource AssemblyTraceSource { get { return m_TraceEvent.Value; } }
    /// <summary>
    /// regular trace message
    /// </summary>
    /// <param name="type">type of message, e.g. Verbose, Error, etc.. please see <see cref="System.Diagnostics.TraceEventType"/></param>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public static void Trace(TraceEventType type, int id, string source, string message)
    {
      m_TraceEvent.Value.TraceEvent(type, id, source, message);
    }
    /// <summary>
    /// Traces <see cref="TraceEventType.Verbose"/> type message  
    /// </summary>
    /// <param name="id">user identifier for the message</param>
    /// <param name="source">source of message</param>
    /// <param name="message">message that we want to trace</param>
    public static void TraceVerbose(int id, string source, string message)
    {
      m_TraceEvent.Value.TraceEvent(TraceEventType.Verbose, id, source, message);
    }
    #endregion

    #region private
    private static Lazy<TraceSource> m_TraceEvent = new Lazy<TraceSource>(() => new TraceSource(Settings.Default.TraceName));
    #endregion

  }
}
