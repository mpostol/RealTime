//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Threading;

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// It allows synchronizing and exchanging events between concurrent threads on the FIFO basis.
  /// </summary>
  public class EventsSynchronization
  {
    #region private

    private object m_CurrEvent;
    private Condition m_MarkNewEvent = new Condition();
    private Condition m_MarkBuffEmpty = new Condition();

    private bool GetEventInt(out object lastEvent, int TimeOut)
    {
      lastEvent = null;
      if ((m_CurrEvent == null) && (!m_MarkNewEvent.Wait(this, TimeOut))) return false;
      lastEvent = m_CurrEvent;
      m_CurrEvent = null;
      m_MarkBuffEmpty.Notify();
      return true;
    }

    #endregion private



    #region public

    /// <summary>
    /// Deposits or gets new event. If there is any events to be get depositing thread enters wait state.
    /// </summary>
    /// <param name="lastEvent">The last event.</param>
    /// <param name="TimeOut">The time out.</param>
    /// <param name="callingMonitor">The calling monitor.</param>
    /// <returns></returns>
    public bool GetEvent(out object lastEvent, int TimeOut, object callingMonitor)
    {
      bool _res;
      lock (this)
      {
        Monitor.Exit(callingMonitor);
        _res = GetEventInt(out lastEvent, TimeOut);
      }
      Monitor.Enter(callingMonitor);
      return _res;
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
      lock (this)
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
      lock (this)
      {
        Monitor.Exit(callingMonitor);
        if (m_CurrEvent != null) m_MarkBuffEmpty.Wait(this);
        m_CurrEvent = newEvent;
        m_MarkNewEvent.Notify();
      }
      Monitor.Enter(callingMonitor);
    }

    /// <summary>
    /// Sets the event.
    /// Adds this event to process list and informs that new event occurs
    /// </summary>
    /// <param name="newEvent">The new event.</param>
    public void SetEvent(object newEvent)
    {
      lock (this)
      {
        if (m_CurrEvent != null) m_MarkBuffEmpty.Wait(this);
        m_CurrEvent = newEvent;
        m_MarkNewEvent.Notify();
      }
    }

    #endregion public
  }
}