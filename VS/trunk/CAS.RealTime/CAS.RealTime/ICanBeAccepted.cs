//<summary>
//  Title   : CanBeAccepted Interface
//  System  : Microsoft Visual C# .NET
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    2006: mzbrzezny: created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>


namespace CAS.Lib.RTLib
{
  /// <summary>
  /// This interface is used by internal component to inform 
  /// parent window that it is possible to enable accept button.
  /// </summary>
  public interface ICanBeAccepted
  {
    /// <summary>
    /// It sets the information if accept button in the parent dialog window can be accepted
    /// </summary>
    /// <param name="pOKState">true if it could be accepted</param>
    void CanBeAccepted( bool pOKState );
  }
}
