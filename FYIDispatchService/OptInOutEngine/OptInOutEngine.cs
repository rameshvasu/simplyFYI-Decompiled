// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.OptInOutEngine
// Assembly: OptInOutEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A266FDB-4584-4F3E-8F81-C6DA1ED9F5D4
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\OptInOutEngine.dll

using RameshInnovation.VoiShare.OfficeLive.DataAccess;
using RameshInnovation.VoiShare.OfficeLive.Types;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive;

public class OptInOutEngine
{
  public VoiShareOLTypes.OptStatus GetOptInStatus(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    return new FYIDataAccess().GetOptInStatus(AccountID, MetaID, EndPointType, EndPointAddress);
  }

  public VoiShareOLTypes.OptStatus GetOptOutStatus(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    return new FYIDataAccess().GetOptOutStatus(AccountID, MetaID, EndPointType, EndPointAddress);
  }

  public bool GetIgnoreOptInValue(int AccountID)
  {
    FYIDataAccess fyiDataAccess = new FYIDataAccess();
    RegistrationData registrationData = fyiDataAccess.GetRegistrationData(AccountID);
    return fyiDataAccess.GetAccountOptionsByParentAccount(registrationData.ParentAccountID).IngnoreOptIn;
  }

  public bool ReceipientHasOptedIn(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    return new FYIDataAccess().GetOptInStatus(AccountID, MetaID, EndPointType, EndPointAddress) == 1;
  }

  public bool ReceipientHasOptedOut(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    return new FYIDataAccess().GetOptOutStatus(AccountID, MetaID, EndPointType, EndPointAddress) == 3;
  }
}
