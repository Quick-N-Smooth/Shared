using BlazorTemplate.Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading;

namespace BlazorTemplate.Client.ApiCalls;

internal class SocialMediaApiCalls
{
    // an alternative to combine the result list within the api methods.
    // NOTE, that it is not the usual way to get the result it is for experiment
    public IEnumerable<string> SharedFollowersList = Array.Empty<string>();

    public ICollection<string> SharedLog = new Collection<string>();

    private IEnumerable<string> getReferenceToSharedResultList()
    {
        return SharedFollowersList;
    }

    private void saveToSharedResultList(IEnumerable<string> result)
    {
        SharedFollowersList = result;
    }

    #region HappyFlow

    internal async Task<SocialMedia?> GetYoutubeSubscribers(HttpClient httpClient, int delay)
    {
        string? result = null;
        try
        {
            SharedLog.Add($"GetYoutubeSubscribers method start on thread: {Thread.CurrentThread.ManagedThreadId}");
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "youtube200" + "?" + "delay=" + delay);
            SharedLog.Add($"GetYoutubeSubscribers method continue on thread: {Thread.CurrentThread.ManagedThreadId}");
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? subscribers = dataObject?.Subscribers;
            CombineEnumerables(getReferenceToSharedResultList, saveToSharedResultList, subscribers);
            return dataObject;
        }
        catch
        {
            return null;
        }

    }

    internal async Task<SocialMedia?> GetTwitterFollowers(HttpClient httpClient, int delay)
    {
        string? result = null;
        try
        {
            SharedLog.Add($"GetTwitterFollowers method start on thread: {Thread.CurrentThread.ManagedThreadId}");
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "twitter200" + "?" + "delay=" + delay);
            SharedLog.Add($"GetTwitterFollowers method continue on thread: {Thread.CurrentThread.ManagedThreadId}");
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? subscribers = dataObject?.Subscribers;
            CombineEnumerables(getReferenceToSharedResultList, saveToSharedResultList, subscribers);
            return dataObject;
        }
        catch
        {
            return null;
        }

    }

    internal async Task<SocialMedia?> GetGithubFollowers(HttpClient httpClient, int delay)
    {
        string? result = null;
        try
        {
            SharedLog.Add($"GetGithubFollowers method start on thread: {Thread.CurrentThread.ManagedThreadId}");
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "github200" + "?" + "delay=" + delay);
            SharedLog.Add($"GetGithubFollowers method continue on thread: {Thread.CurrentThread.ManagedThreadId}");
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? subscribers = dataObject?.Subscribers;
            CombineEnumerables(getReferenceToSharedResultList, saveToSharedResultList, subscribers);
            return dataObject;
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

    // NOTE that Blazor is a single threaded so we do not have to use lock or semaphore like in a console app
    private static void CombineEnumerables(Func<IEnumerable<string>> getReferenceToSharedResultList, Action<IEnumerable<string>> saveToSharedResultList, IEnumerable<string>? sourceList)
    {
        var masterList = getReferenceToSharedResultList.Invoke();

        //var threadId = Thread.CurrentThread.ManagedThreadId;

        var mutableMaster = masterList.ToList();

        if (sourceList is not null)
        {
            foreach (var item in sourceList)
            {
                Thread.Sleep(100);
                mutableMaster.Add($"{item}");
            }
        }

        saveToSharedResultList.Invoke(mutableMaster);
    }

    #endregion
}
