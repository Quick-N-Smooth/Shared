using Newtonsoft.Json.Linq;
namespace CallAsyncMethodsParallel.CallApi
{
    internal static class ApiCalls
    {

        #region HappyFlow

        internal static async Task<int?> GetYoutubeSubscribers(HttpClient httpClient)
        {
            try
            {
                var result = await httpClient.GetStringAsync(httpClient.BaseAddress + "youtube200");
                dynamic data = JObject.Parse(result);
                return data.subscribers;
            }
            catch
            {
                return null;
            }
            
        }

        internal static async Task<int?> GetTwitterFollowers(HttpClient httpClient)
        {
            var result = await httpClient.GetStringAsync(httpClient.BaseAddress + "twitter200");
            dynamic data = JObject.Parse(result);
            return data.followers;
        }

        internal static async Task<int?> GetGithubFollowers(HttpClient httpClient)
        {
            var result = await httpClient.GetStringAsync(httpClient.BaseAddress + "github200");
            dynamic data = JObject.Parse(result);
            return data.followers;
        }

        #endregion

        #region Troubles

        internal static async Task<int?> GetYoutubeSubscribersUnauthorized(HttpClient httpClient)
        {
            try
            {
                var result = await httpClient.GetStringAsync(httpClient.BaseAddress + ApiEndpoints.YoutubeUnauthorized);
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
            try { 
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
    }
}
