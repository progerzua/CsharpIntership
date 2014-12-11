// Task3 and Task4

/*Create mutex using tasks and async/await. The main diierence with System.Threading.Mutex is that 
this mutex must not block current thread (for using in UI thread).
*/

// MSDN Helped me a lot =)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3n4_using_Semaphore
{
    public class AsyncMutex
    {
        private readonly static Task completed = Task.FromResult(true);
        private readonly Queue<TaskCompletionSource<bool>> waiters = new Queue<TaskCompletionSource<bool>>();
        private int currentCount;

        public AsyncMutex(int initialCount)
        {
            if (initialCount < 0) throw new ArgumentOutOfRangeException("initialCount");
            currentCount = initialCount;
        }

        public Task Lock()
        {
            lock (waiters)
            {
                if (currentCount > 0)
                {
                    --currentCount;
                    return completed;
                }
                else
                {
                    var waiter = new TaskCompletionSource<bool>();
                    waiters.Enqueue(waiter);
                    return waiter.Task;
                }
            }
        }

        public void Release()
        {
            TaskCompletionSource<bool> toRelease = null;
            lock (waiters)
            {
                if (waiters.Count > 0)
                    toRelease = waiters.Dequeue();
                else
                    ++currentCount;
            }
            if (toRelease != null)
                toRelease.SetResult(true);
        }
    }

    public class TaskMutex
    {
        private readonly AsyncMutex semaphore;
        private readonly Task<Releaser> releaser;

        public TaskMutex()
        {
            semaphore = new AsyncMutex(1);
            releaser = Task.FromResult(new Releaser(this));
        }

        public Task<Releaser> LockSection()
        {
            var wait = semaphore.Lock();
            return wait.IsCompleted ?
                releaser :
                wait.ContinueWith((_, state) => new Releaser((TaskMutex)state),
                    this, CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }

        public struct Releaser : IDisposable
        {
            private readonly TaskMutex toRelease;

            internal Releaser(TaskMutex mutex) { toRelease = mutex; }

            public void Dispose()
            {
                if (toRelease != null)
                    toRelease.semaphore.Release();
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
