//<summary>
//  Title   : Implements Thread's monitored bt a watch dog. 
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MZbrzezny - 01-06-2005
//      Dodano nowe informacje do eventlog: stack trace i processname
//    MPostol - 09-03-04
//      Wprowadzenie funkcji wstrzymania zmiany czasu timeout - ResetWatchDog
//    MPostol - 0211-2003: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>

namespace CAS.Lib.RTLib.Processes
{
  using System;
  using System.Diagnostics;
  using System.Threading;

  /// <summary>
  ///  Implements the watch dog mechanism for Thread's. 
  /// </summary>
  public class MonitoredThread
  {
    #region PRIVATE STATIC
    private static System.Collections.ArrayList threadsList = new System.Collections.ArrayList();
    private static void Handler()
    {
      while ( true )
      {
        Thread.Sleep( TimeSpan.FromSeconds( 1 ) );
        lock ( threadsList )
          foreach ( MonitoredThread thr in threadsList )
            thr.decCounter();
      }
    }
    static MonitoredThread()
    {
      CAS.Lib.RTLib.Processes.Manager.StartProcess
        ( new ThreadStart( Handler ), "MonitoredThreadHndl", true, ThreadPriority.Highest );
    }
    #endregion
    #region PRIVATE
    private string Name;
    private int internalCounter;
    private bool on;
    private CAS.Lib.RTLib.Processes.EventLogMonitor myEventLog;
    private ushort myTimeOut;
    private void decCounter()
    {
      lock ( this )
      {
        if ( !on )
          return;
        internalCounter--;
        if ( internalCounter == 0 )
        {
          System.Diagnostics.StackTrace a = new System.Diagnostics.StackTrace();
          myEventLog.SetMessage = myEventLog.GetMessage + " process:" + Name +
#if DEBUG
 "; stacktrace:" + a.ToString() +
#endif
 "";
          myEventLog.WriteEntry();
          System.Diagnostics.Debug.Assert
            ( false, "I am about to reboot the system, but reboot is now switched off because of debug mode", "Processes.MonitoredThread" );
#if !DEBUG
          CAS.Lib.RTLib.Processes.Manager.Reboot();
#endif
        }
      }
    }
    #endregion
    #region PUBLIC
    /// <summary>
    /// Resets the watch dog.
    /// </summary>
    /// <param name="category">The category.</param>
    public void ResetWatchDog( short category )
    {
      lock ( this )
      {
        internalCounter = myTimeOut;
        myEventLog.SetCategory = category;
      }
    }
    /// <summary>
    /// Resets the watch dog.
    /// </summary>
    /// <param name="category">The category.</param>
    /// <param name="timeout">The timeout.</param>
    public void ResetWatchDog( short category, ushort timeout )
    {
      lock ( this )
      {
        on = ( timeout > 0 );
        myTimeOut = timeout;
        internalCounter = timeout;
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
    public MonitoredThread
      (
      ushort timeout, string message, int eventID,
      ThreadStart process, string processName, bool isBackground, ThreadPriority priority
      )
    {
      myEventLog = new CAS.Lib.RTLib.Processes.EventLogMonitor( "Time out error: " + message, EventLogEntryType.Error, 2000 + eventID, 0 );
      ResetWatchDog( short.MaxValue, timeout );
      Name = processName;
      lock ( threadsList )
        threadsList.Add( this );
      CAS.Lib.RTLib.Processes.Manager.StartProcess( process, processName, isBackground, priority );
    }
    #endregion
  }
}
