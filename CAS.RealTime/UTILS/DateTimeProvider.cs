//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.ProcessObserver.RealTime.Utils
{

  /// <summary>
  /// Class that allows to use Local or UTC time depending on settings
  /// </summary>
  public class DateTimeProvider
  {

    #region private
    private IDateTimeProvider m_provider;
    private interface IDateTimeProvider
    {
      DateTime GetCurrentTime();
    }
    private class LocalTime : IDateTimeProvider
    {
      #region IDateTimeProvider Members
      DateTime IDateTimeProvider.GetCurrentTime()
      {
        return DateTime.Now;
      }
      #endregion
    }
    private class UTCTime : IDateTimeProvider
    {
      #region IDateTimeProvider Members
      DateTime IDateTimeProvider.GetCurrentTime()
      {
        return DateTime.UtcNow;
      }
      #endregion
    }
    #endregion

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeProvider"/> class that allows to use Local or UTC time depending on settings.
    /// </summary>
    /// <param name="useLocalTime">if set to <c>true</c> use <see cref="DateTime.Now"/>, otherwise use <see cref="DateTime.UtcNow"/>.</param>
    public DateTimeProvider(bool useLocalTime)
    {
      if (useLocalTime)
        m_provider = new LocalTime();
      else
        m_provider = new UTCTime();
    }
    #endregion

    #region public
    /// <summary>
    /// Reads Local or UTC time depending on settings
    /// </summary>
    public DateTime GetCurrentTime()
    {
      return m_provider.GetCurrentTime();
    }
    #endregion
  }
}
