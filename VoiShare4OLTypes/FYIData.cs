// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Types.FYIData
// Assembly: VoiShare4OLTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4300920-3BDD-4B26-9880-36B6DF8D0959
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\VoiShare4OLTypes.dll

using RameshInnovation.VoiShare.OfficeLive.Types.OLClientWS;
using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Web.UI.WebControls;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive.Types;

[Serializable]
public class FYIData
{
  private FYIData.SearchData m_SearchData;
  private FYIData.SelectData m_SelectData;
  private FYIData.ScribeData m_ScribeData;
  private FYIData.ScheduleData m_ScheduleData;

  public FYIData()
  {
  }

  public FYIData.SearchData Search
  {
    set => this.m_SearchData = value;
    get => this.m_SearchData;
  }

  public FYIData.SelectData Select
  {
    set => this.m_SelectData = value;
    get => this.m_SelectData;
  }

  public FYIData.ScribeData Scribe
  {
    set => this.m_ScribeData = value;
    get => this.m_ScribeData;
  }

  public FYIData.ScheduleData Schedule
  {
    set => this.m_ScheduleData = value;
    get => this.m_ScheduleData;
  }

  public static ListItemCollection GetListItemCollectionFromArrayList(
    ArrayList CustomListItemArrayList)
  {
    ListItemCollection collectionFromArrayList = new ListItemCollection();
    foreach (VoiShareOLTypes.CustomListItem customListItemArray in CustomListItemArrayList)
      collectionFromArrayList.Add(new ListItem()
      {
        Text = customListItemArray.ItemText,
        Value = customListItemArray.ItemValue,
        Selected = customListItemArray.Selected
      });
    return collectionFromArrayList;
  }

  public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
  {
    info.AddValue("SearchData", (object) this.m_SearchData);
    info.AddValue("SelectData", (object) this.m_SelectData);
    info.AddValue("ScribeData", (object) this.m_ScribeData);
    info.AddValue("ScheduleData", (object) this.m_ScheduleData);
  }

  public FYIData(SerializationInfo info)
  {
    FYIData.SearchData searchData = (FYIData.SearchData) info.GetValue("SearchData", typeof (FYIData.SearchData));
    FYIData.SelectData selectData = (FYIData.SelectData) info.GetValue("SelectData", typeof (FYIData.SelectData));
    FYIData.ScribeData scribeData = (FYIData.ScribeData) info.GetValue("ScribeData", typeof (FYIData.ScribeData));
    FYIData.ScheduleData scheduleData = (FYIData.ScheduleData) info.GetValue("ScheduleData", typeof (FYIData.ScheduleData));
  }

  [Serializable]
  public class BaseContextData
  {
    private OLView m_CurrentView = (OLView) null;
    private OLResource m_CurrentResource = (OLResource) null;
    private OLResource m_CurrentResourceType = (OLResource) null;
    private OLList m_CurrentList = (OLList) null;
    private OLListField[] m_CurrentViewFields = (OLListField[]) null;
    private VoiShareOLTypes.WhereClause[] m_Clauses = (VoiShareOLTypes.WhereClause[]) null;
    private VoiShareOLTypes.Mode m_Mode = VoiShareOLTypes.Mode.UNDEFINED;
    private VoiShareOLTypes.SubMode m_SubMode = VoiShareOLTypes.SubMode.UNDEFINED;
    private string m_EndPointFieldName = (string) null;
    private VoiShareOLTypes.EndPointType m_EndPointType = VoiShareOLTypes.EndPointType.UNDEFINED;
    private VoiShareOLTypes.LinkedList[] m_LinkedLists = (VoiShareOLTypes.LinkedList[]) null;
    private int m_SelectedLinkedListIndex = -1;
    private int m_SelectedChoiceIndex = -1;
    private bool m_SkipSearch = false;
    private Hashtable m_ExtendedProperties = (Hashtable) null;

    public BaseContextData(SessionContext Context)
    {
      this.m_CurrentView = Context.CurrentView;
      this.m_CurrentResource = Context.CurrentResource;
      this.m_CurrentResourceType = Context.CurrentResourceType;
      this.m_CurrentList = Context.CurrentList;
      this.m_CurrentViewFields = Context.CurrentViewFields;
      this.m_Clauses = Context.Clauses;
      if (Context.Clauses != null)
      {
        for (int index = 0; index < this.m_Clauses.Length; ++index)
        {
          if (this.m_Clauses[index].Include && this.m_Clauses[index].Value.GetType().Equals(typeof (ListItemCollection)))
          {
            ArrayList arrayList = new ArrayList();
            foreach (ListItem listItem in (ListItemCollection) this.m_Clauses[index].Value)
            {
              arrayList.Add((object) new VoiShareOLTypes.CustomListItem()
              {
                ItemText = listItem.Text,
                ItemValue = listItem.Value,
                Selected = listItem.Selected
              });
              this.m_Clauses[index].Value = (object) arrayList;
            }
          }
        }
      }
      this.m_Mode = Context.Mode;
      this.m_SubMode = Context.SubMode;
      this.m_EndPointFieldName = Context.EndPointFieldName;
      this.m_EndPointType = Context.EndPointType;
      this.m_LinkedLists = Context.LinkedLists;
      this.m_SelectedLinkedListIndex = Context.SelectedLinkedListIndex;
      this.m_SelectedChoiceIndex = Context.SelectedChoiceIndex;
      this.m_SkipSearch = Context.SkipSearch;
      this.m_ExtendedProperties = Context.ExtendedProperties;
    }

    public OLView CurrentView => this.m_CurrentView;

    public OLResource CurrentResource => this.m_CurrentResource;

    public OLResource CurrentResourceType => this.m_CurrentResourceType;

    public OLList CurrentList => this.m_CurrentList;

    public OLListField[] CurrentViewFields => this.m_CurrentViewFields;

    public VoiShareOLTypes.WhereClause[] Clauses => this.m_Clauses;

    public VoiShareOLTypes.Mode Mode => this.m_Mode;

    public VoiShareOLTypes.SubMode SubMode => this.m_SubMode;

    public string EndPointFieldName => this.m_EndPointFieldName;

    public VoiShareOLTypes.EndPointType EndPointType => this.m_EndPointType;

    public VoiShareOLTypes.LinkedList[] LinkedLists => this.m_LinkedLists;

    public int SelectedLinkedListIndex => this.m_SelectedLinkedListIndex;

    public int SelectedChoiceIndex => this.m_SelectedChoiceIndex;

    public bool SkipSearch => this.m_SkipSearch;

    public Hashtable ExtendedProperties => this.m_ExtendedProperties;

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
      info.AddValue("CurrentView", (object) this.m_CurrentView);
      info.AddValue("CurrentResource", (object) this.m_CurrentResource);
      info.AddValue("CurrentResourceType", (object) this.m_CurrentResourceType);
      info.AddValue("CurrentList", (object) this.m_CurrentList);
      info.AddValue("CurrentViewFields", (object) this.m_CurrentViewFields);
      info.AddValue("Clauses", (object) this.m_Clauses);
      info.AddValue("Mode", (object) this.m_Mode);
      info.AddValue("SubMode", (object) this.m_SubMode);
      info.AddValue("EndPointFieldName", (object) this.m_EndPointFieldName);
      info.AddValue("EndPointType", (object) this.m_EndPointType);
      info.AddValue("LinkedLists", (object) this.m_LinkedLists);
      info.AddValue("SelectedLinkedListIndex", this.m_SelectedLinkedListIndex);
      info.AddValue("SelectedChoiceIndex", this.m_SelectedChoiceIndex);
      info.AddValue("SkipSearch", this.m_SkipSearch);
      info.AddValue("ExtendedProperties", (object) this.m_ExtendedProperties);
    }
  }

  [Serializable]
  public class SearchData(SessionContext SearchContext) : FYIData.BaseContextData(SearchContext)
  {
  }

  [Serializable]
  public class SelectData(SessionContext SelectContext) : FYIData.BaseContextData(SelectContext)
  {
  }

  [Serializable]
  public class ScribeData
  {
    private VoiShareOLTypes.EndPointType m_EndPoint = VoiShareOLTypes.EndPointType.UNDEFINED;
    private object m_ScribeObject = (object) null;

    public ScribeData(VoiShareOLTypes.EndPointType EndPoint, object Data)
    {
      this.m_EndPoint = EndPoint;
      this.m_ScribeObject = Data;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
      info.AddValue("EndPoint", (object) this.m_EndPoint);
      info.AddValue("ScribeObject", this.m_ScribeObject);
    }

    public VoiShareOLTypes.EndPointType EndPoint => this.m_EndPoint;

    public object ScribeObject => this.m_ScribeObject;
  }

  [Serializable]
  public class ScheduleData
  {
    private VoiShareOLTypes.EndPointType m_EndPoint = VoiShareOLTypes.EndPointType.UNDEFINED;
    private object m_ScheduleObject = (object) null;

    public ScheduleData(VoiShareOLTypes.EndPointType EndPoint, object Data)
    {
      this.m_EndPoint = EndPoint;
      this.m_ScheduleObject = Data;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
      info.AddValue("EndPoint", (object) this.m_EndPoint);
      info.AddValue("ScheduleObject", this.m_ScheduleObject);
    }

    public VoiShareOLTypes.EndPointType EndPoint => this.m_EndPoint;

    public object ScheduleObject => this.m_ScheduleObject;
  }
}
