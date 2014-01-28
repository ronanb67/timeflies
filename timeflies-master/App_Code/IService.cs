using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace TimeFliesBy.WebUI
{

  // NOTE: If you change the interface name "IService" here, you must also update the reference to "IService" in Web.config.
  [ServiceContract]
  public interface IService
  {
    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                              RequestFormat = WebMessageFormat.Xml,
                              ResponseFormat = WebMessageFormat.Xml,
                              UriTemplate = "Test/{val}")]
    string DoWork(string val);

    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                                RequestFormat = WebMessageFormat.Xml,
                                ResponseFormat = WebMessageFormat.Xml,
                                UriTemplate = "GetUserImages/{UserID}")]
    string GetUserImages(string UserID);

    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                                RequestFormat = WebMessageFormat.Xml,
                                ResponseFormat = WebMessageFormat.Xml,
                                UriTemplate = "DelUnDel/{ImageID}/{Action}")]
    string DelUnDel(string ImageID, string Action);

    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                                RequestFormat = WebMessageFormat.Xml,
                                ResponseFormat = WebMessageFormat.Xml,
                                UriTemplate = "ChangeDate/{ImageID}/{NewDate}")]
    string ChangeDate(string ImageID, string NewDate);

    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                              RequestFormat = WebMessageFormat.Xml,
                              ResponseFormat = WebMessageFormat.Xml,
                               UriTemplate = "{UserID}/{REye}/{LEye}/SaveEyes")]
    string SaveEyes(string UserID, string REye, string LEye);

    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                              RequestFormat = WebMessageFormat.Xml,
                              ResponseFormat = WebMessageFormat.Xml,
                               UriTemplate = "{UserID}/GetEyes")]
    string GetEyes(string UserID);

    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                              RequestFormat = WebMessageFormat.Xml,
                              ResponseFormat = WebMessageFormat.Xml,
                               UriTemplate = "RevertImage/{ImageID}")]
    string RevertImage(string ImageID);

    [OperationContract, WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                              RequestFormat = WebMessageFormat.Xml,
                              ResponseFormat = WebMessageFormat.Xml,
                               UriTemplate = "AuthenticateUser/{UserID}/{Token}")]
    string AuthenticateUser(string UserID, string Token);
  }
}