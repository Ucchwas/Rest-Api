using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public class GetPost
{
    public GetPost()
    {
        StringBuilder Sbxml1 = new StringBuilder();
        Sbxml1.Append("<Request>");
        Sbxml1.Append("<Header>");
        Sbxml1.Append("<Identifier>0</Identifier>");
        Sbxml1.Append("<MessageDate>" + DateTime.Now + "</MessageDate>");
        Sbxml1.Append("<MessageTime>" + DateTime.Now.TimeOfDay + "</MessageTime>");
        Sbxml1.Append("</Header>");
        Sbxml1.Append("<Body>");
        Sbxml1.Append("<MessageID>332526</MessageID>");
        Sbxml1.Append("<PhoneNumber>630000000000</PhoneNumber>");
        Sbxml1.Append("<Amount>25</Amount>");
        const string Value = "</Body>";
        Sbxml1.Append(Value);
        Sbxml1.Append("</Request>");
        string X1 = "http://localhost:12138/Default.aspx";
        string Strrequestxml1 = Sbxml1.ToString();
        string reponsestr1;
        reponsestr1 = postXMLData(X1, Strrequestxml1);
        //GetRequest("http://www.contoso.com/PostAccepter.aspx");
        //PostRequest("http://localhost:8080/ding/post.html");
        //Console.ReadKey();
        //Console.WriteLine(reponsestr1);
        //Console.WriteLine("112");

        StringBuilder Sbxml2 = new StringBuilder();
        Sbxml2.Append("<Request>");
        Sbxml2.Append("<Header>");
        Sbxml2.Append("<Identifier>0</Identifier>");
        Sbxml2.Append("<MessageDate>" + DateTime.Now + "</MessageDate>");
        Sbxml2.Append("<MessageTime>" + DateTime.Now.TimeOfDay + "</MessageTime>");
        Sbxml2.Append("</Header>");
        Sbxml2.Append("<Body>");
        Sbxml2.Append("<MessageID>332527</MessageID>");
        Sbxml2.Append("<PhoneNumber>639999999999</PhoneNumber>");
        Sbxml2.Append("<Amount>25</Amount>");
        Sbxml2.Append("</Body>");
        Sbxml2.Append("</Request>");
        string X2 = "http://localhost:12138/Default.aspx";
        string Strrequestxml2 = Sbxml2.ToString();
        string reponsestr2;
        //reponsestr2 = postXMLData(X2, Strrequestxml2);
        //Console.WriteLine(reponsestr2);    
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
            using (HttpResponseMessage response = await client.PostAsync(url, q))
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