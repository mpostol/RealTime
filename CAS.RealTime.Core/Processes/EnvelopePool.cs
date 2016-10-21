//<summary>
//  Title   : EnvelopePool
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

namespace CAS.Lib.RTLib.Processes
{
  using System;
  using System.Collections;
  /// <summary>
  /// The EnvelopePool class manages a pool of IEnvelope objects.
  /// Thread Safety:
  /// Instances members of this type are safe for multi-threaded operations. 
  /// </summary>
  public class EnvelopePool
  {
    #region PRIVATE
    private Queue myQueue = new Queue();
    private int numOfEnvInPool = 0;
    private ulong numOfEnvCreated = 0;
    private CreateEnvelope newEnvelope;
    #endregion
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
        if (numOfEnvInPool == 0)
        {
          numOfEnvCreated++;
          currEnv = newEnvelope(this);
        }
        else
        {
          numOfEnvInPool--;
          currEnv = (IEnvelope)myQueue.Dequeue();
          System.Diagnostics.Debug.Assert(currEnv.InPool, "GetEmptyEnvelope: current envelop is not in pool");
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
        System.Diagnostics.Debug.Assert(!mess.InPool, "GetEmptyEnvelope: current envelop is not in pool");
        mess.InPool = true;
        myQueue.Enqueue(mess);
        numOfEnvInPool++;
      }
    }
    /// <summary>
    /// Creates instance of EnvelopePool
    /// </summary>
    /// <param name="userCreator">Is used to create new <see cref="IEnvelope"/>each time
    /// GetEmptyEnvelope is called while the pool is empty.</param>
    public EnvelopePool(CreateEnvelope userCreator)
    {
      newEnvelope = userCreator;
    }
    #endregion
  }
}
