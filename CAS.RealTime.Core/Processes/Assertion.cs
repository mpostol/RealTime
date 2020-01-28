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
  ///  Implementation of Assertion concept
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable"), Obsolete("Use System.Diagnostics.Debug.Assert")]
  public class Assertion : Exception
  {
    #region private
    private EventLogMonitor myEventLog;
    private short category;
    #endregion

    #region public
    /// <summary>
    /// Throws the assertion.
    /// </summary>
    /// <param name="assertion">the result of condition of assertion</param>
    /// <param name="category">The category.</param>
    /// <param name="newMessage">The  message.</param>
    public void ThrowAssertion(bool assertion, short category, string newMessage)
    {
      if (!assertion)
      {
        myEventLog.SetCategory = category;
        this.category = category;
        myEventLog.SetMessage = newMessage;
        myEventLog.WriteEntry();
        throw this;
      }
    }
    /// <summary>
    /// Throws the assertion.
    /// </summary>
    /// <param name="assertion">the result of condition of assertion</param>
    /// <param name="category">The category.</param>
    /// <param name="logEvent">if set to <c>true</c> event is logged to event log</param>
    public void ThrowAssertion(bool assertion, short category, bool logEvent)
    {
      if (!assertion)
      {
        myEventLog.SetCategory = category;
        this.category = category;
        if (logEvent) myEventLog.WriteEntry();
        throw this;
      }
    }
    /// <summary>
    /// Asserts the specified assertion.
    /// </summary>
    /// <param name="assertion">the condition</param>
    /// <param name="category">The category.</param>
    [Obsolete("This is a deprecated method. Use public void Assert(bool assertion, short category, string CustomMeassage)  instead.")]
    public void Assert(bool assertion, short category)
    {
      Assert(assertion, category, "");
    }
    /// <summary>
    /// Asserts the specified assertion.
    /// </summary>
    /// <param name="assertion">the condition</param>
    /// <param name="category">The category.</param>
    /// <param name="CustomMeassage">The message.</param>
    public void Assert(bool assertion, short category, string CustomMeassage)
    {
      if (!assertion)
      {
        StackTrace _a = new System.Diagnostics.StackTrace(true);
        Processes.AssemblyTraceEvent.Trace(TraceEventType.Error, 81, nameof(Assertion), "Assert: " + CustomMeassage + " ; StackTrace log:" + _a.ToString());
        Manager.AddToErrorQueue();
      }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="Assertion"/> class.
    /// </summary>
    public Assertion() : this
      (
      "Processes.Assertion error - the malicious thread was suspended and added to error queue",
      0, false
      )
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="Assertion"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="eventID">The event ID.</param>
    /// <param name="rebootRequired">if set to <c>true</c> system reboot is required.</param>
    public Assertion(string message, int eventID, bool rebootRequired) :
      base(message)
    {
      EventLogEntryType severity;
      if (rebootRequired) severity = EventLogEntryType.Error;
      else severity = EventLogEntryType.Warning;
      myEventLog = new EventLogMonitor("Assertion error:" + message, severity, 1000 + eventID, 0);
    }
    #endregion
  }
}