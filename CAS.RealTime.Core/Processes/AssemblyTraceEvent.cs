//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
    private const string m_TraceName = "CAS.RealTime";
    private static Lazy<TraceSource> m_TraceEvent = new Lazy<TraceSource>(() => new TraceSource(m_TraceName));
    #endregion

  }
}
