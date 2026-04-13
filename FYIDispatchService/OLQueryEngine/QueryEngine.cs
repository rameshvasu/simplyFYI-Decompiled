// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.QueryEngine
// Assembly: OLQueryEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0AA84B5-3B48-4798-BE12-176F941E870A
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\OLQueryEngine.dll

using RameshInnovation.VoiShare.OfficeLive.Types;
using RameshInnovation.VoiShare.OfficeLive.Types.OLClientWS;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive;

public class QueryEngine
{
  public static bool IsCalendarList(SessionContext Context)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    if (Context.CurrentViewFields != null)
    {
      foreach (OLListField currentViewField in Context.CurrentViewFields)
      {
        if (currentViewField.Name == "EventDate")
          flag2 = true;
        else if (currentViewField.Name == "EndDate")
          flag3 = true;
        else if (currentViewField.Name == "EventType")
          flag5 = true;
        else if (currentViewField.Name == "fRecurrence")
          flag4 = true;
      }
      if (flag2 && flag3 && flag5 && flag4)
        flag1 = true;
    }
    return flag1;
  }

  public OLListField[] GetCurrentViewFields(SessionContext Context)
  {
    ClientConnectWS olClientWsProxy = Context.OLClientWSProxy;
    OLView currentView = Context.CurrentView;
    OLResource currentResource = Context.CurrentResource;
    OLList currentList = Context.CurrentList;
    OLListField[] currentViewFields = (OLListField[]) null;
    XmlNode listViewXml = Context.ListViewXml;
    try
    {
      XmlNode listViewAsXml = olClientWsProxy.GetListViewAsXml(currentResource.Url, currentList.ID, currentView.ID);
      Context.ListViewXml = listViewAsXml;
      currentViewFields = this.GetListView(listViewAsXml);
    }
    catch (Exception ex)
    {
    }
    return currentViewFields;
  }

  public OLListField[] GetCurrentViewFields(
    ClientConnectWS olws,
    string ResourceUrl,
    string ListID,
    string ViewID)
  {
    OLListField[] currentViewFields = (OLListField[]) null;
    try
    {
      currentViewFields = this.GetListView(olws.GetListViewAsXml(ResourceUrl, ListID, ViewID));
    }
    catch (Exception ex)
    {
    }
    return currentViewFields;
  }

  public DataSet GetListItems(SessionContext Context)
  {
    ClientConnectWS olClientWsProxy = Context.OLClientWSProxy;
    OLResource currentResource = Context.CurrentResource;
    OLList currentList = Context.CurrentList;
    VoiShareOLTypes.WhereClause[] clauses = Context.Clauses;
    QueryEngine.Helper helper = new QueryEngine.Helper();
    DataSet listItems;
    try
    {
      listItems = this.JoinLinkedListDataSets(Context);
    }
    catch (Exception ex)
    {
      listItems = new DataSet();
      listItems.Tables.Add();
      listItems.Tables[0].Columns.Add("Error Description");
      listItems.Tables[0].Rows.Add((object) ex.Message);
    }
    return listItems;
  }

  private string FormatCurrencyData(string CurrencyData)
  {
    string str = CurrencyData;
    int num = CurrencyData.IndexOf(".");
    if (num > 0)
      str = CurrencyData.Substring(0, num + 2 + 1);
    return str;
  }

  public DataSet JoinLinkedListDataSets(SessionContext Context)
  {
    DataSet dataSet1 = (DataSet) null;
    DataSet dataSet2 = (DataSet) null;
    QueryEngine.Helper helper = new QueryEngine.Helper();
    DataSet listViewsAsDataSet1 = helper.GetListViewsAsDataSet(Context, false);
    if (listViewsAsDataSet1 != null)
    {
      DataSet[] ChildrenDS = (DataSet[]) null;
      ArrayList arrayList = new ArrayList();
      if (Context.LinkedLists != null && Context.LinkedLists.Length > 0)
      {
        foreach (VoiShareOLTypes.LinkedList linkedList in Context.LinkedLists)
        {
          if (linkedList.ViewFields != null)
          {
            string[] selectedViewFields = helper.GetSelectedViewFields(linkedList.ViewFields);
            if (selectedViewFields != null && selectedViewFields.Length > 0)
            {
              DataSet listViewsAsDataSet2 = helper.GetListViewsAsDataSet(Context.OLClientWSProxy, linkedList.Resource.Url, linkedList.ListID, (string) null, linkedList.ViewFields, (string) null, false);
              arrayList.Add((object) listViewsAsDataSet2);
            }
          }
        }
      }
      if (arrayList.Count > 0)
        ChildrenDS = (DataSet[]) arrayList.ToArray(typeof (DataSet));
      dataSet2 = helper.GetJoinedDataSet(listViewsAsDataSet1, ChildrenDS, Context);
      if (dataSet2 == null)
      {
        dataSet2 = new DataSet();
        dataSet2.Tables.Add("row");
        foreach (DataColumn column in (InternalDataCollectionBase) listViewsAsDataSet1.Tables["row"].Columns)
        {
          for (int index = 0; index < Context.CurrentViewFields.Length; ++index)
          {
            if (Context.CurrentViewFields[index].IsSelected && column.ColumnName.Equals("ows_" + Context.CurrentViewFields[index].Name))
              dataSet2.Tables[0].Columns.Add($"{column.ColumnName}-{Context.CurrentList.Name}", column.DataType);
          }
        }
        int index1 = 0;
        foreach (DataRow row in (InternalDataCollectionBase) listViewsAsDataSet1.Tables["row"].Rows)
        {
          dataSet2.Tables[0].Rows.Add();
          foreach (DataColumn column in (InternalDataCollectionBase) listViewsAsDataSet1.Tables["row"].Columns)
          {
            foreach (OLListField currentViewField in Context.CurrentViewFields)
            {
              if (currentViewField.IsSelected && column.ColumnName.Equals("ows_" + currentViewField.Name))
              {
                int ordinal = dataSet2.Tables[0].Columns[$"ows_{currentViewField.Name}-{Context.CurrentList.Name}"].Ordinal;
                dataSet2.Tables[0].Rows[index1][ordinal] = row["ows_" + currentViewField.Name];
              }
            }
          }
          ++index1;
        }
      }
    }
    else if (Context.LinkedLists != null && Context.LinkedLists.Length > 0)
    {
      foreach (VoiShareOLTypes.LinkedList linkedList in Context.LinkedLists)
      {
        if (linkedList.ViewFields != null)
        {
          string[] selectedViewFields = helper.GetSelectedViewFields(linkedList.ViewFields);
          if (selectedViewFields != null && selectedViewFields.Length > 0)
          {
            dataSet1 = helper.GetListViewsAsDataSet(Context.OLClientWSProxy, linkedList.Resource.Url, linkedList.ListID, (string) null, linkedList.ViewFields, (string) null, false);
            break;
          }
        }
      }
      dataSet2 = new DataSet();
      dataSet2.Tables.Add("row");
      foreach (DataColumn column in (InternalDataCollectionBase) dataSet1.Tables["row"].Columns)
      {
        for (int index = 0; index < Context.LinkedLists[Context.SelectedLinkedListIndex].ViewFields.Length; ++index)
        {
          if (Context.LinkedLists[Context.SelectedLinkedListIndex].ViewFields[index].IsSelected && column.ColumnName.Equals("ows_" + Context.LinkedLists[Context.SelectedLinkedListIndex].ViewFields[index].Name))
            dataSet2.Tables[0].Columns.Add($"{column.ColumnName}-{Context.LinkedLists[Context.SelectedLinkedListIndex].ListName}", column.DataType);
        }
      }
      int index2 = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataSet1.Tables["row"].Rows)
      {
        dataSet2.Tables[0].Rows.Add();
        foreach (DataColumn column in (InternalDataCollectionBase) dataSet1.Tables["row"].Columns)
        {
          foreach (OLListField viewField in Context.LinkedLists[Context.SelectedLinkedListIndex].ViewFields)
          {
            if (viewField.IsSelected && column.ColumnName.Equals("ows_" + viewField.Name))
            {
              int ordinal = dataSet2.Tables[0].Columns[$"ows_{viewField.Name}-{Context.LinkedLists[Context.SelectedLinkedListIndex].ListName}"].Ordinal;
              dataSet2.Tables[0].Rows[index2][ordinal] = row["ows_" + viewField.Name];
            }
          }
        }
        ++index2;
      }
    }
    return dataSet2;
  }

  public SessionContext CopySessionContext(SessionContext Origin)
  {
    SessionContext sessionContext = new SessionContext(Origin.Session);
    sessionContext.PageName = Origin.PageName;
    sessionContext.OLClientWSProxy = Origin.OLClientWSProxy;
    sessionContext.Resources = Origin.Resources;
    sessionContext.ResourceTypes = Origin.ResourceTypes;
    sessionContext.Lists = Origin.Lists;
    sessionContext.ListViews = Origin.ListViews;
    sessionContext.CurrentList = Origin.CurrentList;
    sessionContext.CurrentResource = Origin.CurrentResource;
    sessionContext.CurrentResourceType = Origin.CurrentResourceType;
    sessionContext.CurrentView = Origin.CurrentView;
    if (Origin.LinkedLists != null && Origin.LinkedLists.Length > 0)
    {
      sessionContext.LinkedLists = new VoiShareOLTypes.LinkedList[Origin.LinkedLists.Length];
      sessionContext.SelectedLinkedListIndex = Origin.SelectedLinkedListIndex;
      for (int index1 = 0; index1 < Origin.LinkedLists.Length; ++index1)
      {
        sessionContext.LinkedLists[index1].ListID = Origin.LinkedLists[index1].ListID;
        sessionContext.LinkedLists[index1].ListName = Origin.LinkedLists[index1].ListName;
        sessionContext.LinkedLists[index1].ListViewXml = Origin.LinkedLists[index1].ListViewXml;
        sessionContext.LinkedLists[index1].Resource = Origin.LinkedLists[index1].Resource;
        sessionContext.LinkedLists[index1].ResourceType = Origin.LinkedLists[index1].ResourceType;
        if (Origin.LinkedLists[index1].ViewFields != null)
        {
          sessionContext.LinkedLists[index1].ViewFields = new OLListField[Origin.LinkedLists[index1].ViewFields.Length];
          for (int index2 = 0; index2 < Origin.LinkedLists[index1].ViewFields.Length; ++index2)
          {
            sessionContext.LinkedLists[index1].ViewFields[index2] = new OLListField();
            sessionContext.LinkedLists[index1].ViewFields[index2].DisplayName = Origin.LinkedLists[index1].ViewFields[index2].DisplayName;
            sessionContext.LinkedLists[index1].ViewFields[index2].EndPointType = Origin.LinkedLists[index1].ViewFields[index2].EndPointType;
            sessionContext.LinkedLists[index1].ViewFields[index2].ID = Origin.LinkedLists[index1].ViewFields[index2].ID;
            sessionContext.LinkedLists[index1].ViewFields[index2].IsEndPoint = Origin.LinkedLists[index1].ViewFields[index2].IsEndPoint;
            sessionContext.LinkedLists[index1].ViewFields[index2].IsLookUpField = Origin.LinkedLists[index1].ViewFields[index2].IsLookUpField;
            sessionContext.LinkedLists[index1].ViewFields[index2].IsSelected = Origin.LinkedLists[index1].ViewFields[index2].IsSelected;
            sessionContext.LinkedLists[index1].ViewFields[index2].LookupListUniqueID = Origin.LinkedLists[index1].ViewFields[index2].LookupListUniqueID;
            sessionContext.LinkedLists[index1].ViewFields[index2].Name = Origin.LinkedLists[index1].ViewFields[index2].Name;
            sessionContext.LinkedLists[index1].ViewFields[index2].Type = Origin.LinkedLists[index1].ViewFields[index2].Type;
          }
        }
      }
    }
    if (Origin.CurrentView != null)
    {
      sessionContext.CurrentViewFields = new OLListField[Origin.CurrentViewFields.Length];
      for (int index = 0; index < Origin.CurrentViewFields.Length; ++index)
      {
        sessionContext.CurrentViewFields[index] = new OLListField();
        sessionContext.CurrentViewFields[index].DisplayName = Origin.CurrentViewFields[index].DisplayName;
        sessionContext.CurrentViewFields[index].EndPointType = Origin.CurrentViewFields[index].EndPointType;
        sessionContext.CurrentViewFields[index].ID = Origin.CurrentViewFields[index].ID;
        sessionContext.CurrentViewFields[index].IsEndPoint = Origin.CurrentViewFields[index].IsEndPoint;
        sessionContext.CurrentViewFields[index].IsLookUpField = Origin.CurrentViewFields[index].IsLookUpField;
        sessionContext.CurrentViewFields[index].IsSelected = Origin.CurrentViewFields[index].IsSelected;
        sessionContext.CurrentViewFields[index].LookupListUniqueID = Origin.CurrentViewFields[index].LookupListUniqueID;
        sessionContext.CurrentViewFields[index].Name = Origin.CurrentViewFields[index].Name;
        sessionContext.CurrentViewFields[index].Type = Origin.CurrentViewFields[index].Type;
      }
    }
    sessionContext.DisplayEndPoints = Origin.DisplayEndPoints;
    sessionContext.EndPointType = Origin.EndPointType;
    sessionContext.Clauses = Origin.Clauses;
    sessionContext.Mode = Origin.Mode;
    sessionContext.SubMode = Origin.SubMode;
    return sessionContext;
  }

  public DataSet[] MakeChildrenDSArrayForSessionContext(SessionContext Context)
  {
    DataSet[] dataSetArray = (DataSet[]) null;
    QueryEngine.Helper helper = new QueryEngine.Helper();
    if (Context.LinkedLists != null)
    {
      ArrayList arrayList = new ArrayList();
      foreach (VoiShareOLTypes.LinkedList linkedList in Context.LinkedLists)
      {
        if (helper.GetSelectedViewFields(linkedList.ViewFields) != null)
        {
          DataSet listViewsAsDataSet = helper.GetListViewsAsDataSet(Context.OLClientWSProxy, linkedList.Resource.Url, linkedList.ListID, (string) null, (string[]) null, (string) null);
          arrayList.Add((object) listViewsAsDataSet);
        }
      }
      dataSetArray = (DataSet[]) arrayList.ToArray(typeof (DataSet));
    }
    return dataSetArray;
  }

  public DataSet JoinSearchAndSelectAsDataSet(
    SessionContext SearchContext,
    SessionContext SelectContext,
    VoiShareOLTypes.JoinType JoinType)
  {
    ClientConnectWS olClientWsProxy = SearchContext.OLClientWSProxy;
    QueryEngine.Helper helper = new QueryEngine.Helper();
    DataSet dataSet = (DataSet) null;
    ArrayList arrayList = new ArrayList();
    string str = (string) null;
    if (JoinType == 0)
    {
      SessionContext Context;
      if (SelectContext.LinkedLists != null && SelectContext.LinkedLists.Length > 0)
      {
        Context = this.CopySessionContext(SearchContext);
        string[] selectedViewFields = helper.GetSelectedViewFields(SelectContext.LinkedLists[SelectContext.SelectedLinkedListIndex].ViewFields);
        string listId = SelectContext.LinkedLists[SelectContext.SelectedLinkedListIndex].ListID;
        foreach (VoiShareOLTypes.LinkedList linkedList in Context.LinkedLists)
        {
          if (linkedList.ListID.Equals(listId))
          {
            foreach (OLListField viewField in linkedList.ViewFields)
            {
              if (viewField.Name.Equals(selectedViewFields[0]))
              {
                viewField.IsSelected = true;
                viewField.IsEndPoint = true;
              }
            }
          }
        }
      }
      else
      {
        Context = SearchContext;
        string[] selectedViewFields = helper.GetSelectedViewFields(SelectContext.CurrentViewFields);
        foreach (OLListField currentViewField in SelectContext.CurrentViewFields)
        {
          if (currentViewField.Name.Equals(selectedViewFields[0]))
          {
            currentViewField.IsSelected = true;
            currentViewField.IsEndPoint = true;
          }
        }
      }
      DataSet listViewsAsDataSet1 = helper.GetListViewsAsDataSet(SearchContext, false);
      DataSet[] ChildrenDS1 = this.MakeChildrenDSArrayForSessionContext(Context);
      DataSet listViewsAsDataSet2 = helper.GetListViewsAsDataSet(SelectContext, false);
      DataSet[] ChildrenDS2 = this.MakeChildrenDSArrayForSessionContext(SelectContext);
      if (listViewsAsDataSet1 != null && listViewsAsDataSet2 != null)
        dataSet = this.JoinByCartesianProduct(helper.GetJoinedDataSet(listViewsAsDataSet1, ChildrenDS1, SearchContext), helper.GetJoinedDataSet(listViewsAsDataSet2, ChildrenDS2, SelectContext));
    }
    else if (SearchContext.SubMode == 3 && SelectContext.SubMode == 1)
    {
      string[] selectedViewFields = helper.GetSelectedViewFields(SelectContext.CurrentViewFields);
      str = SelectContext.CurrentList.ID;
      SessionContext Context = this.CopySessionContext(SearchContext);
      foreach (OLListField currentViewField in Context.CurrentViewFields)
      {
        if (currentViewField.Name.Equals(selectedViewFields[0]))
        {
          currentViewField.IsSelected = true;
          currentViewField.IsEndPoint = true;
        }
      }
      DataSet listViewsAsDataSet = helper.GetListViewsAsDataSet(SearchContext, false);
      if (listViewsAsDataSet != null)
        dataSet = helper.GetJoinedDataSet(listViewsAsDataSet, (DataSet[]) null, Context);
    }
    else if (SearchContext.SubMode == 3 && SelectContext.SubMode == 3)
    {
      DataSet listViewsAsDataSet3 = helper.GetListViewsAsDataSet(SearchContext, false);
      DataSet listViewsAsDataSet4 = helper.GetListViewsAsDataSet(SelectContext, false);
      DataSet[] ChildrenDS3 = this.MakeChildrenDSArrayForSessionContext(SearchContext);
      DataSet[] ChildrenDS4 = this.MakeChildrenDSArrayForSessionContext(SelectContext);
      if (listViewsAsDataSet3 != null && listViewsAsDataSet4 != null)
        dataSet = this.JoinByCartesianProduct(helper.GetJoinedDataSet(listViewsAsDataSet3, ChildrenDS3, SearchContext), helper.GetJoinedDataSet(listViewsAsDataSet4, ChildrenDS4, SelectContext));
    }
    else if (SearchContext.SubMode != 3 || SelectContext.SubMode != 2)
      ;
    return dataSet;
  }

  private DataSet JoinByCartesianProduct(DataSet JoinedSearchDS, DataSet JoinedSelectDS)
  {
    DataSet dataSet = new DataSet();
    dataSet.Tables.Add();
    foreach (DataColumn column in (InternalDataCollectionBase) JoinedSearchDS.Tables[0].Columns)
    {
      string columnName = column.ColumnName;
      dataSet.Tables[0].Columns.Add(columnName, column.DataType);
    }
    foreach (DataColumn column in (InternalDataCollectionBase) JoinedSelectDS.Tables[0].Columns)
    {
      string columnName = column.ColumnName;
      dataSet.Tables[0].Columns.Add(columnName, column.DataType);
    }
    foreach (DataRow row1 in (InternalDataCollectionBase) JoinedSearchDS.Tables[0].Rows)
    {
      foreach (DataRow row2 in (InternalDataCollectionBase) JoinedSelectDS.Tables[0].Rows)
      {
        object[] objArray = new object[JoinedSearchDS.Tables[0].Columns.Count + JoinedSelectDS.Tables[0].Columns.Count];
        for (int columnIndex = 0; columnIndex < JoinedSearchDS.Tables[0].Columns.Count; ++columnIndex)
          objArray[columnIndex] = (object) row1[columnIndex].ToString();
        for (int columnIndex = 0; columnIndex < JoinedSelectDS.Tables[0].Columns.Count; ++columnIndex)
          objArray[JoinedSearchDS.Tables[0].Columns.Count + columnIndex] = (object) row2[columnIndex].ToString();
        dataSet.Tables[0].Rows.Add(objArray);
      }
    }
    return dataSet;
  }

  public OLListField[] GetListView(XmlNode ListView)
  {
    OLListField[] listView = (OLListField[]) null;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(ListView.OuterXml);
    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
    nsmgr.AddNamespace("sp", ListView.NamespaceURI);
    XmlNodeList xmlNodeList1 = xmlDocument.SelectNodes("//sp:ListAndView/sp:List/sp:Fields/sp:Field", nsmgr);
    XmlNodeList xmlNodeList2 = xmlDocument.SelectNodes("//sp:ListAndView/sp:View/sp:ViewFields/sp:FieldRef", nsmgr);
    if (xmlNodeList1.Count > 0)
    {
      ArrayList arrayList = new ArrayList();
      foreach (XmlNode xmlNode1 in xmlNodeList1)
      {
        bool flag = false;
        if (xmlNode1.Attributes["Required"] != null && xmlNode1.Attributes["Required"].Value.ToLower().Equals("true"))
        {
          flag = true;
        }
        else
        {
          foreach (XmlNode xmlNode2 in xmlNodeList2)
          {
            if (xmlNode1.Attributes["Name"].Value.Equals(xmlNode2.Attributes["Name"].Value))
              flag = true;
          }
        }
        if (flag)
        {
          OLListField olListField = new OLListField();
          olListField.Name = xmlNode1.Attributes["Name"].Value;
          olListField.DisplayName = xmlNode1.Attributes["DisplayName"].Value;
          olListField.ID = xmlNode1.Attributes["ID"].Value;
          olListField.Type = xmlNode1.Attributes["Type"].Value;
          if (olListField.Type.Equals("Lookup"))
          {
            olListField.IsLookUpField = true;
            olListField.LookupListUniqueID = xmlNode1.Attributes["List"].Value;
          }
          else
          {
            olListField.IsLookUpField = false;
            olListField.LookupListUniqueID = (string) null;
          }
          arrayList.Add((object) olListField);
        }
      }
      listView = (OLListField[]) arrayList.ToArray(typeof (OLListField));
    }
    return listView;
  }

  private class Helper
  {
    public string[] GetContextSelectedViewFields(
      SessionContext Context,
      bool IncludeLinkedListSelections,
      bool IncludeListName)
    {
      ArrayList arrayList = new ArrayList();
      foreach (OLListField currentViewField in Context.CurrentViewFields)
      {
        if (currentViewField.IsSelected)
        {
          string str = !IncludeListName ? currentViewField.Name.Replace("_x0020_", " ") : $"{currentViewField.Name.Replace("_x0020_", " ")}-{Context.CurrentList.Name}";
          arrayList.Add((object) str);
        }
      }
      if (IncludeLinkedListSelections && Context.LinkedLists != null)
      {
        foreach (VoiShareOLTypes.LinkedList linkedList in Context.LinkedLists)
        {
          if (linkedList.ViewFields != null)
          {
            foreach (OLListField viewField in linkedList.ViewFields)
            {
              if (viewField.IsSelected)
              {
                string str = !IncludeListName ? viewField.Name.Replace("_x0020_", " ") : $"{viewField.Name.Replace("_x0020_", " ")}-{linkedList.ListName}";
                arrayList.Add((object) str);
              }
            }
          }
        }
      }
      return (string[]) arrayList.ToArray(typeof (string));
    }

    public string[] GetSelectedViewFields(OLListField[] ViewFields)
    {
      ArrayList arrayList = new ArrayList();
      foreach (OLListField viewField in ViewFields)
      {
        if (viewField.IsSelected)
          arrayList.Add((object) viewField.Name.Replace("_x0020_", " "));
      }
      return (string[]) arrayList.ToArray(typeof (string));
    }

    public string[] GetViewFields(OLListField[] ViewFields)
    {
      ArrayList arrayList = new ArrayList();
      foreach (OLListField viewField in ViewFields)
        arrayList.Add((object) viewField.Name.Replace("_x0020_", " "));
      return (string[]) arrayList.ToArray(typeof (string));
    }

    private string GetParentLookupFieldName(OLListField[] LookupViewFields, string LookupListID)
    {
      string parentLookupFieldName = (string) null;
      foreach (OLListField lookupViewField in LookupViewFields)
      {
        if (lookupViewField.IsLookUpField && lookupViewField.LookupListUniqueID.Equals(LookupListID))
        {
          parentLookupFieldName = "ows_" + lookupViewField.Name;
          break;
        }
      }
      return parentLookupFieldName;
    }

    private string[] GetParentLookupFieldValues(DataSet SearchDS, string ParentLookupFieldName)
    {
      string[] lookupFieldValues = (string[]) null;
      if (SearchDS.Tables["row"].Columns.Contains(ParentLookupFieldName))
      {
        lookupFieldValues = new string[SearchDS.Tables["row"].Rows.Count];
        int index = 0;
        foreach (DataRow row in (InternalDataCollectionBase) SearchDS.Tables["row"].Rows)
        {
          lookupFieldValues[index] = row[ParentLookupFieldName].ToString();
          if (lookupFieldValues[index].IndexOf(";#") >= 0)
          {
            lookupFieldValues[index] = lookupFieldValues[index].Substring(0, lookupFieldValues[index].IndexOf(";#"));
            ++index;
          }
        }
      }
      return lookupFieldValues;
    }

    private DataRow[] GetSearchListDataRows(
      DataSet SearchDS,
      string ParentLookupFieldName,
      string[] ParentLookupFieldValues)
    {
      string filterExpression = (string) null;
      DataRow[] searchListDataRows;
      if (ParentLookupFieldName != null)
      {
        for (int index = 0; index < ParentLookupFieldValues.Length; ++index)
        {
          if (index == 0)
            filterExpression = $"{ParentLookupFieldName} like '%{ParentLookupFieldValues[index]};#%'";
          else
            filterExpression = $"{filterExpression} Or {ParentLookupFieldName} like '%{ParentLookupFieldValues[index]};#%'";
        }
        searchListDataRows = SearchDS.Tables["row"].Select(filterExpression);
      }
      else
        searchListDataRows = SearchDS.Tables["row"].Select();
      return searchListDataRows;
    }

    private DataRow[] GetMatchingSelectListDataRows(
      DataSet SelectDS,
      string ChildLookupFieldID,
      string ParentLookupValue)
    {
      string filterExpression = $"{ChildLookupFieldID}='{ParentLookupValue}'";
      return SelectDS.Tables["row"].Select(filterExpression);
    }

    private DataSet CreateEmptyJoinedDataSet(
      DataSet ParentDS,
      DataSet[] ChildrenDS,
      SessionContext Context)
    {
      int index1 = -1;
      QueryEngine.Helper helper = new QueryEngine.Helper();
      DataSet emptyJoinedDataSet = new DataSet();
      emptyJoinedDataSet.Tables.Add();
      foreach (DataColumn column in (InternalDataCollectionBase) ParentDS.Tables["row"].Columns)
      {
        for (int index2 = 0; index2 < Context.CurrentViewFields.Length; ++index2)
        {
          if (Context.CurrentViewFields[index2].IsSelected)
          {
            string str = column.ColumnName;
            if (column.ColumnName.Contains(" "))
              str = column.ColumnName.Replace(" ", "_x0020_");
            if (str.Equals("ows_" + Context.CurrentViewFields[index2].Name) && !emptyJoinedDataSet.Tables[0].Columns.Contains($"{str}-{Context.CurrentList.Name}"))
              emptyJoinedDataSet.Tables[0].Columns.Add($"{str}-{Context.CurrentList.Name}", column.DataType);
          }
        }
      }
      if (ChildrenDS != null)
      {
        for (int index3 = 0; index3 < Context.LinkedLists.Length; ++index3)
        {
          if (Context.LinkedLists[index3].ViewFields != null && this.GetSelectedViewFields(Context.LinkedLists[index3].ViewFields).Length > 0)
          {
            ++index1;
            DataSet dataSet = ChildrenDS[index1];
            if (dataSet.Tables.Count > 0)
            {
              OLListField[] viewFields = Context.LinkedLists[index3].ViewFields;
              string listName = Context.LinkedLists[index3].ListName;
              foreach (DataColumn column in (InternalDataCollectionBase) dataSet.Tables["row"].Columns)
              {
                for (int index4 = 0; index4 < viewFields.Length; ++index4)
                {
                  string dataSetFieldName = this.GetDataSetFieldName(viewFields[index4].Name, viewFields[index4].DisplayName);
                  if (viewFields[index4].IsSelected && column.ColumnName.Equals(dataSetFieldName))
                    emptyJoinedDataSet.Tables[0].Columns.Add($"{column.ColumnName}-{listName}", column.DataType);
                }
              }
            }
          }
        }
      }
      return emptyJoinedDataSet;
    }

    private DataSet CreateEmptyJoinedDataSet(
      DataSet ParentDS,
      DataSet ChildDS,
      OLListField[] ParentViewFields,
      OLListField[] ChildViewFields,
      string ParentListName,
      string ChildListName)
    {
      QueryEngine.Helper helper = new QueryEngine.Helper();
      DataSet emptyJoinedDataSet = new DataSet();
      emptyJoinedDataSet.Tables.Add();
      string str = (string) null;
      try
      {
        foreach (DataColumn column in (InternalDataCollectionBase) ParentDS.Tables["row"].Columns)
        {
          str = column.ColumnName;
          for (int index = 0; index < ParentViewFields.Length; ++index)
          {
            if (column.ColumnName.Equals("ows_" + ParentViewFields[index].Name) && !ParentViewFields[index].IsLookUpField && ParentViewFields[index].IsSelected)
            {
              emptyJoinedDataSet.Tables[0].Columns.Add($"{column.ColumnName}-{ParentListName}", column.DataType);
              break;
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      foreach (DataColumn column in (InternalDataCollectionBase) ChildDS.Tables["row"].Columns)
      {
        for (int index = 0; index < ChildViewFields.Length; ++index)
        {
          if (ChildViewFields[index].IsSelected && column.ColumnName.Equals("ows_" + ChildViewFields[index].Name))
          {
            emptyJoinedDataSet.Tables[0].Columns.Add($"{column.ColumnName}-{ChildListName}", column.DataType);
            break;
          }
        }
      }
      return emptyJoinedDataSet;
    }

    public DataSet GetListViewsAsDataSet(SessionContext Context, bool OnlySelectedFields)
    {
      ClientConnectWS olClientWsProxy = Context.OLClientWSProxy;
      OLResource currentResource = Context.CurrentResource;
      OLList currentList = Context.CurrentList;
      OLView currentView = Context.CurrentView;
      string[] strArray = (string[]) null;
      DataSet listViewsAsDataSet = (DataSet) null;
      string str1 = (string) null;
      string empty = string.Empty;
      string str2 = (string) null;
      bool flag = false;
      string str3 = Context.ExtendedProperties == null || Context.ExtendedProperties[(object) "WHERE_CLAUSES_BOOLEAN_OPERATOR"] == null ? "And" : Context.ExtendedProperties[(object) "WHERE_CLAUSES_BOOLEAN_OPERATOR"].ToString();
      if (Context.Mode == 1)
      {
        StringBuilder stringBuilder1 = new StringBuilder();
        int num1 = int.Parse(ConfigurationSettings.AppSettings["ConditionSetterLineItems"]);
        int num2 = 0;
        for (int index = 0; index < num1; ++index)
        {
          if (Context.Clauses[index].CAML != null && Context.Clauses[index].Include.Equals(true))
          {
            stringBuilder1.Append(Context.Clauses[index].CAML);
            ++num2;
          }
        }
        flag = QueryEngine.IsCalendarList(Context);
        StringBuilder stringBuilder2 = new StringBuilder();
        if (flag)
        {
          stringBuilder2.Append("<DateRangesOverlap>");
          stringBuilder2.Append("<FieldRef Name=\"EventDate\"></FieldRef>");
          stringBuilder2.Append("<FieldRef Name=\"EndDate\"></FieldRef>");
          stringBuilder2.Append("<FieldRef Name=\"RecurrenceID\"></FieldRef>");
          stringBuilder2.Append("<Value IncludeTimeValue=\"TRUE\" Type=\"DateTime\"><Day/></Value>");
          stringBuilder2.Append("</DateRangesOverlap>");
        }
        if (num2 > 1)
        {
          stringBuilder1.Insert(0, $"<Query><Where><{str3}>");
          if (flag)
            stringBuilder1.Append(stringBuilder2.ToString());
          stringBuilder1.Append($"</{str3}></Where></Query>");
          str1 = stringBuilder1.ToString();
        }
        else if (num2 == 1)
        {
          stringBuilder1.Insert(0, "<Query><Where>");
          if (flag)
            stringBuilder1.Append(stringBuilder2.ToString());
          stringBuilder1.Append("</Where></Query>");
          str1 = stringBuilder1.ToString();
        }
      }
      if (OnlySelectedFields)
        strArray = this.GetContextSelectedViewFields(Context, false, false);
      try
      {
        DataSet SearchDS = olClientWsProxy.ExecuteQuery(currentResource.Url, currentList.ID, currentView.ID, str1, strArray, str2);
        if (SearchDS != null)
        {
          this.FixMissingColumnsInSearchDataset(ref SearchDS, this.GetContextSelectedViewFields(Context, false, false));
          listViewsAsDataSet = !flag ? SearchDS : this.ExpandRecurringCalendarEvents(SearchDS, Context);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return listViewsAsDataSet;
    }

    private DataSet ExpandRecurringCalendarEvents(DataSet SearchDS, SessionContext Context)
    {
      DataSet dataSet = new DataSet();
      string s1 = (string) null;
      string s2 = (string) null;
      DateTime dateTime1 = new DateTime();
      DateTime dateTime2 = new DateTime();
      dataSet.Tables.Add(SearchDS.Tables["row"].Copy());
      dataSet.Tables["row"].Clear();
      DateTime dateTime3;
      DateTime dateTime4;
      if (Context.ExtendedProperties != null && Context.ExtendedProperties[(object) "CALENDAR_DATE_RANGE_OPTION"] != null)
      {
        string str = Context.ExtendedProperties[(object) "CALENDAR_DATE_RANGE_OPTION"].ToString();
        if (str.StartsWith("0"))
        {
          s1 = DateTime.Now.ToShortDateString();
          dateTime3 = DateTime.Parse(s1).AddDays(1.0);
          s2 = dateTime3.ToShortDateString();
        }
        else if (str.StartsWith("1"))
        {
          s1 = Utilities.ResolveTodayDateExpression(str.Substring(2));
          if (s1 != null)
          {
            dateTime4 = DateTime.Parse(s1);
            dateTime3 = dateTime4.AddDays(1.0);
            s2 = dateTime3.ToShortDateString();
          }
        }
        else if (str.StartsWith("2"))
        {
          string[] strArray = str.Substring(2).Split(";".ToCharArray());
          s1 = Utilities.ResolveTodayDateExpression(strArray[0]);
          s2 = Utilities.ResolveTodayDateExpression(strArray[1]);
          if (s1 != null && s2 != null)
          {
            dateTime2 = DateTime.Parse(s1);
            dateTime1 = DateTime.Parse(s2).AddDays(1.0);
          }
        }
      }
      else
      {
        s1 = DateTime.Now.ToShortDateString();
        dateTime3 = DateTime.Parse(s1).AddDays(1.0);
        s2 = dateTime3.ToShortDateString();
      }
      if (s2 != null && s1 != null)
      {
        TimeSpan timeSpan = DateTime.Parse(s2) - DateTime.Parse(s1);
        foreach (DataRow row1 in (InternalDataCollectionBase) SearchDS.Tables["row"].Rows)
        {
          DateTime dateTime5 = Convert.ToDateTime(row1["ows_EventDate"]);
          DateTime dateTime6 = Convert.ToDateTime(row1["ows_EndDate"]);
          if (row1["ows_fRecurrence"].ToString() == "1")
          {
            for (int index = 0; index < timeSpan.Days; ++index)
            {
              dateTime4 = DateTime.Parse(s1);
              dateTime4 = dateTime4.AddDays((double) index);
              dateTime3 = dateTime4;
              if (dateTime4.Ticks > dateTime5.Ticks && dateTime4.Ticks < dateTime6.Ticks)
              {
                dateTime4 = dateTime4.AddHours((double) dateTime5.Hour);
                dateTime4 = dateTime4.AddMinutes((double) dateTime5.Minute);
                dateTime4 = dateTime4.AddSeconds((double) dateTime5.Second);
                dateTime3 = dateTime3.AddHours((double) dateTime6.Hour);
                dateTime3 = dateTime3.AddMinutes((double) dateTime6.Minute);
                dateTime3 = dateTime3.AddSeconds((double) dateTime6.Second);
                DataRow row2 = dataSet.Tables["row"].NewRow();
                for (int columnIndex = 0; columnIndex < row1.Table.Columns.Count; ++columnIndex)
                  row2[columnIndex] = row1[columnIndex];
                row2["ows_EventDate"] = (object) dateTime4;
                row2["ows_EndDate"] = (object) dateTime3;
                dataSet.Tables["row"].Rows.Add(row2);
              }
            }
          }
          else
          {
            dateTime4 = DateTime.Parse(s1);
            dateTime3 = DateTime.Parse(s2);
            if (dateTime5.Ticks >= dateTime4.Ticks && dateTime6.Ticks < dateTime3.Ticks)
            {
              DataRow row3 = dataSet.Tables["row"].NewRow();
              for (int columnIndex = 0; columnIndex < row1.Table.Columns.Count; ++columnIndex)
                row3[columnIndex] = row1[columnIndex];
              row3["ows_EventDate"] = (object) Convert.ToDateTime(row3["ows_EventDate"]);
              row3["ows_EndDate"] = (object) Convert.ToDateTime(row3["ows_EndDate"]);
              dataSet.Tables["row"].Rows.Add(row3);
            }
          }
        }
      }
      return dataSet;
    }

    public DataSet GetListViewsAsDataSet(
      ClientConnectWS olws,
      string CurrentResourceUrl,
      string CurrentListID,
      string CurrentViewID,
      string[] SelectedViewFields,
      string CAML)
    {
      DataSet SearchDS;
      try
      {
        SearchDS = olws.ExecuteQuery(CurrentResourceUrl, CurrentListID, CurrentViewID, CAML, SelectedViewFields, (string) null);
        this.FixMissingColumnsInSearchDataset(ref SearchDS, SelectedViewFields);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return SearchDS;
    }

    public DataSet GetListViewsAsDataSet(
      ClientConnectWS olws,
      string CurrentResourceUrl,
      string CurrentListID,
      string CurrentViewID,
      OLListField[] ViewFields,
      string CAML,
      bool OnlySelectedFields)
    {
      string[] ViewFields1 = (string[]) null;
      if (OnlySelectedFields)
        ViewFields1 = this.GetSelectedViewFields(ViewFields);
      DataSet SearchDS;
      try
      {
        SearchDS = olws.ExecuteQuery(CurrentResourceUrl, CurrentListID, CurrentViewID, CAML, ViewFields1, (string) null);
        this.FixMissingColumnsInSearchDataset(ref SearchDS, ViewFields1);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return SearchDS;
    }

    private DataSet GetJoinedDataSetLinked(
      DataSet SearchDS,
      DataSet SelectDS,
      SessionContext SelectContext,
      SessionContext SearchContext,
      VoiShareOLTypes.JoinType TypeOfJoin)
    {
      return (DataSet) null;
    }

    public DataSet GetJoinedDataSet(DataSet ParentDS, DataSet[] ChildrenDS, SessionContext Context)
    {
      int index1 = -1;
      DataSet emptyJoinedDataSet = this.CreateEmptyJoinedDataSet(ParentDS, ChildrenDS, Context);
      MemoryStream memoryStream = new MemoryStream();
      ParentDS.WriteXml((Stream) memoryStream, XmlWriteMode.WriteSchema);
      memoryStream.Position = 0L;
      new StreamReader((Stream) memoryStream).ReadToEnd();
      if (Context.LinkedLists == null || Context.LinkedLists != null && Context.LinkedLists.Length == 0)
      {
        int index2 = 0;
        foreach (DataRow row in (InternalDataCollectionBase) ParentDS.Tables["row"].Rows)
        {
          emptyJoinedDataSet.Tables[0].Rows.Add();
          foreach (DataColumn column in (InternalDataCollectionBase) ParentDS.Tables["row"].Columns)
          {
            foreach (OLListField currentViewField in Context.CurrentViewFields)
            {
              string str = column.ColumnName.Replace(" ", "_x0020_");
              if (currentViewField.IsSelected && str.Equals("ows_" + currentViewField.Name))
              {
                int ordinal = emptyJoinedDataSet.Tables[0].Columns[$"ows_{currentViewField.Name}-{Context.CurrentList.Name}"].Ordinal;
                emptyJoinedDataSet.Tables[0].Rows[index2][ordinal] = row[column.ColumnName];
              }
            }
          }
          ++index2;
        }
      }
      else
      {
        for (int index3 = 0; index3 < Context.LinkedLists.Length; ++index3)
        {
          string listId = Context.LinkedLists[index3].ListID;
          string parentLookupFieldName = this.GetParentLookupFieldName(Context.CurrentViewFields, listId);
          string[] lookupFieldValues = this.GetParentLookupFieldValues(ParentDS, parentLookupFieldName);
          if (Context.LinkedLists[index3].ViewFields != null)
            ++index1;
          DataRow[] dataRowArray;
          if (lookupFieldValues != null)
          {
            DataRow[] searchListDataRows = this.GetSearchListDataRows(ParentDS, parentLookupFieldName, lookupFieldValues);
            string ChildLookupFieldID = "ows_ID";
            int index4 = 0;
            dataRowArray = (DataRow[]) null;
            foreach (DataRow dataRow in searchListDataRows)
            {
              if (index3 == 0)
                emptyJoinedDataSet.Tables[0].Rows.Add();
              foreach (DataColumn column1 in (InternalDataCollectionBase) ParentDS.Tables["row"].Columns)
              {
                foreach (OLListField currentViewField in Context.CurrentViewFields)
                {
                  if (currentViewField.IsSelected && (column1.ColumnName.Equals("ows_" + currentViewField.Name) || column1.ColumnName.Equals("ows_" + currentViewField.DisplayName)))
                  {
                    int ordinal1 = emptyJoinedDataSet.Tables[0].Columns[$"ows_{currentViewField.Name}-{Context.CurrentList.Name}"].Ordinal;
                    emptyJoinedDataSet.Tables[0].Rows[index4][ordinal1] = dataRow["ows_" + currentViewField.Name];
                    if (ChildrenDS != null && Context.LinkedLists[index3].ViewFields != null && this.GetSelectedViewFields(Context.LinkedLists[index3].ViewFields).Length > 0 && ChildrenDS[index1].Tables.Count > 0)
                    {
                      foreach (DataRow selectListDataRow in this.GetMatchingSelectListDataRows(ChildrenDS[index1], ChildLookupFieldID, lookupFieldValues[index4]))
                      {
                        foreach (DataColumn column2 in (InternalDataCollectionBase) ChildrenDS[index1].Tables["row"].Columns)
                        {
                          foreach (OLListField viewField in Context.LinkedLists[index3].ViewFields)
                          {
                            string dataSetFieldName = this.GetDataSetFieldName(viewField.Name, viewField.DisplayName);
                            if (viewField.IsSelected && column2.ColumnName.Equals(dataSetFieldName))
                            {
                              int ordinal2 = emptyJoinedDataSet.Tables[0].Columns[$"{dataSetFieldName}-{Context.LinkedLists[index3].ListName}"].Ordinal;
                              emptyJoinedDataSet.Tables[0].Rows[index4][ordinal2] = selectListDataRow[dataSetFieldName];
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
              ++index4;
            }
          }
          else
          {
            DataRow[] searchListDataRows = this.GetSearchListDataRows(ParentDS, (string) null, (string[]) null);
            int index5 = 0;
            dataRowArray = (DataRow[]) null;
            foreach (DataRow dataRow in searchListDataRows)
            {
              emptyJoinedDataSet.Tables[0].Rows.Add();
              foreach (DataColumn column in (InternalDataCollectionBase) ParentDS.Tables["row"].Columns)
              {
                foreach (OLListField currentViewField in Context.CurrentViewFields)
                {
                  if (currentViewField.IsSelected && column.ColumnName.Equals("ows_" + currentViewField.Name))
                  {
                    int ordinal = emptyJoinedDataSet.Tables[0].Columns[$"ows_{currentViewField.Name}-{Context.CurrentList.Name}"].Ordinal;
                    emptyJoinedDataSet.Tables[0].Rows[index5][ordinal] = dataRow["ows_" + currentViewField.Name];
                  }
                }
              }
              ++index5;
            }
          }
        }
      }
      return emptyJoinedDataSet;
    }

    private string GetDataSetFieldName(string FieldName, string FieldDisplayName)
    {
      string dataSetFieldName = "ows_" + FieldDisplayName;
      if (FieldName.Equals("Title"))
        dataSetFieldName = "ows_" + FieldName;
      return dataSetFieldName;
    }

    private void FixMissingColumnsInSearchDataset(ref DataSet SearchDS, string[] ViewFields)
    {
      if (ViewFields == null)
        return;
      foreach (string viewField in ViewFields)
      {
        string str = "ows_" + viewField;
        if (!SearchDS.Tables["row"].Columns.Contains(str))
        {
          SearchDS.Tables["row"].Columns.Add(str);
          SearchDS.Tables["row"].Columns[str].DefaultValue = (object) "";
        }
      }
    }
  }
}
