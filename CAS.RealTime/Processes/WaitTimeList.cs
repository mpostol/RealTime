//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CAS.Lib.RTLib.Utils;
using System;
using System.Threading;

namespace CAS.Lib.RTLib.Processes
{
  /// <summary>
  /// List responsible for scheduling objects ( Manager of scheduled in time objects. ).
  /// </summary>
  public class WaitTimeList<TElement> : IDisposable
    where TElement : WaitTimeList<TElement>.TODescriptor
  {
    #region PRIVATE
    private readonly bool c_WaightedPriority = true;
    private readonly System.Threading.Timer m_Timer;
    private TimeSpan m_lastTime = clock.Elapsed;
    /// <summary>
    ///  one link from the chain (chain is managed in time queue)
    ///   
    /// Dzieki tej klasie utrzemujemy liste zarzadzanych obiektow.
    /// Kazdy z obiektow ma swoj element poprzedni i nastepny.
    /// 
    /// jednym z wazniejszych elementow kazdego ogniwa jest counter ktory okresla czy i ile dany elment 
    /// jest przeterminowany.
    /// Elementy (obiekty) sa szeregowane na lisacie w nastepujacy sposob:
    /// - pierwszy element ma ustawiony counter na wartosc opoznienia (jesli counter &gt;0 to znaczy ze 
    /// element czeka na wykonanie, 
    /// jesli counter &lt;0 tzn. ze element jet opozniony i powinien zostac wyjety z kolejki
    /// - nastepny element ma counter ustawiony na wartosc wzgledna wobec poprzedniego, np. jesli dany 
    /// element ma ustawiony counter na 10 tzn ze ma 109 ms zapasu w stosunku do opoznienia elementu poprzedniego.
    ///
    /// </summary>
    private class ChainLink
    {
      #region PRIVATE
      private readonly TElement myTODescriptor;
      private readonly WaitTimeList<TElement> myQueue;
      private TimeSpan myCycle = TimeSpan.MaxValue;
      private TimeSpan Cycle
      {
        get { return myCycle; }
        set { myCycle = Timer.Max(value, minTimeSpan); }
      }
      private long mycounter;
      private long Counter
      {
        get
        {
          return mycounter;
        }
        set
        {
          mycounter = value;
          if (mycounter <= 0)
            myQueue.myTimeExpiredSig.Notify();
        }
      }
      private bool escQueue;
      private ChainLink next = null;
      private ChainLink prev = null;
      private bool myInQueue = false;
      /// <summary>
      /// Gets a value indicating whether this object is in queue.
      /// </summary>
      /// <value><c>true</c> if in queue otherwise, <c>false</c>.</value>
      private bool InQueue
      {
        get { return myInQueue; }
      }
      /// <summary>
      /// Inserts the in queue using default settings (default cycle).
      /// </summary>
      private void InsertInQueue()
      {
        InsertInQueue(Cycle);
      }
      /// <summary>
      /// Inserts the in queue using defined cycle.
      /// 
      /// Ta funkcja wstawia element na poczatek jesli kolejka jest pusta lub 
      /// poszukuje wlasciwego misjca gdzie dany element powinien byc wstawiony .
      /// 
      /// W zaleznosci od znalezienia miejsca odpowienio zmniejszany jest counter elementu,
      /// by stanowil wartosc wzgledna wobec porzedniego oraz zmieniany jest counter nstepnego
      /// </summary>
      /// <param name="currCycle">The curr cycle.</param>
      private void InsertInQueue(TimeSpan currCycle)
      {
        lock (this)
        {
          myQueue.queuelength++;
          Counter = Convert.ToInt64(currCycle.TotalMilliseconds);
          if ((myQueue.myTOQueue == null) || (Counter <= myQueue.myTOQueue.Counter))
          {
            prev = null;
            next = myQueue.myTOQueue;
            myQueue.myTOQueue = this;
          }
          else
          {
            Counter -= myQueue.myTOQueue.Counter;
            prev = myQueue.myTOQueue;
            next = myQueue.myTOQueue.next;
            while ((next != null) && (next.Counter <= Counter))
            {
              Counter -= next.Counter;
              prev = next;
              next = next.next;
            }//while
            prev.next = this;
          } // if ((myQueue.myTOQueue == null) || (counter <= myQueue.myTOQueue.counter))
          if (next != null)
          {
            next.prev = this;
            next.Counter -= Counter;
          }
          myInQueue = true;
        }
      } // InsertInQueue;
      #endregion
      #region PUBLIC
      /// <summary>
      /// Gets the get coupled Time Out Desciptor (<see>TODescriptor</see>).
      /// </summary>
      /// <value>The get coupled TOD.</value>
      internal TElement GetCoupledTOD
      {
        get { return myTODescriptor; }
      }
      /// <summary>
      /// Moves to beginning.
      /// </summary>
      internal void MoveToBeginning()
      {
        Remove();
        escQueue = true;
        InsertInQueue(TimeSpan.Zero);
      }
      /// <summary>
      /// Decrements the counter.
      /// 
      /// if the counter is less than 0 it notify other tasks that it is ready to be removed
      /// </summary>
      /// <param name="value">The value.</param>
      internal void DecCouter(int value)
      {
        Counter -= value;
        if (Counter <= 0)
        {
          myQueue.myTimeExpiredSig.Notify();
          myQueue.RemoveItem();
        }
      }
      /// <summary>
      /// Gets a value indicating whether this instance is ready to by removed.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this instance is ready to by removed; otherwise, <c>false</c>.
      /// </value>
      internal bool IsReadyToByRemoved
      {
        get { return (Counter <= 0); }
      }
      /// <summary>
      /// Removes this instance.
      /// </summary>
      internal void Remove()
      {
        lock (this)
        {
          if (!InQueue)
            return;
          escQueue = false;
          if (next != null)
          {
            next.prev = prev;
            next.Counter += Counter;
          }
          if (prev == null)
            myQueue.myTOQueue = next;
          else
            prev.next = next;
          prev = null;
          next = null;
          myInQueue = false;
          myQueue.queuelength--;
        }
      }//Remove
      /// <summary>
      /// Priorities the specified overtime sum.
      /// </summary>
      /// <param name="overtimeSum">The overtime sum.</param>
      /// <param name="maxPrior">The max prior.</param>
      /// <param name="mostDelayed">The most delayed object on the list .</param>
      /// <returns></returns>
      internal ChainLink Priority(ref long overtimeSum, ref double maxPrior, ref ChainLink mostDelayed)
      {
        overtimeSum += Counter;
        if (overtimeSum > 0)
          return null;
        double myPrior = overtimeSum / Cycle.TotalMilliseconds;
        if ((myPrior < maxPrior) || escQueue)
        {
          maxPrior = myPrior;
          mostDelayed = this;
        }
        if ((next != null) && !escQueue)
          return next;
        else
          return null;
      }//Priority 
      /// <summary>
      /// Sets the set cycle [in ms] of the object .
      /// </summary>
      /// <value>The set cycle.</value>
      internal virtual TimeSpan SetCycle
      {
        set
        {
          Cycle = value;
          if (InQueue)
            ResetCounter();
        }
      }
      /// <summary>
      /// Resets the counter (removes the element from queue and insterts the element again).
      /// </summary>
      internal virtual void ResetCounter()
      {
        Remove();
        InsertInQueue();
      }
      /// <summary>
      /// Initializes a new instance of the <see cref="WaitTimeList&lt;TElement&gt;.ChainLink"/> class.
      /// </summary>
      /// <param name="myQueue">My queue.</param>
      /// <param name="timeOutDescriptor">The coupled time out descriptor.</param>
      /// <param name="myCycle">My cycle - how often this object should be removed from the queue).</param>
      internal ChainLink(WaitTimeList<TElement> myQueue, TElement timeOutDescriptor, TimeSpan myCycle)
      {
        this.myQueue = myQueue;
        this.escQueue = myQueue.c_WaightedPriority;
        this.myTODescriptor = timeOutDescriptor;
        Cycle = myCycle;
        //InsertInQueue();
      }//ChainLink
      public override string ToString()
      {
        string nextelem = "null";
        if (next != null)
          nextelem = next.ToString();
        string data = String.Format("(ChainLink: cycle:{0}, counter:{1},next: ", Cycle, Counter);
        return data + nextelem.ToString() + ")";
      }
      #endregion
    } //ChainLink
    private ChainLink myTOQueue = null;
    /// <summary>
    /// cycleTicks is value in ms that indicate how often TicklistThred checks the counters
    ///  of object from the list
    ///  20 - is the reasonable value (in ms) that Windows schedule tasks
    /// </summary>
    private const ushort cycleMiliseconds = 20;
    private readonly static TimeSpan minTimeSpan = TimeSpan.FromMilliseconds(3 * cycleMiliseconds);
    /// <summary>
    /// this is signal used to inform other tasks that item is ready to be removed
    /// </summary>
    private Condition myTimeExpiredSig = new Condition();
    /// <summary>
    /// statistics item to gather queue priorities statistics
    /// </summary>
    private MinMaxAvr statPriority = new MinMaxAvr(20);
    /// <summary>
    /// clock for this queue
    /// </summary>
    private static System.Diagnostics.Stopwatch clock = new System.Diagnostics.Stopwatch();
    /// <summary>
    /// the name of this list
    /// </summary>
    private readonly string MyHandlerThreadName;
    /// <summary>
    /// length of this queue
    /// </summary>
    private int queuelength = 0;
    /// <summary>
    /// Initializes the static part of <see cref="WaitTimeList&lt;TElement&gt;"/> class - it starts the clock.
    /// </summary>
    static WaitTimeList()
    {
      clock.Start();
    }
    /// <summary>
    /// Ticks the list thread - the main thread resposible for list management.
    /// </summary>
    private void TickListThread(object parameters)
    {
      if (!Monitor.TryEnter(this)) // instead of lock (this)
        return; // we are exiting, because the lock is already acquired (this is not the problem because it wiil be soon launched again by the Timer)
      try
      {
        if (myTOQueue == null)
          return;
        TimeSpan currentTime = clock.Elapsed;
        int difference = Convert.ToInt32(((TimeSpan)(currentTime - m_lastTime)).TotalMilliseconds);
        //sometimes difference >1000 ??
        //System.Diagnostics.Debug.Assert( difference < 5 * cycleTicks );
        m_lastTime = currentTime;
        myTOQueue.DecCouter(difference);
      }
      catch (Exception ex)
      {
        string msg = "An exception has been caught in the WaitTimeList: {0}";
        EventLogMonitor.WriteToEventLogError(String.Format(msg, ex.Message), 339);
      }
      finally
      {
        Monitor.Exit(this);
      }
    }
    /// <summary>
    /// Removes the most delayed item.
    /// </summary>
    /// <returns></returns>
    private TElement RemoveMostDelayedItem()
    {
      ChainLink curr = myTOQueue;
      ChainLink maxPriorFoundPtr = null;
      long overtime = 0;
      double maxPriorFound = double.MaxValue;
      do
        curr = curr.Priority(ref overtime, ref maxPriorFound, ref maxPriorFoundPtr);
      while (curr != null);
      statPriority.Add = Convert.ToInt32(-maxPriorFound * 100);
      //statPriority.Add = - maxPriorFoundPtr.Counter;
      maxPriorFoundPtr.Remove();
      return maxPriorFoundPtr.GetCoupledTOD;
    } //RemoveMostDelayedItem;
    private TElement DoRemoveItem(bool autoreset)
    {
      TElement res = RemoveMostDelayedItem();
      System.Diagnostics.Debug.Assert(res != null, "Processes.WaitTimeList.WaitRemoveItem: res != null");
      if (autoreset)
        res.ResetCounter();
      return res;
    }
    /// <summary>
    /// Removes the item.
    /// </summary>
    /// <param name="autoreset">if set to <c>true</c> do ResetCounter.</param>
    /// <returns></returns>
    internal protected TElement RemoveItem(bool autoreset)
    {
      lock (this)
      {
        if (!IsReadyToRemoved)
          return null;
        return DoRemoveItem(autoreset);
      }
    }
    #endregion
    #region PUBLIC
    /// <summary>
    ///  Title   : Time Out Descriptor - it is a kind of public wrapper on a ChainLink class
    ///  but it is restricted to selected functionality.
    /// </summary>
    public class TODescriptor
    {
      #region PRIVATE
      private readonly ChainLink myChainLink;
      private readonly WaitTimeList<TElement> myQueue;
      #endregion
      #region PUBLIC
      /// <summary>
      /// Sets the new item cycle value and executes ResetCounter() if the item is in queue.
      /// </summary>
      /// <value>The cycle.</value>
      public virtual TimeSpan Cycle
      {
        set
        {
          lock (myQueue)
          {
            myChainLink.SetCycle = value;
          }
        }
      }
      /// <summary>
      /// Removes this item from queue, resets the couter and inserts it again to the queue.
      /// </summary>
      public virtual void ResetCounter()
      {
        lock (myQueue)
        { myChainLink.ResetCounter(); }
      }
      /// <summary>
      /// Moves this item to the beginning of the queue.
      /// </summary>
      public virtual void ClearCounter()
      {
        lock (myQueue)
        {
          myChainLink.MoveToBeginning();
        }
      }
      /// <summary>
      /// Removes this item from the queue.
      /// </summary>
      public virtual void Remove()
      {
        lock (myQueue)
          myChainLink.Remove();
      }
      /// <summary>
      /// Initializes a new instance of the <see cref="WaitTimeList&lt;TElement&gt;.TODescriptor"/> class.
      /// After creation the object is not added to the queue.
      /// </summary>
      /// <param name="queue">The queue.</param>
      /// <param name="cycle">The cycle.</param>
      /// <exception cref="ArgumentNullException">queue is a null reference.</exception>
      /// <remarks>After creation the object is not added to the queue.</remarks>
      public TODescriptor(WaitTimeList<TElement> queue, TimeSpan cycle)
      {
        this.myQueue = queue;
        if (queue == null)
          throw new ArgumentNullException("queue in TODescriptor cannot be null");
        if (cycle.TotalMilliseconds == 0)
          throw new ArgumentNullException("cycle in TODescriptor has to specify TimeSpan > 0 ");
        lock (myQueue)
          myChainLink = new ChainLink(myQueue, (TElement)this, cycle);
      }
      #endregion
    } //TODescriptor
    /// <summary>
    /// While overridden the method can be used to remove first ready item from the queue. 
    /// The method is called by the internal timer callback inside the lock of the instance of this class.
    /// Typically it is used to schedule a work related to removed Item using the <see cref="System.Threading.ThreadPool"/>.
    /// </summary>
    internal protected virtual void RemoveItem() { return; }
    /// <summary>
    /// Gets the length of the queue.
    /// </summary>
    /// <value>The length of the queue.</value>
    public int QueueLength
    {
      get
      {
        return queuelength;
      }
    }
    /// <summary>
    /// Event handler invoked every time new values are available.
    /// </summary>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <param name="average">The average.</param>
    public virtual void NewOvertimeCoefficient(long min, long max, long average)
    { }
    /// <summary>
    /// Gets a value indicating whether item from the queue is ready to be removed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if  instance is ready to removed; otherwise, <c>false</c>.
    /// </value>
    public bool IsReadyToRemoved
    {
      get
      {
        lock (this)
        {
          return (myTOQueue != null) && (myTOQueue.IsReadyToByRemoved);
        }
      }
    }
    /// <summary>
    /// Removes the item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Returns true if item removed, otherwise false and item is null</returns>
    public bool RemoveItem(out TElement item)
    {
      lock (this)
      {
        if (IsReadyToRemoved)
        {
          item = RemoveMostDelayedItem();
          return true;
        }
        else
        {
          item = default(TElement);
          return false;
        }
      }
    } //RemoveItem;
    /// <summary>
    /// Waits and remove an item.
    /// </summary>
    /// <param name="autoreset">if set to <c>true</c> autoreset the item.</param>
    /// <returns></returns>
    public TElement WaitRemoveItem(bool autoreset)
    {
      lock (this)
      {
        while ((myTOQueue == null) || (!myTOQueue.IsReadyToByRemoved))
          myTimeExpiredSig.Wait(this);
        System.Diagnostics.Debug.Assert(myTOQueue.IsReadyToByRemoved, "Signaled but in the queue there is no elements to be removed");
        return DoRemoveItem(autoreset);
      }
    } //WaitRemoveItem;
    /// <summary>
    /// Enables the list manager thread.
    /// </summary>
    public void EnableListManagerThread()
    {
      m_lastTime = clock.Elapsed;
      m_Timer.Change(cycleMiliseconds, cycleMiliseconds);
    }
    /// <summary>
    /// Disables the list manager thread.
    /// </summary>
    public void DisableListManagerThread()
    {
      m_Timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
    }
    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    public override string ToString()
    {
      string myTOQueueStringRepresentation = "";
      lock (this)
      { myTOQueueStringRepresentation = myTOQueue.ToString(); }
      return base.ToString() + MyHandlerThreadName + "MyTickListThreadEnabled: " + myTOQueueStringRepresentation;
    }
    #endregion
    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WaitTimeList&lt;TElement&gt;"/> class.
    /// </summary>
    /// <param name="handlerThreadName">Name of the handler thread.</param>
    /// <param name="waightedPriority">if set to <c>true</c> wighted priority algorithm is enabled.</param>
    public WaitTimeList(string handlerThreadName, bool waightedPriority)
    {
      c_WaightedPriority = waightedPriority;
      statPriority.MarkNewVal += new MinMaxAvr.newVal(NewOvertimeCoefficient);
      MyHandlerThreadName = handlerThreadName;
      m_Timer = new System.Threading.Timer(new System.Threading.TimerCallback(TickListThread));
      EnableListManagerThread();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="WaitTimeList&lt;TElement&gt;"/> class.
    /// </summary>
    /// <param name="handlerThreadName">Name of the handler thread.</param>
    public WaitTimeList(string handlerThreadName) : this(handlerThreadName, true) { }
    #endregion
    #region IDisposable
    private bool disposed = false;
    // 
    /// <summary> Implement IDisposable. Do not make this method virtual. A derived class should not be able to override this method.
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      // This object will be cleaned up by the Dispose method. 
      // Therefore, you should call GC.SupressFinalize to take this object off the finalization queue and prevent finalization code for this object 
      // from executing a second time.
      GC.SuppressFinalize(this);
    }
    // Dispose(bool disposing) executes in two distinct scenarios. 
    // If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources 
    // can be disposed. 
    // If disposing equals false, the method has been called by the runtime from inside the finalizer and you should not reference 
    // other objects. Only unmanaged resources can be disposed. 
    private void Dispose(bool disposing)
    {
      // Check to see if Dispose has already been called. 
      if (!this.disposed)
      {
        // If disposing equals true, dispose all managed and unmanaged resources. 
        if (disposing)
          m_Timer.Dispose();
        // Note disposing has been done.
        disposed = true;

      }
    }
    /// <summary>
    /// Finalizes an instance of the <see cref="WaitTimeList{TElement}"/> class. 
    /// Use C# destructor syntax for finalization code. This destructor will run only if the Dispose method does not get called. 
    /// It gives your base class the opportunity to finalize. Do not provide destructors in types derived from this class.
    /// </summary>
    ~WaitTimeList()
    {
      // Do not re-create Dispose clean-up code here. 
      // Calling Dispose(false) is optimal in terms of 
      // readability and maintainability.
      Dispose(false);
    }
    #endregion
  }
}
