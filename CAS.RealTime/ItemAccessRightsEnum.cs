//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.ProcessObserver.RealTime
{
  /// <summary>
  /// Item Access Rights enum similar to:
  /// EX01-OPCFoundation_NETApi\Source\NET API\Da\Opc.Da.PropertyID.cs
  ///
  /// <code>
  /// public enum accessRights : int
  /// {
  ///    readable     = 0x01,
  ///    writable     = 0x02,
  ///    readWritable = 0x03
  /// }
  /// </code>
  ///
  /// </summary>
  public enum ItemAccessRights : sbyte
  {
    /// <summary>
    /// Read and Write
    /// </summary>
    ReadWrite = 0,

    /// <summary>
    /// Read Only
    /// </summary>
    ReadOnly = 1,

    /// <summary>
    /// Write Only
    /// </summary>
    WriteOnly = 2
  }
}