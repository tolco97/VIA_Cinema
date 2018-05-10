namespace WebService_VIA_Pay
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface IViaPayService
    {
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/MakeTransaction?creditCardNumber={creditCardNumber}&" +
                                                 "pin={pin}&amount={amount}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool MakeTransaction(string creditCardNumber, string pin, decimal amount);
    }

}
