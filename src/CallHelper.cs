using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
#if !CC
using System.Net;
#endif
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Threading;
#if CC
using Iam.Client;
#endif
using Microsoft.IdentityModel.Protocols.WSTrust;

namespace SSLKlient
{
    public class CallHelper
    {
        public static eHtalkMessage GetResponseSync(eHtalkMessage msg, X509Certificate2 extInterfaCertificate, string esbEndpoint, string relyingParty, string identityProviderURL, X509Certificate2 userCertificate, string dnsIdentity, string wsaddressingTo, Stopwatch stopw)
        {
            Debug.WriteLine("Start", "MyCustom");

#if !CC
            IssuedSecurityTokenProvider provider = new IssuedSecurityTokenProvider();
            provider.SecurityTokenSerializer = new WSSecurityTokenSerializer();
            provider.TargetAddress = new EndpointAddress(new Uri(relyingParty), new AddressHeader[0]);
            provider.IssuerAddress = new EndpointAddress(new Uri(identityProviderURL), new AddressHeader[0]);
            provider.SecurityAlgorithmSuite = SecurityAlgorithmSuite.Basic256;
            provider.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
            ClientCredentials credentials = new ClientCredentials
            {
                ClientCertificate = { Certificate = userCertificate }
            };
            provider.IssuerChannelBehaviors.Add(credentials);

            HttpsTransportBindingElement tbe = new HttpsTransportBindingElement
            {
                AuthenticationScheme = AuthenticationSchemes.Digest,
                RequireClientCertificate = true,
                KeepAliveEnabled = false
            };
            CustomBinding stsBinding = new CustomBinding(new BindingElement[] { tbe });
            provider.IssuerBinding = stsBinding;

            provider.Open();
            var token = provider.GetToken(TimeSpan.FromSeconds(30.0)) as GenericXmlSecurityToken;
#endif
#if CC
            var cc = new EhealthCryptoController();
            var token = cc.GetSamlTokenForHealthProfessional(relyingParty);

#endif
            if (token == null)
            {
                throw new ApplicationException("No AT token received");
            }
            Console.WriteLine(string.Format("Ziskany AT token in {0}", stopw.ElapsedMilliseconds));



            CustomBinding binding = new CustomBinding();
            SecurityBindingElement sbe = SecurityBindingElement.CreateIssuedTokenForCertificateBindingElement(new IssuedSecurityTokenParameters() {RequireDerivedKeys = true,KeyType = SecurityKeyType.SymmetricKey});

            sbe.MessageSecurityVersion =
                MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
            sbe.SecurityHeaderLayout = SecurityHeaderLayout.Strict;
            sbe.IncludeTimestamp = true;
            //sbe.AllowInsecureTransport = true;
            sbe.SetKeyDerivation(true);
            sbe.KeyEntropyMode = SecurityKeyEntropyMode.ClientEntropy;
            binding.Elements.Add(sbe);
            binding.Elements.Add(new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, System.Text.Encoding.UTF8));
            binding.Elements.Add(new HttpsTransportBindingElement() {RequireClientCertificate = true, KeepAliveEnabled = true} );
            var channelFactory = new ChannelFactory<IeHealthSyncService>(binding,
                                                                new EndpointAddress(
                                                                        new Uri(wsaddressingTo),
                                                                    new DnsEndpointIdentity(dnsIdentity),
                                                                    new AddressHeader[] { }));
            channelFactory.Credentials.SupportInteractive = false;
            channelFactory.Credentials.ClientCertificate.Certificate = userCertificate;
            channelFactory.Credentials.ServiceCertificate.DefaultCertificate = extInterfaCertificate;

            channelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode =
                X509CertificateValidationMode.None;
            channelFactory.ConfigureChannelFactory<IeHealthSyncService>();
            channelFactory.Endpoint.Behaviors.Add(new ClientViaBehavior(new Uri(esbEndpoint)));
            var channel = channelFactory.CreateChannelWithIssuedToken(token);
            Console.WriteLine(string.Format("vytvoreny kanal: {0}", stopw.ElapsedMilliseconds));
            var stopw1 =new Stopwatch();

            eHtalkMessage data =null;
            int wait = 1;
            for (int i = 0; i < 20; i++)
            {
                stopw1.Reset();
                stopw1.Start();
                msg.Header.MessageInfo.MessageID = Guid.NewGuid().ToString("D");
                Debug.WriteLine("Start calling", "MyCustom");
                try
                {
                    data = channel.GetData(msg);

                }
                catch (CommunicationException ex)
                {
                    data = channel.GetData(msg);
                }
                Console.WriteLine(string.Format("po {1} sekundach: {0}", stopw1.ElapsedMilliseconds, wait));
                Thread.Sleep(wait * 1000);
                wait = wait * 2;
            }

            return data;
        }

    }


}