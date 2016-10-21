//<summary>
//  Title   : General class helper to allow easy event driven monitoring of connection state 
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Diagnostics;
using System.Threading;

namespace CAS.Lib.RTLib.Management
{

  /// <summary>
  /// Summary description for ConnectionState.
  /// </summary>
  public class ConnectionState
  {
    #region PRIVATE
    private Stopwatch m_ActiveTimeStopWatch = new Stopwatch();
    private States m_State = States.ReadyToBeConn;
    private void Thread( object mCurrState )
    {
      if ( MarkNewState != null )
        MarkNewState( (States)mCurrState );
    }
    #endregion PRIVATE
    /// <summary>
    /// enum that provide available states
    /// </summary>
    public enum States
    {
      /// <summary>
      /// object is in connected state
      /// </summary>
      Connected=0,
      /// <summary>
      /// object is in disconnected state
      /// </summary>
      Disconnected=1,
      /// <summary>
      /// object is in ready to be connected
      /// </summary>
      ReadyToBeConn=2,
      /// <summary>
      /// object is in connected state - but some errors occurs
      /// </summary>
      ConnectedButErrorsOccours=3
    }
    /// <summary>
    /// function that should be called in case of state change
    /// </summary>
    public delegate void StateChanged( States currState );
    /// <summary>
    /// event that is used to inform process that state changed
    /// </summary>
    public event StateChanged MarkNewState;
    /// <summary>
    /// properties that is used to change the state
    /// </summary>
    public virtual States CurrState
    {
      set
      {
        if ( m_State == value )
          return;
        m_State = value;
        if ( value == States.Connected || value == States.ConnectedButErrorsOccours )
        {
          m_ActiveTimeStopWatch.Start();
        }
        else
          m_ActiveTimeStopWatch.Stop();
        if ( MarkNewState != null )
          ThreadPool.QueueUserWorkItem( new WaitCallback( Thread ), value );
      }
      get { return m_State; }
    }
    /// <summary>
    /// properties that gives access to the activ time of this object
    /// </summary>
    public ulong ActivTime
    { get { return System.Convert.ToUInt64( Math.Min( ulong.MaxValue, m_ActiveTimeStopWatch.Elapsed.TotalSeconds ) ); } }
    /// <summary>
    /// Connection state constructor
    /// </summary>
    public ConnectionState() { }
  }
}