﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Movie", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Movie : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        private int DurationMinutesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenreField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public int DurationMinutes {
            get {
                return this.DurationMinutesField;
            }
            set {
                if ((this.DurationMinutesField.Equals(value) != true)) {
                    this.DurationMinutesField = value;
                    this.RaisePropertyChanged("DurationMinutes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Genre {
            get {
                return this.GenreField;
            }
            set {
                if ((object.ReferenceEquals(this.GenreField, value) != true)) {
                    this.GenreField = value;
                    this.RaisePropertyChanged("Genre");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ViaCinemaMovieService.ViaCinemaMovieServiceSoap")]
    public interface ViaCinemaMovieServiceSoap {
        
        // CODEGEN: Generating message contract since element name GetAllMoviesResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllMovies", ReplyAction="*")]
        DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponse GetAllMovies(DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllMovies", ReplyAction="*")]
        System.Threading.Tasks.Task<DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponse> GetAllMoviesAsync(DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllMoviesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllMovies", Namespace="http://tempuri.org/", Order=0)]
        public DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequestBody Body;
        
        public GetAllMoviesRequest() {
        }
        
        public GetAllMoviesRequest(DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetAllMoviesRequestBody {
        
        public GetAllMoviesRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllMoviesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllMoviesResponse", Namespace="http://tempuri.org/", Order=0)]
        public DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponseBody Body;
        
        public GetAllMoviesResponse() {
        }
        
        public GetAllMoviesResponse(DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAllMoviesResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.Movie[] GetAllMoviesResult;
        
        public GetAllMoviesResponseBody() {
        }
        
        public GetAllMoviesResponseBody(DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.Movie[] GetAllMoviesResult) {
            this.GetAllMoviesResult = GetAllMoviesResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ViaCinemaMovieServiceSoapChannel : DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.ViaCinemaMovieServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ViaCinemaMovieServiceSoapClient : System.ServiceModel.ClientBase<DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.ViaCinemaMovieServiceSoap>, DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.ViaCinemaMovieServiceSoap {
        
        public ViaCinemaMovieServiceSoapClient() {
        }
        
        public ViaCinemaMovieServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ViaCinemaMovieServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViaCinemaMovieServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViaCinemaMovieServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponse DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.ViaCinemaMovieServiceSoap.GetAllMovies(DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest request) {
            return base.Channel.GetAllMovies(request);
        }
        
        public DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.Movie[] GetAllMovies() {
            DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest inValue = new DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest();
            inValue.Body = new DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequestBody();
            DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponse retVal = ((DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.ViaCinemaMovieServiceSoap)(this)).GetAllMovies(inValue);
            return retVal.Body.GetAllMoviesResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponse> DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.ViaCinemaMovieServiceSoap.GetAllMoviesAsync(DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest request) {
            return base.Channel.GetAllMoviesAsync(request);
        }
        
        public System.Threading.Tasks.Task<DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesResponse> GetAllMoviesAsync() {
            DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest inValue = new DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequest();
            inValue.Body = new DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.GetAllMoviesRequestBody();
            return ((DNP1.ViaCinema.Services.Proxies.ViaCinemaMovieService.ViaCinemaMovieServiceSoap)(this)).GetAllMoviesAsync(inValue);
        }
    }
}
