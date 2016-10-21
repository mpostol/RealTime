  // <summary>
  //  Title   : Events registration
  //  Author  : 
  //  System  : Microsoft Visual C# .NET
  //  History :
  //    MPostol 26-04-2004:
  //      Wykonanie rejestracji EventLog.CreateEventSource wymaga podniesienia uprawnieñ, zatem mo¿e byæ 
  //      wykonane tylko w czasie normalnej instalacji. Na razie wywali³em, ale trzeba ten problem rozwi¹zaæ
  //    MPostol 31-10-2003: created
  //    Wptasinski 11 - 05 -2004
  //      Exception handling added - whenever the log source cannot be created, exception is caught and handled
  //    <Author> - <date>:
  //    <description>
  //
  //  Copyright (C)2003, CAS LODZ POLAND.
  //  TEL: 42' 686 25 47
  //  mailto: techsupp@cas.com.pl
  //  http: www.cas.com.pl
  // </summary>
  
namespace Processes
{
  using System;
  using System.Diagnostics;
  using System.Reflection;

  /// <summary>
  /// Summary description for EventLogMonitor.
  /// </summary>
  public class EventLogMonitor
  {
    #region PRIVATE
    private EventLogEntryType myType;
    private int myEventID;
    private short myCategory;
    private string myMessage;
    #endregion
    #region PRIVATE STATIC
    /// <summary>
    /// elSource: The source by which the application is registered on the specified computer.
    /// </summary>
    private static string elSource = "CASProgDefName";
    /// <summary>
    /// elLogName: The name of the log the source's entries are written to. Possible values include: Application, 
    /// Security, System, or a custom event log.
    /// </summary>
    private const string elLogName = "CAS_ApplicationLog";
    private static System.Diagnostics.EventLog eventLog;
    static EventLogMonitor()
    {
      Assembly myAssemb = Assembly.GetEntryAssembly();// bedziemy mieli jako zrodlo - dokladnie ta aplikacje - ktora wywoluje
      AssemblyProductAttribute progName = 
        (AssemblyProductAttribute)Attribute.GetCustomAttribute(myAssemb, typeof(AssemblyProductAttribute), false);
      if (progName != null) elSource = progName.Product;
      else elSource = myAssemb.FullName;
      try
      {
        if (!EventLog.SourceExists(elSource)) 
          EventLog.CreateEventSource(elSource, elLogName);
      }
      catch
      {
      }
      eventLog = new System.Diagnostics.EventLog(elLogName);
      ((System.ComponentModel.ISupportInitialize)(eventLog)).BeginInit();
      eventLog.Source = elSource;
      ((System.ComponentModel.ISupportInitialize)(eventLog)).EndInit();
    }//EventLogMonitor
    #endregion
    #region PUBLIC
    public static void WriteToEventLog(string Message, System.Diagnostics.EventLogEntryType level)
    {
      new EventLogMonitor(Message,level,0,0).WriteEntry();
    }
    public static void WriteToEventLogInfo(string Message)
    {
      WriteToEventLog(Message, System.Diagnostics.EventLogEntryType.Information);
    }
    public static void WriteToEventLogError(string Message)
    {
      WriteToEventLog(Message, System.Diagnostics.EventLogEntryType.Error);
    }
    /// <summary>
    /// Writes an information type entry, with the given message text, to the event log.
    /// </summary>
    public void WriteEntry()
    {
      eventLog.WriteEntry(myMessage, myType, myEventID, myCategory);
    }
    /// <summary>
    /// set the event message
    /// </summary>
    public string SetMessage 
    {
      set { myMessage = value;}
    }
    /// <summary>
    /// return previously set event message
    /// </summary>
    public string GetMessage
    {
      get {return myMessage;}
    }
    public short SetCategory 
    {
      set { myCategory = value;}
    }
    /// <summary>
    ///  Creates an event log with the application-defined event identifier, and application-defined category 
    ///  to the event log, using the "CASCommServer" registered event source. The category can be used by the 
    ///  event viewer to filter events in the log.
    /// </summary>
    /// <param name="type">One of the EventLogEntryType values.</param>
    /// <param name="eventID">The application-specific identifier for the event.</param>
    /// <param name="category">The application-specific subcategory associated with the message.</param>
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
