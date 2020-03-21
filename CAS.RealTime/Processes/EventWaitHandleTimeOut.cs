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
  /// Represents a thread synchronization event.
  /// </summary>
  public class EventWaitHandleTimeOut
  {
    #region public

    /// <summary>
    /// Returns state of the EventWaitHandleTimeOut
    /// </summary>
    public bool State { get; private set; }

    /// <summary>
    /// Sets the state of the event to not signaled, causing threads to block.
    /// </summary>
    /// <returns><c>true</c> if the operation succeeds; otherwise, false.</returns>
    public void Reset() { State = false; }

    /// <summary>
    /// Sets the state of the event to signaled, allowing one or more waiting threads to proceed.
    /// </summary>
    public void Set()
    {
      lock (this)
      {
        State = true;
        Monitor.PulseAll(this);
      }
    }

    /// <summary>
    ///  Blocks the current thread until the current EventWaitHandleTimeOut receives a signal.
    /// </summary>
    /// <param name="millisecondsTimeout">The number of milliseconds to wait for the signal. </param>
    /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
    public virtual bool WaitOne(int millisecondsTimeout)
    {
      lock (this)
        if ((!State) && (!Monitor.Wait(this, millisecondsTimeout)))
          return State;
      return true;
    }

    #endregion public

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="EventWaitHandleTimeOut"/> class.
    /// </summary>
    /// <param name="state">if set to <c>false</c> the state of the event is non-signaled, causing threads to block..</param>
    public EventWaitHandleTimeOut(bool state)
    {
      State = state;
    }

    #endregion constructor
  }
}