using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TestingWebServer.Tools
{
    public class CommonClient
    {
        const string URL = "http://localhost:8080/";
        RestClient Client;
        public bool Reset()
        {
            RestRequest request = new RestRequest("/reset", Method.GET);
            IRestResponse response = Client.Execute(request);
            return (bool)GetResultValue(response.Content, typeof(bool));
        }

        public string Login(string name, string password)
        {
            RestRequest request = new RestRequest(resource: "/login", method: Method.POST);
            request.AddParameter("name", name);
            request.AddParameter("password", password);
            IRestResponse response = Client.Execute(request);
            return (string)GetResultValue(response.Content, typeof(string));
        }

        public bool Create(string token, string name, string password, bool rights)
        {
            RestRequest request = new RestRequest(resource: "/user", method: Method.POST);
            request.AddParameter("token", token);
            request.AddParameter("name", name);
            request.AddParameter("password", password);
            request.AddParameter("rights", rights);
            IRestResponse response = Client.Execute(request);
            return (bool)GetResultValue(response.Content, typeof(bool));
        }

        public bool Logout(string name, string token)
        {
            RestRequest request = new RestRequest(resource: "/logout", method: Method.POST);
            request.AddParameter("name", name);
            request.AddParameter("token", token);
            IRestResponse response = Client.Execute(request);
            return (bool)GetResultValue(response.Content, typeof(bool));
        }

        public string GetUserName(string token)
        {
            RestRequest request = new RestRequest(resource: "/user", method: Method.GET);
            request.AddParameter("token", token);
            IRestResponse response = Client.Execute(request);
            return (string)GetResultValue(response.Content, typeof(string));
        }

        public string GetAllUsers(string token)
        {
            RestRequest request = new RestRequest(resource: "/users", method: Method.GET);
            request.AddParameter("token", token);
            IRestResponse response = Client.Execute(request);
            return (string)GetResultValue(response.Content, typeof(string));
        }

        public bool RemoveUser(string token, string name)
        {
            RestRequest request = new RestRequest(resource: "/user", method: Method.DELETE);
            request.AddParameter("token", token);
            request.AddParameter("name", name);
            IRestResponse response = Client.Execute(request);
            return (bool)GetResultValue(response.Content, typeof(bool));
        }
        public object GetResultValue(string json, Type type)
        {
            string valueName = "content\":";
            string value;
            //if (json == null || json.Length < 1 || !json.Contains("content")
            //    || json.Contains("["))
            //{
            //    throw new CustomParseException($"Failed to find value, json: {json}");
            //}
            value = json.Substring(json.IndexOf(valueName) + valueName.Length);
            value = value.Substring(0, value.IndexOf("}"));
            if (value.Contains("\""))
            {
                if (type == typeof(string))
                {
                    return value.Replace("\"", "");
                }
                //else
                //{
                //    throw new CustomParseException($"Failed to read value, json: {json}");
                //}
            }
            else if (type == typeof(bool))
            {
                return bool.Parse(value);
            }
            else if (type == typeof(int))
            {
                return int.Parse(value);
            }
            else if (type == typeof(long))
            {
                return long.Parse(value);
            }
            //throw new CustomParseException($"Failed to read value, json: {json}");
        }

        class Result<T>
        {
            public T content;
        }

        string GetValue(string json)
        {
            //if (json == null || json.Length < 1 || !json.Contains("content")
            //    || !json.Contains(":") || !json.Contains("}"))
            //{
            //    throw new CustomParseException($"Failed to get value, json: {json}");
            //}
            string value = json.Substring(json.LastIndexOf(':') + 1
                , json.LastIndexOf('}') - json.LastIndexOf(':') - 1);

            return value;
        }
    }
}
