//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Interface for envelope management - the envelope is a kind of packet that is transmitted in the communication or application layer,
  /// it is base unit for message exchange mechanism.
  /// </summary>
  public interface IEnvelope
  {
    /// <summary>
    /// Used by a user to return an empty envelope to the common pool. It also resets the message content.
    /// </summary>
    void ReturnEmptyEnvelope();

    /// <summary>
    /// Checks if the <see cref="IEnvelope"/> is in the pool or otherwise is alone and used by a user.
    /// Used to the state by the governing pool.
    /// </summary>
    bool InPool
    {
      get;
      set;
    }
  }
}