//<summary>
//  Title   : Minimum, maximum and average values are counted
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    Maciej Zbrzezny - 12-04-2006
//      dopisano do klas Serializable
//    MPostol - 24-10-03:created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;

namespace CAS.Lib.RTLib.Utils
{
  /// <summary>
  /// Minimum, maximum and average values are counted.
  /// </summary>
  [Serializable]
  public class MinMaxAvr
  {
    #region PRIVATE
    private ushort myNumOfElemets;
    private ushort elementIdx;
    private long max = long.MinValue, currmax = 0;
    private long min = long.MaxValue, currmin = 0;
    private long sum = 0, curravr = 0;
    #endregion
    #region PUBLIC
    /// <summary>
    /// Event handler invoked every time new values are available.
    /// </summary>
    public delegate void newVal( long min, long max, long avr );
    /// <summary>
    /// Occurs when new value is calculated.
    /// </summary>
    public event newVal markNewVal = null;
    /// <summary>
    /// Gets the maximum value
    /// </summary>
    /// <value>The max.</value>
    public long Max
    {
      get { return currmax; }
    }
    /// <summary>
    /// Gets the minimum value
    /// </summary>
    /// <value>The min.</value>
    public long Min
    {
      get { return currmin; }
    }
    /// <summary>
    /// Gets the average value
    /// </summary>
    /// <value>The average value.</value>
    public long Avr
    {
      get { return curravr; }
    }
    /// <summary>
    /// adds value to be counted as min, max, average
    /// </summary>
    /// <value>Value to be added </value>
    public long Add
    {
      set
      {
        elementIdx++;
        max = System.Math.Max( value, max );
        min = System.Math.Min( value, min );
        sum += value;
        if ( elementIdx >= myNumOfElemets )
        {
          currmax = max;
          currmin = min;
          curravr = sum / elementIdx;
          elementIdx = 0;
          max = long.MinValue;
          min = long.MaxValue;
          sum = 0;
          if ( markNewVal != null )
            markNewVal( currmin, currmax, curravr );
        }
      }
    }
    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    public override string ToString()
    {
      return currmin.ToString() + "\\" + curravr.ToString() + "\\" + currmax.ToString() + "(Mn\\Av\\Mx)";
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="MinMaxAvr"/> class.
    /// </summary>
    /// <param name="elements">The number of elements that must be collected to count new Min,Max,Avg values </param>
    public MinMaxAvr( ushort elements )
    {
      myNumOfElemets = elements;
    }
    #endregion
  }
}
