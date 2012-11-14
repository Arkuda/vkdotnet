using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;

using ApiCore;

namespace ApiCore
{
    /// <summary>
    /// Class for building url queries to vkontakte api
    /// </summary>
    public class ApiQueryBuilder
    {
        private Dictionary<string, string> paramData;
        //private string query;

        private int apiId;
        private SessionInfo session;
        private string method;

        /// <summary>
        /// Initializes api query builder
        /// </summary>
        /// <param name="apiId">Your desktop api id</param>
        /// <param name="si">Session info, provided by SessionManager</param>
        public ApiQueryBuilder(int apiId, SessionInfo si)
        {
            this.paramData = new Dictionary<string, string>();
            this.apiId = apiId;
            this.session = si;
        }

        /// <summary>
        /// Adds parameters to API request
        /// </summary>
        /// <param name="key">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <returns>Return this</returns>
        public ApiQueryBuilder Add(string key, string value)
        {
            this.paramData.Add(key, value);
            return this;
        }

        /// <summary>
        /// Add method
        /// </summary>
        /// <param name="method">Method name</param>
        /// <param name="xml">Get xml response</param>
        /// <returns>this</returns>
        public ApiQueryBuilder Method(string m, bool xml = false)
        {
            method = m;
            if (xml)
                method += ".xml";

            return this;

        }

        /// <summary>
        /// Clear api parameters
        /// </summary>
        public void Clear()
        {
            this.paramData.Clear();
        }

        /// <summary>
        /// Build query string
        /// </summary>
        /// <returns>Ready query string</returns>
        public string BuildQuery()
        {
            StringBuilder sb = new StringBuilder("https://api.vkontakte.ru/method/");

            sb.Append(method + "?");

            foreach (KeyValuePair<string, string> kp in paramData)
            {
                sb.Append(kp.Key + "=" + kp.Value + "&");
            }
            sb.Append("access_token=" + session.AccessToken);

            return sb.ToString();
        }
    }
}
