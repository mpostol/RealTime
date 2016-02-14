//<summary>
//  Title   : Application configuration access helper
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using CAS.Lib.RTLib.Processes;
using System;
using System.Configuration;

namespace CAS.Lib.RTLib.Management
{
  /// <summary>
  /// Application configuration access helper
  /// </summary>
  [Obsolete("Use Settings application configuration management tools")]
  public class ApplicationConfiguration
  {
    #region private
    private const string ErrorMsg = "Unable to retrieve from an application configuration file the values associated with the key: ";
    /// <summary>
    /// The AppSettingsSection data <see cref="System.Configuration.ConfigurationManager.AppSettings"/>for the current application's 
    /// default configuration. 
    /// </summary>
    private static readonly System.Collections.Specialized.NameValueCollection ConfigValues;
    static ApplicationConfiguration()
    {
      ConfigValues = ConfigurationManager.AppSettings;
      Configuration CurrConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ConfigFilePath = System.IO.Path.GetDirectoryName(CurrConfig.FilePath) + System.IO.Path.DirectorySeparatorChar;
      string AppInfo = "Base directory:" + AppDomain.CurrentDomain.BaseDirectory
        + "| AppDomain.CurrentDomain.ToString():" + AppDomain.CurrentDomain.ToString()
        + "| AppDomain.CurrentDomain.SetupInformation.ConfigurationFile:" + AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
        + "| ConfigFilePath" + ConfigFilePath;
      CAS.Lib.RTLib.Processes.EventLogMonitor.WriteToEventLogInfo
        ("Configuration for: " + AppInfo, (int)Error.DataPorter_ApplicationConfiguration);
    }
    #endregion
    #region public
    /// <summary>
    /// The directory information for the configuration file. 
    /// </summary>
    public static readonly string ConfigFilePath;
    /// <summary>
    /// It allows to access application configuration information.
    /// </summary>
    /// <param name="key">The String key of the entry to locate. The key can be a null reference.</param>
    /// <param name="defaultVal">Default value</param>
    /// <returns>The value from the configuration file or if not available default value. </returns>
    /// <remarks>If the specified key is not found or there is a syntax error a message is written to the application log.</remarks>
    public static string GetAppSetting(string key, string defaultVal)
    {
      if (ConfigValues[key] == null)
      {
        new CAS.Lib.RTLib.Processes.EventLogMonitor
          (ErrorMsg + key, System.Diagnostics.EventLogEntryType.Warning, (int)Error.RTLib_AppConfigManagement, 43).WriteEntry();
        return defaultVal;
      }
      else
        return ConfigValues[key];
    }
    /// <summary>
    /// It allows to access application configuration information.
    /// </summary>
    /// <param name="key">The String key of the entry to locate. The key can be a null reference.</param>
    /// <param name="defaultVal">Default value</param>
    /// <returns>The value from the configuration file or if not available default value. </returns>
    /// <remarks>If the specified key is not found or there is a syntax error a message is written to the application log.</remarks>
    public static int GetAppSetting(string key, int defaultVal)
    {
      string strVal;
      if ((strVal = GetAppSetting(key, string.Empty)) != string.Empty)
        try { return System.Int32.Parse(strVal); }
        catch (Exception ex)
        {
          new CAS.Lib.RTLib.Processes.EventLogMonitor(ex.Message, System.Diagnostics.EventLogEntryType.Warning,
            (int)Processes.Error.RTLib_AppConfigManagement, 26).WriteEntry();
        }
      return defaultVal;
    }
    /// <summary>
    /// It allows to access application configuration information.
    /// </summary>
    /// <param name="key">The String key of the entry to locate. The key can be a null reference.</param>
    /// <param name="defaultVal">Default value</param>
    /// <returns>The value from the configuration file or if not available default value. </returns>
    /// <remarks>If the specified key is not found or there is a syntax error a message is written to the application log.</remarks>
    public static uint GetAppSetting(string key, uint defaultVal)
    {
      string strVal;
      if ((strVal = GetAppSetting(key, string.Empty)) != string.Empty)
        try { return System.UInt32.Parse(strVal); }
        catch (Exception ex)
        {
          new CAS.Lib.RTLib.Processes.EventLogMonitor(ex.Message, System.Diagnostics.EventLogEntryType.Warning,
            (int)Error.RTLib_AppConfigManagement, 26).WriteEntry();
        }
      return defaultVal;
    }
    /// <summary>
    /// It allows to access application configuration information.
    /// </summary>
    /// <param name="key">The String key of the entry to locate. The key can be a null reference.</param>
    /// <param name="defaultVal">Deafoult value</param>
    /// <returns>The value from the configuration file or if not available default value. </returns>
    /// <remarks>If the specified key is not found or there is a syntax error a message is written to the application log.</remarks>
    public static ushort GetAppSetting(string key, ushort defaultVal)
    {
      string strVal;
      if ((strVal = GetAppSetting(key, string.Empty)) != string.Empty)
        try { return System.UInt16.Parse(strVal); }
        catch (Exception ex)
        {
          new CAS.Lib.RTLib.Processes.EventLogMonitor(ex.Message, System.Diagnostics.EventLogEntryType.Warning,
            (int)Processes.Error.RTLib_AppConfigManagement, 26).WriteEntry();
        }
      return defaultVal;
    }
    /// <summary>
    /// It allows to access application configuration information.
    /// </summary>
    /// <param name="key">The String key of the entry to locate. The key can be a null reference.</param>
    /// <param name="defaultVal">Deafoult value</param>
    /// <returns>The value from the configuration file or if not available default value. </returns>
    /// <remarks>If the specified key is not found or there is a syntax error a message is written to the application log.</remarks>
    public static Single GetAppSetting(string key, Single defaultVal)
    {
      string strVal;
      if ((strVal = GetAppSetting(key, string.Empty)) != string.Empty)
        try { defaultVal = System.Single.Parse(ConfigValues[key]); }
        catch (Exception ex)
        {
          new CAS.Lib.RTLib.Processes.EventLogMonitor(ex.Message, System.Diagnostics.EventLogEntryType.Warning,
            (int)Processes.Error.RTLib_AppConfigManagement, 26).WriteEntry();
        }
      return defaultVal;
    }
    /// <summary>
    /// It allows to access application configuration information.
    /// </summary>
    /// <param name="key">The String key of the entry to locate. The key can be a null reference.</param>
    /// <param name="defaultVal">Default value</param>
    /// <returns>The value from the configuration file or if not available default value. </returns>
    /// <remarks>If the specified key is not found or there is a syntax error a message is written to the application log.</remarks>
    public static bool GetAppSetting(string key, bool defaultVal)
    {
      string strVal;
      if ((strVal = GetAppSetting(key, string.Empty)) != string.Empty)
        try { defaultVal = System.Boolean.Parse(ConfigValues[key]); }
        catch (Exception ex)
        {
          new CAS.Lib.RTLib.Processes.EventLogMonitor(ex.Message, System.Diagnostics.EventLogEntryType.Warning,
            (int)Processes.Error.RTLib_AppConfigManagement, 26).WriteEntry();
        }
      return defaultVal;
    }
    #endregion
  }
}
