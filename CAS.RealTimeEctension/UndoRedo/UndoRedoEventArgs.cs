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
      
using System;

namespace CAS.Lib.RTLib.UndoRedo
{

  /// <summary>
  /// Encapsulates the operation record associated with a operation event.
  /// </summary>
  public class UndoRedoEventArgs: EventArgs
  {
    #region Fields
    /// <summary>
    /// The transaction record.
    /// </summary>
    private UndoRedoOperationBase record;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the transaction record associated with the event.
    /// </summary>
    public UndoRedoOperationBase Record
    {
      get { return record; }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Creates a new <see cref="UndoRedoEventArgs"/> object.
    /// </summary>
    /// <param name="record">The transaction record.</param>
    public UndoRedoEventArgs( UndoRedoOperationBase record )
    {
      this.record = record;
    }
    #endregion
  }
}