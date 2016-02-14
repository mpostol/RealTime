//<summary>
//  Title   : Facade class to test WatchDog
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.RTLib.Processes;
using System;
using System.Threading;

namespace CAS.Lib.RTLibCom.Tests
{
  /// <summary>
  /// Facade class to test WatchDog
  /// </summary>
  [Watchdog( "FacadeWatchDog", 30 )]
  public class FacadeWatchDog: ContextBoundObject
  {
    /// <summary>
    /// Long method
    /// </summary>
    /// <param name="par">The par.</param>
    /// <param name="secondParameter">The second parameter.</param>
    public void LongMethod( string par, int secondParameter )
    {
      Thread.Sleep( TimeSpan.FromSeconds( 120 ) );
    }
    /// <summary>
    /// Short method.
    /// </summary>
    /// <param name="par">The par.</param>
    public void ShortMethod( string par )
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="FacadeWatchDog"/> class.
    /// </summary>
    /// <param name="watchdogName">Name of the watchdog.</param>
    /// <param name="secondParameter">The second parameter.</param>
    public FacadeWatchDog( string watchdogName, int secondParameter )
    {
    }
  }
  /// <summary>
  /// Class that has no context
  /// </summary>
  public class NotContexClass
  {
    /// <summary>
    /// Short method.
    /// </summary>
    public void ShortMethod()
    {
    }
  }
}
