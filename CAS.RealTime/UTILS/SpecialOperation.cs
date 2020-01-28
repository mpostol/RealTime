//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace Utils
{
  /// <summary>
  /// Summary description for SpecialOperation.
  /// </summary>
  public static class SpecialOperation
  {
    /// <summary>
    /// Round increments <paramref name="counter"/>.
    /// </summary>
    /// <param name="counter">The counter.</param>
    public static void IncRound(ref uint counter)
    {
      if (counter < uint.MaxValue) counter++;
      else counter = uint.MinValue;
    }
  }
}
