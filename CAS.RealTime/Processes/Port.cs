//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;
using System.Threading;

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Provides access to a queue of IEnvelope messages.
  /// Thread Safety: Instances members of this type are safe for multi-threaded operations.
  /// </summary>
  public sealed class Port
  {
    #region public

    /// <summary>
    /// Gets the number of elements contained in the Port queue.
    /// </summary>
    public int Count
    {
      get
      {
        lock (this)
        {
          return m_NumOfMess;
        }
      }
    }

    /// <summary>
    /// Opens this instance.
    /// </summary>
    public void Open()
    {
      lock (this)
      {
        m_Openned = true;
      }
    }

    /// <summary>
    /// Closes this instance.
    /// </summary>
    public void Close()
    {
      lock (this)
      {
        m_Openned = false;
        m_AtLeastOneMessageInQueue.NotifyAll();
      }
    }

    /// <summary>
    /// Clears this instance.
    /// </summary>
    public void Clear()
    {
      lock (this)
      {
        IEnvelope _messToReturn;
        while (m_NumOfMess != 0)
        {
          _messToReturn = Dequeue();
          _messToReturn.ReturnEmptyEnvelope();
        }
        m_AtLeastOneMessageInQueue.NotifyAll();
      }
    }

    /// <summary>
    /// Sends the message to the 'port'. If there is a process waiting in 'port' it
    /// will be resumed from the 'port' queue. If there is no process, the message will be queued.
    /// </summary>
    /// <param name="mess">Message to be sent</param>
    public void SendMsg(ref IEnvelope mess)
    {
      lock (this)
      {
        Manager.Assert(m_Openned);
        m_Queue.Enqueue(mess);
        mess = null;
        m_NumOfMess++;
        m_AtLeastOneMessageInQueue.Notify();
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
    /// <param name="callingMonitor">TODO: add some description</param>
    /// <returns>
    /// true if the message was received before the specified time elapsed; otherwise, false
    /// </returns>
    public bool WaitMsg(object callingMonitor, out IEnvelope mess, int timeOut)
    {
      bool res = false;
      lock (this)
      {
        Monitor.Exit(callingMonitor);
        mess = null;
        if ((m_NumOfMess == 0) & m_Openned)
          m_AtLeastOneMessageInQueue.Wait(this, timeOut);
        if (m_NumOfMess != 0)
        {
          mess = Dequeue();
          res = true;
        }
      }
      Monitor.Enter(callingMonitor);
      return res;
    }

    #endregion public

    #region private

    private bool m_Openned = true;
    private Queue<IEnvelope> m_Queue = new Queue<IEnvelope>();
    private Condition m_AtLeastOneMessageInQueue = new Condition();
    private int m_NumOfMess = 0;

    private IEnvelope Dequeue()
    {
      Manager.Assert(m_NumOfMess > 0);
      m_NumOfMess--;
      return m_Queue.Dequeue();
    }

    #endregion private
  }
}