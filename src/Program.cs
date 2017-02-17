using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using STS_Zpr.ServiceHost;

namespace SSLKlient
{
    class Program
    {
        public static bool OnValidationCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        static void Main(string[] args)
        {
            Trace.TraceInformation("Custom, started");
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnValidationCallback);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.FindServicePoint(new Uri(ConfigurationManager.AppSettings["SyncExtSSL"])).MaxIdleTime = 3600000;

            //ServicePointManager.SetTcpKeepAlive(true, 60*1000, 30);
           ServicePointManager.SecurityProtocol =SecurityProtocolType.Tls;//(SecurityProtocolType)3072; //SecurityProtocolType.Tls12;
            var message = new eHtalkMessage
            {
                EnvelopeVersion = "2.0",
                Header =
                    new eHtalkMessageHeader
                    {
                        MessageInfo =
                            new eHtalkMessageHeaderMessageInfo
                            {
                                Class = "Ping",
                                MessageID = Guid.NewGuid().ToString("D"),
                                CorrelationID = Guid.NewGuid().ToString("D"),
                                BusinessID = "",
                                ChannelInfoReply = ""
                            },
                        SenderInfo =
                            new eHtalkMessageHeaderSenderInfo
                            {
                                SecurityToken = new eHtalkMessageHeaderSenderInfoSecurityToken
                                {
                                },
                                UserContext = new UserContext
                                {
                                    IdentifikatorOUPZS =
                                        new UserContextIdentifikatorOUPZS
                                        {
                                            rootOID =
                                                "1.3.158.00165387.100.40.70",
                                            extension = "00000000000"
                                        },
                                    Specialization =
                                        new UserContextSpecialization
                                        {
                                            codeValue = "00000000000",
                                            codingSchemeOID =
                                                "1.3.158.00165387.100.10.34",
                                            codingSchemeVersion = "1"
                                        }
                                }
                            },
                    },
            };
            message.Body = new eHtalkMessageBody
            {
                Data = new eHtalkMessageBodyData
                {
                },
                Result = new eHtalkMessageBodyResult
                {
                    Code = "0"
                }
            };

            var stopw = new Stopwatch();
            stopw.Start();

            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);


            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection zcollection = (X509Certificate2Collection)fcollection.Find(X509FindType.FindByIssuerName, "NCZI PreProd HPRO Authentication CA R1-1", false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(zcollection, "Test Certificate Select", "Select a certificate from the following list to get information on that certificate", X509SelectionFlag.SingleSelection);
            Console.WriteLine("Number of certificates: {0}{1}", scollection.Count, Environment.NewLine);


            var result = CallHelper.GetResponseSync(message,
                new X509Certificate2(Assembly.GetExecutingAssembly().GetManifestResourceStream("SSLKlient.services_preprod_npz_sk.cer").ReadAllBytes()),
                ConfigurationManager.AppSettings["SyncExtSSL"],
                ConfigurationManager.AppSettings["TargetServiceIdentifier"],
                String.Format("{0}{1}", ConfigurationManager.AppSettings["IamStsServiceBaseUri"], "Trust/2005/CertificateTransport/"),
                scollection[0],
                ConfigurationManager.AppSettings["SyncExt"], stopw);
            
            var ser = new XmlSerializer(typeof(eHtalkMessage));
            var sw =new StringWriter();
            ser.Serialize(sw, result);
            Console.WriteLine(result.Body.Result.Code);
            Console.WriteLine(string.Format("Mam odpoved od ESB: {0}", stopw.ElapsedMilliseconds));
            Console.ReadLine();
        }
    }
}
