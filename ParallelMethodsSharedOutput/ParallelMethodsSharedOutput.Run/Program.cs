using CallAsyncMethodsParallel.CallApi;
using System.Diagnostics;

public class Program
{
    public static async Task Main(string[] args)
    {
        await SequentialRun();

        await ParallelRun();

        Console.ReadLine();
    }

    private static async Task SequentialRun()
    {
        Console.WriteLine("Sequential API call");

        var stopWatch = Stopwatch.StartNew();

        var client = new Client();

        await client.GetYoutubeSubscribers();
        await client.GetTwitterFollowers();
        await client.GetGithubFollowers();

        Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds} ms");

        foreach (var item in client.FinalResult) 
        {
            Console.WriteLine(item);
        }
    }

    private static async Task ParallelRun()
    {

        Console.WriteLine("Parallel");

        var stopWatch = Stopwatch.StartNew();

        var client = new Client();

        var youtubeSubscribers = client.GetYoutubeSubscribers();
        var twitterFollowers = client.GetTwitterFollowers();
        var githubFollowers = client.GetGithubFollowers();

        await Task.WhenAll(youtubeSubscribers, twitterFollowers, githubFollowers);

        Console.WriteLine($"Done is {stopWatch.ElapsedMilliseconds} ms");

        // Note that after WhenAll, it is safe to use Result
        foreach (var item in client.FinalResult)
        {
            Console.WriteLine(item);
        }
    }
}