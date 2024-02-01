using CallAsyncMethodsParallel.CallApi;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

public class Program
{
    public static async Task Main(string[] args)
    {
        await SequentialRun();
        await ParallelRun();

        await SequentialRunWithException();
        await ParallelRunWithExceptions();

        Console.ReadLine();
    }

    private static async Task SequentialRun()
    {
        Console.WriteLine("Sequential API call with 200 response");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");
        var stopWatch = Stopwatch.StartNew();

        var youtubeSubscribers = await ApiCalls.GetYoutubeSubscribers(httpClient);
        var twitterFollowers = await ApiCalls.GetTwitterFollowers(httpClient);
        var githubFollowers = await ApiCalls.GetGithubFollowers(httpClient);

        Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds} ms");

        dynamic userProfile = new
        {
            Name = "Tom",
            YoutubeSubscribers = youtubeSubscribers?.ToString() ?? "N/A",
            TwitterFollowers = twitterFollowers,
            GetGithubFollowers = githubFollowers
        };

        Console.WriteLine(userProfile.ToString());
    }

    private static async Task ParallelRun()
    {

        Console.WriteLine("Parallel call with 200 response");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");
        var stopWatch = Stopwatch.StartNew();

        var youtubeSubscribers = ApiCalls.GetYoutubeSubscribers(httpClient);
        var twitterFollowers = ApiCalls.GetTwitterFollowers(httpClient);
        var githubFollowers = ApiCalls.GetGithubFollowers(httpClient);

        await Task.WhenAll(youtubeSubscribers, twitterFollowers, githubFollowers);

        Console.WriteLine($"Done is {stopWatch.ElapsedMilliseconds} ms");

        // Note that after WhenAll, it is safe to use Result
        dynamic userProfile = new
        {
            Name = "Tom",
            YoutubeSubscribers = youtubeSubscribers.Result,
            TwitterFollowers = twitterFollowers.Result,
            GetGithubFollowers = githubFollowers.Result
        };

        Console.WriteLine(userProfile.ToString());
    }

    private static async Task SequentialRunWithException()
    {
        Console.WriteLine("Sequential API call with 500 response");

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5052");
        var stopWatch = Stopwatch.StartNew();

        // exception is handled by a null result but can be a http code other than 200
        var youtubeSubscribers = await ApiCalls.GetYoutubeSubscribersUnauthorized(httpClient);
        var twitterFollowers = await ApiCalls.GetTwitterFollowers(httpClient);
        var githubFollowers = await ApiCalls.GetGithubFollowersWithException(httpClient);

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

        Task allResultTask = null;

        var youtubeSubscribers = ApiCalls.GetYoutubeSubscribers(httpClient);
        var twitterFollowers = ApiCalls.GetTwitterFollowersWithException(httpClient);
        var githubFollowers = ApiCalls.GetTwitterFollowersWithBadRequest(httpClient);

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