﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF_Server.FileRef {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://services.recive.cesi.com/", ConfigurationName="FileRef.FileWebService")]
    public interface FileWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://services.recive.cesi.com/FileWebService/printHelloRequest", ReplyAction="http://services.recive.cesi.com/FileWebService/printHelloResponse")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string printHello(string arg0);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FileWebServiceChannel : WCF_Server.FileRef.FileWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileWebServiceClient : System.ServiceModel.ClientBase<WCF_Server.FileRef.FileWebService>, WCF_Server.FileRef.FileWebService {
        
        public FileWebServiceClient() {
        }
        
        public FileWebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string printHello(string arg0) {
            return base.Channel.printHello(arg0);
        }
    }
}
