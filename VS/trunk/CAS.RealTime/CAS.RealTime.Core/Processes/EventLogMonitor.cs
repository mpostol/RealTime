//<summary>
//  Title   : Events registration and events management
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPostol 31-10-2003: created
//    MPostol 26-04-2004:
//      Wykonanie rejestracji EventLog.CreateEventSource wymaga podniesienia uprawnieñ, zatem mo¿e byæ 
//      wykonane tylko w czasie normalnej instalacji. Na razie wywali³em, ale trzeba ten problem rozwi¹zaæ
//    Wptasinski 11 - 05 -2004
//      Exception handling added - whenever the log source cannot be created, exception is caught and handled
//    20080612: mzbrzezny: Logs that goes to event log, goes to trace also.
//                         This is first step to remove logging using eventLog.WriteEntry
//                         and use only trace mechanism.
//                         This change adds trace source to eventlogmonitor class, to use this 
//                         trace add trace source: "TracesFromEventLogMonitor" to the application configuration
//    20080625: mzbrzezny: new write to log function that supports category
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Diagnostics;
using System.Reflection;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Summary description for EventLogMonitor.
  /// </summary>
  public class EventLogMonitor
  {

    #region PRIVATE
    #region trace
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
    #endregion
    private EventLogEntryType myType;
    private int myEventID;
    private short myCategory;
    private string myMessage;
    #endregion

    #region PRIVATE STATIC
    /// <summary>
    /// this name appears in the tracing messages
    /// </summary>
    private const string m_traceName = "TracesFromEventLogMonitor";
    /// <summary>
    /// elSource: The source by which the application is registered on the specified computer.
    /// </summary>
    private static string elSource = "CASProgDefName";
    /// <summary>
    /// elLogName: The name of the log the source's entries are written to. Possible values include: Application, 
    /// Security, System, or a custom event log.
    /// </summary>
    private const string elLogName = "Application";
    private static EventLog eventLog;
    private static TraceSource m_TraceSource;
    //static constructor
    static EventLogMonitor()
    {
      Assembly _Assembly = Assembly.GetExecutingAssembly();
      AssemblyProductAttribute progName = (AssemblyProductAttribute)Attribute.GetCustomAttribute(_Assembly, typeof(AssemblyProductAttribute), false);
      if (progName != null)
        elSource = progName.Product;
      else
        elSource = _Assembly.FullName;
      try
      {
        if (!EventLog.SourceExists(elSource))
          EventLog.CreateEventSource(elSource, elLogName);
      }
      catch
      {
        //bool x = false;
      }
      eventLog = new EventLog();
      ((System.ComponentModel.ISupportInitialize)(eventLog)).BeginInit();
      eventLog.Source = elSource;
      ((System.ComponentModel.ISupportInitialize)(eventLog)).EndInit();
      m_TraceSource = new TraceSource(m_traceName);
    }//EventLogMonitor
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
    public static void WriteToEventLog(string Message, System.Diagnostics.EventLogEntryType level, int eventID)
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
    public static void WriteToEventLog(string Message, System.Diagnostics.EventLogEntryType level)
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
      eventLog.WriteEntry(myMessage, myType, myEventID, myCategory);
      TraceEvent(ConvertToTraceEventType(myType), myEventID, System.String.Format("Category: {0}, {1}", myCategory, myMessage));
    }
    /// <summary>
    /// set the event message
    /// </summary>
    public string SetMessage
    {
      set { myMessage = value; }
    }
    /// <summary>
    /// return previously set event message
    /// </summary>
    public string GetMessage
    {
      get { return myMessage; }
    }
    /// <summary>
    /// Sets the set category.
    /// </summary>
    /// <value>The set category.</value>
    public short SetCategory
    {
      set { myCategory = value; }
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
