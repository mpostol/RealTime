//<summary>
//  Title   : Item Access Rights Enum
//  System  : Microsoft Visual C# .NET
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20081006: mzbrzezny: created
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>


namespace CAS.Lib.RTLib
{
  /// <summary>
  /// Item Access Rights enum
  /// similar to: 
  /// EX01-OPCFoundation_NETApi\Source\NET API\Da\Opc.Da.PropertyID.cs
  ///   public enum accessRights : int
  ///{
  ///  readable     = 0x01,
  ///  writable     = 0x02,
  ///  readWritable = 0x03
  ///}
  /// </summary>
  public enum ItemAccessRights: sbyte
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
