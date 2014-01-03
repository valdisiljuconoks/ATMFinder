﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 11.0.50727.1
// 
namespace Viiar.AtmFinder.W8.GeoLocationServices {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GeoServiceResponse", Namespace="http://viiar-consulting.lv/atmfinder/azureservices")]
    public partial class GeoServiceResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Collections.ObjectModel.ObservableCollection<Viiar.AtmFinder.W8.GeoLocationServices.GeoLocationMatch> LocationsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<Viiar.AtmFinder.W8.GeoLocationServices.GeoLocationMatch> Locations {
            get {
                return this.LocationsField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationsField, value) != true)) {
                    this.LocationsField = value;
                    this.RaisePropertyChanged("Locations");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="GeoLocationMatch", Namespace="http://viiar-consulting.lv/atmfinder/azureservices")]
    public partial class GeoLocationMatch : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string AddressField;
        
        private string CountryField;
        
        private double LatitudeField;
        
        private double LongitudeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Latitude {
            get {
                return this.LatitudeField;
            }
            set {
                if ((this.LatitudeField.Equals(value) != true)) {
                    this.LatitudeField = value;
                    this.RaisePropertyChanged("Latitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Longitude {
            get {
                return this.LongitudeField;
            }
            set {
                if ((this.LongitudeField.Equals(value) != true)) {
                    this.LongitudeField = value;
                    this.RaisePropertyChanged("Longitude");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://viiar-consulting.lv/atmfinder/azureservices", ConfigurationName="GeoLocationServices.IGeoLocationService")]
    public interface IGeoLocationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://viiar-consulting.lv/atmfinder/azureservices/IGeoLocationService/FindCoordi" +
            "nates", ReplyAction="http://viiar-consulting.lv/atmfinder/azureservices/IGeoLocationService/FindCoordi" +
            "natesResponse")]
        System.Threading.Tasks.Task<Viiar.AtmFinder.W8.GeoLocationServices.GeoServiceResponse> FindCoordinatesAsync(string searchTerm);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGeoLocationServiceChannel : Viiar.AtmFinder.W8.GeoLocationServices.IGeoLocationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GeoLocationServiceClient : System.ServiceModel.ClientBase<Viiar.AtmFinder.W8.GeoLocationServices.IGeoLocationService>, Viiar.AtmFinder.W8.GeoLocationServices.IGeoLocationService {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public GeoLocationServiceClient() : 
                base(GeoLocationServiceClient.GetDefaultBinding(), GeoLocationServiceClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.CustomBinding_IGeoLocationService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GeoLocationServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(GeoLocationServiceClient.GetBindingForEndpoint(endpointConfiguration), GeoLocationServiceClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GeoLocationServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(GeoLocationServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GeoLocationServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(GeoLocationServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GeoLocationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<Viiar.AtmFinder.W8.GeoLocationServices.GeoServiceResponse> FindCoordinatesAsync(string searchTerm) {
            return base.Channel.FindCoordinatesAsync(searchTerm);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.CustomBinding_IGeoLocationService)) {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                result.Elements.Add(new System.ServiceModel.Channels.BinaryMessageEncodingBindingElement());
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.CustomBinding_IGeoLocationService)) {
                return new System.ServiceModel.EndpointAddress("http://127.0.0.1:81/GeoLocationService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return GeoLocationServiceClient.GetBindingForEndpoint(EndpointConfiguration.CustomBinding_IGeoLocationService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return GeoLocationServiceClient.GetEndpointAddress(EndpointConfiguration.CustomBinding_IGeoLocationService);
        }
        
        public enum EndpointConfiguration {
            
            CustomBinding_IGeoLocationService,
        }
    }
}