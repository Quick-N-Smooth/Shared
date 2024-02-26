﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace CallAsyncMethodsParallel.CallApi;

internal class SocialMediaApiCalls
{
    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
    public IEnumerable<string> SharedResultList = Array.Empty<string>();

    private IEnumerable<string> getReferenceToSharedResultList()
    {
        return SharedResultList;
    }

    private void saveToSharedResultList(IEnumerable<string> result)
    {
        SharedResultList = result;
    }

    #region HappyFlow

    internal async Task<int?> GetYoutubeSubscribers(HttpClient httpClient, int delay)
    {
        string? result = null;
        try
        {
            Console.WriteLine($"GetYoutubeSubscribers start method on thread: {Thread.CurrentThread.ManagedThreadId}");
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "youtube200" + "?" + "delay=" + delay).ConfigureAwait(false);
            Console.WriteLine($"GetYoutubeSubscribers digest result on thread: {Thread.CurrentThread.ManagedThreadId}");
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? list = dataObject?.Subscribers;
            CombineEnumerables(getReferenceToSharedResultList, saveToSharedResultList, list);
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
            Console.WriteLine($"GetTwitterFollowers start method on thread: {Thread.CurrentThread.ManagedThreadId}");
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "twitter200" + "?" + "delay=" + delay).ConfigureAwait(false);
            Console.WriteLine($"GetTwitterFollowers digest result on thread: {Thread.CurrentThread.ManagedThreadId}");
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? list = dataObject?.Subscribers;
            CombineEnumerables(getReferenceToSharedResultList, saveToSharedResultList, list);
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
            Console.WriteLine($"GetGithubFollowers start method on thread: {Thread.CurrentThread.ManagedThreadId}");
            result = await httpClient.GetStringAsync(httpClient.BaseAddress + "github200" + "?" + "delay=" + delay).ConfigureAwait(false);
            Console.WriteLine($"GetGithubFollowers digest result on thread: {Thread.CurrentThread.ManagedThreadId}");
            var dataObject = JsonConvert.DeserializeObject<SocialMedia>(result);
            IEnumerable<string>? list = dataObject?.Subscribers;
            CombineEnumerables(getReferenceToSharedResultList, saveToSharedResultList, list);
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

    private static void CombineEnumerables(Func<IEnumerable<string>> getReferenceToSharedResultList, Action<IEnumerable<string>> saveToSharedResultList, IEnumerable<string>? sourceList)
    {
        semaphoreSlim.Wait();

        try
        {
            var masterList = getReferenceToSharedResultList.Invoke();

            var threadId = Thread.CurrentThread.ManagedThreadId;

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
        finally
        {
            semaphoreSlim.Release();
        }
    }

    #endregion
}