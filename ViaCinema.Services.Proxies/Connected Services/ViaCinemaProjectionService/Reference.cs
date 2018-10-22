﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ViaCinema.Services.Proxies.ViaCinemaProjectionService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Projection", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Projection : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ViaCinema.Services.Proxies.ViaCinemaProjectionService.Movie ProjectedMovieField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ViaCinema.Services.Proxies.ViaCinemaProjectionService.Seat[] SeatsField;
        
        private System.DateTime MovieStartTimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.Movie ProjectedMovie {
            get {
                return this.ProjectedMovieField;
            }
            set {
                if ((object.ReferenceEquals(this.ProjectedMovieField, value) != true)) {
                    this.ProjectedMovieField = value;
                    this.RaisePropertyChanged("ProjectedMovie");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.Seat[] Seats {
            get {
                return this.SeatsField;
            }
            set {
                if ((object.ReferenceEquals(this.SeatsField, value) != true)) {
                    this.SeatsField = value;
                    this.RaisePropertyChanged("Seats");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public System.DateTime MovieStartTime {
            get {
                return this.MovieStartTimeField;
            }
            set {
                if ((this.MovieStartTimeField.Equals(value) != true)) {
                    this.MovieStartTimeField = value;
                    this.RaisePropertyChanged("MovieStartTime");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Seat", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Seat : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int SeatNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ViaCinema.Services.Proxies.ViaCinemaProjectionService.UserAccount SeatOwnerField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int SeatNumber {
            get {
                return this.SeatNumberField;
            }
            set {
                if ((this.SeatNumberField.Equals(value) != true)) {
                    this.SeatNumberField = value;
                    this.RaisePropertyChanged("SeatNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.UserAccount SeatOwner {
            get {
                return this.SeatOwnerField;
            }
            set {
                if ((object.ReferenceEquals(this.SeatOwnerField, value) != true)) {
                    this.SeatOwnerField = value;
                    this.RaisePropertyChanged("SeatOwner");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserAccount", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class UserAccount : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserPasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        private System.DateTime BirthdayField;
        
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
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string UserPassword {
            get {
                return this.UserPasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.UserPasswordField, value) != true)) {
                    this.UserPasswordField = value;
                    this.RaisePropertyChanged("UserPassword");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public System.DateTime Birthday {
            get {
                return this.BirthdayField;
            }
            set {
                if ((this.BirthdayField.Equals(value) != true)) {
                    this.BirthdayField = value;
                    this.RaisePropertyChanged("Birthday");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap")]
    public interface ViaCinemaProjectionServiceSoap {
        
        // CODEGEN: Generating message contract since element name movieName from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetProjections", ReplyAction="*")]
        ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponse GetProjections(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetProjections", ReplyAction="*")]
        System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponse> GetProjectionsAsync(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest request);
        
        // CODEGEN: Generating message contract since element name GetAllProjectionsResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllProjections", ReplyAction="*")]
        ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponse GetAllProjections(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllProjections", ReplyAction="*")]
        System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponse> GetAllProjectionsAsync(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest request);
        
        // CODEGEN: Generating message contract since element name email from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BookSeat", ReplyAction="*")]
        ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponse BookSeat(ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BookSeat", ReplyAction="*")]
        System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponse> BookSeatAsync(ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetProjectionsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetProjections", Namespace="http://tempuri.org/", Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequestBody Body;
        
        public GetProjectionsRequest() {
        }
        
        public GetProjectionsRequest(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetProjectionsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string movieName;
        
        public GetProjectionsRequestBody() {
        }
        
        public GetProjectionsRequestBody(string movieName) {
            this.movieName = movieName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetProjectionsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetProjectionsResponse", Namespace="http://tempuri.org/", Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponseBody Body;
        
        public GetProjectionsResponse() {
        }
        
        public GetProjectionsResponse(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetProjectionsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.Projection[] GetProjectionsResult;
        
        public GetProjectionsResponseBody() {
        }
        
        public GetProjectionsResponseBody(ViaCinema.Services.Proxies.ViaCinemaProjectionService.Projection[] GetProjectionsResult) {
            this.GetProjectionsResult = GetProjectionsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllProjectionsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllProjections", Namespace="http://tempuri.org/", Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequestBody Body;
        
        public GetAllProjectionsRequest() {
        }
        
        public GetAllProjectionsRequest(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetAllProjectionsRequestBody {
        
        public GetAllProjectionsRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAllProjectionsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAllProjectionsResponse", Namespace="http://tempuri.org/", Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponseBody Body;
        
        public GetAllProjectionsResponse() {
        }
        
        public GetAllProjectionsResponse(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAllProjectionsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.Projection[] GetAllProjectionsResult;
        
        public GetAllProjectionsResponseBody() {
        }
        
        public GetAllProjectionsResponseBody(ViaCinema.Services.Proxies.ViaCinemaProjectionService.Projection[] GetAllProjectionsResult) {
            this.GetAllProjectionsResult = GetAllProjectionsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class BookSeatRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="BookSeat", Namespace="http://tempuri.org/", Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequestBody Body;
        
        public BookSeatRequest() {
        }
        
        public BookSeatRequest(ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class BookSeatRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int projectionId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string email;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string seatNumbers;
        
        public BookSeatRequestBody() {
        }
        
        public BookSeatRequestBody(int projectionId, string email, string seatNumbers) {
            this.projectionId = projectionId;
            this.email = email;
            this.seatNumbers = seatNumbers;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class BookSeatResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="BookSeatResponse", Namespace="http://tempuri.org/", Order=0)]
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponseBody Body;
        
        public BookSeatResponse() {
        }
        
        public BookSeatResponse(ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class BookSeatResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool BookSeatResult;
        
        public BookSeatResponseBody() {
        }
        
        public BookSeatResponseBody(bool BookSeatResult) {
            this.BookSeatResult = BookSeatResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ViaCinemaProjectionServiceSoapChannel : ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ViaCinemaProjectionServiceSoapClient : System.ServiceModel.ClientBase<ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap>, ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap {
        
        public ViaCinemaProjectionServiceSoapClient() {
        }
        
        public ViaCinemaProjectionServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ViaCinemaProjectionServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViaCinemaProjectionServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ViaCinemaProjectionServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponse ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap.GetProjections(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest request) {
            return base.Channel.GetProjections(request);
        }
        
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.Projection[] GetProjections(string movieName) {
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest inValue = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest();
            inValue.Body = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequestBody();
            inValue.Body.movieName = movieName;
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponse retVal = ((ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap)(this)).GetProjections(inValue);
            return retVal.Body.GetProjectionsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponse> ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap.GetProjectionsAsync(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest request) {
            return base.Channel.GetProjectionsAsync(request);
        }
        
        public System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsResponse> GetProjectionsAsync(string movieName) {
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest inValue = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequest();
            inValue.Body = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetProjectionsRequestBody();
            inValue.Body.movieName = movieName;
            return ((ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap)(this)).GetProjectionsAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponse ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap.GetAllProjections(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest request) {
            return base.Channel.GetAllProjections(request);
        }
        
        public ViaCinema.Services.Proxies.ViaCinemaProjectionService.Projection[] GetAllProjections() {
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest inValue = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest();
            inValue.Body = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequestBody();
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponse retVal = ((ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap)(this)).GetAllProjections(inValue);
            return retVal.Body.GetAllProjectionsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponse> ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap.GetAllProjectionsAsync(ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest request) {
            return base.Channel.GetAllProjectionsAsync(request);
        }
        
        public System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsResponse> GetAllProjectionsAsync() {
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest inValue = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequest();
            inValue.Body = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.GetAllProjectionsRequestBody();
            return ((ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap)(this)).GetAllProjectionsAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponse ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap.BookSeat(ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest request) {
            return base.Channel.BookSeat(request);
        }
        
        public bool BookSeat(int projectionId, string email, string seatNumbers) {
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest inValue = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest();
            inValue.Body = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequestBody();
            inValue.Body.projectionId = projectionId;
            inValue.Body.email = email;
            inValue.Body.seatNumbers = seatNumbers;
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponse retVal = ((ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap)(this)).BookSeat(inValue);
            return retVal.Body.BookSeatResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponse> ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap.BookSeatAsync(ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest request) {
            return base.Channel.BookSeatAsync(request);
        }
        
        public System.Threading.Tasks.Task<ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatResponse> BookSeatAsync(int projectionId, string email, string seatNumbers) {
            ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest inValue = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequest();
            inValue.Body = new ViaCinema.Services.Proxies.ViaCinemaProjectionService.BookSeatRequestBody();
            inValue.Body.projectionId = projectionId;
            inValue.Body.email = email;
            inValue.Body.seatNumbers = seatNumbers;
            return ((ViaCinema.Services.Proxies.ViaCinemaProjectionService.ViaCinemaProjectionServiceSoap)(this)).BookSeatAsync(inValue);
        }
    }
}
