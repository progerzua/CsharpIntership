// Task3 and Task4

/*Create mutex using tasks and async/await. The main diierence with System.Threading.Mutex is that 
this mutex must not block current thread (for using in UI thread).
*/

// Actually, its not doing what we have to.
// I realize that later =)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3n4_using_Mutex
{
    public class LockSection
    {
        private readonly Mutex _mutex = new Mutex();
        private readonly ManualResetEventSlim _resetEvent = new ManualResetEventSlim();
        private Thread _thread;

        public Task LockAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            _thread = new Thread(() =>
            {
                // Emulate long running operation
                Thread.Sleep(TimeSpan.FromSeconds(5));

                _mutex.WaitOne();
                tcs.SetResult(true);

                _resetEvent.Wait();
                _resetEvent.Reset();
                _mutex.ReleaseMutex();
            });

            _thread.Start();
            return tcs.Task;
        }

        public void Release()
        {
            _resetEvent.Set();
            _thread.Join();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
