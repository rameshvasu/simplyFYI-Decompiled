// Decompiled with JetBrains decompiler
// Type: Utils.OLClientWS.OfficeLiveResourceType
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace Utils.OLClientWS;

[XmlType(Namespace = "http://schemas.microsoft.com/officelive/soap/")]
[GeneratedCode("System.Xml", "4.0.30319.233")]
[Serializable]
public enum OfficeLiveResourceType
{
  Custom,
  Site,
  BusinessApplications,
  HumanResources,
  Customers,
  Vendors,
  WebBCM,
  Projects,
  CustomerSupport,
  Jobs,
  TimeManager,
  Assets,
  Competitors,
  Documents,
  Employees,
  Estimates,
  Expenses,
  Candidates,
  Trainings,
  SbaSharing,
  TeamWorkspace,
  BlankWorkspace,
  DocumentWorkspace,
  BasicMeetingWorkspace,
  BlankMeetingWorkspace,
  DecisionMeetingWorkspace,
  SocialMeetingWorkspace,
  MultiPageMeetingWorkspace,
  Wiki,
  Blog,
  TimeManagerJP,
}
