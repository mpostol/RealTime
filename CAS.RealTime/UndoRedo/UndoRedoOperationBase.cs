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
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace UAOOI.ProcessObserver.RealTime.UndoRedo
{
  /// <summary>
  /// Base class for UndoRedoOperation data object
  /// </summary>
  /// <remarks>
  /// Responsibility:
  /// - base class for UndoRedoRowOperation and UndoRedoFieldOperation objects
  /// - provides access to undo and redo data comon for all operation types
  /// - 
  /// </remarks>
  public abstract class UndoRedoOperationBase
  {

    #region protected Fields
    /// <summary>
    /// The table on which we are tracking operations.
    /// </summary>
    protected DataTable table;

    /// <summary>
    /// The row in data table on which we are tracking operations.
    /// </summary>
    protected DataRow row;

    /// <summary>
    /// The operation's identifier.
    /// </summary>
    protected int id;

    /// <summary>
    /// The operation type.
    /// </summary>
    protected UndoRedoOperationBaseType operationType;

    /// <summary>
    /// The collection of column values
    /// </summary>
    /// <remarks>Its use only when row is deleted</remarks>
    protected Dictionary<string, object> columnValues;

    /// <summary>
    /// Transaction identifier
    /// </summary>
    /// <remarks>If this operation occurs not in the transaction it is null</remarks>
    protected int? transactionId;

    #endregion

    #region Properties
    /// <summary>
    /// Gets the operation's identifier.
    /// </summary>
    public int ID
    {
      get
      {
        return id;
      }
    }
    /// <summary>
    /// Gets the data table
    /// </summary>
    public DataTable Table
    {
      get
      {
        return table;
      }
    }

    /// <summary>
    /// Gets or sets the data row
    /// </summary>
    public DataRow Row
    {
      get
      {
        return row;
      }
      set
      {
        row = value;
      }
    }

    /// <summary>
    /// Gets the operation type.
    /// </summary>
    public UndoRedoOperationBaseType OperationType
    {
      get
      {
        return operationType;
      }
    }

    /// <summary>
    /// Get the state of the field-value buffer, which indicates that a the collection of field-values
    /// has been populated, thus the row has been deleted at some point.  True if the collection is
    /// populated, false if the collection is empty.
    /// </summary>
    public bool WasDeleted
    {
      get
      {
        return columnValues.Count > 0;
      }
    }
    /// <summary>
    /// Gets or sets the transaction identifier.
    /// </summary>
    /// <remarks>If its not a transaction this identifier is null</remarks>
    public int? TransactionId
    {
      get
      {
        return transactionId;
      }
      set
      {
        transactionId = value;
      }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Common initialization called by constructors.
    /// </summary>
    protected void Initialize()
    {
      columnValues = new Dictionary<string, object>();
    }
    #endregion
  }
}
