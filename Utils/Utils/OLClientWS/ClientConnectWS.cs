// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.ClientConnectWS
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using Utils.Properties;

#nullable disable
namespace Utils.OLClientWS;

[DebuggerStepThrough]
[WebServiceBinding(Name = "ClientConnectWSSoap", Namespace = "http://tempuri.org/")]
[DesignerCategory("code")]
[GeneratedCode("System.Web.Services", "4.0.30319.1")]
public class ClientConnectWS : SoapHttpClientProtocol
{
  private SendOrPostCallback ConnectOperationCompleted;
  private SendOrPostCallback AuthenticateOperationCompleted;
  private SendOrPostCallback GetSubscriptionsOperationCompleted;
  private SendOrPostCallback GetBaseResourceUrlOperationCompleted;
  private SendOrPostCallback IsUserResourceOwnerOperationCompleted;
  private SendOrPostCallback GetResourcesForUserSubscriptionOperationCompleted;
  private SendOrPostCallback GetUsersForResourceOperationCompleted;
  private SendOrPostCallback GetListsForResourceOperationCompleted;
  private SendOrPostCallback ExecuteQueryOperationCompleted;
  private SendOrPostCallback GetChoicesFieldValuesOperationCompleted;
  private SendOrPostCallback GetListItemsAsDataSetOperationCompleted;
  private SendOrPostCallback DeleteAllListItemsOperationCompleted;
  private SendOrPostCallback AddListItemsOperationCompleted;
  private SendOrPostCallback UpdateListItemsOperationCompleted;
  private SendOrPostCallback GetListViewAsXmlOperationCompleted;
  private SendOrPostCallback GetListViewOperationCompleted;
  private SendOrPostCallback QueryListOperationCompleted;
  private SendOrPostCallback GetListViewsOperationCompleted;
  private SendOrPostCallback WriteQueryStringToCacheOperationCompleted;
  private bool useDefaultCredentialsSetExplicitly;

  public ClientConnectWS()
  {
    this.Url = Settings.Default.Utils_OLClientWS_ClientConnectWS;
    if (this.IsLocalFileSystemWebService(this.Url))
    {
      this.UseDefaultCredentials = true;
      this.useDefaultCredentialsSetExplicitly = false;
    }
    else
      this.useDefaultCredentialsSetExplicitly = true;
  }

  public new string Url
  {
    get => base.Url;
    set
    {
      if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
        base.UseDefaultCredentials = false;
      base.Url = value;
    }
  }

  public new bool UseDefaultCredentials
  {
    get => base.UseDefaultCredentials;
    set
    {
      base.UseDefaultCredentials = value;
      this.useDefaultCredentialsSetExplicitly = true;
    }
  }

  public event ConnectCompletedEventHandler ConnectCompleted;

  public event AuthenticateCompletedEventHandler AuthenticateCompleted;

  public event GetSubscriptionsCompletedEventHandler GetSubscriptionsCompleted;

  public event GetBaseResourceUrlCompletedEventHandler GetBaseResourceUrlCompleted;

  public event IsUserResourceOwnerCompletedEventHandler IsUserResourceOwnerCompleted;

  public event GetResourcesForUserSubscriptionCompletedEventHandler GetResourcesForUserSubscriptionCompleted;

  public event GetUsersForResourceCompletedEventHandler GetUsersForResourceCompleted;

  public event GetListsForResourceCompletedEventHandler GetListsForResourceCompleted;

  public event ExecuteQueryCompletedEventHandler ExecuteQueryCompleted;

  public event GetChoicesFieldValuesCompletedEventHandler GetChoicesFieldValuesCompleted;

  public event GetListItemsAsDataSetCompletedEventHandler GetListItemsAsDataSetCompleted;

  public event DeleteAllListItemsCompletedEventHandler DeleteAllListItemsCompleted;

  public event AddListItemsCompletedEventHandler AddListItemsCompleted;

  public event UpdateListItemsCompletedEventHandler UpdateListItemsCompleted;

  public event GetListViewAsXmlCompletedEventHandler GetListViewAsXmlCompleted;

  public event GetListViewCompletedEventHandler GetListViewCompleted;

  public event QueryListCompletedEventHandler QueryListCompleted;

  public event GetListViewsCompletedEventHandler GetListViewsCompleted;

  public event WriteQueryStringToCacheCompletedEventHandler WriteQueryStringToCacheCompleted;

  [SoapDocumentMethod("http://tempuri.org/Connect", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public string Connect(string UserName)
  {
    return (string) this.Invoke(nameof (Connect), new object[1]
    {
      (object) UserName
    })[0];
  }

  public void ConnectAsync(string UserName) => this.ConnectAsync(UserName, (object) null);

  public void ConnectAsync(string UserName, object userState)
  {
    if (this.ConnectOperationCompleted == null)
      this.ConnectOperationCompleted = new SendOrPostCallback(this.OnConnectOperationCompleted);
    this.InvokeAsync("Connect", new object[1]
    {
      (object) UserName
    }, this.ConnectOperationCompleted, userState);
  }

  private void OnConnectOperationCompleted(object arg)
  {
    if (this.ConnectCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ConnectCompleted((object) this, new ConnectCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/Authenticate", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void Authenticate(string Ticket)
  {
    this.Invoke(nameof (Authenticate), new object[1]
    {
      (object) Ticket
    });
  }

  public void AuthenticateAsync(string Ticket) => this.AuthenticateAsync(Ticket, (object) null);

  public void AuthenticateAsync(string Ticket, object userState)
  {
    if (this.AuthenticateOperationCompleted == null)
      this.AuthenticateOperationCompleted = new SendOrPostCallback(this.OnAuthenticateOperationCompleted);
    this.InvokeAsync("Authenticate", new object[1]
    {
      (object) Ticket
    }, this.AuthenticateOperationCompleted, userState);
  }

  private void OnAuthenticateOperationCompleted(object arg)
  {
    if (this.AuthenticateCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AuthenticateCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetSubscriptions", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public string[] GetSubscriptions()
  {
    return (string[]) this.Invoke(nameof (GetSubscriptions), new object[0])[0];
  }

  public void GetSubscriptionsAsync() => this.GetSubscriptionsAsync((object) null);

  public void GetSubscriptionsAsync(object userState)
  {
    if (this.GetSubscriptionsOperationCompleted == null)
      this.GetSubscriptionsOperationCompleted = new SendOrPostCallback(this.OnGetSubscriptionsOperationCompleted);
    this.InvokeAsync("GetSubscriptions", new object[0], this.GetSubscriptionsOperationCompleted, userState);
  }

  private void OnGetSubscriptionsOperationCompleted(object arg)
  {
    if (this.GetSubscriptionsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetSubscriptionsCompleted((object) this, new GetSubscriptionsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetBaseResourceUrl", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public string GetBaseResourceUrl(string subscriptionID)
  {
    return (string) this.Invoke(nameof (GetBaseResourceUrl), new object[1]
    {
      (object) subscriptionID
    })[0];
  }

  public void GetBaseResourceUrlAsync(string subscriptionID)
  {
    this.GetBaseResourceUrlAsync(subscriptionID, (object) null);
  }

  public void GetBaseResourceUrlAsync(string subscriptionID, object userState)
  {
    if (this.GetBaseResourceUrlOperationCompleted == null)
      this.GetBaseResourceUrlOperationCompleted = new SendOrPostCallback(this.OnGetBaseResourceUrlOperationCompleted);
    this.InvokeAsync("GetBaseResourceUrl", new object[1]
    {
      (object) subscriptionID
    }, this.GetBaseResourceUrlOperationCompleted, userState);
  }

  private void OnGetBaseResourceUrlOperationCompleted(object arg)
  {
    if (this.GetBaseResourceUrlCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetBaseResourceUrlCompleted((object) this, new GetBaseResourceUrlCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/IsUserResourceOwner", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public bool IsUserResourceOwner(string subscriptionID)
  {
    return (bool) this.Invoke(nameof (IsUserResourceOwner), new object[1]
    {
      (object) subscriptionID
    })[0];
  }

  public void IsUserResourceOwnerAsync(string subscriptionID)
  {
    this.IsUserResourceOwnerAsync(subscriptionID, (object) null);
  }

  public void IsUserResourceOwnerAsync(string subscriptionID, object userState)
  {
    if (this.IsUserResourceOwnerOperationCompleted == null)
      this.IsUserResourceOwnerOperationCompleted = new SendOrPostCallback(this.OnIsUserResourceOwnerOperationCompleted);
    this.InvokeAsync("IsUserResourceOwner", new object[1]
    {
      (object) subscriptionID
    }, this.IsUserResourceOwnerOperationCompleted, userState);
  }

  private void OnIsUserResourceOwnerOperationCompleted(object arg)
  {
    if (this.IsUserResourceOwnerCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.IsUserResourceOwnerCompleted((object) this, new IsUserResourceOwnerCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetResourcesForUserSubscription", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public OLResource[] GetResourcesForUserSubscription(string subscriptionID)
  {
    return (OLResource[]) this.Invoke(nameof (GetResourcesForUserSubscription), new object[1]
    {
      (object) subscriptionID
    })[0];
  }

  public void GetResourcesForUserSubscriptionAsync(string subscriptionID)
  {
    this.GetResourcesForUserSubscriptionAsync(subscriptionID, (object) null);
  }

  public void GetResourcesForUserSubscriptionAsync(string subscriptionID, object userState)
  {
    if (this.GetResourcesForUserSubscriptionOperationCompleted == null)
      this.GetResourcesForUserSubscriptionOperationCompleted = new SendOrPostCallback(this.OnGetResourcesForUserSubscriptionOperationCompleted);
    this.InvokeAsync("GetResourcesForUserSubscription", new object[1]
    {
      (object) subscriptionID
    }, this.GetResourcesForUserSubscriptionOperationCompleted, userState);
  }

  private void OnGetResourcesForUserSubscriptionOperationCompleted(object arg)
  {
    if (this.GetResourcesForUserSubscriptionCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetResourcesForUserSubscriptionCompleted((object) this, new GetResourcesForUserSubscriptionCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetUsersForResource", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public string[] GetUsersForResource(string resource)
  {
    return (string[]) this.Invoke(nameof (GetUsersForResource), new object[1]
    {
      (object) resource
    })[0];
  }

  public void GetUsersForResourceAsync(string resource)
  {
    this.GetUsersForResourceAsync(resource, (object) null);
  }

  public void GetUsersForResourceAsync(string resource, object userState)
  {
    if (this.GetUsersForResourceOperationCompleted == null)
      this.GetUsersForResourceOperationCompleted = new SendOrPostCallback(this.OnGetUsersForResourceOperationCompleted);
    this.InvokeAsync("GetUsersForResource", new object[1]
    {
      (object) resource
    }, this.GetUsersForResourceOperationCompleted, userState);
  }

  private void OnGetUsersForResourceOperationCompleted(object arg)
  {
    if (this.GetUsersForResourceCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetUsersForResourceCompleted((object) this, new GetUsersForResourceCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetListsForResource", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public OLList[] GetListsForResource(OLResource resource)
  {
    return (OLList[]) this.Invoke(nameof (GetListsForResource), new object[1]
    {
      (object) resource
    })[0];
  }

  public void GetListsForResourceAsync(OLResource resource)
  {
    this.GetListsForResourceAsync(resource, (object) null);
  }

  public void GetListsForResourceAsync(OLResource resource, object userState)
  {
    if (this.GetListsForResourceOperationCompleted == null)
      this.GetListsForResourceOperationCompleted = new SendOrPostCallback(this.OnGetListsForResourceOperationCompleted);
    this.InvokeAsync("GetListsForResource", new object[1]
    {
      (object) resource
    }, this.GetListsForResourceOperationCompleted, userState);
  }

  private void OnGetListsForResourceOperationCompleted(object arg)
  {
    if (this.GetListsForResourceCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetListsForResourceCompleted((object) this, new GetListsForResourceCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/ExecuteQuery", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public DataSet ExecuteQuery(
    string ResourceUrl,
    string DocumentListID,
    string ListViewID,
    string CAMLQueryString,
    string[] ViewFields,
    string QueryOptions)
  {
    return (DataSet) this.Invoke(nameof (ExecuteQuery), new object[6]
    {
      (object) ResourceUrl,
      (object) DocumentListID,
      (object) ListViewID,
      (object) CAMLQueryString,
      (object) ViewFields,
      (object) QueryOptions
    })[0];
  }

  public void ExecuteQueryAsync(
    string ResourceUrl,
    string DocumentListID,
    string ListViewID,
    string CAMLQueryString,
    string[] ViewFields,
    string QueryOptions)
  {
    this.ExecuteQueryAsync(ResourceUrl, DocumentListID, ListViewID, CAMLQueryString, ViewFields, QueryOptions, (object) null);
  }

  public void ExecuteQueryAsync(
    string ResourceUrl,
    string DocumentListID,
    string ListViewID,
    string CAMLQueryString,
    string[] ViewFields,
    string QueryOptions,
    object userState)
  {
    if (this.ExecuteQueryOperationCompleted == null)
      this.ExecuteQueryOperationCompleted = new SendOrPostCallback(this.OnExecuteQueryOperationCompleted);
    this.InvokeAsync("ExecuteQuery", new object[6]
    {
      (object) ResourceUrl,
      (object) DocumentListID,
      (object) ListViewID,
      (object) CAMLQueryString,
      (object) ViewFields,
      (object) QueryOptions
    }, this.ExecuteQueryOperationCompleted, userState);
  }

  private void OnExecuteQueryOperationCompleted(object arg)
  {
    if (this.ExecuteQueryCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.ExecuteQueryCompleted((object) this, new ExecuteQueryCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetChoicesFieldValues", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public string[] GetChoicesFieldValues(XmlNode ListViewAsXml, string ChoicesFieldName)
  {
    return (string[]) this.Invoke(nameof (GetChoicesFieldValues), new object[2]
    {
      (object) ListViewAsXml,
      (object) ChoicesFieldName
    })[0];
  }

  public void GetChoicesFieldValuesAsync(XmlNode ListViewAsXml, string ChoicesFieldName)
  {
    this.GetChoicesFieldValuesAsync(ListViewAsXml, ChoicesFieldName, (object) null);
  }

  public void GetChoicesFieldValuesAsync(
    XmlNode ListViewAsXml,
    string ChoicesFieldName,
    object userState)
  {
    if (this.GetChoicesFieldValuesOperationCompleted == null)
      this.GetChoicesFieldValuesOperationCompleted = new SendOrPostCallback(this.OnGetChoicesFieldValuesOperationCompleted);
    this.InvokeAsync("GetChoicesFieldValues", new object[2]
    {
      (object) ListViewAsXml,
      (object) ChoicesFieldName
    }, this.GetChoicesFieldValuesOperationCompleted, userState);
  }

  private void OnGetChoicesFieldValuesOperationCompleted(object arg)
  {
    if (this.GetChoicesFieldValuesCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetChoicesFieldValuesCompleted((object) this, new GetChoicesFieldValuesCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetListItemsAsDataSet", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public DataSet GetListItemsAsDataSet(string ResourceUrl, string ListID, string ViewID)
  {
    return (DataSet) this.Invoke(nameof (GetListItemsAsDataSet), new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) ViewID
    })[0];
  }

  public void GetListItemsAsDataSetAsync(string ResourceUrl, string ListID, string ViewID)
  {
    this.GetListItemsAsDataSetAsync(ResourceUrl, ListID, ViewID, (object) null);
  }

  public void GetListItemsAsDataSetAsync(
    string ResourceUrl,
    string ListID,
    string ViewID,
    object userState)
  {
    if (this.GetListItemsAsDataSetOperationCompleted == null)
      this.GetListItemsAsDataSetOperationCompleted = new SendOrPostCallback(this.OnGetListItemsAsDataSetOperationCompleted);
    this.InvokeAsync("GetListItemsAsDataSet", new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) ViewID
    }, this.GetListItemsAsDataSetOperationCompleted, userState);
  }

  private void OnGetListItemsAsDataSetOperationCompleted(object arg)
  {
    if (this.GetListItemsAsDataSetCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetListItemsAsDataSetCompleted((object) this, new GetListItemsAsDataSetCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/DeleteAllListItems", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void DeleteAllListItems(string ResourceUrl, string ListID, DataSet ListItemsDS)
  {
    this.Invoke(nameof (DeleteAllListItems), new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) ListItemsDS
    });
  }

  public void DeleteAllListItemsAsync(string ResourceUrl, string ListID, DataSet ListItemsDS)
  {
    this.DeleteAllListItemsAsync(ResourceUrl, ListID, ListItemsDS, (object) null);
  }

  public void DeleteAllListItemsAsync(
    string ResourceUrl,
    string ListID,
    DataSet ListItemsDS,
    object userState)
  {
    if (this.DeleteAllListItemsOperationCompleted == null)
      this.DeleteAllListItemsOperationCompleted = new SendOrPostCallback(this.OnDeleteAllListItemsOperationCompleted);
    this.InvokeAsync("DeleteAllListItems", new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) ListItemsDS
    }, this.DeleteAllListItemsOperationCompleted, userState);
  }

  private void OnDeleteAllListItemsOperationCompleted(object arg)
  {
    if (this.DeleteAllListItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.DeleteAllListItemsCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/AddListItems", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void AddListItems(string ResourceUrl, string ListID, string strBatch)
  {
    this.Invoke(nameof (AddListItems), new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) strBatch
    });
  }

  public void AddListItemsAsync(string ResourceUrl, string ListID, string strBatch)
  {
    this.AddListItemsAsync(ResourceUrl, ListID, strBatch, (object) null);
  }

  public void AddListItemsAsync(
    string ResourceUrl,
    string ListID,
    string strBatch,
    object userState)
  {
    if (this.AddListItemsOperationCompleted == null)
      this.AddListItemsOperationCompleted = new SendOrPostCallback(this.OnAddListItemsOperationCompleted);
    this.InvokeAsync("AddListItems", new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) strBatch
    }, this.AddListItemsOperationCompleted, userState);
  }

  private void OnAddListItemsOperationCompleted(object arg)
  {
    if (this.AddListItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.AddListItemsCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/UpdateListItems", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public void UpdateListItems(
    string ResourceUrl,
    string ListID,
    OLListField[] CurrentViewFields,
    DataSet BaselineDS,
    DataSet ListItemsDS,
    int[] DeletedRowIndexes)
  {
    this.Invoke(nameof (UpdateListItems), new object[6]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) CurrentViewFields,
      (object) BaselineDS,
      (object) ListItemsDS,
      (object) DeletedRowIndexes
    });
  }

  public void UpdateListItemsAsync(
    string ResourceUrl,
    string ListID,
    OLListField[] CurrentViewFields,
    DataSet BaselineDS,
    DataSet ListItemsDS,
    int[] DeletedRowIndexes)
  {
    this.UpdateListItemsAsync(ResourceUrl, ListID, CurrentViewFields, BaselineDS, ListItemsDS, DeletedRowIndexes, (object) null);
  }

  public void UpdateListItemsAsync(
    string ResourceUrl,
    string ListID,
    OLListField[] CurrentViewFields,
    DataSet BaselineDS,
    DataSet ListItemsDS,
    int[] DeletedRowIndexes,
    object userState)
  {
    if (this.UpdateListItemsOperationCompleted == null)
      this.UpdateListItemsOperationCompleted = new SendOrPostCallback(this.OnUpdateListItemsOperationCompleted);
    this.InvokeAsync("UpdateListItems", new object[6]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) CurrentViewFields,
      (object) BaselineDS,
      (object) ListItemsDS,
      (object) DeletedRowIndexes
    }, this.UpdateListItemsOperationCompleted, userState);
  }

  private void OnUpdateListItemsOperationCompleted(object arg)
  {
    if (this.UpdateListItemsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.UpdateListItemsCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetListViewAsXml", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public XmlNode GetListViewAsXml(string ResourceUrl, string ListID, string ViewID)
  {
    return (XmlNode) this.Invoke(nameof (GetListViewAsXml), new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) ViewID
    })[0];
  }

  public void GetListViewAsXmlAsync(string ResourceUrl, string ListID, string ViewID)
  {
    this.GetListViewAsXmlAsync(ResourceUrl, ListID, ViewID, (object) null);
  }

  public void GetListViewAsXmlAsync(
    string ResourceUrl,
    string ListID,
    string ViewID,
    object userState)
  {
    if (this.GetListViewAsXmlOperationCompleted == null)
      this.GetListViewAsXmlOperationCompleted = new SendOrPostCallback(this.OnGetListViewAsXmlOperationCompleted);
    this.InvokeAsync("GetListViewAsXml", new object[3]
    {
      (object) ResourceUrl,
      (object) ListID,
      (object) ViewID
    }, this.GetListViewAsXmlOperationCompleted, userState);
  }

  private void OnGetListViewAsXmlOperationCompleted(object arg)
  {
    if (this.GetListViewAsXmlCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetListViewAsXmlCompleted((object) this, new GetListViewAsXmlCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetListView", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public OLListField[] GetListView(XmlNode ListView)
  {
    return (OLListField[]) this.Invoke(nameof (GetListView), new object[1]
    {
      (object) ListView
    })[0];
  }

  public void GetListViewAsync(XmlNode ListView) => this.GetListViewAsync(ListView, (object) null);

  public void GetListViewAsync(XmlNode ListView, object userState)
  {
    if (this.GetListViewOperationCompleted == null)
      this.GetListViewOperationCompleted = new SendOrPostCallback(this.OnGetListViewOperationCompleted);
    this.InvokeAsync("GetListView", new object[1]
    {
      (object) ListView
    }, this.GetListViewOperationCompleted, userState);
  }

  private void OnGetListViewOperationCompleted(object arg)
  {
    if (this.GetListViewCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetListViewCompleted((object) this, new GetListViewCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/QueryList", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public string QueryList(string DocumentListName, XmlNode CamlQuery, XmlNode CamlQueryViewFields)
  {
    return (string) this.Invoke(nameof (QueryList), new object[3]
    {
      (object) DocumentListName,
      (object) CamlQuery,
      (object) CamlQueryViewFields
    })[0];
  }

  public void QueryListAsync(
    string DocumentListName,
    XmlNode CamlQuery,
    XmlNode CamlQueryViewFields)
  {
    this.QueryListAsync(DocumentListName, CamlQuery, CamlQueryViewFields, (object) null);
  }

  public void QueryListAsync(
    string DocumentListName,
    XmlNode CamlQuery,
    XmlNode CamlQueryViewFields,
    object userState)
  {
    if (this.QueryListOperationCompleted == null)
      this.QueryListOperationCompleted = new SendOrPostCallback(this.OnQueryListOperationCompleted);
    this.InvokeAsync("QueryList", new object[3]
    {
      (object) DocumentListName,
      (object) CamlQuery,
      (object) CamlQueryViewFields
    }, this.QueryListOperationCompleted, userState);
  }

  private void OnQueryListOperationCompleted(object arg)
  {
    if (this.QueryListCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.QueryListCompleted((object) this, new QueryListCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/GetListViews", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public OLView[] GetListViews(OLResource resource, string ListID)
  {
    return (OLView[]) this.Invoke(nameof (GetListViews), new object[2]
    {
      (object) resource,
      (object) ListID
    })[0];
  }

  public void GetListViewsAsync(OLResource resource, string ListID)
  {
    this.GetListViewsAsync(resource, ListID, (object) null);
  }

  public void GetListViewsAsync(OLResource resource, string ListID, object userState)
  {
    if (this.GetListViewsOperationCompleted == null)
      this.GetListViewsOperationCompleted = new SendOrPostCallback(this.OnGetListViewsOperationCompleted);
    this.InvokeAsync("GetListViews", new object[2]
    {
      (object) resource,
      (object) ListID
    }, this.GetListViewsOperationCompleted, userState);
  }

  private void OnGetListViewsOperationCompleted(object arg)
  {
    if (this.GetListViewsCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.GetListViewsCompleted((object) this, new GetListViewsCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapDocumentMethod("http://tempuri.org/WriteQueryStringToCache", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public string WriteQueryStringToCache(string QueryString)
  {
    return (string) this.Invoke(nameof (WriteQueryStringToCache), new object[1]
    {
      (object) QueryString
    })[0];
  }

  public void WriteQueryStringToCacheAsync(string QueryString)
  {
    this.WriteQueryStringToCacheAsync(QueryString, (object) null);
  }

  public void WriteQueryStringToCacheAsync(string QueryString, object userState)
  {
    if (this.WriteQueryStringToCacheOperationCompleted == null)
      this.WriteQueryStringToCacheOperationCompleted = new SendOrPostCallback(this.OnWriteQueryStringToCacheOperationCompleted);
    this.InvokeAsync("WriteQueryStringToCache", new object[1]
    {
      (object) QueryString
    }, this.WriteQueryStringToCacheOperationCompleted, userState);
  }

  private void OnWriteQueryStringToCacheOperationCompleted(object arg)
  {
    if (this.WriteQueryStringToCacheCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.WriteQueryStringToCacheCompleted((object) this, new WriteQueryStringToCacheCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  public new void CancelAsync(object userState) => base.CancelAsync(userState);

  private bool IsLocalFileSystemWebService(string url)
  {
    if (url == null || url == string.Empty)
      return false;
    Uri uri = new Uri(url);
    return uri.Port >= 1024 /*0x0400*/ && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
  }
}
