using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;

namespace Colg_UWP.Util
{
    public static class Extensions
    {
        #region LINQ Extensions
        public static IEnumerable<T> GetSingle<T>(this T item)
        {
            return Enumerable.Repeat(item, 1);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (second == null)
            {
                return first;
            }
            else
            {
                return Enumerable.Concat(first, second);

            }
        }
        public static IEnumerable<T> ConcatMany<T>(this IEnumerable<T> first, params IEnumerable<T>[] remaining)
        {
            IEnumerable<T> concat = first;
            foreach (IEnumerable<T> item in remaining)
            {
                concat = concat.Concat(item);
            }
            return concat;
        }
        #endregion

        #region IAsyncOperation Extensions

        public static ConfiguredTaskAwaitable<TResult> AsTask<TResult, TProgress>(this IAsyncOperationWithProgress<TResult,TProgress> operation, bool continueOnCapturedContext,CancellationToken token)
        {
            return operation.AsTask(token).ConfigureAwait(false);
        }
        public static ConfiguredTaskAwaitable<TResult> AsTask<TResult, TProgress>(this IAsyncOperationWithProgress<TResult, TProgress> operation, bool continueOnCapturedContext)
        {
            return operation.AsTask().ConfigureAwait(false);
        }


        public static ConfiguredTaskAwaitable<TResult> AsTask<TResult>(this IAsyncOperation<TResult> operation, bool continueOnCapturedContext, CancellationToken token)
        {
            return operation.AsTask(token).ConfigureAwait(false);
        }
        public static ConfiguredTaskAwaitable<TResult> AsTask<TResult>(this IAsyncOperation<TResult> operation, bool continueOnCapturedContext)
        {
            return operation.AsTask().ConfigureAwait(false);
        }


        public static ConfiguredTaskAwaitable AsTask(this IAsyncAction operation, bool continueOnCapturedContext, CancellationToken token)
        {
            return operation.AsTask(token).ConfigureAwait(false);
        }
        public static ConfiguredTaskAwaitable AsTask(this IAsyncAction operation, bool continueOnCapturedContext)
        {
            return operation.AsTask().ConfigureAwait(false);
        }

        #endregion


    }
}
