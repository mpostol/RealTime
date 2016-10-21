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
using System.Data;

namespace CAS.Lib.RTLib.UndoRedo
{
  /// <summary>
  /// Undo or redo operation data used when changing fields.
  /// </summary>
  public sealed class UndoRedoFieldOperation: UndoRedoOperationBase
  {
    #region Fields
    /// <summary>
    /// The column name.
    /// </summary>
    private string columnName;

    /// <summary>
    /// The old value
    /// </summary>
    private object oldValue;

    /// <summary>
    /// The new column value
    /// </summary>
    private object newValue;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the new field value.
    /// </summary>
    public object NewValue
    {
      get
      {
        return newValue;
      }
      set
      {
        newValue = value;
      }
    }

    /// <summary>
    /// Gets the old field value. 
    /// </summary>
    public object OldValue
    {
      get
      {
        return oldValue;
      }
    }

    /// <summary>
    /// Gets the name of the column associated with this operation.  
    /// </summary>
    public string ColumnName
    {
      get
      {
        return columnName;
      }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new <see cref="UndoRedoFieldOperation"/> object.
    /// </summary>
    /// <param name="id">The transaction number.  This can be any integer value.</param>
    /// <param name="dt">The data table.</param>
    /// <param name="row">The row being added/deleted.</param>
    /// <param name="columnName">The column name of the field being changed.</param>
    /// <param name="oldValue">The old field's value.</param>
    /// <param name="newValue">The new field's value.</param>
    /// <exception cref="ArgumentNullException">If the data table, data row or column name is null</exception>
    public UndoRedoFieldOperation( int id, DataTable dt, DataRow row, string columnName, object oldValue, object newValue )
    {
      if ( columnName == null )
      {
        throw new ArgumentNullException( "Column name cannot be null." );
      }

      if ( row == null )
      {
        throw new ArgumentNullException( "DataRow cannot be null." );
      }

      if ( dt == null )
      {
        throw new ArgumentNullException( "Data table cannot be null." );
      }

      this.id = id;
      this.table = dt;
      this.row = row;
      this.operationType = UndoRedoOperationBaseType.ChangeField;
      this.columnName = columnName;
      this.oldValue = oldValue;
      this.newValue = newValue;
      Initialize();
    }
    #endregion
  }
}
