using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using System.Collections.Specialized;
using LogService;
using SensingStore.ClientSDK;
using SensingStore.ClientSDK.Contract;

namespace SesingStore.ClientSDK
{
    public class SensingStoreServiceClient : ISensingStoreServiceClient
    {

        public const string ServerBase = "http://store.troncell.com/";
        //public const string ServerBase = "http://wx.troncell.com/";
        //public const string ServerBase = "http://localhost:4469.troncell.com/";

        public const string SignalRPath = ServerBase + "signalr";
        private const string ServiceHost = ServerBase + "api/ProductApi";

        /// <summary>
        /// The json header
        /// </summary>
        private const string JsonHeader = "application/json";

        /// <summary>
        /// The GetDeviceAuth.
        /// </summary>
        private const string GetDeviceForAuth = "GetDeviceForAuth";

        private const string GetProduct = "GetProducts";

        #region Inner Keys.
        /// <summary>
        /// The subscription key.
        /// </summary>
        private string _subscriptionKey;
        private string _mac;
        #endregion

        private static readonly IBizLogger logger = ServerLogFactory.GetLogger(typeof(SensingStoreServiceClient));

        /// <summary>
        /// The default resolver.
        /// </summary>
        private static CamelCasePropertyNamesContractResolver s_defaultResolver = new CamelCasePropertyNamesContractResolver();

        private static JsonSerializerSettings s_settings = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = s_defaultResolver
        };

        private static HttpClient s_httpClient = new HttpClient();

        static SensingStoreServiceClient()
        {
            s_httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public SensingStoreServiceClient(string subscriptionKeyForActivity,string mac)
        {
            this._subscriptionKey = subscriptionKeyForActivity;
            this._mac = mac;
        }

        public async Task<DeviceResult> GetDevice()
        { 
            var absolutePath = $"{ServiceHost}/{GetDeviceForAuth}";
            var formNameValues = $"{GetBasicFormNameValues()}";
            try
            {
                var deviceResult = await SendRequestAsync<string, DeviceResult>(HttpMethod.Post, absolutePath, formNameValues);
                return deviceResult;
            }
            catch (Exception ex)
            {
                logger.Error("GetDevice", ex);
            }
            return default(DeviceResult);
        }

        public async Task<ProductResult> GetProducts()
        {
            var absolutePath = $"{ServiceHost}/{GetProduct}";
            var formNameValues = $"{GetBasicFormNameValues()}";
            try
            {
                var productResult = await SendRequestAsync<string, ProductResult>(HttpMethod.Post, absolutePath, formNameValues);
                return productResult;
            }
            catch (Exception ex)
            {
                logger.Error("GetProducts", ex);
            }
            return default(ProductResult);
        }


        #region the json client
        private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(HttpMethod httpMethod, string requestUrl, TRequest requestBody)
        {
            var request = new HttpRequestMessage(httpMethod, ServiceHost);
            request.RequestUri = new Uri(requestUrl);
            if (requestBody != null)
            {
                if (requestBody is Stream)
                {
                    request.Content = new StreamContent(requestBody as Stream);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                }
                else if (requestBody is string)
                {
                    request.Content = new StringContent(requestBody as string);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                }
                else
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestBody, s_settings), Encoding.UTF8, JsonHeader);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
            }

            HttpResponseMessage response = await s_httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = null;
                if (response.Content != null)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }

                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    return JsonConvert.DeserializeObject<TResponse>(responseContent, s_settings);
                }

                return default(TResponse);
            }
            else
            {
                if (response.Content != null && response.Content.Headers.ContentType.MediaType.Contains(JsonHeader))
                {
                    var errorObjectString = await response.Content.ReadAsStringAsync();
                    ClientError errorCollection = JsonConvert.DeserializeObject<ClientError>(errorObjectString);
                    if (errorCollection != null)
                    {
                        throw new ClientException(errorCollection, response.StatusCode);
                    }
                }

                response.EnsureSuccessStatusCode();
            }

            return default(TResponse);
        }

        private async Task<TResponse> SendMultipartFormRequestAsync<TResponse>(string requestUrl, string[] files, string[] names, NameValueCollection data)
        {

            using (MultipartFormDataContent form = new MultipartFormDataContent(("Upload----" + DateTime.Now.Ticks.ToString())))
            {
                //1.1 key/value
                foreach (string key in data.Keys)
                {
                    //Content-Disposition : form-data; name="json".
                    var stringContent = new StringContent(data[key]);
                    stringContent.Headers.Add("Content-Disposition", $"form-data; name={key}");
                    form.Add(stringContent, key);
                }

                //1.2 file
                for (int index = 0; index < files.Length; index++)
                {
                    var filePath = files[index];
                    FileStream stream = File.OpenRead(filePath);
                    var streamContent = new StreamContent(stream);
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    streamContent.Headers.Add("Content-Disposition", $"form-data; name={names[index]}; filename={Path.GetFileName(filePath)}");
                    form.Add(streamContent, names[index], Path.GetFileName(filePath));
                }

                HttpResponseMessage response = await s_httpClient.PostAsync(requestUrl, form);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = null;
                    if (response.Content != null)
                    {
                        responseContent = await response.Content.ReadAsStringAsync();
                    }
                    if (!string.IsNullOrWhiteSpace(responseContent))
                    {
                        return JsonConvert.DeserializeObject<TResponse>(responseContent, s_settings);
                    }
                    return default(TResponse);
                }
                else
                {
                    if (response.Content != null && response.Content.Headers.ContentType.MediaType.Contains(JsonHeader))
                    {
                        var errorObjectString = await response.Content.ReadAsStringAsync();
                        ClientError errorCollection = JsonConvert.DeserializeObject<ClientError>(errorObjectString);
                        if (errorCollection != null)
                        {
                            throw new ClientException(errorCollection, response.StatusCode);
                        }
                    }

                    response.EnsureSuccessStatusCode();
                }
                return default(TResponse);
            }
        }


        private string GetBasicFormNameValues()
        {
            return $"mac={_mac}&subscriptionKey={_subscriptionKey}";
        }

        private void AddBasicNameValues(NameValueCollection collections)
        {
            collections.Add("clientUniueId", _mac);
            collections.Add("subscriptionKey", _subscriptionKey);
        }
        #endregion

    }
}
