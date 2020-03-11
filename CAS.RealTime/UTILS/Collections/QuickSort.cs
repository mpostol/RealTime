//<summary>
//  Title   : QuickSort
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
  /// A simple version of the Quicksort.
  /// </summary>
  public class QuickSort
  {
    // A recursive version of Quicksort for characters. 
    private static void qs(IComparable[] items, int left, int right)  
    {  
      int i, j;  
      IComparable x, y;  
  
      i = left; j = right;  
      x = items[(left+right)/2];  
  
      do 
      {  
      while((items[i].CompareTo(x) < 0 ) && (i < right)) i++;  
      while((items[j].CompareTo(x) > 0) && (j > left)) j--;  
  
        if(i <= j) 
        {  
          y = items[i];  
          items[i] = items[j];  
          items[j] = y;  
          i++; j--;  
        }  
      } while(i <= j);  
  
      if(left < j) qs(items, left, j);  
      if(i < right) qs(items, i, right);  
    } 
  }
}

