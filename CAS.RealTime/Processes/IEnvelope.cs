//<summary>
//  Title   : Envelope interface definition - base unit for message exchange mechanism
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MPostol - 12-10-2003 comments were added
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>
namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Interface for envelope management 
  /// (envelope is a kind of packet that is transmitted in the communication or application layer, 
  /// it is base unit for message exchange mechanism).
  /// </summary>
  public interface IEnvelope
  {
    /// <summary>
    /// Used by a user to return an empty envelope to the common pool. It also resets the message content.
    /// </summary>
    void ReturnEmptyEnvelope();
    /// <summary>
    /// Checks if the buffer is in the pool or otherwise is alone and used by a user. 
    /// Used to the state by the governing pool.
    /// </summary>
    bool InPool
    {
      get;
      set;
    }
  }
}
