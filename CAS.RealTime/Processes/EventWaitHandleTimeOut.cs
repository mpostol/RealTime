//<summary>
//  Title   : Represents a thread synchronization event.
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    Mpostol: 07-03-2007:
//      WaitOne - poprawilem - zwraca aktualny stan zmiennej stanu.
//    MPostol: 16-02-2007: created
//    <Author> - <date>:
//      <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
using System.Threading;
namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Represents a thread synchronization event.
  /// </summary>
  public class EventWaitHandleTimeOut
  {
    #region private
    private bool m_state;
    #endregion private
    #region public
    /// <summary>
    /// Returns state of the EventWaitHandleTimeOut
    /// </summary>
    public bool State { get { return m_state; } }
    /// <summary>
    /// Sets the state of the event to nonsignaled, causing threads to block.
    /// </summary>
    /// <returns>true if the operation succeeds; otherwise, false.</returns>
    public void Reset() { m_state = false; }
    /// <summary>
    /// Sets the state of the event to signaled, allowing one or more waiting threads to proceed.
    /// </summary>
    public void Set()
    {
      lock ( this )
      {
        m_state = true;
        Monitor.PulseAll( this );
      }
    }
    /// <summary>
    ///  Blocks the current thread until the current EventWaitHandleTimeOut receives a signal. 
    /// </summary>
    /// <param name="millisecondsTimeout">The number of milliseconds to wait for the signal. </param>
    /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
    public virtual bool WaitOne( int millisecondsTimeout )
    {
      lock ( this )
        if ( ( !m_state ) && ( !Monitor.Wait( this, millisecondsTimeout ) ) )
          return m_state;
      return true;
    }
    #endregion public
    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="EventWaitHandleTimeOut"/> class.
    /// </summary>
    /// <param name="state">if set to <c>false</c> the state of the event is nonsignaled, causing threads to block..</param>
    public EventWaitHandleTimeOut( bool state )
    {
      m_state = state;
    }
    #endregion
  }
}
