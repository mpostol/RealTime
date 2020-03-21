//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Data buffer to be used by unmanaged platform calls.
  /// </summary>
  public interface IDBuffer
  {
    /// <summary>
    /// returns pointer to message in unmanaged memory
    /// </summary>
    IntPtr uMessagePtr
    {
      get;
    }

    /// <summary>
    /// capacity of the data buffer
    /// </summary>
    /// <returns>length in bytes</returns>
    ushort userBuffLength
    {
      get;
    }

    /// <summary>
    /// user data length
    /// </summary>
    /// <returns>length in bytes</returns>
    ushort userDataLength
    {
      get;
    }

    /// <summary>
    ///  Copies data from unmanaged memory pointer to IDBuffer starting
    ///  at offset 0 and assigns userDataLength.
    /// </summary>
    /// <param name="source">The memory pointer to copy from.</param>
    /// <param name="length">The number of bytes to copy.</param>
    void CopyToBuffor(IntPtr source, uint length);

    /// <summary>
    ///  Copies data from IDBuffer to unmanaged memory pointer starting
    ///  at offset 0.
    /// </summary>
    /// <param name="destination">The memory pointer to copy to.</param>
    void CopyFromBuffor(IntPtr destination);
  }
}