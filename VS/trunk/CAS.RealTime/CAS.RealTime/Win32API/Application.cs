//<summary>
//  Title   : Application
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>

using System;

namespace CAS.Lib.RTLib.Win32API
{
  /// <summary>
  /// Class that provides common information about Application - path, etc...
  /// </summary>
  [Obsolete("Should be removed it duplicates the .NET Framework")]
  public static class Application
  {
    /// <summary>
    /// gives simple access to application path 
    /// </summary>
    public static string Path
    {
      get
      {
        return AppDomain.CurrentDomain.BaseDirectory;
      }
    }
  }
}
