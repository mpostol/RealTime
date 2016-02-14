﻿//<summary>
//  Title   : RelativeFilePathsCalculator - This class is reposnsible for operation on path, relative path, absolute path etc...
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

using System;
using System.IO;
using System.Text;
using CAS.Lib.CodeProtect;

namespace CAS.Lib.RTLib.Utils
{
  /// <summary>
  /// This class is responsible for operation on path, relative path, absolute path etc...
  /// </summary>
  public class RelativeFilePathsCalculator
  {
    /// <summary>
    /// Tests if path is absolute.
    /// </summary>
    /// <param name="PathToBeTested">The path to be tested.</param>
    /// <returns>true if path is absolute</returns>
    public static bool TestIfPathIsAbsolute( string PathToBeTested )
    {
      if ( PathToBeTested.Contains( "\\\\" ) || PathToBeTested.Contains( ":\\" ) )
        return true;
      else
        return false;
    }
    /// <summary>
    /// Tries the compute relative path.
    /// </summary>
    /// <param name="BaseAbsolutePath">The base absolute path.</param>
    /// <param name="RelativeToBeComputed">The relative to be computed.</param>
    /// <returns>relative path if it was possible</returns>
    public static string TryComputeRelativePath( string BaseAbsolutePath, string RelativeToBeComputed )
    {
      if ( String.IsNullOrEmpty( RelativeToBeComputed ) )
        return RelativeToBeComputed;
      string[] absoluteDirectories = BaseAbsolutePath.Split( '\\' );
      string[] relativeDirectories = RelativeToBeComputed.Split( '\\' );
      if ( !TestIfPathIsAbsolute( RelativeToBeComputed ) )
        return RelativeToBeComputed;
      if ( !TestIfPathIsAbsolute( BaseAbsolutePath ) )
        throw new ArgumentException( "BaseAbsolutePath must be absolute path" );
      //Get the shortest of the two paths
      int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

      //Use to determine where in the loop we exited
      int lastCommonRoot = -1;
      int index;

      //Find common root
      for ( index = 0; index < length; index++ )
        if ( absoluteDirectories[ index ] == relativeDirectories[ index ] )
          lastCommonRoot = index;
        else
          break;

      //If we didn't find a common prefix then throw
      if ( lastCommonRoot == -1 )
        return RelativeToBeComputed;

      //Build up the relative path
      StringBuilder relativePath = new StringBuilder();

      //Add on the ..
      for ( index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++ )
        if ( absoluteDirectories[ index ].Length > 0 )
          relativePath.Append( "..\\" );

      //Add on the folders
      for ( index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++ )
        relativePath.Append( relativeDirectories[ index ] + "\\" );
      relativePath.Append( relativeDirectories[ relativeDirectories.Length - 1 ] );

      return relativePath.ToString();
    }
    /// <summary>
    /// Gets the absolute path to file in application data folder.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static FileInfo GetAbsolutePathToFileInApplicationDataFolder( string filePath )
    {
      bool isAbsolute = TestIfPathIsAbsolute( filePath );
      if ( isAbsolute )
        return new FileInfo( filePath );
      else
        return new FileInfo( Path.Combine( InstallContextNames.ApplicationDataPath, filePath ) );
    }
  }
}
