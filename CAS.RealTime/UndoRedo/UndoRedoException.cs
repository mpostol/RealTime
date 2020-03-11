//<summary>
//  Title   : NetworkConfig.Exceptions
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

namespace UAOOI.ProcessObserver.RealTime.UndoRedo
{
  /// <summary>
  /// Exception class for exceptions occurring in the UndoRedo module.
  /// </summary>
  public class UndoRedoException: ApplicationException
  {
    /// <summary>
    /// Creates a new <see cref="UndoRedoException"/> object.
    /// </summary>
    /// <param name="msg"></param>
    public UndoRedoException( string msg )
      : base( msg )
    {
    }
  }
}
