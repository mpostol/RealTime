//<summary>
//  Title   : Handling engine for WaitTimeList
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    Mariusz Postó³ - 28-09-2003: created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47 
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Diagnostics;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Summary description for HandlerWaitTimeList.
  /// This is is able to execute handler functions that belongs to objects that oare removed form Wait Time List  
  /// </summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  public abstract class HandlerWaitTimeList<TElement>: WaitTimeList<TElement> 
    where TElement: WaitTimeList<TElement>.TODescriptor
  {
    #region PRIVATE
    private readonly bool myAutoreset;
    private string m_Source;
    private bool m_busy = false;
    /// <summary>
    /// The method removes first ready item from the queue and schedules the work using <see cref="System.Threading.ThreadPool"/>.
    /// The method is called by the internal timer callback inside the lock of the instance of this class.
    /// </summary>
    internal protected override void RemoveItem()
    {
      if ( m_busy )
        return;
      m_busy = true;
      System.Threading.ThreadPool.QueueUserWorkItem( new System.Threading.WaitCallback( QueueHandler ) );
    }
    /// <summary>
    /// The main thread that executes handlers.
    /// </summary>
    private void QueueHandler( object parameters )
    {
      try
      {
        TElement myDsc = this.RemoveItem( myAutoreset );
        if ( myDsc == null )
          return;
        Handler( myDsc );
      }
      catch ( Exception ex )
      {
        string message = String.Format( CAS.Lib.RTLib.Properties.Resources.ExceptionTraceFormat, ex.Message, ex.Source, ex.StackTrace );
        CAS.Lib.RTLib.Processes.EventLogMonitor.WriteToEventLogError( message, 42 );
      }
      finally { m_busy = false; }
    }
    #endregion
    #region PUBLIC
    /// <summary>
    /// If overridden in the derived class can be used 
    /// to handle the work related to the <paramref name="myDsc"/>. 
    /// Only one call is guaranteed at any instant of time. 
    /// This method is executed by a separate thread.
    /// </summary>
    /// <param name="myDsc">descriptor handler</param>
    protected abstract void Handler( TElement myDsc );
    /// <summary>
    /// Creates HandlerWaitTimeList
    /// </summary>
    /// <param name="autoreset">true if after removing reset is to be applied</param>
    /// <param name="handlerThreadName">Name of the handler for debug purpose</param>
    /// <param name="waightedPriority">if set to <c>true</c> weighted priority algorithm is enabled.</param>
    public HandlerWaitTimeList( bool autoreset, string handlerThreadName, bool waightedPriority )
      : base( handlerThreadName, waightedPriority )
    {
      m_Source = handlerThreadName;
      myAutoreset = autoreset;
    }
    /// <summary>
    /// Creates HandlerWaitTimeList
    /// </summary>
    /// <param name="autoreset">true if after removing reset is to be applied</param>
    /// <param name="handlerThreadName">Name of the handler for debug purpose</param>
    public HandlerWaitTimeList( bool autoreset, string handlerThreadName )
      : this( autoreset, handlerThreadName, true )
    { }
    #endregion
  }
}
