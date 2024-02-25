using CallAsyncMethodsParallel.CallApi;
using System.Diagnostics;

public class Program
{

    public static async Task Main(string[] args)
    {
        var syncObject = SynchronizationContext.Current is null ? "null" : SynchronizationContext.Current.ToString();
        Console.WriteLine($"The SynchronizationContext is {syncObject}");

        var taskScheduler = TaskScheduler.Current.ToString();

        Console.WriteLine($"The Task Scheduler is {taskScheduler}");

        await SequentialRun();
        await ParallelRun();

        //await SequentialRunWithException();
        //await ParallelRunWithExceptions();

        Console.ReadLine();
    }

    private static async Task SequentialRun()
    {
        Console.WriteLine($"Sequential API call, start on thread: {Thread.CurrentThread.ManagedThreadId}");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");

        var stopWatch = Stopwatch.StartNew();

        var apiClient = new SocialMediaApiCalls();

        var youtubeSubscribers = await apiClient.GetYoutubeSubscribers(httpClient, delay: 2000);
        var twitterFollowers = await apiClient.GetTwitterFollowers(httpClient, delay: 1900);
        var githubFollowers = await apiClient.GetGithubFollowers(httpClient, delay: 1800);

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

    private static async Task ParallelRun()
    {

        Console.WriteLine($"Parallel API call, start on thread: {Thread.CurrentThread.ManagedThreadId} ");

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

        Console.WriteLine($"Parallel API call, write out result on thread: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine(result);

        foreach (var item in apiClient.SharedResultList)
        {
            Console.WriteLine(item);
        }
    }

    private static async Task SequentialRunWithException()
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

    private static async Task ParallelRunWithExceptions()
    {

        // https://github.com/dotnet/core/issues/7011
        // https://gist.github.com/morgankenyon/686b8004932be1d8e02356fb6b652cfc


        Console.WriteLine("Parallel with 500 response");

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