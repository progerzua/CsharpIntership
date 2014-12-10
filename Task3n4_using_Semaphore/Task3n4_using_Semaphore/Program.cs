﻿// Task3 and Task4
// Without Demo =(

/*Create mutex using tasks and async/await. The main diierence with System.Threading.Mutex is that 
this mutex must not block current thread (for using in UI thread).
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3n4_using_Semaphore
{
    class AsyncLock
    {
        private readonly Task<IDisposable> _releaserTask;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly IDisposable _releaser;

        public AsyncLock()
        {
            _releaser = new Releaser(_semaphore);
            _releaserTask = Task.FromResult(_releaser);
        }
        public IDisposable Lock()
        {
            _semaphore.Wait();
            return _releaser;
        }
        public Task<IDisposable> LockAsync()
        {
            var waitTask = _semaphore.WaitAsync();
            return waitTask.IsCompleted
                ? _releaserTask
                : waitTask.ContinueWith(
                    (_, releaser) => (IDisposable)releaser,
                    _releaser,
                    CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
        }
        private class Releaser : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;
            public Releaser(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }
            public void Dispose()
            {
                _semaphore.Release();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /* Usage:
             * using (await _asyncLock.LockAsync())
                {
                    ... use shared resource.
                }
             
             */
        }
    }
}
