//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Diagnostics;

namespace CAS.Lib.RTLib.Processes
{

  ///<summary>
  /// Specifies the event type of an event log entry.
  ///</summary>
  public enum EventLogEntryType
  {
    Error = 1, //An error event. This indicates a significant problem the user should know about; usually a loss of functionality or data.
    FailureAudit = 16, //A failure audit event. This indicates a security event that occurs when an audited access attempt fails; for example, a failed attempt to open a file.
    Information = 4, //An information event. This indicates a significant, successful operation.
    SuccessAudit = 8, //A success audit event. This indicates a security event that occurs when an audited access attempt is successful; for example, logging on successfully.
    Warning = 2 //A warning event. This indicates a problem that is not immediately significant, but that may signify conditions that could cause future problems.
  }

  /// <summary>
  /// Summary description for EventLogMonitor.
  /// </summary>
  public class EventLogMonitor
  {

    #region private
    private static TraceEventType ConvertToTraceEventType(EventLogEntryType eventLogEntryType)
    {
      switch (eventLogEntryType)
      {
        case EventLogEntryType.Error:
          return TraceEventType.Error;
        case EventLogEntryType.Information:
          return TraceEventType.Information;
        case EventLogEntryType.Warning:
          return TraceEventType.Warning;
        default:
          return TraceEventType.Information;
      }
    }
    private EventLogEntryType myType;
    private readonly int myEventID;
    private short myCategory;
    private string myMessage;

    #region static
    /// <summary>
    /// this name appears in the tracing messages
    /// </summary>
    private const string m_traceName = "TracesFromEventLogMonitor";
    private static TraceSource m_TraceSource;
    //static constructor
    static EventLogMonitor()
    {
      m_TraceSource = new TraceSource(m_traceName);
    }
    #endregion
    #endregion

    #region PUBLIC
    /// <summary>
    /// Writes a trace event message to the trace listeners in the System.Diagnostics.TraceSource.Listeners collection 
    /// using the specified event type and event identifier.
    /// </summary>
    /// <param name="pEventType">
    /// One of the System.Diagnostics.TraceEventType values that specifies the event type of the trace data.
    /// </param>
    /// <param name="pId">A numeric identifier for the event.</param>
    /// <param name="pMessage">The trace message to write.</param>
    [Conditional("TRACE")]
    public void TraceEvent(TraceEventType pEventType, int pId, string pMessage)
    {
      m_TraceSource.TraceEvent(pEventType, pId, m_traceName + ":" + pMessage);
    }
    /// <summary>
    /// Writes to event log.
    /// </summary>
    /// <param name="Message">The message.</param>
    /// <param name="level">The level.</param>
    /// <param name="eventID">The event ID.</param>
    public static void WriteToEventLog(string Message, EventLogEntryType level, int eventID)
    {
      WriteToEventLog(Message, level, eventID, 0);
    }
    /// <summary>
    /// Writes to event log.
    /// </summary>
    /// <param name="Message">The message.</param>
    /// <param name="level">The level.</param>
    /// <param name="eventID">The event ID.</param>
    /// <param name="category">The category.</param>
    public static void WriteToEventLog(string Message, EventLogEntryType level, int eventID, short category)
    {
      new EventLogMonitor(Message, level, eventID, category).WriteEntry();
    }
    /// <summary>
    /// Writes to event log.
    /// </summary>
    /// <param name="Message">The message.</param>
    /// <param name="level">The level.</param>
    public static void WriteToEventLog(string Message, EventLogEntryType level)
    {
      WriteToEventLog(Message, level, 0);
    }
    /// <summary>
    /// Writes to event log info.
    /// </summary>
    /// <param name="Message">The message.</param>
    /// <param name="eventID">The event ID.</param>
    public static void WriteToEventLogInfo(string Message, int eventID)
    {
      WriteToEventLog(Message, EventLogEntryType.Information, eventID);
    }
    /// <summary>
    /// Writes to event log error.
    /// </summary>
    /// <param name="Message">The message.</param>
    /// <param name="eventID">The event ID.</param>
    public static void WriteToEventLogError(string Message, int eventID)
    {
      WriteToEventLog(Message, EventLogEntryType.Error);
    }
    /// <summary>
    /// Writes an information type entry, with the given message text, to the event log.
    /// </summary>
    public void WriteEntry()
    {
      TraceEvent(ConvertToTraceEventType(myType), myEventID, string.Format("Category: {0}, {1}", myCategory, myMessage));
    }
    /// <summary>
    /// set the event message
    /// </summary>
    public string SetMessage
    {
      set => myMessage = value;
    }
    /// <summary>
    /// return previously set event message
    /// </summary>
    public string GetMessage => myMessage;
    /// <summary>
    /// Sets the set category.
    /// </summary>
    /// <value>The set category.</value>
    public short SetCategory
    {
      set => myCategory = value;
    }
    /// <summary>
    ///  Creates an event log with the application-defined event identifier, and application-defined category 
    ///  to the event log, using the "CASCommServer" registered event source. The category can be used by the 
    ///  event viewer to filter events in the log.
    /// </summary>
    /// <param name="type">One of the EventLogEntryType values.</param>
    /// <param name="eventID">The application-specific identifier for the event.</param>
    /// <param name="category">The application-specific subcategory associated with the message.</param>
    /// <param name="message">message to be stored</param>
    public EventLogMonitor(string message, EventLogEntryType type, int eventID, short category)
    {
      myMessage = message;
      myType = type;
      myEventID = eventID;
      myCategory = category;
    }
    #endregion

  }
}
