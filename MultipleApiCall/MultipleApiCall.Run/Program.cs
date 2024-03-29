﻿using CallAsyncMethodsParallel.CallApi;
using Nito.AsyncEx;
using System.Diagnostics;

public class Program
{

    public static async Task Main(string[] args)
    {
        // NOTE: NITO PACKAGE INSTALLS A CUSTOM SynchronizationContext THAT MAKES THE CONSOLE APPLICATION A
        // SINGLE THREADED AS LONG AS .ConfigureAwait(true) IS USED 
        // BY THIS WAY THE CONSOLE APP WORKS LIKE BLAZOR
        AsyncContext.Run(async () =>
        {

            var syncObject = SynchronizationContext.Current is null ? "null" : SynchronizationContext.Current.ToString();
            Console.WriteLine($"The SynchronizationContext is {syncObject}");

            var taskScheduler = TaskScheduler.Current.ToString();

            Console.WriteLine($"The Task Scheduler is {taskScheduler}");

            await SyncRun();
            await AsyncRun();

            //await SequentialRunWithException();
            //await ParallelRunWithExceptions();

            Console.ReadLine();
        });
    }

    private static async Task SyncRun()
    {
        Console.WriteLine($"Sequential API call, start on thread: {Thread.CurrentThread.ManagedThreadId}");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");

        var stopWatch = Stopwatch.StartNew();

        var apiClient = new SocialMediaApiCalls();

        var youtubeSubscribers = await apiClient.GetYoutubeSubscribers(httpClient, delay: 2000).ConfigureAwait(true);
        var twitterFollowers = await apiClient.GetTwitterFollowers(httpClient, delay: 1900).ConfigureAwait(true); ;
        var githubFollowers = await apiClient.GetGithubFollowers(httpClient, delay: 1800).ConfigureAwait(true); ;

        var result = $"Sequential done in {stopWatch.ElapsedMilliseconds} ms";

        dynamic userProfile = new
        {
            Name = "Tom",
            YoutubeSubscribers = youtubeSubscribers?.ToString() ?? "N/A",
            TwitterFollowers = twitterFollowers,
            GetGithubFollowers = githubFollowers
        };

        result += userProfile;

        Console.WriteLine($"Sequential API call, write out result on thread: {Thread.CurrentThread.ManagedThreadId}");

        Console.WriteLine(result);

        foreach (var item in apiClient.SharedResultList)
        {
            Console.WriteLine(item);
        }
    }

    private static async Task AsyncRun()
    {

        Console.WriteLine($"Async API call, start on thread: {Thread.CurrentThread.ManagedThreadId} ");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");

        var stopWatch = Stopwatch.StartNew();

        var apiClient = new SocialMediaApiCalls();

        var youtubeSubscribers = apiClient.GetYoutubeSubscribers(httpClient, delay: 2000);
        var twitterFollowers = apiClient.GetTwitterFollowers(httpClient, delay: 1900);
        var githubFollowers = apiClient.GetGithubFollowers(httpClient, delay: 1800);

        await Task.WhenAll(youtubeSubscribers, twitterFollowers, githubFollowers);

        var result = $"Parallel done in {stopWatch.ElapsedMilliseconds} ms";

        dynamic userProfile = new
        {
            Name = "Tom",
            YoutubeSubscribers = youtubeSubscribers.Result,
            TwitterFollowers = twitterFollowers.Result,
            GetGithubFollowers = githubFollowers.Result
        };

        result += userProfile;

        Console.WriteLine($"Async API call, write out result on thread: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine(result);

        foreach (var item in apiClient.SharedResultList)
        {
            Console.WriteLine(item);
        }
    }

    private static async Task SyncRunWithException()
    {
        Console.WriteLine("Sequential API call with 500 response");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");
        var stopWatch = Stopwatch.StartNew();

        // exception is handled by a null result but can be a http code other than 200
        var youtubeSubscribers = await SocialMediaApiCalls.GetYoutubeSubscribersUnauthorized(httpClient);
        var twitterFollowers = await SocialMediaApiCalls.GetTwitterFollowersWithException(httpClient);
        var githubFollowers = await SocialMediaApiCalls.GetGithubFollowersWithException(httpClient);

        Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds} ms");

        dynamic userProfile = new
        {
            Name = "Tom",
            YoutubeSubscribers = youtubeSubscribers?.ToString() ?? "N/A",
            TwitterFollowers = twitterFollowers?.ToString() ?? "N/A",
            GetGithubFollowers = githubFollowers?.ToString() ?? "N/A"
        };

        Console.WriteLine(userProfile.ToString());
    }

    private static async Task AsyncRunWithExceptions()
    {

        // https://github.com/dotnet/core/issues/7011
        // https://gist.github.com/morgankenyon/686b8004932be1d8e02356fb6b652cfc


        Console.WriteLine("Async with 500 response");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");
        var stopWatch = Stopwatch.StartNew();

        Task allResultTask;

        var youtubeSubscribers = SocialMediaApiCalls.GetYoutubeSubscribersUnauthorized(httpClient);
        var twitterFollowers = SocialMediaApiCalls.GetTwitterFollowersWithException(httpClient);
        var githubFollowers = SocialMediaApiCalls.GetTwitterFollowersWithBadRequest(httpClient);

        allResultTask = Task.WhenAll(youtubeSubscribers, twitterFollowers, githubFollowers);

        await allResultTask;

        // Note that after WhenAll, it is safe to use task.Result
        dynamic userProfile = new
        {
            Name = "Tom",
            YoutubeSubscribers = youtubeSubscribers.Result?.ToString() ?? "N/A",
            TwitterFollowers = twitterFollowers.Result?.ToString() ?? "N/A",
            GetGithubFollowers = githubFollowers.Result?.ToString() ?? "N/A"
        };

        Console.WriteLine(userProfile.ToString());

        Console.WriteLine($"Done is {stopWatch.ElapsedMilliseconds} ms");


    }
}