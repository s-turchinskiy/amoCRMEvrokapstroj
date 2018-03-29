using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using AmoCRM.Models;
using Newtonsoft.Json;

namespace AmoCRM.Classes
{
	public static class Provider
	{
		public static CookieContainer GetCookieContainer(String url)
		{
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.Method = "POST";
			if (request.CookieContainer == null)
				request.CookieContainer = new CookieContainer();
			request.AllowAutoRedirect = false;

			Log.WriteInfo("Try to get response");
			var response = (HttpWebResponse) request.GetResponse();
			var resStream = response.GetResponseStream();
			var reader = new StreamReader(resStream);
			var json = reader.ReadToEnd();
			var dict = JsonConvert.DeserializeObject<Auth>(json);
			Dictionary<string, string> ress = new Dictionary<string, string>();
			if (dict.response.auth)
			{
				Log.WriteInfo("Set cookies in var");
				foreach (Cookie cookieValue in response.Cookies)
				{
					Log.WriteInfo(cookieValue.ToString());
					ress.Add(cookieValue.Name, cookieValue.Value);
				}
				/*Иногда куки могут быть только в request.CookieContainer)*/
			}
			var cookieCollection = response.Cookies;
			var cookieContainer = new CookieContainer();
			cookieContainer.Add(cookieCollection);

			return cookieContainer;
		}

        public static string SendPOSTResponse(String url, String json, CookieContainer cookieContainer = null)
        {
            var responseText = "";
            try
            {

                var body = Encoding.UTF8.GetBytes(json);
                var charArray = Encoding.UTF8.GetString(body).ToCharArray();

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = @"application/json";
                if (cookieContainer != null)
                {
                    request.CookieContainer = cookieContainer;
                }
                //request.AllowAutoRedirect = false;
                //request.ContentLength = charArray.Length;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(charArray);
                    //streamWriter.Write(charArray, 0, charArray.Length);
                    streamWriter.Close();
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                    //Log.WriteError(responseText);
                    response.Close();
                }
            }
            catch (Exception e)
            {
                var error = "Error. " + e.HResult + ": " + e.Message;
                Log.WriteError(error);
            }

            return responseText;
        }

        public static string SendGetResponse(String url, CookieContainer cookieContainer = null)
		{
			var responseText = "";

			var request = (HttpWebRequest) WebRequest.Create(url);
			request.Method = "GET";
			request.ContentType = @"application/json";
			if (cookieContainer != null)
			{
				request.CookieContainer = cookieContainer;
			}
			request.AllowAutoRedirect = false;

			try
			{
				var response = (HttpWebResponse) request.GetResponse();

				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					responseText = reader.ReadToEnd();
					response.Close();
				}
			}
			catch (Exception e)
			{
				var error = "error. " + e.HResult + ": " + e.Message;
				responseText = "error";
				Log.WriteError(error);
			}

			return responseText;
		}

		public static string GetUrlOfData<T>(String Host, T dataFor1C)
		{
			var urlAsString = Host;

			Type typeDataFor1C = typeof(T);

			var firstElement = "?";
			foreach (var field in typeDataFor1C.GetProperties())
			{
				var fieldValue = field.GetValue(dataFor1C);
				urlAsString += firstElement + field.Name + "=" + fieldValue;
				firstElement = "&";
			}

			return urlAsString;
		}
	}
}
