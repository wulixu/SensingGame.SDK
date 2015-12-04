using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;
using System.IO;
using System.Net;
using System.Reflection;

namespace SensingGame.ClientSDK.Common
{
    public class MediaType
    {
        public static string Json = "application/json";
        public static string Xml = "application/xml";
        public static string Html = "text/html";
    }
    public class HttpClientHelper :IDisposable
    {
        public static TimeSpan TIME_OUT = TimeSpan.FromSeconds(10);
        private HttpClient httpClient;
        public HttpClientHelper() 
        {
            //HttpClientHandler hch = new HttpClientHandler();
            //hch.Credentials = new NetworkCredential("wulixu@troncell.com", "1qaz@WSX");
            //httpClient = new HttpClient(hch);
            httpClient = new HttpClient();
            httpClient.Timeout = TIME_OUT;
        }


        public async Task<string> PostInfo(string serviceAddress, string jsonStr)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.Content = new StringContent(jsonStr);
                    
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    
                request.RequestUri = new Uri(serviceAddress);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Method = HttpMethod.Post;

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var returnValue = await response.Content.ReadAsStringAsync();
                return returnValue;
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }

        public async Task<string> GetInfo(string serviceAddress)
        {
            try
            {
                //var returnValue = HttpReqUtility.GetResponseText(serviceAddress);
                //var request = HttpWebRequest.Create(serviceAddress);
                //var response = await request.GetResponseAsync();

                //string uri = "http://www.windows.com/";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("http://118.242.1.146:8090/storm/backgroundinfo/getallbackground");

                //var request = new HttpRequestMessage();
                //request.Content = new StringContent(contentEchoed);
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //request.RequestUri = new Uri(serviceAddress);
                //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //request.Method = HttpMethod.Get;

                //var returnValue = await httpClient.GetStringAsync(BaseUrl +"/getdefaultname");

                //var response = await httpClient.GetAsync(serviceAddress);

                // Check that response was successful or throw exception
                response.EnsureSuccessStatusCode();

                var returnValue = await response.Content.ReadAsStringAsync();
                return returnValue;
             }
            catch (Exception exception)
            {
                return "Error:cannot connect internet.";
            }
            //HttpClient client = new HttpClient();
            //string content = client.GetStringAsync(serviceAddress).Result;
            //return content;
        }

        public  async Task<bool> PostForm(string url, IDictionary<string,string> dic)
        {
            try
            {
                FormUrlEncodedContent form =
                    new FormUrlEncodedContent(dic.Select(pair =>
                        new KeyValuePair<string, string>(pair.Key, pair.Value)));
                HttpResponseMessage response = await httpClient.PostAsync(url, form);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
            return true;
        }

       
        public  async Task<bool> PostFile(string url,string name ,string fileName)
        {
            httpClient.Timeout = TIME_OUT;
            try
            {
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                using (FileStream stream = File.OpenRead(fileName))
                {
                    form.Add(new StreamContent(stream), name, fileName);
                    HttpResponseMessage response = await httpClient.PostAsync(url, form);
                    response.EnsureSuccessStatusCode();

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
            return true;
        }

        public async Task<string> PostFormFile(string url, string name, string fileName)
        {
            httpClient.Timeout = TIME_OUT;
            try
            {
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                using (FileStream stream = File.OpenRead(fileName))
                {
                    form.Add(new StreamContent(stream), name, fileName);
                    HttpResponseMessage response = await httpClient.PostAsync(url, form);
                    response.EnsureSuccessStatusCode();


                    var returnValue = await response.Content.ReadAsStringAsync();
                    return returnValue;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "Error:cannot connect internet.";
            }
        }


        public void Dispose()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
            }
        }
    }
}
