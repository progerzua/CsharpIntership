// Task2
/* Description:
Create set of methods for Task and Task<T>, each of them will be similar to ContinueWith() from TPL, 
but will execute continuation in main (UI thread).
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_withDemo_WPF
{
    public static class WrappedContinuation
    {
        public static Task ContinueOnUI(this Task task, Action action)
        {
            return task.ContinueWith(t => action.Invoke(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task ContinueOnUI(this Task task, Action<Task> action)
        {
            return task.ContinueWith((t) => action.Invoke(t), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task<T> ContinueOnUI<T>(this Task<T> task, Func<T> action)
        {
            return task.ContinueWith(t => action.Invoke(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task<T> ContinueOnUI<T>(this Task<T> task, Func<Task, T> action)
        {
            return task.ContinueWith(t => action.Invoke(t), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task<T> ContinueOnUI<T>(this Task<T> task, Func<Task, Task<T>> action)
        {
            return task.ContinueWith<T>(t => action.Invoke(t).Result, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
