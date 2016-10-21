//<summary>
//  Title   : Classes Responsible for provide current time in the net server
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20071002 created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http://www.cas.eu
//</summary>

using CAS.Lib.RTLib.Management;
using System;
using System.Diagnostics;

namespace CAS.Lib.RTLib.Utils
{

  /// <summary>
  /// Class that allows to receive current time (Local or UTC depend on settings)
  /// </summary>
  public class DateTimeProvider
  {
    
    #region private
    static private IDateTimeProvider m_provider;
    internal interface IDateTimeProvider
    {
      DateTime GetCurrentTime();
    }
    class LocalTime: IDateTimeProvider
    {
      #region IDateTimeProvider Members
      DateTime IDateTimeProvider.GetCurrentTime()
      {
        return DateTime.Now;
      }
      #endregion
    }
    class UTCTime: IDateTimeProvider
    {
      #region IDateTimeProvider Members
      DateTime IDateTimeProvider.GetCurrentTime()
      {
        return DateTime.UtcNow;
      }
      #endregion
    }
    #endregion
    
    #region creator
    static DateTimeProvider()
    {
      try
      {
        if ( AppConfigManagement.UseLocalTime )
        {
          m_provider = new LocalTime();
        }
        else
        {
          m_provider = new UTCTime();
        }
      }
      catch ( Exception ex )
      {
        m_provider = new UTCTime();
        Debug.Assert( false, ex.ToString() );
      }
    }
    #endregion

    #region public
    /// <summary>
    /// Function that allows to receive current time (Local or UTC depend on settings)
    /// </summary>
    /// <returns>DateTime</returns>
    static public DateTime GetCurrentTime()
    {
      try
      {
        return m_provider.GetCurrentTime();
      }
      catch ( Exception ex )
      {
        Debug.Assert( false, ex.ToString() );
        return DateTime.UtcNow;
      }
    }
    #endregion
  }
}
