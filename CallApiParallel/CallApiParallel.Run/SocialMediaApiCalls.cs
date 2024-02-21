using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CallAsyncMethodsParallel.CallApi;

internal class SocialMediaApiCalls
{
    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
    public IEnumerable<string> FinalResult = Array.Empty<string>();

    private IEnumerable<string> getFinalResult()
    {
        return FinalResult;
    }

    #region HappyFlow

    internal async Task<int?> GetYoutubeSubscribers(HttpClient httpClient, int delay)
    {
        string? result = null;
        try
        {
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "youtube200" + "?" + "delay=" + delay);
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? list = dataObject?.Subscribers;
            FinalResult = CombineEnumerables<string>(getFinalResult, list);
            return list?.Count();
        }
        catch
        {
            return null;
        }

    }

    internal async Task<int?> GetTwitterFollowers(HttpClient httpClient, int delay)
    {
        string? result = null;
        try
        {
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "twitter200" + "?" + "delay=" + delay);
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? list = dataObject?.Subscribers;
            FinalResult = CombineEnumerables(getFinalResult, list);
            return list?.Count();
        }
        catch
        {
            return null;
        }

    }

    internal async Task<int?> GetGithubFollowers(HttpClient httpClient, int delay)
    {
        string? result = null;
        try
        {
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "github200" + "?" + "delay=" + delay);
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? list = dataObject?.Subscribers;
            FinalResult = CombineEnumerables(getFinalResult, list);
            return list?.Count();
        }
        catch
        {
            return null;
        }

    }

    #endregion

    #region Troubles

    internal static async Task<int?> GetYoutubeSubscribersUnauthorized(HttpClient httpClient)
    {
        try
        {
            var result = await httpClient.GetStringAsync(httpClient.BaseAddress + "youtubeunauthorized");
            dynamic data = JObject.Parse(result);
            return data.subscribers;
        }
        catch
        {
            return null;
        }
    }

    internal static async Task<int?> GetGithubFollowersWithException(HttpClient httpClient)
    {
        try
        {
            var result = await httpClient.GetStringAsync(httpClient.BaseAddress + "github500");
            dynamic data = JObject.Parse(result);
            return data.followers;
        }
        catch
        {
            return null;
        }
    }

    internal static async Task<int?> GetTwitterFollowersWithException(HttpClient httpClient)
    {
        try
        {
            var result = await httpClient.GetStringAsync(httpClient.BaseAddress + "twitter500");
            dynamic data = JObject.Parse(result);
            return data.followers;
        }
        catch
        {
            return null;
        }
    }

    internal static async Task<int?> GetTwitterFollowersWithBadRequest(HttpClient httpClient)
    {
        try
        {
            var result = await httpClient.GetStringAsync(httpClient.BaseAddress + "twitter400");
            dynamic data = JObject.Parse(result);
            return data.message;
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region "Display"

    private static IEnumerable<T> CombineEnumerables<T>(Func<IEnumerable<T>> getMasterFunc, IEnumerable<T>? source)
    {
        semaphoreSlim.Wait();

        try
        {
            var master = getMasterFunc.Invoke();

            var editMaster = master.ToList();

            if (source is not null)
            {
                foreach (T item in source)
                {
                    Thread.Sleep(100);
                    editMaster.Add(item);
                }
            }

            return editMaster;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    #endregion
}
