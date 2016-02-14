//<summary>
//  Title   : Port
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
using System;
using System.Collections;
using System.Threading;
using CAS.Lib.RTLib.Processes;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Provides access to a queue of IEnvelope messages.
  /// Thread Safety:
  /// Instances members of this type are safe for multi-threaded operations. 
  /// </summary>
  public sealed class Port
  {
    #region PUBLIC
    /// <summary>
    /// Gets the number of elements contained in the Port queue.
    /// </summary>
    public int Count
    {
      get
      {
        lock(this)
        {
          return numOfMess;
        }
      }
    }
    /// <summary>
    /// Opens this instance.
    /// </summary>
    public void Open()
    {
      lock(this)
      {
        openned = true;
      }
    }
    /// <summary>
    /// Closes this instance.
    /// </summary>
    public void Close()
    {
      lock(this)
      {
        openned = false;
        atLeastOneMessageInQueue.NotifyAll();
      }
    }
    /// <summary>
    /// Clears this instance.
    /// </summary>
    public void Clear()
    {
      lock(this)
      {
        IEnvelope messToReturn;
        while (numOfMess != 0)
        {
          messToReturn = Dequeue();
          messToReturn.ReturnEmptyEnvelope();
        }
        atLeastOneMessageInQueue.NotifyAll();
      }
    }
    /// <summary>
    /// Sends the message to the 'port'. If there is a process waiting in 'port' it 
    /// will be resumed from the 'port' queue. If there is no process, the message 
    /// will be queued.
    /// </summary>
    /// <param name="mess">Message to be sent</param>
    public void SendMsg(ref IEnvelope mess)
    {
      lock(this)
      {
        Manager.Assert(openned);
        myQueue.Enqueue(mess);
        mess = null;
        numOfMess++;
        atLeastOneMessageInQueue.Notify();
      }
    }
    /// <summary>
    /// Receive message from 'port'. If there is no message in the 'port', the calling 
    /// thread will be blocked until it receives a message or a specified amount of 
    /// time elapses.
    /// </summary>
    /// <param name="mess">UMessage removed from the beginning of the port Queue</param>
    /// <param name="timeOut">The number of milliseconds to wait before this method returns. 0 means wait forever.
    /// </param>
    /// <param name="callingMonitor">TODO: add some descriptio</param>
    /// <returns>
    /// true if the message was received before the specified time elapsed; otherwise, false
    /// </returns>
    public bool WaitMsg(object callingMonitor, out IEnvelope mess, int timeOut)
    {
      bool res = false;
      lock(this)
      {
        Monitor.Exit(callingMonitor);
        mess = null;
		if ((numOfMess==0) & openned)
		{
		  atLeastOneMessageInQueue.Wait(this, timeOut);
		}
        if (numOfMess != 0) 
        {
          mess = Dequeue();
          res = true;
        }
      }
	  Monitor.Enter(callingMonitor);
	  return res;
    }
    #endregion
    #region PRIVATE
    private bool openned = true;
    private Queue myQueue = new Queue();
    private Condition atLeastOneMessageInQueue = new Condition();
    private int numOfMess = 0;
    private IEnvelope Dequeue()
    {
      Manager.Assert(numOfMess > 0);
      numOfMess--;
      return (IEnvelope) myQueue.Dequeue();
    }
    #endregion
  }
}
