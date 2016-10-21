namespace Utils
{
  using System;
  /// <summary>
  /// Summary description for IntPtrComarable.
  /// </summary>
  public class Handle : IComparable
  {
    private uint val;
    int IComparable.CompareTo( object valToComapare)
    {
      if (val.Equals(((Handle)valToComapare).val)) return 0;
      if (val > ((Handle)valToComapare).val) return 1;
      else return -1;
    }
    internal uint Value
    {
      set{ val = value;}
      get{return val;}
    }
  }
}
