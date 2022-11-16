using Elsheimy.Components.RemoteApi.Formatters;
using Elsheimy.Components.RemoteApi.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Elsheimy.Components.RemoteApi
{
    /// <summary>
    /// Base for remote services
    /// </summary>
    public abstract class RemoteService
    {
        /// <summary>
        /// Service base address
        /// </summary>
        public abstract string BaseAddress { get; }
        /// <summary>
        /// Param (query/headers/body) provider. Default is <see cref="Elsheimy.Components.RemoteApi.ParamProvider"/>
        /// </summary>
        public ParamProvider ParamProvider { get; set; }
        /// <summary>
        /// Request and response encoding
        /// </summary>
        public Encoding Encoding { get; set; } = System.Text.Encoding.UTF8;
        /// <summary>
        /// Request and response formatter. Default is <see cref="Elsheimy.Components.RemoteApi.Formatters.JsonFormatter"/>.
        /// </summary>
        public FormatterBase Formatter { get; set; }


        public RemoteService()
        {
            this.ParamProvider = new ParamProvider();
            this.Formatter = new JsonFormatter();
        }

        /// <summary>
        /// Generates URL for the given request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual string GenerateRequestUrl(RemoteRequest req)
        {
            string url = UrlHelper.Concat(this.BaseAddress, req.EndpointAddress);

            url += "?";

            var queryParameters =
              ParamProvider.ExtractQueryParameters(req).Concat(ParamProvider.ExtractQueryParameters(this))
              .Distinct(new ParameterNameComparer());

            url += QueryHelper.GenerateQueryString(queryParameters);

            return url;
        }

        /// <summary>
        /// Returns formatted body of the given request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual string GetFormattedBody(RemoteRequest req)
        {
            object body = ParamProvider.ExtractBodyParameter(req);
            if (null == body)
                body = ParamProvider.ExtractBodyParameter(this);

            if (null == body)
                return null;

            string bodyStr = null;
            if (null != Formatter)
                bodyStr = Formatter.Format(body);
            else
                bodyStr = string.Empty;

            return bodyStr;
        }

        /// <summary>
        /// Returns headers list for the given request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual IEnumerable<Parameter> GetHeaders(RemoteRequest req)
        {
            IEnumerable<Parameter> headers =
              ParamProvider.ExtractHeaderParameters(req).Concat(ParamProvider.ExtractHeaderParameters(this));

            return headers.Distinct(new ParameterNameComparer());
        }

        /// <summary>
        /// Sends a request asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public Task<T> SendRequestAsync<T>(RemoteRequest req) where T : RemoteResponse
        {
            return Task.Run<T>(() =>
            {
                var response = SendRequest<T>(req);
                return response;
            });
        }

        /// <summary>
        /// Sends a request synchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public T SendRequest<T>(RemoteRequest req) where T : RemoteResponse
        {
            string url = GenerateRequestUrl(req);
            IEnumerable<Parameter> headers = GetHeaders(req);

            string body = GetFormattedBody(req);

            string responseStr = SendRequest(url, req.Method.Method, this.Formatter?.ContentTypeHeader, headers, body);

            if (responseStr.Length > 0)
            {
                object response = Formatter.FormatBack(responseStr, typeof(T));
                return (T)response;
            }
            else
                return default(T);
        }

        /// <summary>
        /// Internal implementation of send request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        protected virtual string SendRequest(string url, string method, string contentType, IEnumerable<Parameter> headers, string body)
        {
            WebRequest webReq = HttpWebRequest.Create(url);

            webReq.Method = method;
            if (null != contentType)
                webReq.ContentType = contentType;

            if (null != headers)
                foreach (var param in headers)
                    webReq.Headers.Add(param.Name, param.Value);

            if (null != body)
            {
                using (Stream stm = webReq.GetRequestStream())
                using (StreamWriter writer = new StreamWriter(stm, Encoding))
                {
                    writer.Write(body);
                }
            }

            byte[] data = null;
            using (WebResponse response = webReq.GetResponse())
            using (Stream stm = response.GetResponseStream())
            {
                data = new byte[response.ContentLength];
                stm.Read(data, 0, (int)response.ContentLength);
            }

            return this.Encoding.GetString(data);
        }
    }
}
