//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections;
using System.Diagnostics;

namespace UAOOI.ProcessObserver.RealTime.Processes
{

  /// <summary>
  /// The EnvelopePool class manages a pool of IEnvelope objects.
  /// Thread Safety:
  /// Instances members of this type are safe for multi-threaded operations.
  /// </summary>
  public class EnvelopePool
  {
    #region PRIVATE

    private Queue m_Queue = new Queue();
    private int m_NumOfEnvInPool = 0;
    private ulong m_NumOfEnvCreated = 0;
    private readonly CreateEnvelope m_NewEnvelope;

    #endregion PRIVATE

    #region PUBLIC

    /// <summary>
    /// Delegate used to create new envelope. New envelope is created each time
    /// GetEmptyEnvelope is called while the pool is empty.
    /// </summary>
    public delegate IEnvelope CreateEnvelope(EnvelopePool source);

    /// <summary>
    /// It gets an empty envelope from the common pool, or if empty creates ones.
    /// </summary>
    public IEnvelope GetEmptyEnvelope()
    {
      lock (this)
      {
        IEnvelope currEnv;
        if (m_NumOfEnvInPool == 0)
        {
          m_NumOfEnvCreated++;
          currEnv = m_NewEnvelope(this);
        }
        else
        {
          m_NumOfEnvInPool--;
          currEnv = (IEnvelope)m_Queue.Dequeue();
          Debug.Assert(currEnv.InPool, "GetEmptyEnvelope: current envelop is not in pool");
        }
        currEnv.InPool = false;
        return currEnv;
      }
    }

    /// <summary>
    /// Returns an empty envelope to the common pool.
    /// </summary>
    /// <param name="mess">Envelope to return</param>
    public void ReturnEmptyEnvelope(ref IEnvelope mess)
    {
      lock (this)
      {
        Debug.Assert(!mess.InPool, "GetEmptyEnvelope: current envelop is not in pool");
        mess.InPool = true;
        m_Queue.Enqueue(mess);
        m_NumOfEnvInPool++;
      }
    }

    /// <summary>
    /// Creates instance of EnvelopePool
    /// </summary>
    /// <param name="userCreator">Is used to create new <see cref="IEnvelope"/>each time
    /// GetEmptyEnvelope is called while the pool is empty.</param>
    public EnvelopePool(CreateEnvelope userCreator)
    {
      m_NewEnvelope = userCreator;
    }

    #endregion PUBLIC
  }
}