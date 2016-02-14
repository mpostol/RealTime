//<summary>
//  Title   : NetworkConfig.UndoRedo
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    <Author> Tomek Siwecki - 26.12.2006 - Created <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>

namespace CAS.Lib.RTLib.UndoRedo
{
  /// <summary>
  /// Operation types
  /// </summary>
  public enum UndoRedoOperationBaseType
  {
    /// <summary>
    /// Adding new row
    /// </summary>
    NewRow,
    /// <summary>
    /// Deleting a row
    /// </summary>
    DeleteRow,
    /// <summary>
    /// Changing fields
    /// </summary>
    ChangeField
  }
}
