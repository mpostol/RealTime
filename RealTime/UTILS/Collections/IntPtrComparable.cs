//<summary>
//  Title   : Name of Application
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

 using System;

namespace UAOOI.ProcessObserver.RealTime.Utils.Collections
{
 /// <summary>
  /// Summary description for IntPtrComarable.
  /// </summary>
  internal class IntPtrComparable : IComparable
  {
    private IntPtr val;
    int IComparable.CompareTo( object valToComapare)
    {
      if (val.Equals(((IntPtrComparable)valToComapare).val)) return 0;
      if (val.ToInt32() > ((IntPtrComparable)valToComapare).val.ToInt32()) return 1;
      else return -1;
    }
    internal IntPtrComparable(IntPtr myVal)
    {
      val = myVal;
    }
  }
}
