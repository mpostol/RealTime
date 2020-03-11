//<summary>
//  Title   : Condition
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>

using System.Threading;
using System;

namespace UAOOI.ProcessObserver.RealTime.Processes
{

  /// <summary>
  /// The condition concept offers a way of the synchronization. Each condition is 
  /// associated with an important event (condition); hence the appearance of a signal 
  /// is meant as appearance of the associated event, which manifests in establishing 
  /// the relevant condition. Generally, three kinds of operations on signals are 
  /// defined, namely, wait - to suspend process until the associated signal will be 
  /// sent, Notify - to awake one of the waiting processes, and IsAwaiting - to check 
  /// if any process is waiting for the specified signal.
  /// </summary>
  public sealed class Condition
  {
    private ushort awitingCount = 0;
    /// <summary>
    /// Notifies this instance.
    /// use Notify - to awake one of the waiting processes
    /// </summary>
    public void Notify()
    {
      lock ( this )
      {
        Monitor.Pulse( this );
      }
    }
    /// <summary>
    /// Notifies all.
    /// use NotifyAll - to awake all of the waiting processes
    /// </summary>
    public void NotifyAll()
    {
      lock ( this )
      {
        Monitor.PulseAll( this );
      }
    }
    /// <summary>
    /// Wykonuje Wait
    /// </summary>
    /// <remarks>Mo¿e byæ wykonana tylko wewnatrz instrukcji lock </remarks>
    /// <param name="callingMonitor">zwalnia wskazany monitor</param>
    public void Wait( object callingMonitor )
    {
      lock ( this )
      {
        Monitor.Exit( callingMonitor );
        awitingCount++;
        Monitor.Wait( this );
        awitingCount--;
      }
      Monitor.Enter( callingMonitor );
    }
    /// <summary>
    /// Waits the specified calling monitor.
    /// Wykonuje Wait z uwzglednieniem timeout'u
    /// </summary>
    /// <remarks>Mo¿e byæ wykonana tylko wewnatrz instrukcji lock </remarks>
    /// <param name="callingMonitor">The calling monitor.</param>
    /// <param name="TimeOut">The time out.</param>
    /// <returns></returns>
    public bool Wait( object callingMonitor, int TimeOut )
    {
      bool res = false;
      lock ( this )
      {
        Monitor.Exit( callingMonitor );
        awitingCount++;
        res = Monitor.Wait( this, TimeOut );
        awitingCount--;
      }
      Monitor.Enter( callingMonitor );
      return res;
    }
    /// <summary>
    /// Releases the lock on a monitor and blocks the current thread until 
    /// it receives Notification / Signal or a specified amount of time elapses.
    /// </summary>
    /// <param name="callingMonitor">The monitor which to releases the lock on.</param>
    /// <param name="TimeOut">The number of milliseconds to wait before this method returns. </param>
    /// <returns>true if the lock was reacquired before the specified time elapsed; otherwise, false. 
    /// </returns>
    public bool Wait( object callingMonitor, TimeSpan TimeOut )
    {
      bool res = false;
      lock ( this )
      {
        Monitor.Exit( callingMonitor );
        awitingCount++;
        res = Monitor.Wait( this, TimeOut );
        awitingCount--;
      }
      Monitor.Enter( callingMonitor );
      return res;
    }
    /// <summary>
    /// Determines whether this instance is awaiting.
    /// checks if any process is waiting for the specified signal
    /// </summary>
    /// <returns>
    /// 	<c>true</c> if this instance is awaiting; otherwise, <c>false</c>.
    /// </returns>
    public bool IsAwaiting()
    {
      return awitingCount > 0;
    }
  }
}
