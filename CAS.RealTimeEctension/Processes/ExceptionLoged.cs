//<summary>
//  Title   : ExceptionLoged
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    2007: mpostol: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

namespace CAS.Lib.RTLib.Processes
{
  using System;
  using System.Diagnostics;
  /// <summary>
  /// Summary description for ExceptionLoged.
  /// </summary>
  [Serializable]
  [Obsolete("Use trace infrastructure to write to EventLog")]
  public class ExceptionLoged : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionLoged"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="type">The type.</param>
    /// <param name="eventID">The event ID.</param>
    /// <param name="category">The category.</param>
    public ExceptionLoged(string message, EventLogEntryType type, Error eventID, short category)
    {
      new CAS.Lib.RTLib.Processes.EventLogMonitor(message, type, (int)eventID, category).WriteEntry();
    }
  }
}
