//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Threading;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// Manager is a module that provides structural operations designed to create, 
  /// synchronize and communicate of concurrent processes, as well as all operations 
  /// needed to manage programs behavior in various situations. 
  /// All multiprogramming mechanisms exported from the module have been designed under 
  /// the assumption that for purpose of the process-to-process communication the 
  /// well-known monitor concept is used.
  /// </summary>
  public sealed class Manager
  {

    #region PRIVATE
    /// <summary>
    ///  Title   : Management of concurrent processes 
    /// </summary>
    private class ErrorQueueManager
    {
      private static readonly object errorQueueHand = new object();
      internal uint numOfErrors = 0;
      public void Wait()
      {
        lock (this)
        {
          numOfErrors++;
          lock (errorQueueHand) Monitor.PulseAll(errorQueueHand);
          Monitor.Wait(this);
        }
      }
    }
    private static uint procNum = 0;
    private static ErrorQueueManager errorQueue = new ErrorQueueManager();
    #endregion

    #region PUBLIC
    /// <summary>
    /// Adds to error queue.
    /// </summary>
    public static void AddToErrorQueue()
    {
      errorQueue.Wait();
    }
    /// <summary>
    /// Gets the number of errors.
    /// </summary>
    /// <value>The number of errors.</value>
    public static uint NumOfErrors => errorQueue.numOfErrors;
    /// <summary>
    /// Asserts if the condition is true.
    /// </summary>
    /// <param name="assertion">condition of assertion.</param>
    public static void Assert(bool assertion)
    {
      if (!assertion)
        errorQueue.Wait();
    }
    /// <summary>
    /// Starts the process.
    /// </summary>
    /// <param name="proces">The process.</param>
    /// <returns>thread that is started</returns>
    public static Thread StartProcess(ThreadStart proces)
    {
      return StartProcess(proces, "process" + procNum.ToString());
    }
    /// <summary>
    /// Starts the process.
    /// </summary>
    /// <param name="proces">The process.</param>
    /// <param name="name">The name.</param>
    /// <returns>thread that is started</returns>
    public static Thread StartProcess(ThreadStart proces, string name)
    {
      return StartProcess(proces, name, true, ThreadPriority.Normal);
    }
    /// <summary>
    /// Initializes a new instance of the Thread class and causes it to be scheduled for execution. 
    /// </summary>
    /// <param name="proces">A ThreadStart delegate that represents the methods to be invoked when this thread 
    /// begins executing. 
    /// </param>
    /// <param name="name">A string containing the name of the thread, or a null reference if no name was set.</param>
    /// <param name="isBackground">A value indicating whether or not a thread is a background thread. </param>
    /// <param name="priority">A value indicating the scheduling priority of a thread.</param>
    /// <returns></returns>
    /// <remarks>A thread is either a background thread or a foreground thread. Background threads 
    /// are identical to foreground threads, except that background threads do not prevent a process from 
    /// terminating. Once all foreground threads belonging to a process have terminated, the common language 
    /// runtime ends the process. Any remaining background threads are stopped and do not complete.
    /// </remarks>
    public static Thread StartProcess(ThreadStart proces, string name, bool isBackground, ThreadPriority priority)
    {
      procNum++;
      Thread procToStart = new Thread(proces)
      {
        Name = name,
        IsBackground = isBackground,
        Priority = priority
      };
      procToStart.Start();
      return procToStart;
    }
    #endregion

  }
}