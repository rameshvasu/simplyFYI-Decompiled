// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Types.FYIDataExpress
// Assembly: VoiShare4OLTypes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4300920-3BDD-4B26-9880-36B6DF8D0959
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\VoiShare4OLTypes.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive.Types;

[Serializable]
public class FYIDataExpress
{
  private VoiShareOLTypes.EndPointType m_EndPointType;
  private FYIDataExpress.ScribeData m_ScribeData;
  private FYIDataExpress.ScheduleData m_ScheduleData;

  public FYIDataExpress()
  {
  }

  public VoiShareOLTypes.EndPointType EndPointType
  {
    set => this.m_EndPointType = value;
    get => this.m_EndPointType;
  }

  public FYIDataExpress.ScribeData Scribe
  {
    set => this.m_ScribeData = value;
    get => this.m_ScribeData;
  }

  public FYIDataExpress.ScheduleData Schedule
  {
    set => this.m_ScheduleData = value;
    get => this.m_ScheduleData;
  }

  public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
  {
    info.AddValue("EndPointType", (object) this.m_EndPointType);
    info.AddValue("ScribeData", (object) this.m_ScribeData);
    info.AddValue("ScheduleData", (object) this.m_ScheduleData);
  }

  public FYIDataExpress(SerializationInfo info, StreamingContext ctxt)
  {
    VoiShareOLTypes.EndPointType endPointType = (VoiShareOLTypes.EndPointType) info.GetValue(nameof (EndPointType), typeof (VoiShareOLTypes.EndPointType));
    FYIDataExpress.ScribeData scribeData = (FYIDataExpress.ScribeData) info.GetValue("ScribeData", typeof (FYIDataExpress.ScribeData));
    FYIDataExpress.ScheduleData scheduleData = (FYIDataExpress.ScheduleData) info.GetValue("ScheduleData", typeof (FYIDataExpress.ScheduleData));
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
