//<summary>
//  Title   : Synchronous exchange of events between concurrent processes.
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPostol 16-02-2006
//      Zmienile atrybut GetEventInt na private
//    MPostol - 24-08-2004
//      Created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// It allows synchronizing and exchanging events between concurrent threads on the FIFO basis.
  /// </summary>
  public class EventsSynchronization
  {
    #region PRIVATE
    private object currEvent;
    private Condition markNewEvent  = new Condition();
    private Condition markBuffEmpty = new Condition();
    private bool GetEventInt( out object lastEvent, int TimeOut )
    {
      lastEvent = null;
      if ((currEvent == null) && ( ! markNewEvent.Wait(this, TimeOut))) return false;
      lastEvent = currEvent;
      currEvent = null;
      markBuffEmpty.Notify();
      return true;
    }
    #endregion
    #region PUBLIC
    /// <summary>
    /// Deposits or gets new event. If there is any events to be get depositing thread enters wait state.
    /// </summary>
    /// <param name="lastEvent">The last event.</param>
    /// <param name="TimeOut">The time out.</param>
    /// <param name="callingMonitor">The calling monitor.</param>
    /// <returns></returns>
    public bool GetEvent(out object lastEvent, int TimeOut, object callingMonitor)
    {
      bool res;
      lock(this)
      {
        System.Threading.Monitor.Exit(callingMonitor);
        res = GetEventInt(out lastEvent, TimeOut);
      }
      System.Threading.Monitor.Enter(callingMonitor);
      return res;
    }
    /// <summary>
    /// Gets the event.
    /// Deposits or gets new event. If there is any events to be get depositing thread enters wait state.
    /// </summary>
    /// <param name="lastEvent">The last event.</param>
    /// <param name="TimeOut">The time out.</param>
    /// <returns></returns>
    public bool GetEvent(out object lastEvent, int TimeOut)
    {
      lock(this)
      {
        return GetEventInt(out lastEvent, TimeOut);
      }
    }
    /// <summary>
    /// Sets the event.
    /// Adds this event to process list and informs that new event occurs
    /// </summary>
    /// <param name="newEvent">The new event.</param>
    /// <param name="callingMonitor">The calling monitor.</param>
    public void SetEvent(object newEvent, object callingMonitor)
    {
      lock(this)
      {
        System.Threading.Monitor.Exit(callingMonitor);
        if (currEvent != null) markBuffEmpty.Wait(this);
        currEvent = newEvent;
        markNewEvent.Notify();
      }
      System.Threading.Monitor.Enter(callingMonitor);
    }
    /// <summary>
    /// Sets the event.
    /// Adds this event to process list and informs that new event occurs
    /// </summary>
    /// <param name="newEvent">The new event.</param>
    public void SetEvent(object newEvent)
    {
      lock(this)
      {
        if (currEvent != null) markBuffEmpty.Wait(this);
        currEvent = newEvent;
        markNewEvent.Notify();
      }
    }
    #endregion
  }
}
