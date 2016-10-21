namespace Utils
{
  using System;
  /// <summary>
  /// Summary description for SpecialOperation.
  /// </summary>
  public class SpecialOperation
  {
    public static void IncRound(ref uint counter)
    {
      if (counter < uint.MaxValue) counter++;
      else counter = uint.MinValue;
    }
  }
}
