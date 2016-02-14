//<summary>
//  Title   : Trace listener to be used for writing logs
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2009, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using CAS.Lib.CodeProtect;

namespace CAS.Lib.RTLib.Diagnostics
{
  internal class AdvancedTraceListenerHelper
  {
    #region private
    private const string specialFormatToken = "|{0}|";
    private const string environmentVariablePattern = "\\%{0}\\%";
    private const string applicationDataPathText = "ApplicationDataPath"; //path to application data folder

    /// <summary>
    /// Exception in advanced trace listener
    /// </summary>
    [Serializable]
    private class AdvancedTraceListenerException: Exception
    {
      internal AdvancedTraceListenerException( string message ) : base( message ) { }
    }
    /// <summary>
    /// Prepares the name of the log file.
    /// </summary>
    /// <param name="logFileName">Name of the log file.</param>
    /// <returns>Ready log filename</returns>
    private static string PrepareLogFileName( string logFileName )
    {
      if ( logFileName.Contains( "|ApplicationDataPath|" ) )
      {
        logFileName = logFileName.Replace( string.Format( specialFormatToken, applicationDataPathText ), InstallContextNames.ApplicationDataPath );
        return logFileName;
      }
      else
      {
        foreach ( Environment.SpecialFolder folder in Enum.GetValues( typeof( Environment.SpecialFolder ) ) )
        {
          if ( !logFileName.Contains( "|" ) )
            break;
          logFileName = logFileName.Replace( string.Format( specialFormatToken, folder.ToString() ), Environment.GetFolderPath( folder ) );
        }
        foreach ( DictionaryEntry variable in Environment.GetEnvironmentVariables() )
        {
          if ( !logFileName.Contains( "%" ) )
            break;
          logFileName = Regex.Replace( logFileName, string.Format( environmentVariablePattern, (string)variable.Key ), (string)variable.Value, RegexOptions.IgnoreCase );
        }
        return logFileName;
      }
    }
    #endregion private

    # region internal
    /// <summary>
    /// Prepares the and update log filename.
    /// </summary>
    /// <param name="textWriterTraceListener">The text writer trace listener.</param>
    /// <param name="logFileName">Name of the log file.</param>
    internal static void PrepareAndUpdateLogFilename( TextWriterTraceListener textWriterTraceListener, string logFileName )
    {
      try
      {
        string org_logFileName = logFileName;
        logFileName = AdvancedTraceListenerHelper.PrepareLogFileName( logFileName );
        if ( org_logFileName != logFileName )
        {
          FieldInfo fi = typeof( TextWriterTraceListener ).GetField( "fileName", BindingFlags.NonPublic | BindingFlags.Instance );
          if ( fi != null )
            fi.SetValue( textWriterTraceListener, logFileName );
          else
            throw new AdvancedTraceListenerException( "Current .NET framework is not supported." );
        }
      }
      catch ( AdvancedTraceListenerException )
      {
        throw ;
      }
      catch ( Exception ) { }
    }
    #endregion internal
  }
  /// <summary>
  /// Advanced delimited trace listener which derives from DelimitedListTraceListener and prepares the log file name
  /// </summary>
  public class AdvancedDelimitedListTraceListener: DelimitedListTraceListener
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvancedDelimitedListTraceListener"/> class.
    /// </summary>
    /// <param name="logFileName">Name of the log file.</param>
    public AdvancedDelimitedListTraceListener( string logFileName )
      : base( logFileName )
    {
      AdvancedTraceListenerHelper.PrepareAndUpdateLogFilename( this, logFileName );
    }
  }
  /// <summary>
  /// Advanced delimited XML writer trace listener which derives from XmlWriterTraceListener and prepares the log file name
  /// </summary>
  public class AdvancedXmlWriterTraceListener: XmlWriterTraceListener
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvancedXmlWriterTraceListener"/> class.
    /// </summary>
    /// <param name="logFileName">Name of the log file.</param>
    public AdvancedXmlWriterTraceListener( string logFileName )
      : base( logFileName )
    {
      AdvancedTraceListenerHelper.PrepareAndUpdateLogFilename( this, logFileName );
    }
  }
}
