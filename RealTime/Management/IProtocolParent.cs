//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.ProcessObserver.RealTime.Management
{
  /// <summary>
  /// Statistical information about the communication performance
  /// </summary>
  [Obsolete("Must be moved to PR37-CAS_LIB_PCKG")]
  public interface IProtocolParent
  {
    /// <summary>
    /// Incrementing number of CRC errors.
    /// </summary>
    void IncStRxCRCErrorCounter();
    /// <summary>
    /// Incrementing number of incomplete frames.
    /// </summary>
    void IncStRxFragmentedCounter();
    /// <summary>
    /// Incrementing number of complete received frames.
    /// </summary>
    void IncStRxFrameCounter();
    /// <summary>
    /// Incrementing number of invalid frames.
    /// </summary>
    void IncStRxInvalid();
    /// <summary>
    /// Incrementing number of received NAK (negative acknowledge).
    /// </summary>
    void IncStRxNAKCounter();
    /// <summary>
    /// Incrementing number of timeouts.
    /// </summary>
    void IncStRxNoResponseCounter();
    /// <summary>
    /// Incrementing number of synchronization errors.
    /// </summary>
    void IncStRxSynchError();
    /// <summary>
    /// Incrementing number of successfully wrote operations.
    /// </summary>
    void IncStTxACKCounter();
    /// <summary>
    /// Incrementing number of sent bytes.
    /// </summary>
    void IncStTxDATACounter();
    /// <summary>
    /// Incrementing number of successfully sent frames.
    /// </summary>
    void IncStTxFrameCounter();
    /// <summary>
    /// Incrementing number of received NAK (negative acknowledge).
    /// </summary>
    void IncStTxNAKCounter();
    /// <summary>
    /// Incrementing number of received data blocks.
    /// </summary>
    /// <param name="succ">true - if received frame is good, false otherwise </param>
    void RxDataBlock( bool succ );
    /// <summary>
    /// Incrementing number of transmuted data blocks.
    /// </summary>
    /// <param name="succ">true - if frame is transmitted successfully, false otherwise </param>
    void TxDataBlock( bool succ );
    /// <summary>
    /// Establishing the maximal time of response.
    /// </summary>
    /// <param name="val">value of last measurement</param>
    void TimeMaxResponseDelayAdd( long val );
    /// <summary>
    /// Establishing the maximal time between characters in response.
    /// </summary>
    /// <param name="val">value of last measurement</param>
    void TimeCharGapAdd( long val );
  }
}
