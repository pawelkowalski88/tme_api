using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace TME_API_v1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TME API Start\n");
            List<String> ProductIdentifiers = new List<string>() { "NE555D", "1N4007-DC", "16CTU04PBF", "1N4001-DCO" };
            Task<GetPricesAndStock.response> ResponseTask = Task.Run(() => APIGetPrices(ProductIdentifiers));
            PrintResults(ResponseTask.Result);
            Console.ReadKey();
        }

        static async Task<GetPricesAndStock.response> APIGetPrices(List<String> products)
        {
            //Set the API URL, security and creating the HTTP client
            string Url = "https://api.tme.eu/Products/GetPricesAndStocks.xml";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient MainClient = new HttpClient();

            //Converting the product list into http parameters
            List<KeyValuePair<string, string>> ParamList = CreateParametersList(products);

            //Generating the signature
            byte[] key = Encoding.ASCII.GetBytes("9cdcef850cfbdfa01b26");
            byte[] input = Encoding.ASCII.GetBytes(CreateSignatureFromParameters(Url, ParamList));
            string Signature = EncodeSignature(input, key);

            //Adding the signature to the parameter list
            ParamList.Add(new KeyValuePair<string, string>("ApiSignature", Signature));

            try
            {
                //Executing the HTTP POST request and receiving the answer.
                FormUrlEncodedContent Content = new FormUrlEncodedContent(ParamList);
                HttpResponseMessage HttpsResponse = await MainClient.PostAsync(Url, Content);
                //String res = await HttpsResponse.Content.ReadAsStringAsync();
                //Console.WriteLine(res);
                return SerializeResponseContent(await HttpsResponse.Content.ReadAsStreamAsync());
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        static string EncodeSignature(byte[] signaturebase, byte[] secret)
        {
            byte[] hashValue;
            using (HMACSHA1 hmac = new HMACSHA1(secret))
            {
                hashValue = hmac.ComputeHash(signaturebase);
            }
            return Convert.ToBase64String(hashValue);
        }

        static GetPricesAndStock.response SerializeResponseContent(Stream content)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GetPricesAndStock.response));
            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
            GetPricesAndStock.response Response = (GetPricesAndStock.response)serializer.Deserialize(content);
            return Response;
        }

        static List<KeyValuePair<string, string>> CreateParametersList(List<string> parameterList)
        {
            List<KeyValuePair<string, string>> ParamList = new List<KeyValuePair<string, string>>();
            foreach (var p in parameterList)
            {
               string SymbolIdentifier = String.Format("SymbolList[{0}]", ParamList.Count);
                ParamList.Add(new KeyValuePair<string, string>(SymbolIdentifier.ToString(), p));
            }

            ParamList.Add(new KeyValuePair<string, string>("Country", "PL"));
            ParamList.Add(new KeyValuePair<string, string>("Currency", "PLN"));
            ParamList.Add(new KeyValuePair<string, string>("Language", "PL"));
            ParamList.Add(new KeyValuePair<string, string>("Token", "dc26fdaad5a1bbcff9858b2161f54fe964c94b43e7a25"));

            ParamList.Sort((x, y) => x.Key.CompareTo(y.Key));
            return ParamList;
        }

        static string CreateSignatureFromParameters(string url, List<KeyValuePair<string, string>> paramList)
        {
            StringBuilder EncodedParameters = new StringBuilder();
            foreach (var p in paramList)
            {
                if (paramList.IndexOf(p) != 0)
                {
                    EncodedParameters.Append("&");
                }
                EncodedParameters.Append(WebUtility.UrlEncode(p.Key));
                EncodedParameters.Append("=");
                EncodedParameters.Append(WebUtility.UrlEncode(p.Value));
            }

            StringBuilder SignatureBase = new StringBuilder();
            SignatureBase.Append("POST&");
            SignatureBase.Append(WebUtility.UrlEncode(url));
            SignatureBase.Append("&");
            SignatureBase.Append(WebUtility.UrlEncode(EncodedParameters.ToString()));

            return SignatureBase.ToString();
        }

        static void PrintResults(GetPricesAndStock.response response)
        {
            if (response == null) return;
            foreach (var p in response.Data.ProductList)
            {
                Console.WriteLine(p.Symbol);
                Console.WriteLine();

                foreach (var price in p.PriceList)
                {
                    Console.WriteLine(price.Amount + "; " + price.PriceValue);
                }
                Console.WriteLine();
                Console.WriteLine(String.Format("W magazynie: {0}", p.Amount.ToString()));
                Console.WriteLine("\n");
            }
        }

        protected static void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        protected static void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}
