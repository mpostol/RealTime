//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  using System;
  using System.Collections;
  using System.Threading;

  /// <summary>
  ///  Implements the watch dog mechanism for Thread's.
  /// </summary>
  public class MonitoredThread
  {
    #region private static

    private static ArrayList threadsList = new ArrayList();

    private static void Handler()
    {
      while (true)
      {
        Thread.Sleep(TimeSpan.FromSeconds(1));
        lock (threadsList)
          foreach (MonitoredThread _monitoredthred in threadsList)
            _monitoredthred.DecCounter();
      }
    }

    static MonitoredThread()
    {
      Manager.StartProcess(new ThreadStart(Handler), "MonitoredThreadHndl", true, ThreadPriority.Highest);
    }

    #endregion private static

    #region PRIVATE

    private Thread m_Thread = null;
    private readonly string Name;
    private int m_InternalCounter;
    private ushort m_TimeOut;
    private EventLogMonitor myEventLog;

    private void DecCounter()
    {
      lock (this)
      {
        if (m_InternalCounter < +0)
          return;
        m_InternalCounter--;
        if (m_InternalCounter == 0)
        {
          string _exceptionMessage = myEventLog.GetMessage + " process:" + Name;
          myEventLog.SetMessage = _exceptionMessage;
          myEventLog.WriteEntry();
          m_Thread.Abort();
        }
      }
    }

    #endregion PRIVATE

    #region PUBLIC

    /// <summary>
    /// Resets the watch dog.
    /// </summary>
    /// <param name="category">The category.</param>
    public void ResetWatchDog(short category)
    {
      lock (this)
      {
        m_InternalCounter = m_TimeOut;
        myEventLog.SetCategory = category;
      }
    }

    /// <summary>
    /// Resets the watch dog.
    /// </summary>
    /// <param name="category">The category.</param>
    /// <param name="timeout">The timeout.</param>
    public void ResetWatchDog(short category, ushort timeout)
    {
      lock (this)
      {
        m_TimeOut = timeout;
        m_InternalCounter = timeout;
        myEventLog.SetCategory = category;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonitoredThread"/> class.
    /// </summary>
    /// <param name="timeout">The timeout.</param>
    /// <param name="message">The message.</param>
    /// <param name="eventID">The event ID.</param>
    /// <param name="process">The process.</param>
    /// <param name="processName">Name of the process.</param>
    /// <param name="isBackground">if set to <c>true</c> [is background].</param>
    /// <param name="priority">The priority.</param>
    public MonitoredThread(ushort timeout, string message, int eventID, ThreadStart process, string processName, bool isBackground, ThreadPriority priority)
    {
      myEventLog = new EventLogMonitor("Time out error: " + message, EventLogEntryType.Error, 2000 + eventID, 0);
      ResetWatchDog(short.MaxValue, timeout);
      Name = processName;
      lock (threadsList)
        threadsList.Add(this);
      m_Thread = Manager.StartProcess(process, processName, isBackground, priority);
    }

    #endregion PUBLIC
  }
}