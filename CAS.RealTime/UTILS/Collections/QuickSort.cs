//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.ProcessObserver.RealTime.Utils.Collections
{
  /// <summary>
  /// A simple version of the QuickSort.
  /// </summary>
  public class QuickSort
  {
    // A recursive version of QuickSort for characters.
    private static void qs(IComparable[] items, int left, int right)
    {
      int i, j;
      IComparable x, y;
      i = left;
      j = right;
      x = items[(left + right) / 2];
      do
      {
        while ((items[i].CompareTo(x) < 0) && (i < right)) i++;
        while ((items[j].CompareTo(x) > 0) && (j > left)) j--;
        if (i <= j)
        {
          y = items[i];
          items[i] = items[j];
          items[j] = y;
          i++; j--;
        }
      } while (i <= j);
      if (left < j) qs(items, left, j);
      if (i < right) qs(items, i, right);
    }
  }
}