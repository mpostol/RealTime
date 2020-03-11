//<summary>
//  Title   : Data bufer to be used by unmanaged platform calls.
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPOstol created.
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>
using System;
namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Data bufer to be used by unmanaged platform calls.
  /// </summary>
  public interface IDBuffer
  {
    /// <summary>
    /// returns pointer tu message in umanaged memory
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
