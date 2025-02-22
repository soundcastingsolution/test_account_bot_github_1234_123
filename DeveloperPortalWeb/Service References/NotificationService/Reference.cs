﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InContact.DeveloperPortal.Web.NotificationService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EmailInfo", Namespace="http://schemas.datacontract.org/2004/07/InContact.EmailNotificationService.Models" +
        "")]
    [System.SerializableAttribute()]
    public partial class EmailInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EmailTemplateIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HtmlBodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] RecipientsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SubjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextBodyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EmailTemplateId {
            get {
                return this.EmailTemplateIdField;
            }
            set {
                if ((this.EmailTemplateIdField.Equals(value) != true)) {
                    this.EmailTemplateIdField = value;
                    this.RaisePropertyChanged("EmailTemplateId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string From {
            get {
                return this.FromField;
            }
            set {
                if ((object.ReferenceEquals(this.FromField, value) != true)) {
                    this.FromField = value;
                    this.RaisePropertyChanged("From");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HtmlBody {
            get {
                return this.HtmlBodyField;
            }
            set {
                if ((object.ReferenceEquals(this.HtmlBodyField, value) != true)) {
                    this.HtmlBodyField = value;
                    this.RaisePropertyChanged("HtmlBody");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Recipients {
            get {
                return this.RecipientsField;
            }
            set {
                if ((object.ReferenceEquals(this.RecipientsField, value) != true)) {
                    this.RecipientsField = value;
                    this.RaisePropertyChanged("Recipients");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Subject {
            get {
                return this.SubjectField;
            }
            set {
                if ((object.ReferenceEquals(this.SubjectField, value) != true)) {
                    this.SubjectField = value;
                    this.RaisePropertyChanged("Subject");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TextBody {
            get {
                return this.TextBodyField;
            }
            set {
                if ((object.ReferenceEquals(this.TextBodyField, value) != true)) {
                    this.TextBodyField = value;
                    this.RaisePropertyChanged("TextBody");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EmailResponseEnum", Namespace="http://schemas.datacontract.org/2004/07/InContact.EmailNotificationService.Enums")]
    public enum EmailResponseEnum : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ConnectionFailure = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FailureSendingToRecipient = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Success = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GeneralFailure = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NotificationService.ISupportEmail")]
    public interface ISupportEmail {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupportEmail/Echo", ReplyAction="http://tempuri.org/ISupportEmail/EchoResponse")]
        string Echo(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupportEmail/Echo", ReplyAction="http://tempuri.org/ISupportEmail/EchoResponse")]
        System.Threading.Tasks.Task<string> EchoAsync(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupportEmail/CreateEmailMessage", ReplyAction="http://tempuri.org/ISupportEmail/CreateEmailMessageResponse")]
        InContact.DeveloperPortal.Web.NotificationService.EmailResponseEnum CreateEmailMessage(InContact.DeveloperPortal.Web.NotificationService.EmailInfo details);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupportEmail/CreateEmailMessage", ReplyAction="http://tempuri.org/ISupportEmail/CreateEmailMessageResponse")]
        System.Threading.Tasks.Task<InContact.DeveloperPortal.Web.NotificationService.EmailResponseEnum> CreateEmailMessageAsync(InContact.DeveloperPortal.Web.NotificationService.EmailInfo details);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupportEmail/SendEmail", ReplyAction="http://tempuri.org/ISupportEmail/SendEmailResponse")]
        bool SendEmail(string[] recipients, string from, string subject, string htmlBody, string textBody, int emailTemplateID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISupportEmail/SendEmail", ReplyAction="http://tempuri.org/ISupportEmail/SendEmailResponse")]
        System.Threading.Tasks.Task<bool> SendEmailAsync(string[] recipients, string from, string subject, string htmlBody, string textBody, int emailTemplateID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISupportEmailChannel : InContact.DeveloperPortal.Web.NotificationService.ISupportEmail, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SupportEmailClient : System.ServiceModel.ClientBase<InContact.DeveloperPortal.Web.NotificationService.ISupportEmail>, InContact.DeveloperPortal.Web.NotificationService.ISupportEmail {
        
        public SupportEmailClient() {
        }
        
        public SupportEmailClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SupportEmailClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupportEmailClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SupportEmailClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Echo(string value) {
            return base.Channel.Echo(value);
        }
        
        public System.Threading.Tasks.Task<string> EchoAsync(string value) {
            return base.Channel.EchoAsync(value);
        }
        
        public InContact.DeveloperPortal.Web.NotificationService.EmailResponseEnum CreateEmailMessage(InContact.DeveloperPortal.Web.NotificationService.EmailInfo details) {
            return base.Channel.CreateEmailMessage(details);
        }
        
        public System.Threading.Tasks.Task<InContact.DeveloperPortal.Web.NotificationService.EmailResponseEnum> CreateEmailMessageAsync(InContact.DeveloperPortal.Web.NotificationService.EmailInfo details) {
            return base.Channel.CreateEmailMessageAsync(details);
        }
        
        public bool SendEmail(string[] recipients, string from, string subject, string htmlBody, string textBody, int emailTemplateID) {
            return base.Channel.SendEmail(recipients, from, subject, htmlBody, textBody, emailTemplateID);
        }
        
        public System.Threading.Tasks.Task<bool> SendEmailAsync(string[] recipients, string from, string subject, string htmlBody, string textBody, int emailTemplateID) {
            return base.Channel.SendEmailAsync(recipients, from, subject, htmlBody, textBody, emailTemplateID);
        }
    }
}
