using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading;
namespace CallAsyncMethodsParallel.CallApi
{
    internal static class Server
    {

        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public static ICollection<string> FinalResult = Array.Empty<string>();

        internal static async Task<Collection<string>> GetYoutubeSubscribers(int delay)
        {
            var result = new Collection<string>();
            for (var i = 0; i < 10; i++) 
            { 
                result.Add("YoutubeSubscriber_" + i.ToString());
            }
            await Task.Delay(delay);
            return await Task.FromResult(result);
        }

        internal static async Task<Collection<string>> GetTwitterFollowers(int delay)
        {
            var result = new Collection<string>();
            for (var i = 0; i < 10; i++)
            {
                result.Add("TwitterSubscriber_" + i.ToString());
            }
            await Task.Delay(delay);
            return await Task.FromResult(result);
        }

        internal static async Task<Collection<string>> GetGithubFollowers(int delay)
        {
            var result = new Collection<string>();
            for (var i = 0; i < 10; i++)
            {
                result.Add("GithubSubscriber_" + i.ToString());
            }
            await Task.Delay(delay);
            return await Task.FromResult(result);
        }

        internal static async Task<Collection<string>> GetTwitterFollowersThrowTimeout(int delay)
        {
            await Task.Delay(delay);
            throw new TimeoutException();
        }
    }
}
