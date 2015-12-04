using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;

namespace SensingSite.ClientSDK.Common
{

    public class HttpReqUtility
    {
        private static readonly Encoding DEFAULTENCODE = Encoding.UTF8;
        public static readonly ILog log = LogManager.GetLogger(typeof(HttpReqUtility));
        public static readonly string HttpCallErrorFlag = "-1111";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns>HttpCallErrorFlag:GET异常，</returns>
        public static string GetResponseText(string Url)
        {
            string responseText = String.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);
                request.Method = "GET";
                request.Timeout = 10000;
                //SetBasicAuthHeader(request, "JohnControl", "JohnControl");

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream responseStream = response.GetResponseStream();
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responseText = reader.ReadToEnd();
                        }
                    }
                }
                return responseText;
            }
            catch (Exception ex)
            {
                log.Error(Url, ex);
                return HttpCallErrorFlag;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns>HttpCallErrorFlag:POST异常，</returns>
        public static string PostRequest(string Url, string param)
        {
            string responseText = String.Empty;

            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);
                request.ServicePoint.Expect100Continue = false;
                request.Method = "POST";
                request.Timeout = 10000;
                //SetBasicAuthHeader(request, "JohnControl", "JohnControl");

                byte[] bs = Encoding.ASCII.GetBytes(param);
                string responseData = String.Empty;
                request.ContentType = "application/json";
                request.ContentLength = bs.Length;

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream responseStream = response.GetResponseStream();
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responseText = reader.ReadToEnd();
                        }
                    }
                }
                return responseText;
            }
            catch (Exception)
            {
                return HttpCallErrorFlag;
            }
        }


        public static bool ResponseFailed(string response)
        {
            return response == HttpReqUtility.HttpCallErrorFlag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns>HttpCallErrorFlag:POST异常，</returns>
        public static string PostFormRequest(string Url, string param)
        {
            string responseText = String.Empty;

            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);
                request.ServicePoint.Expect100Continue = false;
                request.Method = "POST";
                request.Timeout = 10000;
                //SetBasicAuthHeader(request, "JohnControl", "JohnControl");

                byte[] bs = Encoding.ASCII.GetBytes(param);
                string responseData = String.Empty;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bs.Length;

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream responseStream = response.GetResponseStream();
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responseText = reader.ReadToEnd();
                        }
                    }
                }
                return responseText;
            }
            catch (Exception)
            {
                return HttpCallErrorFlag;
            }
        }
        public static string ComposeParameterizedUrl(string baseUrl, Dictionary<string, string> paramPairs)
        {
            Encoding encoding = Encoding.UTF8;
            StringBuilder paramsUrl = new StringBuilder(baseUrl);
            paramsUrl.Append("?");
            int count = 1;

            foreach (var key in paramPairs.Keys)
            {
                paramsUrl.Append(key);
                paramsUrl.Append("=");
                paramsUrl.Append(HttpUtility.UrlEncode(paramPairs[key], encoding));

                if (count < paramPairs.Keys.Count)
                {
                    paramsUrl.Append("&");
                }
                count++;
            }

            return paramsUrl.ToString();

        }

        private static void SetBasicAuthHeader(WebRequest req, String userName, String userPassword)
        {
            string authInfo = String.Format("{0}:{1}", userName, userPassword);
            authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            req.Headers["Authorization"] = "Basic " + authInfo;
        }


        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string file, NameValueCollection data)
        {
            return HttpUploadFile(url, file, data, DEFAULTENCODE);
        }

        public static string HttpUploadFile(string url, string file, string fileName, NameValueCollection data)
        {
            return HttpUploadFile(url, file, fileName, data, DEFAULTENCODE);
        }

        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string file, NameValueCollection data, Encoding encoding)
        {
            return HttpUploadFile(url, new string[] { file }, data, encoding);
        }

        public static string HttpUploadFile(string url, string file,string fileName, NameValueCollection data, Encoding encoding)
        {
            return HttpUploadFile(url, new string[] { file }, new string[] { fileName }, data, encoding);
        }

        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string[] files, NameValueCollection data)
        {
            return HttpUploadFile(url, files, data, DEFAULTENCODE);
        }

        public static string HttpUploadFile(string url, string[] files,string[] fileNames, NameValueCollection data)
        {
            return HttpUploadFile(url, files, fileNames, data, DEFAULTENCODE);
        }

        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string[] files, NameValueCollection data, Encoding encoding)
        {
            try {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

                //1.HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                using (Stream stream = request.GetRequestStream())
                {
                    //1.1 key/value
                    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    if (data != null)
                    {
                        foreach (string key in data.Keys)
                        {
                            stream.Write(boundarybytes, 0, boundarybytes.Length);
                            string formitem = string.Format(formdataTemplate, key, data[key]);
                            byte[] formitembytes = encoding.GetBytes(formitem);
                            stream.Write(formitembytes, 0, formitembytes.Length);
                        }
                    }

                    //1.2 file
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    for (int i = 0; i < files.Length; i++)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string header = string.Format(headerTemplate, "file", Path.GetFileName(files[i]));
                        //string header = string.Format(headerTemplate, "file" + i, Path.GetFileName(files[i]));
                        byte[] headerbytes = encoding.GetBytes(header);
                        stream.Write(headerbytes, 0, headerbytes.Length);
                        using (FileStream fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                        {
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                stream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }

                    //1.3 form end
                    stream.Write(endbytes, 0, endbytes.Length);
                }
                //2.WebResponse
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    return stream.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                return HttpCallErrorFlag;
            }
        }


        /// <summary>
        /// HttpUploadFile
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpUploadFile(string url, string[] files,string[] filesNames, NameValueCollection data, Encoding encoding)
        {
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

                //1.HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                using (Stream stream = request.GetRequestStream())
                {
                    //1.1 key/value
                    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    if (data != null)
                    {
                        foreach (string key in data.Keys)
                        {
                            stream.Write(boundarybytes, 0, boundarybytes.Length);
                            string formitem = string.Format(formdataTemplate, key, data[key]);
                            byte[] formitembytes = encoding.GetBytes(formitem);
                            stream.Write(formitembytes, 0, formitembytes.Length);
                        }
                    }

                    //1.2 file
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    for (int i = 0; i < files.Length; i++)
                    {
                        var fileName = "file";
                        if (i < filesNames.Length)
                        {
                            fileName = filesNames[i];
                        }
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string header = string.Format(headerTemplate, fileName, Path.GetFileName(files[i]));
                        //string header = string.Format(headerTemplate, "file" + i, Path.GetFileName(files[i]));
                        byte[] headerbytes = encoding.GetBytes(header);
                        stream.Write(headerbytes, 0, headerbytes.Length);
                        using (FileStream fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                        {
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                stream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }

                    //1.3 form end
                    stream.Write(endbytes, 0, endbytes.Length);
                }
                //2.WebResponse
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    return stream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return HttpCallErrorFlag;
            }
        }
    }
}
