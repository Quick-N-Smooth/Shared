using System.Collections.ObjectModel;
namespace CallAsyncMethodsParallel.CallApi
{
    internal class Client
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public IEnumerable<string> FinalResult = Array.Empty<string>();

        internal async Task GetYoutubeSubscribers()
        {
            var result = await Server.GetYoutubeSubscribers(delay: 2000);
            try
            {
                await semaphoreSlim.WaitAsync().ConfigureAwait(false);
                //FinalResult = await CombineEnumerablesAsync<string>(FinalResult, result)
                //    .ContinueWith(combineTask => combineTask.Result.ToArray(), TaskContinuationOptions.OnlyOnRanToCompletion);
                FinalResult = CombineEnumerables<string>(FinalResult, result).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        internal async Task GetTwitterFollowers()
        {
            var result = await Server.GetTwitterFollowers(delay: 1800);
            try
            {
                await semaphoreSlim.WaitAsync().ConfigureAwait(false);
                //FinalResult = await CombineEnumerablesAsync<string>(FinalResult, result)
                //    .ContinueWith(combineTask => combineTask.Result.ToArray(), TaskContinuationOptions.OnlyOnRanToCompletion);
                FinalResult = CombineEnumerables<string>(FinalResult, result).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        internal async Task GetGithubFollowers()
        {
            var result = await Server.GetGithubFollowers(delay: 1600);
            try
            {
                await semaphoreSlim.WaitAsync().ConfigureAwait(false);
                //FinalResult = await CombineEnumerablesAsync<string>(FinalResult, result)
                //    .ContinueWith(combineTask => combineTask.Result.ToArray(), TaskContinuationOptions.OnlyOnRanToCompletion);
                FinalResult = CombineEnumerables<string>(FinalResult, result).ToArray();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        // async alternative
        private static async Task<IEnumerable<T>> CombineEnumerablesAsync<T>(IEnumerable<T> master, IEnumerable<T>? source)
        {
            var result = new Collection<T>();

            foreach (T item in master)
            {
                await Task.Delay(100);
                result.Add(item);
            }

            if (source is not null)
            {
                foreach (T item in source)
                {
                    await Task.Delay(100);
                    result.Add(item);
                }
            }

            return result;
        }

        private static IEnumerable<T> CombineEnumerables<T>(IEnumerable<T> master, IEnumerable<T>? source)
        {
            var result = new Collection<T>();

            foreach (T item in master)
            {
                Thread.Sleep(100);
                result.Add(item);
            }

            if (source is not null)
            {
                foreach (T item in source)
                {
                    Thread.Sleep(100);
                    result.Add(item);
                }
            }

            return result;
        }

        private static IEnumerable<T> CombineEnumerablesThrowException<T>(IEnumerable<T> master, IEnumerable<T>? source)
        {
            Thread.Sleep(100);
            throw new NotImplementedException();
        }
    }
}
