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
  /// Undo or redo operation data used when adding or deleting rows
  /// </summary>
  public sealed class UndoRedoRowOperation: UndoRedoOperationBase
  {

    #region Constructors
    /// <summary>
    /// Creates a new <see cref="UndoRedoRowOperation"/> object.
    /// </summary>
    /// <param name="id">The associated operation number. Can be any integer value.</param>
    /// <param name="dt">The data table.</param>
    /// <param name="row">The row being added/deleted.</param>
    /// <param name="operationType">The operation type.</param>
    /// <exception cref="UndoRedoException">If the transaction is ChangeField</exception>
    /// <exception cref="ArgumentNullException">If the data table or data row is null</exception>
    public UndoRedoRowOperation( int id, DataTable dt, DataRow row, UndoRedoOperationBaseType operationType )
    {
      if ( operationType == UndoRedoOperationBaseType.ChangeField )
      {
        throw new UndoRedoException( "You cannot use this constructor when changing field" );
      }

      if ( row == null )
      {
        throw new ArgumentNullException( "DataRow cannot be null." );
      }

      if ( dt == null )
      {
        throw new ArgumentNullException( "DataRow cannot be null." );
      }

      this.id = id;
      this.table = dt;
      this.row = row;
      this.operationType = operationType;
      Initialize();
    }
    #endregion

    #region public
    /// <summary>
    /// Adds the value associated with a field to the internal field-value collection.
    /// </summary>
    /// <param name="columnName">The column name.</param>
    /// <param name="val">The associated value.</param>
    /// <exception cref="ArgumentNullException">If the column name is null</exception>
    public void AddColumnNameValuePair( string columnName, object val )
    {
      if ( columnName == null )
      {
        throw new ArgumentNullException( "Column name cannot be null." );
      }
      columnValues.Add( columnName, val );
    }
    /// <summary>
    /// Gets the value from the field-value collection for the specified field. 
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If the column name is null</exception>
    public object GetValue( string columnName )
    {
      if ( columnName == null )
      {
        throw new ArgumentNullException( "Column name cannot be null." );
      }

      return columnValues[ columnName ];
    }
    #endregion

  }
}
