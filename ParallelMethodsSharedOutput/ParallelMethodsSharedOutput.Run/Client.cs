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
                FinalResult = CombineEnumerables<string>(FinalResult, result);
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
                FinalResult = CombineEnumerables<string>(FinalResult, result);
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
                FinalResult = CombineEnumerables<string>(FinalResult, result);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        // async alternative
        private static async Task<IEnumerable<T>> CombineCollectionsAsync<T>(IEnumerable<T> master, IEnumerable<T>? source)
        {
            if (source is not null)
            {
                master = master.Concat(source);
            }

            return master;
        }

        private static IEnumerable<T> CombineEnumerables<T>(IEnumerable<T> master, IEnumerable<T>? source)
        {
            if (source is not null)
            {
               master = master.Concat(source);
            }

            return master;
        }
    }
}
