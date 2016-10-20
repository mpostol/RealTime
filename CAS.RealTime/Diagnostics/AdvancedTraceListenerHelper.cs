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

using CAS.Lib.CodeProtect;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CAS.Lib.RTLib.Diagnostics
{
  internal static class AdvancedTraceListenerHelper
  {
    #region private
    private const string specialFormatToken = "|{0}|";
    private const string environmentVariablePattern = "\\%{0}\\%";
    private const string applicationDataPathText = "|ApplicationDataPath|"; //path to application data folder

    /// <summary>
    /// Prepares the name of the log file.
    /// </summary>
    /// <param name="logFileName">Name of the log file.</param>
    /// <returns>Ready log filename</returns>
    internal static string PrepareLogFileName(this string logFileName)
    {
      if (logFileName.Contains(applicationDataPathText))
      {
        logFileName = logFileName.Replace(applicationDataPathText, InstallContextNames.ApplicationDataPath);
        return logFileName;
      }
      else
      {
        foreach (Environment.SpecialFolder folder in Enum.GetValues(typeof(Environment.SpecialFolder)))
        {
          if (!logFileName.Contains("|"))
            break;
          logFileName = logFileName.Replace(string.Format(specialFormatToken, folder.ToString()), Environment.GetFolderPath(folder));
        }
        foreach (DictionaryEntry variable in Environment.GetEnvironmentVariables())
        {
          if (!logFileName.Contains("%"))
            break;
          logFileName = Regex.Replace(logFileName, string.Format(environmentVariablePattern, (string)variable.Key), (string)variable.Value, RegexOptions.IgnoreCase);
        }
        return logFileName;
      }
    }
    #endregion private

  }
  /// <summary>
  /// Advanced delimited trace listener which derives from DelimitedListTraceListener and prepares the log file name replacing the tags by the path to selected folder.
  /// </summary>
  public class AdvancedDelimitedListTraceListener : DelimitedListTraceListener
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvancedDelimitedListTraceListener"/> class.
    /// </summary>
    /// <param name="logFileName">Name of the log file.</param>
    /// <remarks>
    /// The <paramref name="logFileName"/> may contain the following tags:
    /// <c>|ApplicationDataPath|</c> by the ApplicationDataPath defined by the application manifest file.
    /// <c>|{Environment.SpecialFolder}|</c> by the path to special folder
    /// <c>|{environment variable}|</c> by the key value of this variable
    /// </remarks>
    public AdvancedDelimitedListTraceListener(string logFileName)
      : base(logFileName.PrepareLogFileName())
    { }
  }
  /// <summary>
  /// Advanced delimited XML writer trace listener which derives from XmlWriterTraceListener and prepares the log file name replacing the tags by the path to selected folder.
  /// </summary>
  public class AdvancedXmlWriterTraceListener : XmlWriterTraceListener
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AdvancedXmlWriterTraceListener"/> class.
    /// </summary>
    /// <param name="logFileName">Name of the log file.</param>
    /// <remarks>
    /// The <paramref name="logFileName"/> may contain the following tags:
    /// <c>|ApplicationDataPath|</c> by the ApplicationDataPath defined by the application manifest file.
    /// <c>|{Environment.SpecialFolder}|</c> by the path to special folder
    /// <c>|{environment variable}|</c> by the key value of this variable
    /// </remarks>
    public AdvancedXmlWriterTraceListener(string logFileName)
      : base(logFileName.PrepareLogFileName())
    { }
  }
}
