//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.ProcessObserver.RealTime.Win32API
{

  using System;
  using System.Runtime.InteropServices;

  /// <summary>
  /// Win32 API Time Functions Wrapper.
  /// </summary>
  public class TimeFunctionsWrapper
  {
    /// <summary>
    /// The QueryPerformanceCounter function retrieves the current value of the high-resolution 
    /// performance counter.
    /// </summary>
    /// <param name="lpPerformanceCount">
    /// [out] Pointer to a variable that receives the current performance-counter value, in counts.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is nonzero. If the function fails, the return value 
    /// is zero. To get extended error information, call GetLastError.
    /// </returns>
    /// <remarks>
    /// On a multiprocessor machine, it should not matter which processor is called. However, you can get 
    /// different results on different processors due to bugs in the BIOS or the HAL. To specify processor affinity for a thread, 
    /// use the SetThreadAffinityMask function.
    /// </remarks>
    [DllImport("kernel32")]
    [Obsolete]
    public static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
    /// <summary>
    /// The QueryPerformanceFrequency function retrieves the frequency of the high-resolution performance counter, 
    /// if one exists. The frequency cannot change while the system is running.
    /// </summary>
    /// <param name="lpFrequency"> [out] Pointer to a variable that receives the current performance-counter 
    /// frequency, in counts per second. If the installed hardware does not support a high-resolution performance 
    /// counter, this parameter can be zero.
    /// </param>
    /// <returns>
    /// If the installed hardware supports a high-resolution performance counter, the return value is 
    /// nonzero. If the function fails, the return value is zero. To get extended error information, call 
    /// GetLastError. For example, if the installed hardware does not support a high-resolution performance 
    /// counter, the function fails.
    /// </returns>
    /// <remarks>
    /// Note  The frequency of the high-resolution performance counter is not the processor speed.
    /// </remarks>>
    [DllImport("kernel32")]
    [Obsolete]
    public static extern bool QueryPerformanceFrequency(ref long lpFrequency);

  }
}
