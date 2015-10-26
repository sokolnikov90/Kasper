using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentQueue
{
    public class ConcurentQueue<T>
    {
        private Queue<T> itemsContainer;

        private AutoResetEvent ewhSignal;

        private int itemsCount;

        private SpinLock spinLock;

        private bool spinLockTaken;

        public int Count
        {
            get { return itemsCount; }
        }

        // TimeOut in milliseconds
        public int TimeOut { get; private set; }

        public ConcurentQueue()
        {
            itemsContainer = new Queue<T>();
            ewhSignal = new AutoResetEvent(false);
            TimeOut = 60*1000;
        }

        public ConcurentQueue(int timeOut) : this()
        {
            TimeOut = timeOut;
        }

        public void Push(T value)
        {
            while (true)
            {
                spinLockTaken = false;

                try
                {
                    spinLock.Enter(ref spinLockTaken);
                }
                catch
                {
                    continue;
                }

                itemsContainer.Enqueue(value);  

                spinLock.Exit();

                break;
            }

            Interlocked.Increment(ref itemsCount);

            ewhSignal.Set();
        }

        public T Pop()
        {
            T result = default(T);

            bool wasTimeOut = false;

            while (true)
            {
                if (itemsCount > 0)
                {
                    spinLockTaken = false;

                    try
                    {
                        spinLock.Enter(ref spinLockTaken);
                    }
                    catch
                    {
                        continue;
                    }

                    result = itemsContainer.Dequeue();

                    spinLock.Exit();

                    Interlocked.Decrement(ref itemsCount);

                    return result;
                }

                if (!ewhSignal.WaitOne(TimeOut))
                {
                    throw new TimeoutException();
                }
            }
        }
    }
}
