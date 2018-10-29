﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap")]
    public interface ViaCinemaPaymentServiceSoap {
        
        // CODEGEN: Generating message contract since element name creditCardNumber from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MakeTransaction", ReplyAction="*")]
        DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponse MakeTransaction(DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MakeTransaction", ReplyAction="*")]
        System.Threading.Tasks.Task<DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponse> MakeTransactionAsync(DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class MakeTransactionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="MakeTransaction", Namespace="http://tempuri.org/", Order=0)]
        public DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequestBody Body;
        
        public MakeTransactionRequest() {
        }
        
        public MakeTransactionRequest(DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class MakeTransactionRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string creditCardNumber;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string pin;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public decimal amount;
        
        public MakeTransactionRequestBody() {
        }
        
        public MakeTransactionRequestBody(string creditCardNumber, string pin, decimal amount) {
            this.creditCardNumber = creditCardNumber;
            this.pin = pin;
            this.amount = amount;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class MakeTransactionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="MakeTransactionResponse", Namespace="http://tempuri.org/", Order=0)]
        public DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponseBody Body;
        
        public MakeTransactionResponse() {
        }
        
        public MakeTransactionResponse(DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class MakeTransactionResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool MakeTransactionResult;
        
        public MakeTransactionResponseBody() {
        }
        
        public MakeTransactionResponseBody(bool MakeTransactionResult) {
            this.MakeTransactionResult = MakeTransactionResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ViaCinemaPaymentServiceSoapChannel : DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ViaCinemaPaymentServiceSoapClient : System.ServiceModel.ClientBase<DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap>, DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap {
        
        public ViaCinemaPaymentServiceSoapClient() {
        }
        
        public ViaCinemaPaymentServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ViaCinemaPaymentServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViaCinemaPaymentServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViaCinemaPaymentServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponse DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap.MakeTransaction(DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest request) {
            return base.Channel.MakeTransaction(request);
        }
        
        public bool MakeTransaction(string creditCardNumber, string pin, decimal amount) {
            DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest inValue = new DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest();
            inValue.Body = new DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequestBody();
            inValue.Body.creditCardNumber = creditCardNumber;
            inValue.Body.pin = pin;
            inValue.Body.amount = amount;
            DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponse retVal = ((DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap)(this)).MakeTransaction(inValue);
            return retVal.Body.MakeTransactionResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponse> DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap.MakeTransactionAsync(DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest request) {
            return base.Channel.MakeTransactionAsync(request);
        }
        
        public System.Threading.Tasks.Task<DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionResponse> MakeTransactionAsync(string creditCardNumber, string pin, decimal amount) {
            DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest inValue = new DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequest();
            inValue.Body = new DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.MakeTransactionRequestBody();
            inValue.Body.creditCardNumber = creditCardNumber;
            inValue.Body.pin = pin;
            inValue.Body.amount = amount;
            return ((DNP1.ViaCinema.Services.Proxies.ViaCinemaPaymentService.ViaCinemaPaymentServiceSoap)(this)).MakeTransactionAsync(inValue);
        }
    }
}
