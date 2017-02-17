using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;

namespace SSLKlient
{
    [ServiceContract(ConfigurationName = "IeHealthSyncService", Namespace = "http://eHealth.gov.sk/eHtalk", ProtectionLevel = ProtectionLevel.Sign)]
    public interface IeHealthSyncService
    {
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [OperationContract(Action = "http://eHealth.gov.sk/eHtalk/IeHealthSyncService/Receive", ReplyAction = "http://eHealth.gov.sk/eHtalk/IeHealthSyncService/ReceiveResponse")]
        [XmlSerializerFormat]
        eHtalkMessage GetData(eHtalkMessage value);
    }

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [XmlRoot(IsNullable = false, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [DesignerCategory("code")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [Serializable]
    public class eHtalkMessage
    {
        private string envelopeVersionField;
        private eHtalkMessageHeader headerField;
        private eHtalkMessageBody bodyField;
        private XmlElement anyField;

        public string EnvelopeVersion
        {
            get
            {
                return this.envelopeVersionField;
            }
            set
            {
                this.envelopeVersionField = value;
            }
        }

        public eHtalkMessageHeader Header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        public eHtalkMessageBody Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }

        [XmlAnyElement]
        public XmlElement Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [DesignerCategory("code")]
    [DebuggerStepThrough]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [Serializable]
    public class eHtalkMessageBody
    {
        private eHtalkMessageBodyData dataField;
        private eHtalkMessageBodyResult resultField;

        public eHtalkMessageBodyData Data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }

        public eHtalkMessageBodyResult Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [DesignerCategory("code")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [DebuggerStepThrough]
    [Serializable]
    public class eHtalkMessageBodyResult
    {
        private string codeField;
        private string messageField;
        private eHtalkMessageBodyResultDetails detailsField;

        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        public eHtalkMessageBodyResultDetails Details
        {
            get
            {
                return this.detailsField;
            }
            set
            {
                this.detailsField = value;
            }
        }
    }

    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class eHtalkMessageBodyResultDetails
    {
        private XmlElement[] anyField;

        [XmlAnyElement]
        public XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class eHtalkMessageBodyData
    {
        private XmlElement[] anyField;

        [XmlAnyElement]
        public XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [DesignerCategory("code")]
    [DebuggerStepThrough]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [Serializable]
    public class eHtalkMessageHeader
    {
        private eHtalkMessageHeaderMessageInfo messageInfoField;
        private eHtalkMessageHeaderSenderInfo senderInfoField;

        public eHtalkMessageHeaderMessageInfo MessageInfo
        {
            get
            {
                return this.messageInfoField;
            }
            set
            {
                this.messageInfoField = value;
            }
        }

        public eHtalkMessageHeaderSenderInfo SenderInfo
        {
            get
            {
                return this.senderInfoField;
            }
            set
            {
                this.senderInfoField = value;
            }
        }
    }

    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [Serializable]
    public class eHtalkMessageHeaderSenderInfo
    {
        private eHtalkMessageHeaderSenderInfoSecurityToken securityTokenField;
        private UserContext userContextField;

        public eHtalkMessageHeaderSenderInfoSecurityToken SecurityToken
        {
            get
            {
                return this.securityTokenField;
            }
            set
            {
                this.securityTokenField = value;
            }
        }

        public UserContext UserContext
        {
            get
            {
                return this.userContextField;
            }
            set
            {
                this.userContextField = value;
            }
        }
    }

    [GeneratedCode("xsd", "4.0.30319.1")]
    [XmlType(Namespace = "http://eHealth.gov.sk/Common/v1")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class UserContext
    {
        private UserContextSpecialization specializationField;
        private UserContextIdentifikatorOUPZS identifikatorOUPZSField;

        public UserContextSpecialization Specialization
        {
            get
            {
                return this.specializationField;
            }
            set
            {
                this.specializationField = value;
            }
        }

        public UserContextIdentifikatorOUPZS IdentifikatorOUPZS
        {
            get
            {
                return this.identifikatorOUPZSField;
            }
            set
            {
                this.identifikatorOUPZSField = value;
            }
        }
    }

    [GeneratedCode("xsd", "4.0.30319.1")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/Common/v1")]
    [Serializable]
    public class UserContextIdentifikatorOUPZS : II
    {
    }

    [XmlType(Namespace = "http://eHealth.gov.sk/Common/v1")]
    [XmlInclude(typeof(PacientSifrovanyIdentifikator))]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class II
    {
        private string extensionField;
        private string rootOIDField;

        public string extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }

        public string rootOID
        {
            get
            {
                return this.rootOIDField;
            }
            set
            {
                this.rootOIDField = value;
            }
        }
    }

    [DesignerCategory("code")]
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://eHealth.gov.sk/Common/v1")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [Serializable]
    public class PacientSifrovanyIdentifikator : II
    {
    }

    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/Common/v1")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class UserContextSpecialization : CV
    {
    }

    [GeneratedCode("xsd", "4.0.30319.1")]
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://eHealth.gov.sk/Common/v1")]
    [DesignerCategory("code")]
    [Serializable]
    public class CV
    {
        private string codingSchemeOIDField;
        private string codingSchemeVersionField;
        private string codeValueField;

        public string codingSchemeOID
        {
            get
            {
                return this.codingSchemeOIDField;
            }
            set
            {
                this.codingSchemeOIDField = value;
            }
        }

        public string codingSchemeVersion
        {
            get
            {
                return this.codingSchemeVersionField;
            }
            set
            {
                this.codingSchemeVersionField = value;
            }
        }

        public string codeValue
        {
            get
            {
                return this.codeValueField;
            }
            set
            {
                this.codeValueField = value;
            }
        }
    }

    [GeneratedCode("xsd", "4.0.30319.1")]
    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [Serializable]
    public class eHtalkMessageHeaderSenderInfoSecurityToken
    {
        private XmlElement[] anyField;

        [XmlAnyElement]
        public XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://eHealth.gov.sk/eHtalkMessage")]
    [GeneratedCode("xsd", "4.0.30319.1")]
    [Serializable]
    public class eHtalkMessageHeaderMessageInfo
    {
        private string classField;
        private string messageIDField;
        private string correlationIDField;
        private string businessIDField;
        private string channelInfoReplyField;

        public string Class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }

        public string MessageID
        {
            get
            {
                return this.messageIDField;
            }
            set
            {
                this.messageIDField = value;
            }
        }

        public string CorrelationID
        {
            get
            {
                return this.correlationIDField;
            }
            set
            {
                this.correlationIDField = value;
            }
        }

        public string BusinessID
        {
            get
            {
                return this.businessIDField;
            }
            set
            {
                this.businessIDField = value;
            }
        }

        public string ChannelInfoReply
        {
            get
            {
                return this.channelInfoReplyField;
            }
            set
            {
                this.channelInfoReplyField = value;
            }
        }
    }
}
