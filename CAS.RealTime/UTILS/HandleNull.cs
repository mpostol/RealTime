namespace Utils
{
  using System;
  /// <summary>
  /// Summary description for IntPtrComarable.
  /// </summary>
  public class Handle : IComparable
  {
    int IComparable.CompareTo( object valToComapare)
    {
      if (Value.Equals(((Handle)valToComapare).Value)) return 0;
      if (Value > ((Handle)valToComapare).Value) return 1;
      else return -1;
    }
    internal uint Value { set; get; }
  }
}
