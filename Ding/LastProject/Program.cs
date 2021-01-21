using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LastProject
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder Sb1 = new StringBuilder();
            Sb1.Append("<Request>");
            Sb1.Append("<Header>");
            Sb1.Append("<Identifier>0</Identifier>");
            Sb1.Append("<MessageDate>" + DateTime.Now + "</MessageDate>");
            Sb1.Append("<MessageTime>" + DateTime.Now.TimeOfDay + "</MessageTime>");
            Sb1.Append("</Header>");
            Sb1.Append("<Body>");
            Sb1.Append("<MessageID>332526</MessageID>");
            Sb1.Append("<PhoneNumber>630000000000</PhoneNumber>");
            Sb1.Append("<Amount>25</Amount>");
            const string Value = "</Body>";
            Sb1.Append(Value);
            Sb1.Append("</Request>");
            string X1 = "http://www.contoso.com/PostAccepter.aspx";
            string Strrequestxml1 = Sb1.ToString();
            string reponsestr1;
            reponsestr1 = postXMLData(X1, Strrequestxml1);
            //GetRequest("http://www.contoso.com/PostAccepter.aspx");
            //PostRequest("http://localhost:8080/ding/post.html");
            //Console.ReadKey();
            //Console.WriteLine(reponsestr1);

            StringBuilder Sb2 = new StringBuilder();
            Sb2.Append("<Request>");
            Sb2.Append("<Header>");
            Sb2.Append("<Identifier>0</Identifier>");
            Sb2.Append("<MessageDate>" + DateTime.Now + "</MessageDate>");
            Sb2.Append("<MessageTime>" + DateTime.Now.TimeOfDay + "</MessageTime>");
            Sb2.Append("</Header>");
            Sb2.Append("<Body>");
            Sb2.Append("<MessageID>332527</MessageID>");
            Sb2.Append("<PhoneNumber>639999999999</PhoneNumber>");
            Sb2.Append("<Amount>25</Amount>");
            Sb2.Append("</Body>");
            Sb2.Append("</Request>");
            string X2 = "http://www.contoso.com/PostAccepter.aspx";
            string Strrequestxml2 = Sb2.ToString();
            string reponsestr2;
            reponsestr2 = postXMLData(X2, Strrequestxml2);
            Console.WriteLine(reponsestr2);
        }
        async static void GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;
                        Console.WriteLine(mycontent);
                    }
                }
            }
        }

        async static void PostRequest(string url)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("query1","Ucchwas"),
                new KeyValuePair<string, string>("query2","Talukder")
            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url,q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;
                        Console.WriteLine(mycontent);
                    }
                }
            }
        }
        public static string postXMLData(string destinationUrl, string requestXml)
        {
            //string xml = "&lt;?xml version='1.0' encoding='UTF-8'?><request><header><userId>*GUEST</userId><password>guest</password></header><body><command>searchJobs</command><parms><parm>100</parm><parm>Open</parm><parm></parm><parm></parm><parm></parm><parm></parm><parm></parm><parm></parm></parms></body></request>";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            //string postData = "This is a test that posts this string to a Web server.";
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            //bytes = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "text/xml; encoding=utf-8";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                //Console.WriteLine(responseStr);
                return responseStr;
            }
            return null;
        }
    }
}
