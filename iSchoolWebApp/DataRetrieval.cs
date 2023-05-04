using System.Net.Http.Headers;

namespace iSchoolWebApp.utils
{
    public class DataRetrieval
    {
        /*
         * Task - has async/await and a return value
         * THREAD - no direct way to get a callback from a thread :/
         * 
         * the below method will take in a string (like "about/")
         * and return a string of a JSON object
         */

        public async Task<string> GetData(string dataInput)
        {
            // 'using' allows for auto-garbage collect of
            // the very resource-intensive HttpClient
            // at the end, it is auto deleted.

            // we create a new HttpClient because we're working
            // with a server, which has no browser to tell it
            // what a request should be or look like
            using (var client = new HttpClient())
            {
                // a uri is a DATA pointer whereas a url is a webpage!
                client.BaseAddress = new Uri("http://ist.rit.edu/api/");

                // clear out all request headers
                client.DefaultRequestHeaders.Accept.Clear();
                
                // declare that the request's return has to be of type application/json. */* would accept everything
                // without this the browser will treat the response as HTML by default!
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // setup complete!

                try
                {
                    // send request to http://ist.rit.edu/api/about/
                    HttpResponseMessage response = await client.GetAsync(dataInput, HttpCompletionOption.ResponseHeadersRead);

                    // make sure it worked (check that response status code = 200 OK)
                    response.EnsureSuccessStatusCode();

                    // FINALLY go get the damn data!

                    var dataOutput = await response.Content.ReadAsStringAsync();
                    // at this point, data is just a string

                    return dataOutput;
                } 
                
                // specific exception
                catch (HttpRequestException hre)
                {
                    var msg = hre.Message;
                    return "HttpRequest: " + msg;
                }

                // general exception
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return "Exception: " + msg;
                }
            }
        }
    }
}
