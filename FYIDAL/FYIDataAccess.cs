// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.DataAccess.FYIDataAccess
// Assembly: FYIDAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74592EFF-167F-4DCA-9504-937FCCCAE305
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\FYIDAL.dll

using RameshInnovation.VoiShare.OfficeLive.Types;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive.DataAccess;

public class FYIDataAccess
{
  private OleDbConnection m_Connection;
  private OleDbTransaction m_Transaction;

  public int GetAccountIDFromApiKey(Guid ApiKey)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    int accountIdFromApiKey = -1;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountIDFromApiKey", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@apikey", OleDbType.Guid);
    oleDbParameter.Value = (object) ApiKey;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    object obj = oleDbCommand.ExecuteScalar();
    if (obj != null)
      accountIdFromApiKey = Convert.ToInt32(obj);
    dbConnection.Close();
    return accountIdFromApiKey;
  }

  public DataSet GetInboundSMSNumberRegistryEntry(string SMSNumber)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetInboundSMSNumberRegistryEntry", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@sourcemetaid", OleDbType.Integer);
    oleDbParameter.Value = (object) SMSNumber;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetOrganizationsFromInboundSMSOrganizationKeysByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetOrganizationsFromInboundSMSOrganizationKeysByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetOrganizationsFromInboundSMSNumberRegistryEntryByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetOrganizationsFromInboundSMSNumberRegistryEntryByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public int CopyAppFromFYIMetaMessagesToAppBazaar(
    int SourceMetaID,
    string AuthorName,
    string AuthorOrganization)
  {
    int appBazaar = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spCopyAppFromFYIMetaMessagesToAppBazaar", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@sourcemetaid", OleDbType.Integer);
      oleDbParameter1.Value = (object) SourceMetaID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@authorname", OleDbType.VarChar);
      oleDbParameter2.Value = (object) AuthorName;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@authororganization", OleDbType.VarChar);
      oleDbParameter3.Value = (object) AuthorOrganization;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      appBazaar = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return appBazaar;
  }

  public DataSet GetAccountInfoOfProductionEnabledFYIHandlersForPayPal()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountInfoOfProductionEnabledFYIHandlersForPayPal", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAccountDetailsForAllAutoResponses()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountDetailsForAllAutoResponses", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public int UpdateFYIMessageActualSendDateTimeAndStatus(
    int FYIMessageID,
    string ActualSendDate,
    string ActualSendTime,
    string PXID,
    int StatusCode)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageActualSendDateTimePXIDAndStatus", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimessageid", OleDbType.Integer);
      oleDbParameter1.Value = (object) FYIMessageID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@actualsenddate", OleDbType.VarChar);
      oleDbParameter2.Value = (object) ActualSendDate;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@actualsendtime", OleDbType.VarChar);
      oleDbParameter3.Value = (object) ActualSendTime;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      OleDbParameter oleDbParameter4 = new OleDbParameter("@pxid", OleDbType.VarChar);
      oleDbParameter4.Value = (object) PXID;
      oleDbCommand.Parameters.Add(oleDbParameter4);
      OleDbParameter oleDbParameter5 = new OleDbParameter("@statuscode", OleDbType.Integer);
      oleDbParameter5.Value = (object) StatusCode;
      oleDbCommand.Parameters.Add(oleDbParameter5);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int ReleaseMessageForImmediateSend(int FYIMessageID)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageScheduledDateTimeAndSendStatus", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimessageid", OleDbType.Integer);
      oleDbParameter1.Value = (object) FYIMessageID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@scheduledsenddate", OleDbType.VarChar);
      oleDbParameter2.Value = (object) DateTime.UtcNow.ToShortDateString();
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@scheduledsendtime", OleDbType.VarChar);
      oleDbParameter3.Value = (object) DateTime.UtcNow.ToShortTimeString();
      oleDbCommand.Parameters.Add(oleDbParameter3);
      OleDbParameter oleDbParameter4 = new OleDbParameter("@statuscode", OleDbType.Integer);
      oleDbParameter4.Value = (object) VoiShareOLTypes.SendStatusCode.QUEUED;
      oleDbCommand.Parameters.Add(oleDbParameter4);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int HoldBackFYIMessage(int FYIMessageID)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageStatus", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimessageid", OleDbType.Integer);
      oleDbParameter1.Value = (object) FYIMessageID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@statuscode", OleDbType.Integer);
      oleDbParameter2.Value = (object) Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.HOLD_NO_BALANCE);
      oleDbCommand.Parameters.Add(oleDbParameter2);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int UnHoldHeldFYIMessage(
    int FYIMessageID,
    string ScheduledSendDate,
    string ScheduledSendTime)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUnHoldHeldFYIMessage", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimessageid", OleDbType.Integer);
      oleDbParameter1.Value = (object) FYIMessageID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@statuscode", OleDbType.Integer);
      oleDbParameter2.Value = (object) Convert.ToInt32((object) VoiShareOLTypes.SendStatusCode.QUEUED);
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@ScheduledSendDate", OleDbType.VarChar);
      oleDbParameter3.Value = (object) ScheduledSendDate;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      OleDbParameter oleDbParameter4 = new OleDbParameter("@ScheduledSendTime", OleDbType.VarChar);
      oleDbParameter4.Value = (object) ScheduledSendTime;
      oleDbCommand.Parameters.Add(oleDbParameter4);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public DataSet GetReport_SendBouncedMessageStatsByParentAcctByMetaIDByEndPointTypeByDateRange(
    int ParentAccountID,
    int FYIMetaID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetBouncedCountByParentAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendBouncedMessageStatsByAccountByEndPointTypeByDateRange(
    int AccountID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetBouncedCountByAccountForExpress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendBouncedMessageStatsByAccountByBatchIDByEndPointTypeByDateRangeForExpress(
    int AccountID,
    string BatchID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetBouncedCountByAccountByBatchIDForExpress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@batchid", OleDbType.VarChar);
    oleDbParameter4.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendErroredMessageStatsByParentAcctByMetaIDByEndPointTypeByDateRange(
    int ParentAccountID,
    int FYIMetaID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetErroredCountByParentAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_GetDashboardDataByAccountIDByDateRange(
    int AccountID,
    DateTime SentFromDate,
    DateTime SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetDashboardDataByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) SentFromDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) SentToDate.AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendErroredMessageStatsByAccountByBatchIDByEndPointTypeByDateRangeForExpress(
    int AccountID,
    string BatchID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetErroredCountByAccountByBatchIDForExpress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@batchid", OleDbType.VarChar);
    oleDbParameter4.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendErroredMessageStatsByAcctByMetaIDByEndPointTypeByDateRange(
    int AccountID,
    int FYIMetaID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetErroredCount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_ReportGetCustomerSignupCountByAccount(int ParentAccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetCustomerSignupCountByAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendQueuedMessageStatsByParentAcctByMetaIDByEndPointTypeByDateRange(
    int ParentAccountID,
    int FYIMetaID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetQueuedCountByParentAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendQueuedMessageStatsByAccountByBatchIDByEndPointTypeByDateRangeForExpress(
    int AccountID,
    string BatchID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetQueuedCountByAccountByBatchIDForExpress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@batchid", OleDbType.VarChar);
    oleDbParameter4.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendQueuedMessageStatsByAcctByMetaIDByEndPointTypeByDateRange(
    int AccountID,
    int FYIMetaID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetQueuedCount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendStatsByParentAcctByMetaIDByEndPointTypeByDateRange(
    int ParentAccountID,
    int FYIMetaID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetSendsAndOpensCountByParentAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendStatsByAccountByBatchIDByEndPointTypeByDateRangeForExpress(
    int AccountID,
    string BatchID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetSendsAndOpensCountByAccountByBatchIDForExpress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@batchid", OleDbType.VarChar);
    oleDbParameter4.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetReport_SendStatsByAcctByMetaIDByEndPointTypeByDateRange(
    int AccountID,
    int FYIMetaID,
    string SentFromDate,
    string SentToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spReportGetSendsAndOpensCount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdatebegin", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) DateTime.Parse(SentFromDate);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@creationdateend", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) DateTime.Parse(SentToDate).AddDays(1.0);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public string[] GetMessagesPXIDsToQueryForStatus()
  {
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("sp_GetMessagesPXIDsToQueryForStatus", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
      arrayList.Add((object) $"{row[0].ToString()};{row[1].ToString()}");
    dbConnection.Close();
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public DataSet GetInboundSMSOrganizationInfoFromOrganizationKeyID(int OrganizationKeyID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetInboundSMSOrganizationInfoFromOrganizationKeyID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) OrganizationKeyID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetMetaFYIAppsInfoByEndPointType(int AccountID, int EndPointType)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMetaFYIAppsInfoByEndPointType", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter1 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@endpointtype", OleDbType.Integer);
    oleDbParameter2.Value = (object) EndPointType;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public bool PayPalContactExists(string PayPalPayerID, int AccountID)
  {
    bool flag = true;
    if (this.GetPayerContactDataByPayPalPayerIDAndAccountID(PayPalPayerID, AccountID).Tables[0].Rows.Count == 0)
      flag = false;
    return flag;
  }

  public bool PayPalContactExists(string PayPalPayerID)
  {
    bool flag = true;
    if (this.GetPayerContactDataByPayPalPayerID(PayPalPayerID).Tables[0].Rows.Count == 0)
      flag = false;
    return flag;
  }

  public DataSet GetPayerContactDataAllFieldsByPayPalPayerID(string PayPalPayerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataAllFieldsByPayPalPayerID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@paypalpayerid", OleDbType.VarChar);
    oleDbParameter.Value = (object) PayPalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataAllFieldsByPayPalPayerID(string PayPalPayerID, int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataAllFieldsByPayPalPayerID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@paypalpayerid", OleDbType.VarChar);
    oleDbParameter.Value = (object) PayPalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataByPayPalPayerIDAndAccountID(string PayPalPayerID, int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataByPayPalPayerIDAndAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter1 = new OleDbParameter("@paypalpayerid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) PayPalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter2.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataByPayPalPayerID(string PayPalPayerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataByPayPalPayerID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@paypalpayerid", OleDbType.VarChar);
    oleDbParameter.Value = (object) PayPalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetLockEntryForPayPalBillingAgreementTransaction(string CLID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetLockEntryForPayPalBillingAgreementTransaction", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataByAccountIDByItemKeyword(
    int AccountID,
    string ItemKeyword,
    string WhereModifier)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = !WhereModifier.Equals("like") ? new OleDbCommand("spGetPayerContactDataByAccountID2ByItemKeywordNotLike", dbConnection) : new OleDbCommand("spGetPayerContactDataByAccountID2ByItemKeyword", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@itemkeyword", OleDbType.VarChar);
    oleDbParameter2.Value = (object) ItemKeyword;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataByAccountID2(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataByAccountID2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataOrderedByPaymentAmountByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataOrderedByPaymentAmountByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataOrderedByPaymentAmountByAccountID2(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataOrderedByPaymentAmountByAccountID2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataByEmailAddressForAnAccountID(
    int AccountID,
    string PayerEmailAddress)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataByEmailAddressForAnAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@PayerEmailAddress", OleDbType.VarChar);
    oleDbParameter2.Value = (object) PayerEmailAddress;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerContactDataByEmailAddressForAnAccountID2(
    int AccountID,
    string PayerEmailAddress)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataByEmailAddressForAnAccountID2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@PayerEmailAddress", OleDbType.VarChar);
    oleDbParameter2.Value = (object) PayerEmailAddress;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public string GetIPNOriginatorTransactionIDByFYIMessageID(int FYIMessageID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    string empty = string.Empty;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetIPNOriginatorTransactionIDByFYIMessageID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@fyimessageid", OleDbType.Integer);
    oleDbParameter.Value = (object) FYIMessageID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    object obj = oleDbCommand.ExecuteScalar();
    if (obj != null)
      empty = obj.ToString();
    dbConnection.Close();
    return empty;
  }

  public int GetAccountIDByPayerID(int PayerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    int accountIdByPayerId = -1;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountIDByPayerID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@payerid", OleDbType.Integer);
    oleDbParameter.Value = (object) PayerID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    object obj = oleDbCommand.ExecuteScalar();
    if (obj != null)
      accountIdByPayerId = Convert.ToInt32(obj);
    dbConnection.Close();
    return accountIdByPayerId;
  }

  public DataSet GetQueuedMessages(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetQueuedMessagesSummary", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetMandrillWebhookEventsToProcess()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMandrillWebhookEventsToProcess", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetEmailBounces(string MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetEmailStatusByBatchIDAndStatusType", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) Convert.ToInt32(MetaID);
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@sendstatus", OleDbType.Integer);
    oleDbParameter2.Value = (object) 3;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public string[] GetUnopenedMessagesPXIDs(DateTime SentFromDate, DateTime SentToDate)
  {
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetUnopenedMessagesPXIDs", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@sentdatefrom", OleDbType.DBTimeStamp);
    oleDbParameter1.Value = (object) SentFromDate;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@sentdateto", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) SentToDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
      arrayList.Add((object) row[0].ToString());
    dbConnection.Close();
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public DateTime GetPayPalTransactionMonitorServiceLastRunDateTime()
  {
    DateTime dateTime = new DateTime();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayPalTransactionMonitorServiceLastRunDateTime", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    DateTime LastRunDateTime;
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      object obj = oleDbDataReader["LastRunDateTime"];
      LastRunDateTime = obj.GetType().Equals(typeof (DBNull)) ? DateTime.Now.ToUniversalTime() : Convert.ToDateTime(obj);
    }
    else
    {
      int int32 = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TimerIntervalInSeconds"));
      LastRunDateTime = DateTime.Now.ToUniversalTime();
      this.SetPayPalTransactionMonitorServiceLastRunDateTime(LastRunDateTime);
      LastRunDateTime = LastRunDateTime.AddSeconds((double) (-1 * int32));
    }
    dbConnection.Close();
    return LastRunDateTime;
  }

  public DateTime GetDispatchServiceLastRunDateTime()
  {
    int int32 = Convert.ToInt32(ConfigurationManager.AppSettings.Get("LookbackWindowInMinutes"));
    DateTime dateTime1 = new DateTime();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetDispatchServiceLastRunDateTime", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    DateTime LastRunDateTime;
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      object obj = oleDbDataReader["LastRunDateTime"];
      if (!obj.GetType().Equals(typeof (DBNull)))
      {
        LastRunDateTime = Convert.ToDateTime(obj).AddMinutes((double) (-1 * int32));
      }
      else
      {
        DateTime dateTime2 = DateTime.Now;
        dateTime2 = dateTime2.ToUniversalTime();
        LastRunDateTime = dateTime2.AddMinutes((double) (-1 * int32));
      }
    }
    else
    {
      DateTime dateTime3 = DateTime.Now;
      dateTime3 = dateTime3.ToUniversalTime();
      LastRunDateTime = dateTime3.AddMinutes((double) (-1 * int32));
      this.SetDispatchServiceLastRunDateTime(LastRunDateTime);
    }
    dbConnection.Close();
    return LastRunDateTime;
  }

  public DateTime GetDispatchServiceLastBounceCheckDateTime()
  {
    DateTime LastBounceCheckDateTime = DateTime.Now.ToUniversalTime();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetDispatchServiceLastBounceCheckDateTime", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      object obj = oleDbDataReader["LastBounceCheckDateTime"];
      if (!obj.GetType().Equals(typeof (DBNull)))
        LastBounceCheckDateTime = Convert.ToDateTime(obj);
      else
        this.SetDispatchServiceLastBounceCheckDateTime(LastBounceCheckDateTime);
    }
    else
      this.SetDispatchServiceLastBounceCheckDateTime(LastBounceCheckDateTime);
    dbConnection.Close();
    return LastBounceCheckDateTime;
  }

  public void SetPayPalTransactionMonitorServiceLastRunDateTime(DateTime LastRunDateTime)
  {
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spSetPayPalTransactionMonitorServiceLastRunDateTime", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter = new OleDbParameter("@lastrunat", OleDbType.DBTimeStamp);
      oleDbParameter.Value = (object) LastRunDateTime;
      oleDbCommand.Parameters.Add(oleDbParameter);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
  }

  public void SetDispatchServiceLastRunDateTime(DateTime LastRunDateTime)
  {
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spSetDispatchServiceLastRunDateTime", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter = new OleDbParameter("@lastrunat", OleDbType.DBTimeStamp);
      oleDbParameter.Value = (object) LastRunDateTime;
      oleDbCommand.Parameters.Add(oleDbParameter);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
  }

  public void SetDispatchServiceLastBounceCheckDateTime(DateTime LastBounceCheckDateTime)
  {
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spSetDispatchServiceLastBounceCheckDateTime", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter = new OleDbParameter("@lastbouncecheckat", OleDbType.DBTimeStamp);
      oleDbParameter.Value = (object) LastBounceCheckDateTime;
      oleDbCommand.Parameters.Add(oleDbParameter);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
  }

  public int UpdateFYIMessageOpenedDateTime(int FYIMessageID, string PXID, DateTime OpenedDateTime)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageOpenedDateTime", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimessageid", OleDbType.Integer);
      oleDbParameter1.Value = (object) FYIMessageID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@pxid", OleDbType.VarChar);
      oleDbParameter2.Value = (object) PXID;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@openeddatetime", OleDbType.DBTimeStamp);
      oleDbParameter3.Value = (object) OpenedDateTime;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int UpdateFYIMessageBouncedDateTimeByFYIMessageID(
    int FYIMessageID,
    string PXID,
    DateTime BouncedDateTime)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageBouncedDateTimeByFYIMessageID", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@FYIMessageID", OleDbType.Integer);
      oleDbParameter1.Value = (object) FYIMessageID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@pxid", OleDbType.VarChar);
      oleDbParameter2.Value = (object) PXID;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@bounceddatetime", OleDbType.DBTimeStamp);
      oleDbParameter3.Value = (object) BouncedDateTime;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int UpdateFYIMessageBouncedDateTime(string PXID, DateTime BouncedDateTime)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageBouncedDateTime", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@pxid", OleDbType.VarChar);
      oleDbParameter1.Value = (object) PXID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@bounceddatetime", OleDbType.DBTimeStamp);
      oleDbParameter2.Value = (object) BouncedDateTime;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int UpdateAppCategory(int MetaID, int AppCategory)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateAppCategory", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
      oleDbParameter1.Value = (object) MetaID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@appcategory", OleDbType.Integer);
      oleDbParameter2.Value = (object) AppCategory;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int UpdateCustomerImageScale(string CLID, string ImageFilename, int ImageScale)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpdateCustomerImageScale", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@imagefilename", OleDbType.VarChar);
    oleDbParameter2.Value = (object) ImageFilename;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@imagescale", OleDbType.Integer);
    oleDbParameter3.Value = (object) ImageScale;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    if (dbConnection.State == ConnectionState.Closed)
      dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int UpdateEndPointSubType(int MetaID, int EndPointSubType)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateEndPointSubType", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
      oleDbParameter1.Value = (object) MetaID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@endpointsubtype", OleDbType.Integer);
      oleDbParameter2.Value = (object) EndPointSubType;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int UpdateFYIName(int MetaID, string FYIName)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIName", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
      oleDbParameter1.Value = (object) MetaID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@metadataname", OleDbType.VarChar);
      oleDbParameter2.Value = (object) FYIName;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public void WriteSignUpCredential(
    string UsernameEmail,
    string Password,
    Guid EmailVerificationCode,
    DateTime VerificationEmailSendTime)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertSignUpCredentials", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@UsernameEmail", OleDbType.VarChar);
    oleDbParameter1.Value = (object) UsernameEmail;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@Password", OleDbType.VarChar);
    oleDbParameter2.Value = (object) Password;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@EmailVerificationCode", OleDbType.Guid);
    oleDbParameter3.Value = (object) EmailVerificationCode;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@VerificationEmailSendTime", OleDbType.VarChar);
    oleDbParameter4.Value = (object) VerificationEmailSendTime;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
  }

  public void WriteFYIMetaMessagesExToDB(int MetaID, int AppCategory, int EndPointSubType)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertFYIMetaMessagesEx", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@appcategory", OleDbType.Integer);
    oleDbParameter2.Value = (object) AppCategory;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@endpointsubtype", OleDbType.Integer);
    oleDbParameter3.Value = (object) EndPointSubType;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
  }

  public string GetAccountCLIDForForwardingEmailAddress(string ForwardingEmailAddress)
  {
    string empty = string.Empty;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountCLIDForForwardingEmailAddress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@ForwardingEmailAddress", OleDbType.VarChar);
    oleDbParameter.Value = (object) ForwardingEmailAddress;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    string forwardingEmailAddress = oleDbCommand.ExecuteScalar() != null ? oleDbCommand.ExecuteScalar().ToString() : string.Empty;
    dbConnection.Close();
    return forwardingEmailAddress;
  }

  public void WritePayPalPrimaryEmailAddress(
    string CLID,
    string PayPalPrimaryEmailAddress,
    bool PayPalAccessPermissionGranted)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertPayPalPrimaryEmailAddress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@paypalprimaryemail", OleDbType.VarChar);
    oleDbParameter1.Value = (object) PayPalPrimaryEmailAddress;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@CLID", OleDbType.VarChar);
    oleDbParameter2.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@PermissionGranted", OleDbType.Boolean);
    oleDbParameter3.Value = (object) PayPalAccessPermissionGranted;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
  }

  public void WriteCLID2WixInstanceIDMap(string WixInstanceID, string CLID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertCLID2WixInstanceIDMap", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@wixinstanceid", OleDbType.VarChar);
    oleDbParameter2.Value = (object) WixInstanceID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
  }

  public string WriteQueryStringToDB(string QueryString)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spInsertQueryStringIntoCache", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@guididentifier", OleDbType.Guid);
    oleDbParameter1.Value = (object) Guid.NewGuid();
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@querystring", OleDbType.VarChar);
    oleDbParameter2.Value = (object) QueryString;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return oleDbParameter1.Value.ToString();
  }

  public int UpdateMandrillWebhookEventProcessedFlag(string MessageID, bool EventProcessed)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpdateMandrillWebhookEventProcessedFlag", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@messageid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) MessageID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@statuscode", OleDbType.Boolean);
    oleDbParameter2.Value = (object) EventProcessed;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    if (dbConnection.State == ConnectionState.Closed)
      dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public void WriteMandrillWebhookEvent(
    string EventType,
    string MessageID,
    string WebhookJsonResponse,
    DateTime EventDateTime,
    bool EventProcessed,
    string FromEmail,
    string ToEmail,
    string EventDescription)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertMandrillWebhookEvents", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@eventtype", OleDbType.VarChar);
    oleDbParameter1.Value = (object) EventType;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@messageid", OleDbType.VarChar);
    oleDbParameter2.Value = (object) MessageID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@webhookresponse", OleDbType.VarChar);
    oleDbParameter3.Value = (object) WebhookJsonResponse;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@eventdatetime", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) EventDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@processed", OleDbType.Boolean);
    oleDbParameter5.Value = (object) EventProcessed;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@fromemail", OleDbType.VarChar);
    oleDbParameter6.Value = FromEmail == string.Empty ? (object) DBNull.Value : (object) FromEmail;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@toemail", OleDbType.VarChar);
    oleDbParameter7.Value = ToEmail == string.Empty ? (object) DBNull.Value : (object) ToEmail;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@eventdescription", OleDbType.VarChar);
    oleDbParameter8.Value = EventDescription == string.Empty ? (object) DBNull.Value : (object) EventDescription;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
  }

  public int WritePayPalBillingAgreementID(
    string PayPalBillingAgreementID,
    string CLID,
    DateTime AgreementDateTime,
    Decimal AmountToBill)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertPayPalBillingAgreementID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@BillingAgreementID", OleDbType.VarChar);
    oleDbParameter1.Value = (object) PayPalBillingAgreementID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@CLID", OleDbType.VarChar);
    oleDbParameter2.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@BillingAgreementDate", OleDbType.DBDate);
    oleDbParameter3.Value = (object) AgreementDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@AmountToBillBillingAgreementDate", OleDbType.Decimal);
    oleDbParameter4.Value = (object) AmountToBill;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WriteTransactionForPayPalBillingAgreementID(
    string PayPalBillingAgreementID,
    string TransactionID,
    DateTime TransactionDateTime,
    Decimal GrossAmount,
    string NVPResponse)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertPayPalBillingAgreementTransaction", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@BillingAgreementID", OleDbType.VarChar);
    oleDbParameter1.Value = (object) PayPalBillingAgreementID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@paypaltransactionid", OleDbType.VarChar);
    oleDbParameter2.Value = (object) TransactionID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@TransactionDateTime", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) TransactionDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@GrossAmount", OleDbType.Decimal);
    oleDbParameter4.Value = (object) GrossAmount;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@NVPResponse", OleDbType.VarChar);
    oleDbParameter5.Value = (object) NVPResponse;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WritePayPalBillingAgreementTransactionLock(string CLID, DateTime TransactionDateTime)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spWritePayPalBillingAgreementTransactionLock", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@datetime", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) TransactionDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WriteUsageInventoryToDB(
    int AccountID,
    VoiShareOLTypes.InventoryTransactionType InventoryTransactionType,
    string ReferenceID,
    int Quantity,
    int UnitsBalance)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spInsertUsageInventory", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@transactiondatetime", OleDbType.Date);
    oleDbParameter2.Value = (object) DateTime.Now.ToUniversalTime();
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@inventorytransactiontype", OleDbType.Integer);
    oleDbParameter3.Value = (object) Convert.ToInt32((object) InventoryTransactionType);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@referenceid", OleDbType.VarChar);
    oleDbParameter4.Value = (object) ReferenceID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@quantity", OleDbType.Integer);
    oleDbParameter5.Value = (object) Quantity;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@unitsbalance", OleDbType.Integer);
    oleDbParameter6.Value = (object) UnitsBalance;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public int WriteShipmentConfirmation(
    int AccountID,
    int ShipmentConfirmationID,
    string OriginatorTransactionID,
    string PayerName,
    string PayerEmail,
    Decimal GrossAmount,
    string Currency,
    string TrackingNumber,
    string ShippingCarrier,
    int FYIMessageID,
    int IPNTransactionDetailID,
    DateTime TransactionDateTime,
    DateTime ShipmentDateTime)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertShipmentConfirmation", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@shipmentconfirmationid", OleDbType.Integer);
    oleDbParameter2.Value = (object) ShipmentConfirmationID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@originatortransactionid", OleDbType.VarChar);
    oleDbParameter3.Value = (object) OriginatorTransactionID;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@payername", OleDbType.VarChar);
    oleDbParameter4.Value = (object) PayerName;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@payeremail", OleDbType.VarChar);
    oleDbParameter5.Value = (object) PayerEmail;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@grossamount", OleDbType.Decimal);
    oleDbParameter6.Value = (object) GrossAmount;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@currency", OleDbType.VarChar);
    oleDbParameter7.Value = (object) Currency;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@trackingnumber", OleDbType.VarChar);
    oleDbParameter8.Value = (object) TrackingNumber;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@shippingcarrier", OleDbType.VarChar);
    oleDbParameter9.Value = (object) ShippingCarrier;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@fyimessageid", OleDbType.Integer);
    oleDbParameter10.Value = (object) FYIMessageID;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@ipntransactiondetailid", OleDbType.Integer);
    oleDbParameter11.Value = (object) IPNTransactionDetailID;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    OleDbParameter oleDbParameter12 = new OleDbParameter("@transactiondatetime", OleDbType.DBTimeStamp);
    oleDbParameter12.Value = (object) TransactionDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter12);
    OleDbParameter oleDbParameter13 = new OleDbParameter("@shipmentdatetime", OleDbType.DBTimeStamp);
    oleDbParameter13.Value = (object) ShipmentDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter13);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WriteTransaction2HandlerMapping(
    string CLID,
    string TransactionType,
    string TransactionOriginator,
    int HandlerFYIMetaID,
    bool HandlerEnabled)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertTransaction2HandlerMapping", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@TransactionType", OleDbType.VarChar);
    oleDbParameter1.Value = (object) TransactionType;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@TransactionOriginator", OleDbType.VarChar);
    oleDbParameter2.Value = (object) TransactionOriginator;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@HandlerFYIMetaID", OleDbType.Integer);
    oleDbParameter3.Value = (object) HandlerFYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@HandlerEnabled", OleDbType.Boolean);
    oleDbParameter4.Value = (object) HandlerEnabled;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@CLID", OleDbType.VarChar);
    oleDbParameter5.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WriteTransaction2HandlerMapping2(
    string CLID,
    string TransactionType,
    string TransactionOriginator,
    int HandlerFYIMetaID,
    bool HandlerEnabled,
    int MessageFlowMode)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertTransaction2HandlerMapping2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@TransactionType", OleDbType.VarChar);
    oleDbParameter1.Value = (object) TransactionType;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@TransactionOriginator", OleDbType.VarChar);
    oleDbParameter2.Value = (object) TransactionOriginator;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@HandlerFYIMetaID", OleDbType.Integer);
    oleDbParameter3.Value = (object) HandlerFYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@HandlerEnabled", OleDbType.Boolean);
    oleDbParameter4.Value = (object) HandlerEnabled;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@CLID", OleDbType.VarChar);
    oleDbParameter5.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@messageflowmode", OleDbType.Integer);
    oleDbParameter6.Value = (object) MessageFlowMode;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WriteFYIHandlerDynamicSelector(
    int SelectorID,
    int DefaultHandlerID,
    string SelectorFieldName,
    string SelectionCriteria,
    string SelectorFieldValue,
    int SelectorRank,
    int SelectorFYIHandlerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertFYIHandlerDynamicSelector", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@selectorid", OleDbType.Integer);
    oleDbParameter1.Value = (object) SelectorID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@defaulthandlerid", OleDbType.Integer);
    oleDbParameter2.Value = (object) DefaultHandlerID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@selectorfieldname", OleDbType.VarChar);
    oleDbParameter3.Value = (object) SelectorFieldName;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@selectioncriteria", OleDbType.VarChar);
    oleDbParameter4.Value = (object) SelectionCriteria;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@selectorfieldvalue", OleDbType.VarChar);
    oleDbParameter5.Value = (object) SelectorFieldValue;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@selectorrank", OleDbType.Integer);
    oleDbParameter6.Value = (object) SelectorRank;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@selectorfyihandlerid", OleDbType.Integer);
    oleDbParameter7.Value = (object) SelectorFYIHandlerID;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WriteFYIMessageToDB(
    int MetaID,
    string CreationDate,
    string ScheduledSendDate,
    string ScheduledSendTime,
    int EndPointType,
    int ScheduleType,
    string XMLMessageBody,
    int SendStatusCode,
    bool IsTransactional,
    int SendMode,
    Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertFYIMessage", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@endpointtype", OleDbType.Integer);
    oleDbParameter2.Value = (object) EndPointType;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@creationdate", OleDbType.VarChar);
    oleDbParameter3.Value = (object) CreationDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    MemoryStream memoryStream = this.SerializeFYIXMLMessageBody(XMLMessageBody);
    byte[] buffer = new byte[Convert.ToInt32(memoryStream.Length)];
    memoryStream.Read(buffer, 0, Convert.ToInt32(memoryStream.Length));
    memoryStream.Close();
    OleDbParameter oleDbParameter4 = new OleDbParameter("@xmlmessagebody", OleDbType.LongVarBinary, buffer.Length);
    oleDbParameter4.Value = (object) buffer;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@actualsenddate", OleDbType.VarChar);
    oleDbParameter5.Value = (object) null;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@actualsendtime", OleDbType.VarChar);
    oleDbParameter6.Value = (object) null;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@scheduledsenddate", OleDbType.VarChar);
    oleDbParameter7.Value = (object) ScheduledSendDate;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@scheduledsendtime", OleDbType.VarChar);
    oleDbParameter8.Value = (object) ScheduledSendTime;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@scheduletype", OleDbType.Integer);
    oleDbParameter9.Value = (object) ScheduleType;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@sendstatuscode", OleDbType.Integer);
    oleDbParameter10.Value = (object) SendStatusCode;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@pxid", OleDbType.VarChar);
    oleDbParameter11.Value = (object) null;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    OleDbParameter oleDbParameter12 = new OleDbParameter("@istransactionalmessage", OleDbType.Boolean);
    oleDbParameter12.Value = (object) IsTransactional;
    oleDbCommand.Parameters.Add(oleDbParameter12);
    OleDbParameter oleDbParameter13 = new OleDbParameter("@sendmode", OleDbType.Integer);
    oleDbParameter13.Value = (object) SendMode;
    oleDbCommand.Parameters.Add(oleDbParameter13);
    OleDbParameter oleDbParameter14 = new OleDbParameter("@openeddatetime", OleDbType.Date);
    oleDbParameter14.Value = (object) DBNull.Value;
    oleDbCommand.Parameters.Add(oleDbParameter14);
    OleDbParameter oleDbParameter15 = new OleDbParameter("@bounceddatetime", OleDbType.Date);
    oleDbParameter15.Value = (object) DBNull.Value;
    oleDbCommand.Parameters.Add(oleDbParameter15);
    OleDbParameter oleDbParameter16 = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter16.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter16);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public int UpdateIVRResponseToDB(
    Guid CallSid,
    string FromPhone,
    string ToPhone,
    string CallDirection,
    string CallParticipant,
    Guid BatchID,
    DateTime Timestamp,
    string AnsweredBy,
    int PromptID,
    string PromptMetaData,
    string ResponseData)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertIVRResponse", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@callsid", OleDbType.Guid);
    oleDbParameter1.Value = (object) CallSid;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fromphone", OleDbType.VarChar);
    oleDbParameter2.Value = (object) FromPhone;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@tophone", OleDbType.VarChar);
    oleDbParameter3.Value = (object) ToPhone;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@calldirection", OleDbType.VarChar);
    oleDbParameter4.Value = (object) CallDirection;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@callparticipant", OleDbType.VarChar);
    oleDbParameter5.Value = CallParticipant == null ? (object) DBNull.Value : (object) CallParticipant;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter6.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@timestamp", OleDbType.VarChar);
    oleDbParameter7.Value = (object) Timestamp;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@answeredby", OleDbType.VarChar);
    oleDbParameter8.Value = AnsweredBy == null ? (object) DBNull.Value : (object) AnsweredBy;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@promptid", OleDbType.Integer);
    oleDbParameter9.Value = (object) PromptID;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@promptmetadata", OleDbType.VarChar);
    oleDbParameter10.Value = PromptMetaData == null ? (object) DBNull.Value : (object) PromptMetaData;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@responsedata", OleDbType.VarChar);
    oleDbParameter11.Value = ResponseData == null ? (object) DBNull.Value : (object) ResponseData;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public int WriteFYIDataToDB(
    int AccountID,
    int MetaID,
    string MetaDataName,
    VoiShareOLTypes.EndPointType EndPoint,
    SessionContext SearchContext,
    SessionContext SelectContext,
    object ScribeData,
    object ScheduleData,
    VoiShareOLTypes.ScheduleType Schedule,
    string ScheduledTime)
  {
    ScheduledTime = DateTime.Parse(ScheduledTime).ToString("HH:mm");
    OleDbConnection dbConnection = this.GetDBConnection();
    MemoryStream memoryStream = this.SerializeFYIData(EndPoint, SearchContext, SelectContext, ScribeData, ScheduleData);
    byte[] buffer = new byte[Convert.ToInt32(memoryStream.Length)];
    memoryStream.Read(buffer, 0, Convert.ToInt32(memoryStream.Length));
    memoryStream.Close();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertFYIMetaMessage", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@FYIAccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@DateCreated", OleDbType.Date);
    oleDbParameter2.Value = (object) DateTime.Now.ToUniversalTime();
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@Active", OleDbType.Char, 1);
    oleDbParameter3.Value = (object) 'Y';
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@Data", OleDbType.LongVarBinary, buffer.Length);
    oleDbParameter4.Value = (object) buffer;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@EndPointType", OleDbType.Integer);
    oleDbParameter5.Value = (object) Convert.ToInt32((object) EndPoint);
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@ScheduleType", OleDbType.Integer);
    oleDbParameter6.Value = (object) Convert.ToInt32((object) Schedule);
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@ScheduledTime", OleDbType.VarChar);
    oleDbParameter7.Value = (object) ScheduledTime;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@MetaDataName", OleDbType.VarChar);
    oleDbParameter8.Value = (object) MetaDataName;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter9.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    dbConnection.Open();
    if (oleDbCommand.ExecuteNonQuery() > 0)
      MetaID = (int) new OleDbCommand($"{$"{"SELECT MetaID from FYIMETAMESSAGES WHERE FYIAccountID=" + (object) AccountID} AND DateCreated='{oleDbParameter2.Value}'"} OR DateCreated='{DateTime.Parse(oleDbParameter2.Value.ToString()).AddSeconds(1.0).ToString()}'", dbConnection).ExecuteScalar();
    dbConnection.Close();
    return MetaID;
  }

  public int WriteFYIDataToDB_2(
    int AccountID,
    int MetaID,
    string MetaDataName,
    VoiShareOLTypes.EndPointType EndPoint,
    object ScribeData,
    object ScheduleData,
    VoiShareOLTypes.ScheduleType Schedule,
    string ScheduledTime,
    string Version)
  {
    ScheduledTime = DateTime.Parse(ScheduledTime).ToString("HH:mm");
    OleDbConnection dbConnection = this.GetDBConnection();
    MemoryStream memoryStream = this.SerializeFYIDataForExpress(EndPoint, ScribeData, ScheduleData);
    byte[] buffer = new byte[Convert.ToInt32(memoryStream.Length)];
    memoryStream.Read(buffer, 0, Convert.ToInt32(memoryStream.Length));
    memoryStream.Close();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertFYIMetaMessage2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@FYIAccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@DateCreated", OleDbType.Date);
    oleDbParameter2.Value = (object) DateTime.Now.ToUniversalTime();
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@Active", OleDbType.Char, 1);
    oleDbParameter3.Value = (object) 'Y';
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@Data", OleDbType.LongVarBinary, buffer.Length);
    oleDbParameter4.Value = (object) buffer;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@EndPointType", OleDbType.Integer);
    oleDbParameter5.Value = (object) Convert.ToInt32((object) EndPoint);
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@ScheduleType", OleDbType.Integer);
    oleDbParameter6.Value = (object) Convert.ToInt32((object) Schedule);
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@ScheduledTime", OleDbType.VarChar);
    oleDbParameter7.Value = (object) ScheduledTime;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@MetaDataName", OleDbType.VarChar);
    oleDbParameter8.Value = (object) MetaDataName;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter9.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@Version", OleDbType.VarChar);
    oleDbParameter10.Value = (object) Version;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    dbConnection.Open();
    if (oleDbCommand.ExecuteNonQuery() > 0)
    {
      string str1 = $"{"SELECT MetaID from FYIMETAMESSAGES WHERE FYIAccountID=" + (object) AccountID} AND DateCreated='{oleDbParameter2.Value}'";
      DateTime dateTime = DateTime.Parse(oleDbParameter2.Value.ToString());
      dateTime = dateTime.AddSeconds(1.0);
      string str2 = dateTime.ToString();
      MetaID = (int) new OleDbCommand($"{str1} OR DateCreated='{str2}'", dbConnection).ExecuteScalar();
    }
    dbConnection.Close();
    return MetaID;
  }

  public DataSet spGetFYIMetaData(int FYIAppID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand(nameof (spGetFYIMetaData), dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("fyiid", (object) SqlDbType.Int);
    oleDbParameter.Value = (object) FYIAppID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public FYIMessageCompact ReadFYIDataFromDBByPXID(string PXID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetFYIMessageByPXID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@pxid", OleDbType.VarChar);
    oleDbParameter.Value = (object) PXID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    ArrayList arrayList = new ArrayList();
    FYIMessageCompact fyiMessageCompact = (FYIMessageCompact) null;
    if (oleDbDataReader.HasRows)
    {
      while (oleDbDataReader.Read())
      {
        fyiMessageCompact = new FYIMessageCompact();
        fyiMessageCompact.MessageID = Convert.ToInt32(oleDbDataReader["FYIMessageID"].ToString());
        fyiMessageCompact.MetaID = Convert.ToInt32(oleDbDataReader["MetaID"].ToString());
        fyiMessageCompact.BatchID = new Guid(oleDbDataReader["BatchID"].ToString());
        byte[] FYIXMLMessageBodyBinaryDataArray = (byte[]) oleDbDataReader["XMLMessageBody"];
        fyiMessageCompact.XMLMessageBody = this.DeserializeFYIXMLMessageBody(FYIXMLMessageBodyBinaryDataArray);
        fyiMessageCompact.SendMode = (EmailContext.SendMode) oleDbDataReader["SendMode"];
      }
    }
    dbConnection.Close();
    return fyiMessageCompact;
  }

  public FYIMessageCompact[] ReadFYIDataForAllHeldMessagesFromDBForAccount(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetFYIDataForHeldMessagesByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) Convert.ToInt32(AccountID);
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    ArrayList arrayList = new ArrayList();
    if (oleDbDataReader.HasRows)
    {
      while (oleDbDataReader.Read())
      {
        FYIMessageCompact fyiMessageCompact = new FYIMessageCompact();
        fyiMessageCompact.AccountID = Convert.ToInt32(oleDbDataReader["FYIAccountID"].ToString());
        fyiMessageCompact.MessageID = Convert.ToInt32(oleDbDataReader["FYIMessageID"].ToString());
        fyiMessageCompact.MetaID = Convert.ToInt32(oleDbDataReader["MetaID"].ToString());
        fyiMessageCompact.BatchID = new Guid(oleDbDataReader["BatchID"].ToString());
        byte[] FYIXMLMessageBodyBinaryDataArray = (byte[]) oleDbDataReader["XMLMessageBody"];
        fyiMessageCompact.XMLMessageBody = this.DeserializeFYIXMLMessageBody(FYIXMLMessageBodyBinaryDataArray);
        fyiMessageCompact.SendMode = (EmailContext.SendMode) oleDbDataReader["SendMode"];
        arrayList.Add((object) fyiMessageCompact);
      }
    }
    dbConnection.Close();
    return (FYIMessageCompact[]) arrayList.ToArray(typeof (FYIMessageCompact));
  }

  public FYIMessageCompact[] ReadFYIDataForAllHeldMessagesFromDB()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetFYIDataForAllHeldMessages", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    ArrayList arrayList = new ArrayList();
    if (oleDbDataReader.HasRows)
    {
      while (oleDbDataReader.Read())
      {
        FYIMessageCompact fyiMessageCompact = new FYIMessageCompact();
        fyiMessageCompact.AccountID = Convert.ToInt32(oleDbDataReader["FYIAccountID"].ToString());
        fyiMessageCompact.MessageID = Convert.ToInt32(oleDbDataReader["FYIMessageID"].ToString());
        fyiMessageCompact.MetaID = Convert.ToInt32(oleDbDataReader["MetaID"].ToString());
        fyiMessageCompact.BatchID = new Guid(oleDbDataReader["BatchID"].ToString());
        byte[] FYIXMLMessageBodyBinaryDataArray = (byte[]) oleDbDataReader["XMLMessageBody"];
        fyiMessageCompact.XMLMessageBody = this.DeserializeFYIXMLMessageBody(FYIXMLMessageBodyBinaryDataArray);
        fyiMessageCompact.SendMode = (EmailContext.SendMode) oleDbDataReader["SendMode"];
        arrayList.Add((object) fyiMessageCompact);
      }
    }
    dbConnection.Close();
    return (FYIMessageCompact[]) arrayList.ToArray(typeof (FYIMessageCompact));
  }

  public FYIMessageCompact[] ReadFYIDataFromDB(
    VoiShareOLTypes.EndPointType EndPointType,
    VoiShareOLTypes.ScheduleType ScheduleType,
    DateTime ScheduledStartDateTime,
    DateTime ScheduledEndDateTime,
    int StatusCode)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetFYIMessages", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@endpointtype", OleDbType.Integer);
    oleDbParameter1.Value = (object) Convert.ToInt32((object) EndPointType);
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@scheduletype", OleDbType.Integer);
    oleDbParameter2.Value = (object) Convert.ToInt32((object) ScheduleType);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@scheduledstartdatetime", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) ScheduledStartDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@scheduledenddatetime", OleDbType.DBTimeStamp);
    oleDbParameter4.Value = (object) ScheduledEndDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@sendstatuscode", OleDbType.Integer);
    oleDbParameter5.Value = (object) Convert.ToInt32(StatusCode);
    oleDbCommand.Parameters.Add(oleDbParameter5);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    ArrayList arrayList = new ArrayList();
    if (oleDbDataReader.HasRows)
    {
      while (oleDbDataReader.Read())
      {
        FYIMessageCompact fyiMessageCompact = new FYIMessageCompact();
        fyiMessageCompact.AccountID = Convert.ToInt32(oleDbDataReader["FYIAccountID"].ToString());
        fyiMessageCompact.MessageID = Convert.ToInt32(oleDbDataReader["FYIMessageID"].ToString());
        fyiMessageCompact.MetaID = Convert.ToInt32(oleDbDataReader["MetaID"].ToString());
        fyiMessageCompact.BatchID = new Guid(oleDbDataReader["BatchID"].ToString());
        byte[] FYIXMLMessageBodyBinaryDataArray = (byte[]) oleDbDataReader["XMLMessageBody"];
        fyiMessageCompact.XMLMessageBody = this.DeserializeFYIXMLMessageBody(FYIXMLMessageBodyBinaryDataArray);
        fyiMessageCompact.SendMode = (EmailContext.SendMode) oleDbDataReader["SendMode"];
        arrayList.Add((object) fyiMessageCompact);
      }
    }
    dbConnection.Close();
    return (FYIMessageCompact[]) arrayList.ToArray(typeof (FYIMessageCompact));
  }

  public Hashtable[] BulkReadFYIDataFromDB(int[] MetaIDs)
  {
    ArrayList arrayList = new ArrayList();
    foreach (int metaId in MetaIDs)
      arrayList.Add((object) this.ReadFYIDataFromDB(metaId));
    return (Hashtable[]) arrayList.ToArray(typeof (Hashtable));
  }

  public int DeleteFYIHandlerDynamicSelector(int SelectorID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteFYIHandlerDynamicSelector", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@SelectorID", OleDbType.Integer);
    oleDbParameter.Value = (object) SelectorID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int DeleteLockEntryForPayPalBillingAgreementTransaction(string CLID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteLockEntryForPayPalBillingAgreementTransaction", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int DeleteAutoSaveDataFromDB(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteAutoSaveDataByFYIMetaID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@fyimetaid", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int DeletePayerContactDataAnonByExternalPayerID(string ExternalPayerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeletePayerContactDataAnonByExternalPayerID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@ExternalPayerID", OleDbType.VarChar);
    oleDbParameter.Value = (object) ExternalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int DeleteFYIDataFromDB(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteFYIDataFromDB", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int ActivateFYIDataInDB(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spActivateFYI", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public void BeginTransaction()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    dbConnection.Open();
    this.m_Transaction = dbConnection.BeginTransaction();
  }

  public void CommitTransaction()
  {
    if (this.m_Transaction == null)
      return;
    this.m_Transaction.Commit();
    this.GetDBConnection().Close();
  }

  public void RollbackTransaction()
  {
    if (this.m_Transaction == null)
      return;
    this.m_Transaction.Rollback();
    this.GetDBConnection().Close();
  }

  public int DeleteCustomerImageMetaData(string CLID, string ImageFilename)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    dbConnection.Open();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteCustomerImageFile", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@imagefilename", OleDbType.VarChar);
    oleDbParameter2.Value = (object) ImageFilename;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int DeleteShipmentConfirmationByFYIMessageID(int FYIMessageID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    dbConnection.Open();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteShipmentConfirmationByFYIMessageID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@fyimessageid", OleDbType.Integer);
    oleDbParameter.Value = (object) FYIMessageID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int DeleteMessagesByBatchID(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    dbConnection.Open();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteMessagesByBatchID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int DeleteCachedQueryStringDB(Guid GuidIdentifier)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spDeleteCachedQueryStringFromDB", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@GuidIdentifier", OleDbType.Guid);
    oleDbParameter.Value = (object) GuidIdentifier;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public Hashtable ReadFYIDataFromDB(int MetaID)
  {
    Hashtable hashtable = (Hashtable) null;
    OleDbConnection dbConnection = this.GetDBConnection();
    string selectCommandText = $"SELECT MetaID,Data,Version FROM FYIMETAMESSAGES WHERE MetaID={(object) MetaID}";
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, dbConnection);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    if (dataSet.Tables.Count > 0)
    {
      hashtable = new Hashtable();
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
      {
        if (row["Version"].ToString().ToLower().Contains("express"))
          hashtable.Add((object) (int) row[0], (object) this.DeserializeFYIDataExpress((byte[]) row[1]));
        else
          hashtable.Add((object) (int) row[0], (object) this.DeserializeFYIData((byte[]) row[1]));
      }
    }
    dbConnection.Close();
    return hashtable;
  }

  public void WriteRegistrationDataToDB(RegistrationData RegData, AccountOptions AcctOptions)
  {
    this.WriteRegistrationDataToDB(RegData);
    if (AcctOptions == null)
      return;
    this.WriteAccountOptionsDataToDB(RegData.AccountID, AcctOptions);
  }

  public int WriteAccountOptionsDataToDB(int AccountID, AccountOptions OptionsData)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertAccountOptions", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@emailfromname", OleDbType.VarChar);
    oleDbParameter2.Value = (object) OptionsData.FromEmailName;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@companylogourl", OleDbType.VarChar);
    oleDbParameter3.Value = (object) OptionsData.CompanyLogoUrl;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@footertext", OleDbType.VarChar);
    oleDbParameter4.Value = (object) OptionsData.FooterText;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public int WriteAutoSaveDataToDB(
    int FYIMetaID,
    DateTime AutoSaveDateTime,
    object AutoSaveData,
    VoiShareOLTypes.EndPointType EndPointType)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertAutoSaveData", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimetaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@autosavetimestamp", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) AutoSaveDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    MemoryStream memoryStream = this.SerializeAutoSaveData(AutoSaveData);
    byte[] buffer = new byte[Convert.ToInt32(memoryStream.Length)];
    memoryStream.Read(buffer, 0, Convert.ToInt32(memoryStream.Length));
    memoryStream.Close();
    OleDbParameter oleDbParameter3 = new OleDbParameter("@autosavedata", OleDbType.LongVarBinary, buffer.Length);
    oleDbParameter3.Value = (object) buffer;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@autosavedatatype", OleDbType.Integer);
    oleDbParameter4.Value = (object) Convert.ToInt32((object) EndPointType);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public int WriteAccountEndPointToDB(int AccountID, EndPointIdentity EndPoint)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertAccountEndPoint", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@endpointtype", OleDbType.Integer);
    oleDbParameter2.Value = (object) Convert.ToInt32((object) EndPoint.EndPointType);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@endpointname", OleDbType.VarChar);
    oleDbParameter3.Value = EndPoint.SenderName == null ? (object) EndPoint.SenderAddress : (object) EndPoint.SenderName;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@endpointaddress", OleDbType.VarChar);
    oleDbParameter4.Value = EndPoint.SenderAddress == null ? (object) DBNull.Value : (object) EndPoint.SenderAddress;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public int InsertFYIMessageInboundSMS(
    int InboundSMSOrganizationID,
    int FYIMetaID,
    DateTime MessageDate,
    string InboundPXID,
    string SMSMessageBody,
    Guid BatchID,
    int MessageDirection,
    string From,
    string To)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spInsertFYIMessageInboundSMS", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimetaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) InboundSMSOrganizationID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fyimetaid", OleDbType.Integer);
    oleDbParameter2.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@messagedate", OleDbType.VarChar);
    oleDbParameter3.Value = (object) MessageDate.ToString();
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@inboundpxid", OleDbType.VarChar);
    oleDbParameter4.Value = (object) InboundPXID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@smsmessagebody", OleDbType.VarChar);
    oleDbParameter5.Value = (object) SMSMessageBody;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter6.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@messagedirection", OleDbType.Integer);
    oleDbParameter7.Value = (object) MessageDirection;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@from", OleDbType.VarChar);
    oleDbParameter8.Value = (object) From;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@to", OleDbType.VarChar);
    oleDbParameter9.Value = (object) To;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int InsertCustomerImageMetadata(string CLID, string ImageFilename, int ImageScale)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spInsertCustomerImageMetadata", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@imagefilename", OleDbType.VarChar);
    oleDbParameter2.Value = (object) ImageFilename;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@imagescale", OleDbType.Integer);
    oleDbParameter3.Value = (object) ImageScale;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int InsertCustomerContactListName(int AccountID, string ContactListName)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spInsertCustomerContactListName", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@ContactListName", OleDbType.VarChar);
    oleDbParameter2.Value = (object) ContactListName;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int InsertInboundSMSOrganizationKey(
    int AccountID,
    string OrganizationKey,
    string CalledSMSNumber)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spInsertInboundSMSOrganizationKey", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@OrganizationKey", OleDbType.VarChar);
    oleDbParameter2.Value = (object) OrganizationKey;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@CalledSMSNumber", OleDbType.VarChar);
    oleDbParameter3.Value = (object) CalledSMSNumber;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int InsertInboundSMSNumberRegistryEntry(
    string CalledSMSNumber,
    int OrganizationKeyID,
    string CommandVerb,
    int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spInsertInboundSMSNumberRegistryEntry", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@CalledSMSNumber", OleDbType.VarChar);
    oleDbParameter1.Value = (object) CalledSMSNumber;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@OrganizationKeyID", OleDbType.Integer);
    oleDbParameter2.Value = (object) OrganizationKeyID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@Command", OleDbType.VarChar);
    oleDbParameter3.Value = (object) CommandVerb;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter4.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public DataSet GetAccountActivityFromHistory(int AccountID, int ActivityID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountActivityFromHistory", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@activityid", OleDbType.Integer);
    oleDbParameter2.Value = (object) Convert.ToInt32((object) VoiShareOLTypes.AccountActivity.CREATED);
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public int WriteAccountActivtyToDB(int AccountID, string Notes)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertAccountHistory", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@activitydatetime", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) DateTime.Now.ToUniversalTime();
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@activityid", OleDbType.Integer);
    oleDbParameter3.Value = (object) Convert.ToInt32((object) VoiShareOLTypes.AccountActivity.CREATED);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@notes", OleDbType.VarChar);
    oleDbParameter4.Value = Notes == null ? (object) DBNull.Value : (object) Notes;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public int WriteCustomerContactData(
    int InboundSMSMessageID,
    string Name,
    string Email,
    string SMSNumber,
    string Address1,
    string Address2,
    string City,
    string State,
    string ZIP,
    string Country,
    int AccountID,
    DateTime JoinDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertCustomerContactData", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@InboundSMSMessageID", OleDbType.Integer);
    oleDbParameter1.Value = (object) InboundSMSMessageID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@name", OleDbType.VarChar);
    oleDbParameter2.Value = (object) Name;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@email", OleDbType.VarChar);
    oleDbParameter3.Value = (object) Email;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@SMSNumber", OleDbType.VarChar);
    oleDbParameter4.Value = (object) SMSNumber;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@Address1", OleDbType.VarChar);
    oleDbParameter5.Value = Address1 == null ? (object) DBNull.Value : (object) Address1;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@Address2", OleDbType.VarChar);
    oleDbParameter6.Value = Address2 == null ? (object) DBNull.Value : (object) Address2;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@City", OleDbType.VarChar);
    oleDbParameter7.Value = City == null ? (object) DBNull.Value : (object) City;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@State", OleDbType.VarChar);
    oleDbParameter8.Value = State == null ? (object) DBNull.Value : (object) State;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@ZIP", OleDbType.VarChar);
    oleDbParameter9.Value = ZIP == null ? (object) DBNull.Value : (object) ZIP;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@Country", OleDbType.VarChar);
    oleDbParameter10.Value = Country == null ? (object) DBNull.Value : (object) Country;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter11.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    OleDbParameter oleDbParameter12 = new OleDbParameter("@joindate", OleDbType.DBTimeStamp);
    oleDbParameter12.Value = (object) JoinDate;
    oleDbCommand.Parameters.Add(oleDbParameter12);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WritePayPalPayerContactData(
    string FirstName,
    string LastName,
    string StreetAddress,
    string City,
    string State,
    string ZipCode,
    string Country,
    string Email,
    string Phone,
    string BusinessName,
    string ExternalPayerID,
    int SimplyFYIAccountID)
  {
    int num = 0;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertPayPalPayerContactData", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@FirstName", OleDbType.VarChar);
    oleDbParameter1.Value = (object) FirstName;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@LastName", OleDbType.VarChar);
    oleDbParameter2.Value = (object) LastName;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@StreetAddress", OleDbType.VarChar);
    oleDbParameter3.Value = (object) StreetAddress;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@City", OleDbType.VarChar);
    oleDbParameter4.Value = City == null ? (object) DBNull.Value : (object) City;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@State", OleDbType.VarChar);
    oleDbParameter5.Value = State == null ? (object) DBNull.Value : (object) State;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@ZipCode", OleDbType.VarChar);
    oleDbParameter6.Value = ZipCode == null ? (object) DBNull.Value : (object) ZipCode;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@Country", OleDbType.VarChar);
    oleDbParameter7.Value = Country == null ? (object) DBNull.Value : (object) Country;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@email", OleDbType.VarChar);
    oleDbParameter8.Value = (object) Email;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@Phone", OleDbType.VarChar);
    oleDbParameter9.Value = (object) Phone;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@BusinessName", OleDbType.VarChar);
    oleDbParameter10.Value = (object) BusinessName;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@ExternalPayerID", OleDbType.VarChar);
    oleDbParameter11.Value = (object) ExternalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    OleDbParameter oleDbParameter12 = new OleDbParameter("@SimplyFYIAccountID", OleDbType.Integer);
    oleDbParameter12.Value = (object) SimplyFYIAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter12);
    dbConnection.Open();
    if (oleDbCommand.ExecuteNonQuery() > 0)
    {
      oleDbCommand.Parameters.Clear();
      if (!string.IsNullOrEmpty(ExternalPayerID))
      {
        oleDbCommand.CommandText = "spGetPayerIDFromPayerContactDataByExternalPayerID";
        OleDbParameter oleDbParameter13 = new OleDbParameter("@externalpayerid", OleDbType.VarChar);
        oleDbParameter13.Value = (object) ExternalPayerID;
        oleDbCommand.Parameters.Add(oleDbParameter13);
        OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
        oleDbDataAdapter.SelectCommand = oleDbCommand;
        DataSet dataSet = new DataSet();
        oleDbDataAdapter.Fill(dataSet);
        dbConnection.Close();
        num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["PayerID"]);
      }
      else
      {
        oleDbCommand.Parameters.Clear();
        oleDbCommand.CommandText = "spGetPayerContactDataByEmailAddressForAnAccountID";
        OleDbParameter oleDbParameter14 = new OleDbParameter("@SimplyFYIAccountID", OleDbType.Integer);
        oleDbParameter14.Value = (object) SimplyFYIAccountID;
        oleDbCommand.Parameters.Add(oleDbParameter14);
        OleDbParameter oleDbParameter15 = new OleDbParameter("@email", OleDbType.VarChar);
        oleDbParameter15.Value = (object) Email;
        oleDbCommand.Parameters.Add(oleDbParameter15);
        OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
        oleDbDataAdapter.SelectCommand = oleDbCommand;
        DataSet dataSet = new DataSet();
        oleDbDataAdapter.Fill(dataSet);
        dbConnection.Close();
        num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["PayerID"]);
      }
    }
    dbConnection.Close();
    return num;
  }

  public int WriteAccount2PayerMap(int AccountID, int PayerContactID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertAccount2PayerMap", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@payerid", OleDbType.Integer);
    oleDbParameter2.Value = (object) PayerContactID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WriteAccountAccountAddOnFeatures(int AccountID, string AddOnFeatures)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertAccountAddOnFeatures", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@addonfeatures", OleDbType.VarChar);
    oleDbParameter2.Value = (object) AddOnFeatures;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    int num = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return num;
  }

  public int WritePayPalPayerContactData2(
    string FirstName,
    string LastName,
    string StreetAddress,
    string City,
    string State,
    string ZipCode,
    string Country,
    string Email,
    string Phone,
    string BusinessName,
    string ExternalPayerID,
    int SimplyFYIAccountID)
  {
    int num = 0;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertPayPalPayerContactData2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@FirstName", OleDbType.VarChar);
    oleDbParameter1.Value = (object) FirstName;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@LastName", OleDbType.VarChar);
    oleDbParameter2.Value = (object) LastName;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@StreetAddress", OleDbType.VarChar);
    oleDbParameter3.Value = (object) StreetAddress;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@City", OleDbType.VarChar);
    oleDbParameter4.Value = City == null ? (object) DBNull.Value : (object) City;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@State", OleDbType.VarChar);
    oleDbParameter5.Value = State == null ? (object) DBNull.Value : (object) State;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@ZipCode", OleDbType.VarChar);
    oleDbParameter6.Value = ZipCode == null ? (object) DBNull.Value : (object) ZipCode;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@Country", OleDbType.VarChar);
    oleDbParameter7.Value = Country == null ? (object) DBNull.Value : (object) Country;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@email", OleDbType.VarChar);
    oleDbParameter8.Value = (object) Email;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@Phone", OleDbType.VarChar);
    oleDbParameter9.Value = (object) Phone;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@BusinessName", OleDbType.VarChar);
    oleDbParameter10.Value = (object) BusinessName;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@ExternalPayerID", OleDbType.VarChar);
    oleDbParameter11.Value = (object) ExternalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    dbConnection.Open();
    if (oleDbCommand.ExecuteNonQuery() > 0)
    {
      oleDbCommand.Parameters.Clear();
      if (!string.IsNullOrEmpty(ExternalPayerID))
      {
        oleDbCommand.CommandText = "spGetPayerIDFromPayerContactDataByExternalPayerID";
        OleDbParameter oleDbParameter12 = new OleDbParameter("@externalpayerid", OleDbType.VarChar);
        oleDbParameter12.Value = (object) ExternalPayerID;
        oleDbCommand.Parameters.Add(oleDbParameter12);
        OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
        oleDbDataAdapter.SelectCommand = oleDbCommand;
        DataSet dataSet = new DataSet();
        oleDbDataAdapter.Fill(dataSet);
        dbConnection.Close();
        num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["PayerID"]);
      }
      else
      {
        oleDbCommand.Parameters.Clear();
        oleDbCommand.CommandText = "spGetPayerContactDataByEmailAddress";
        OleDbParameter oleDbParameter13 = new OleDbParameter("@email", OleDbType.VarChar);
        oleDbParameter13.Value = (object) Email;
        oleDbCommand.Parameters.Add(oleDbParameter13);
        OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
        oleDbDataAdapter.SelectCommand = oleDbCommand;
        DataSet dataSet = new DataSet();
        oleDbDataAdapter.Fill(dataSet);
        dbConnection.Close();
        num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["PayerID"]);
      }
    }
    dbConnection.Close();
    return num;
  }

  public DataSet GetPayPalPayerContactDataAnonByInvoiceID(string PayPalInvoiceID)
  {
    DataSet dataSet1 = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayerContactDataAnonByPayPalInvoiceID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbParameter oleDbParameter = new OleDbParameter("@paypalpayerid", OleDbType.VarChar);
    oleDbParameter.Value = (object) PayPalInvoiceID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet2 = new DataSet();
    oleDbDataAdapter.Fill(dataSet2);
    dbConnection.Close();
    return dataSet2;
  }

  public int WritePayPalPayerContactDataAnon(
    string FirstName,
    string LastName,
    string StreetAddress,
    string City,
    string State,
    string ZipCode,
    string Country,
    string Email,
    string Phone,
    string BusinessName,
    string ExternalPayerID,
    string ExternalInvoiceID,
    int IPNTransactionDetailID,
    int SimplyFYIAccountID)
  {
    int num = 0;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertPayPalPayerContactDataAnon", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@FirstName", OleDbType.VarChar);
    oleDbParameter1.Value = (object) FirstName;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@LastName", OleDbType.VarChar);
    oleDbParameter2.Value = (object) LastName;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@StreetAddress", OleDbType.VarChar);
    oleDbParameter3.Value = (object) StreetAddress;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@City", OleDbType.VarChar);
    oleDbParameter4.Value = City == null ? (object) DBNull.Value : (object) City;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@State", OleDbType.VarChar);
    oleDbParameter5.Value = State == null ? (object) DBNull.Value : (object) State;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@ZipCode", OleDbType.VarChar);
    oleDbParameter6.Value = ZipCode == null ? (object) DBNull.Value : (object) ZipCode;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@Country", OleDbType.VarChar);
    oleDbParameter7.Value = Country == null ? (object) DBNull.Value : (object) Country;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@email", OleDbType.VarChar);
    oleDbParameter8.Value = (object) Email;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@Phone", OleDbType.VarChar);
    oleDbParameter9.Value = (object) Phone;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@BusinessName", OleDbType.VarChar);
    oleDbParameter10.Value = (object) BusinessName;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@ExternalPayerID", OleDbType.VarChar);
    oleDbParameter11.Value = (object) ExternalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    OleDbParameter oleDbParameter12 = new OleDbParameter("@IPNTransactionDetailID", OleDbType.Integer);
    oleDbParameter12.Value = (object) IPNTransactionDetailID;
    oleDbCommand.Parameters.Add(oleDbParameter12);
    OleDbParameter oleDbParameter13 = new OleDbParameter("@SimplyFYIAccountID", OleDbType.Integer);
    oleDbParameter13.Value = (object) SimplyFYIAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter13);
    OleDbParameter oleDbParameter14 = new OleDbParameter("@ExternalInvoiceID", OleDbType.VarChar);
    oleDbParameter14.Value = (object) ExternalInvoiceID;
    oleDbCommand.Parameters.Add(oleDbParameter14);
    dbConnection.Open();
    if (oleDbCommand.ExecuteNonQuery() > 0)
    {
      oleDbCommand.Parameters.Clear();
      oleDbCommand.CommandText = "spGetPayerIDFromPayerContactDataByExternalPayerID";
      oleDbCommand.Parameters.Add(oleDbParameter11);
      num = Convert.ToInt32(oleDbCommand.ExecuteScalar());
    }
    dbConnection.Close();
    return num;
  }

  public int WriteRegistrationDataToDB(RegistrationData RegData)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@wlid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) RegData.WindowsLiveUserID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter2.Value = RegData.WindowsLiveClientID == null ? (object) DBNull.Value : (object) RegData.WindowsLiveClientID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@fullname", OleDbType.VarChar);
    oleDbParameter3.Value = RegData.FullName == null ? (object) DBNull.Value : (object) RegData.FullName;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@phone", OleDbType.VarChar);
    oleDbParameter4.Value = (object) RegData.Phone;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@businessname", OleDbType.VarChar);
    oleDbParameter5.Value = (object) RegData.BusinessName;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@addr1", OleDbType.VarChar);
    oleDbParameter6.Value = (object) RegData.Address1;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@addr2", OleDbType.VarChar);
    oleDbParameter7.Value = (object) RegData.Address2;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@city", OleDbType.VarChar);
    oleDbParameter8.Value = (object) RegData.City;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@state", OleDbType.VarChar);
    oleDbParameter9.Value = (object) RegData.State;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@zip", OleDbType.VarChar);
    oleDbParameter10.Value = (object) RegData.ZIP;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    OleDbParameter oleDbParameter11 = new OleDbParameter("@country", OleDbType.VarChar);
    oleDbParameter11.Value = (object) RegData.Country;
    oleDbCommand.Parameters.Add(oleDbParameter11);
    OleDbParameter oleDbParameter12 = new OleDbParameter("@timezonecode", OleDbType.VarChar);
    oleDbParameter12.Value = (object) RegData.UserTimeZoneCode;
    oleDbCommand.Parameters.Add(oleDbParameter12);
    OleDbParameter oleDbParameter13 = new OleDbParameter("@subscription", OleDbType.VarChar);
    oleDbParameter13.Value = (object) RegData.Subscription;
    oleDbCommand.Parameters.Add(oleDbParameter13);
    OleDbParameter oleDbParameter14 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter14.Value = (object) RegData.ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter14);
    OleDbParameter oleDbParameter15 = new OleDbParameter("@enabled", OleDbType.Boolean);
    oleDbParameter15.Value = (object) RegData.Enabled;
    oleDbCommand.Parameters.Add(oleDbParameter15);
    dbConnection.Open();
    int db = oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public bool IsValidUserCredential(string UsernameEmail, string Password)
  {
    bool flag = false;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spIsValidUserCredential", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@UsernameEmail", OleDbType.VarChar);
    oleDbParameter1.Value = (object) UsernameEmail;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@Password", OleDbType.VarChar);
    oleDbParameter2.Value = (object) Password;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@Result", OleDbType.Integer);
    oleDbParameter3.Value = (object) -1;
    oleDbParameter3.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    if (Convert.ToInt32(oleDbParameter3.Value) == 1)
      flag = true;
    dbConnection.Close();
    return flag;
  }

  public bool IsValidRegistrantEmailAddress(string UsernameEmail, Guid EmailVerificationCode)
  {
    bool flag = false;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spIsValidRegistrantEmailAddress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@UsernameEmail", OleDbType.VarChar);
    oleDbParameter1.Value = (object) UsernameEmail;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@EmailVerificationCode", OleDbType.Guid);
    oleDbParameter2.Value = (object) EmailVerificationCode;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@Result", OleDbType.Integer);
    oleDbParameter3.Value = (object) -1;
    oleDbParameter3.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    if (Convert.ToInt32(oleDbParameter3.Value) == 1)
      flag = true;
    dbConnection.Close();
    return flag;
  }

  public bool IsAccountATenant(string OwnerWLID, string SubAccountWLID, string Subscription)
  {
    bool flag = false;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spIsAccountTenant", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@ownerwlid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) OwnerWLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@subaccountwlid", OleDbType.VarChar);
    oleDbParameter2.Value = (object) SubAccountWLID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@subscription", OleDbType.VarChar);
    oleDbParameter3.Value = (object) Subscription;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@Result", OleDbType.Integer);
    oleDbParameter4.Value = (object) -1;
    oleDbParameter4.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    if (Convert.ToInt32(oleDbParameter4.Value) == 1)
      flag = true;
    dbConnection.Close();
    return flag;
  }

  public bool IsAccountInTrialPeriod(int AccountID)
  {
    bool flag = false;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spIsAccountInTrialPeriod", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@istrial", OleDbType.Integer);
    oleDbParameter2.Value = (object) -1;
    oleDbParameter2.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    if (Convert.ToInt32(oleDbParameter2.Value) == 1)
      flag = true;
    dbConnection.Close();
    return flag;
  }

  public bool CustomerExists(string SMSNumber, string Email)
  {
    bool flag = false;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spCheckIfCustomerExists", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@smsnumber", OleDbType.VarChar);
    oleDbParameter1.Value = (object) SMSNumber;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@email", OleDbType.VarChar);
    oleDbParameter2.Value = (object) Email;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@exists", OleDbType.Integer);
    oleDbParameter3.Value = (object) -1;
    oleDbParameter3.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    if (Convert.ToInt32(oleDbParameter3.Value) == 1)
      flag = true;
    dbConnection.Close();
    return flag;
  }

  public bool IsUserRegistered(string WLID, string Subscription)
  {
    bool flag = false;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spCheckIfUserIsRegistered", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@wlid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) WLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@subscription", OleDbType.VarChar);
    oleDbParameter2.Value = (object) Subscription;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@Result", OleDbType.Integer);
    oleDbParameter3.Value = (object) -1;
    oleDbParameter3.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    if (Convert.ToInt32(oleDbParameter3.Value) == 1)
      flag = true;
    dbConnection.Close();
    return flag;
  }

  public bool AuthenticateDeveloper(Guid AppID, Guid ApiKey, Guid UserID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spAuthenticateDeveloper", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@appid", OleDbType.Guid);
    oleDbParameter1.Value = (object) AppID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@apikey", OleDbType.Guid);
    oleDbParameter2.Value = (object) ApiKey;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@userid", OleDbType.Guid);
    oleDbParameter3.Value = (object) UserID;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@isautheticated", OleDbType.Integer);
    oleDbParameter4.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    return Convert.ToInt32(oleDbParameter4.Value) == 1;
  }

  public bool CheckIfMetaDataNameExists(int AccountID, string MetaDataName)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spCheckIfMetaDataNameExists", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@metadataname", OleDbType.VarChar);
    oleDbParameter2.Value = (object) MetaDataName;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@exists", OleDbType.Integer);
    oleDbParameter3.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    return Convert.ToInt32(oleDbParameter3.Value) == 1;
  }

  public DataSet GetSavedFYIsByAccountAndEndPointType(
    int AccountID,
    VoiShareOLTypes.EndPointType EndPointType)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetSavedFYIsByAccountIDAndEndPointType", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@endpointtype", OleDbType.Integer);
    oleDbParameter2.Value = (object) Convert.ToInt32((object) EndPointType);
    selectCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public string[] GetSavedFYIsByAccount(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    string[] savedFyIsByAccount = (string[]) null;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetSavedFYIsByAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    ArrayList arrayList = (ArrayList) null;
    if (oleDbDataReader.HasRows)
    {
      arrayList = new ArrayList();
      while (oleDbDataReader.Read())
        arrayList.Add((object) oleDbDataReader["FYI"].ToString());
    }
    dbConnection.Close();
    if (arrayList != null)
      savedFyIsByAccount = (string[]) arrayList.ToArray(typeof (string));
    return savedFyIsByAccount;
  }

  public DataSet GetInactiveFYIsByParentAccountAsDataSet(int ParentAccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetInactiveFYIsByParentAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter.Value = (object) ParentAccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetInactiveFYIsByAccountAsDataSet(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetInactiveFYIsByAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetSavedFYIsByParentAccountAsDataSet(int ParentAccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetSavedFYIsByParentAccount", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter.Value = (object) ParentAccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetFYIByIDAsDataSet(int FYIMetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetFYIByIDAsDataSet", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter.Value = (object) FYIMetaID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public RegistrationData GetMerchantAccountRegistrationInfoByInvoiceID(string InvoiceID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMerchantAccountRegistrationInfoByInvoiceID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@InvoiceID", OleDbType.VarChar);
    oleDbParameter.Value = (object) InvoiceID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    RegistrationData registrationInfoByInvoiceId = new RegistrationData();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      registrationInfoByInvoiceId = new RegistrationData()
      {
        AccountID = (int) oleDbDataReader["ID"],
        FullName = oleDbDataReader["Fullname"].ToString(),
        Email = oleDbDataReader["WLID"].ToString(),
        WindowsLiveUserID = oleDbDataReader["WLID"].ToString(),
        WindowsLiveClientID = oleDbDataReader["CLID"].ToString()
      };
      registrationInfoByInvoiceId.FullName = oleDbDataReader["FullName"].ToString();
      registrationInfoByInvoiceId.Phone = oleDbDataReader["Phone"].ToString();
      registrationInfoByInvoiceId.BusinessName = oleDbDataReader["Businessname"].ToString();
      registrationInfoByInvoiceId.Address1 = oleDbDataReader["Address1"].ToString();
      registrationInfoByInvoiceId.Address2 = oleDbDataReader["Address2"].ToString();
      registrationInfoByInvoiceId.Address2 = oleDbDataReader["Address2"].ToString();
      registrationInfoByInvoiceId.City = oleDbDataReader["City"].ToString();
      registrationInfoByInvoiceId.State = oleDbDataReader["State"].ToString();
      registrationInfoByInvoiceId.ZIP = oleDbDataReader["ZIP"].ToString();
      registrationInfoByInvoiceId.Country = oleDbDataReader["Country"].ToString();
      registrationInfoByInvoiceId.UserTimeZoneCode = oleDbDataReader["TimeZoneCode"].ToString();
      registrationInfoByInvoiceId.Subscription = oleDbDataReader["Subscription"].ToString();
      registrationInfoByInvoiceId.ParentAccountID = (int) oleDbDataReader["ParentAccountID"];
    }
    dbConnection.Close();
    return registrationInfoByInvoiceId;
  }

  public DataSet GetNonConfirmationEngagementHistory(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetNonConfirmationEngagementHistory", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetNonConfirmationEngagementHistoryByPayerID(int AccountID, int PayerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetNonConfirmationEngagementHistoryByPayerID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@payerid", OleDbType.Integer);
    oleDbParameter2.Value = (object) PayerID;
    selectCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerTransactionAndConfirmationEmailHistoryByPayerID(int AccountID, int PayerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetPayerTransactionAndConfirmationEmailHistoryByPayerID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@payerid", OleDbType.Integer);
    oleDbParameter2.Value = (object) PayerID;
    selectCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPayerTransactionAndConfirmationEmailHistory(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetPayerTransactionAndConfirmationEmailHistory", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetSavedFYIsByAccountAsDataSet(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetSavedFYIsByAccountAsDataSet", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetSavedFYIsByAccountAsDataSetWithAppCategory(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetSavedFYIsByAccountAsDataSetWithAppCategory", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public void SetSenderIdentity(int MetaID, string SenderFromAddress, string SenderFromName)
  {
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpsertSenderIdentity", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimetaid", OleDbType.Integer);
      oleDbParameter1.Value = (object) MetaID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@senderfromaddress", OleDbType.VarChar);
      oleDbParameter2.Value = (object) SenderFromAddress;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@senderfromname", OleDbType.VarChar);
      oleDbParameter3.Value = (object) SenderFromAddress;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
  }

  public VoiShareOLTypes.SenderIdentity GetSpecialSenderIdentity(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    VoiShareOLTypes.SenderIdentity specialSenderIdentity = new VoiShareOLTypes.SenderIdentity();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetSpecialSenderIdentity", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      while (oleDbDataReader.Read())
      {
        specialSenderIdentity.FromAddress = oleDbDataReader["SenderFromAddress"].ToString();
        specialSenderIdentity.FromName = oleDbDataReader["SenderFromName"].ToString();
      }
    }
    dbConnection.Close();
    return specialSenderIdentity;
  }

  public string[] GetSavedFYIsByParentAccount(int ParentAccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    string[] isByParentAccount = (string[]) null;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetSavedFYIsByParentAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    ArrayList arrayList = (ArrayList) null;
    if (oleDbDataReader.HasRows)
    {
      arrayList = new ArrayList();
      while (oleDbDataReader.Read())
        arrayList.Add((object) oleDbDataReader["FYI"].ToString());
    }
    dbConnection.Close();
    if (arrayList != null)
      isByParentAccount = (string[]) arrayList.ToArray(typeof (string));
    return isByParentAccount;
  }

  public bool EndPointsExistForAccount(int AccountID)
  {
    bool flag = false;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spCheckIfEndPointsExistForAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@result", OleDbType.Integer);
    oleDbParameter2.Value = (object) -1;
    oleDbParameter2.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    if (Convert.ToInt32(oleDbParameter2.Value) == 1)
      flag = true;
    dbConnection.Close();
    return flag;
  }

  public DataSet GetCustomerImageMetaData(string CLID, string ImageFilename)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetCustomerImageByFilename", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) CLID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@imagefilename", OleDbType.VarChar);
    oleDbParameter2.Value = (object) ImageFilename;
    selectCommand.Parameters.Add(oleDbParameter2);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetCustomerImages(string CLID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetCustomerImages", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter.Value = (object) CLID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public AccountEndPointIdentities GetAccountEndPoints(int AccountID)
  {
    ArrayList arrayList = new ArrayList();
    AccountEndPointIdentities accountEndPoints = (AccountEndPointIdentities) null;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountEndPoints", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      while (oleDbDataReader.Read())
        arrayList.Add((object) new EndPointIdentity()
        {
          EndPointType = (VoiShareOLTypes.EndPointType) oleDbDataReader["EndPointType"],
          SenderAddress = oleDbDataReader["FromAddress"].ToString(),
          SenderName = oleDbDataReader["FromName"].ToString()
        });
      accountEndPoints = new AccountEndPointIdentities();
    }
    dbConnection.Close();
    EndPointIdentity[] array = (EndPointIdentity[]) arrayList.ToArray(typeof (EndPointIdentity));
    accountEndPoints.AccountID = AccountID;
    accountEndPoints.EndPointIdentities = array;
    return accountEndPoints;
  }

  public string GetMetaFYIName(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMetaFYIName", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    string metaFyiName = oleDbCommand.ExecuteScalar().ToString();
    dbConnection.Close();
    return metaFyiName;
  }

  public DataSet GetMetaFYINameAndVersion(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetMetaFYINameAndVersion", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DateTime GetMostRecentPaymentDateForPayer(int PayPalPayerID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMostRecentTransactionDateByPayPalPayerID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@paypalpayerid", OleDbType.Integer);
    oleDbParameter.Value = (object) PayPalPayerID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    DateTime dateTime = Convert.ToDateTime(oleDbCommand.ExecuteScalar().ToString());
    dbConnection.Close();
    return dateTime;
  }

  public DataSet GetPayPalPrimaryEmailForAccount(string CLID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetPayPalPrimaryEmailForAccount", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter.Value = (object) CLID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetIPNTransactionsByOriginatorTransactionID(string OriginatorTransactionID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetIPNTransactionsByOriginatorTransactionID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@OriginatorTransactionID", OleDbType.VarChar);
    oleDbParameter.Value = (object) OriginatorTransactionID;
    selectCommand.Parameters.Add(oleDbParameter);
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public string GetMetaFYINameByBatchID(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMetaFYINameByBatchID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    object obj = oleDbCommand.ExecuteScalar();
    string fyiNameByBatchId = obj == null ? string.Empty : obj.ToString().Trim();
    dbConnection.Close();
    return fyiNameByBatchId;
  }

  public int GetQueueTypeForFYIMessageBatchID(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetQueueTypeForFYIMessageBatchID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter1.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@queuetype", OleDbType.Integer);
    oleDbParameter2.Value = (object) -1;
    oleDbParameter2.Direction = ParameterDirection.Output;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    oleDbCommand.ExecuteScalar();
    dbConnection.Close();
    return Convert.ToInt32(oleDbParameter2.Value);
  }

  public string GetMetaFYINameByBatchIDForInboundSMS(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMetaFYINameByBatchIDForInboundSMS", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    object obj = oleDbCommand.ExecuteScalar();
    string batchIdForInboundSms = obj == null ? string.Empty : obj.ToString().Trim();
    dbConnection.Close();
    return batchIdForInboundSms;
  }

  public RegistrationData GetRegistrationData(string WLID, string Subscription)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    RegistrationData registrationData = (RegistrationData) null;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetRegistrationRecord", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@wlid", OleDbType.VarChar);
    oleDbParameter1.Value = (object) WLID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@subscription", OleDbType.VarChar);
    oleDbParameter2.Value = (object) Subscription;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      registrationData = new RegistrationData()
      {
        AccountID = (int) oleDbDataReader["ID"],
        FullName = oleDbDataReader["Fullname"].ToString(),
        Email = oleDbDataReader[nameof (WLID)].ToString()
      };
      registrationData.WindowsLiveUserID = registrationData.Email;
      registrationData.WindowsLiveClientID = oleDbDataReader["CLID"].ToString();
      registrationData.FullName = oleDbDataReader["FullName"].ToString();
      registrationData.Phone = oleDbDataReader["Phone"].ToString();
      registrationData.BusinessName = oleDbDataReader["BusinessName"].ToString();
      registrationData.Address1 = oleDbDataReader["Address1"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.City = oleDbDataReader["City"].ToString();
      registrationData.State = oleDbDataReader["State"].ToString();
      registrationData.ZIP = oleDbDataReader["ZIP"].ToString();
      registrationData.Country = oleDbDataReader["Country"].ToString();
      registrationData.UserTimeZoneCode = oleDbDataReader["TimeZoneCode"].ToString();
      registrationData.Subscription = oleDbDataReader[nameof (Subscription)].ToString();
      registrationData.ParentAccountID = (int) oleDbDataReader["ParentAccountID"];
      registrationData.Enabled = Convert.ToBoolean(oleDbDataReader["Enabled"]);
    }
    dbConnection.Close();
    return registrationData;
  }

  public bool IsParentAccount(int AccountID)
  {
    bool flag = false;
    RegistrationData registrationData = this.GetRegistrationData(AccountID);
    if (registrationData != null && registrationData.ParentAccountID == 999999)
      flag = true;
    return flag;
  }

  public RegistrationData[] GetAllSubAccounts(int ParentAccountID)
  {
    return this.GetSubAccountRegistrationData(ParentAccountID, (string) null);
  }

  public RegistrationData[] GetSubAccountRegistrationData(
    int ParentAccountID,
    string SubAccountWLID)
  {
    this.GetRegistrationData(ParentAccountID);
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetSubAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@subaccountwlid", OleDbType.VarChar);
    if (SubAccountWLID != null)
      oleDbParameter2.Value = (object) SubAccountWLID;
    else
      oleDbParameter2.Value = (object) DBNull.Value;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      while (oleDbDataReader.Read())
      {
        RegistrationData registrationData = new RegistrationData()
        {
          AccountID = (int) oleDbDataReader["ID"],
          FullName = oleDbDataReader["Fullname"] == DBNull.Value ? (string) null : oleDbDataReader["Fullname"].ToString(),
          WindowsLiveUserID = oleDbDataReader["WLID"].ToString(),
          WindowsLiveClientID = oleDbDataReader["CLID"] == DBNull.Value ? (string) null : oleDbDataReader["CLID"].ToString(),
          Enabled = Convert.ToBoolean(oleDbDataReader["Enabled"]),
          ParentAccountID = (int) oleDbDataReader[nameof (ParentAccountID)],
          Address1 = oleDbDataReader["Address1"].ToString().Trim(),
          Address2 = oleDbDataReader["Address2"].ToString().Trim(),
          BusinessName = oleDbDataReader["BusinessName"].ToString().Trim(),
          City = oleDbDataReader["City"].ToString().Trim(),
          Country = oleDbDataReader["Country"].ToString().Trim(),
          Phone = oleDbDataReader["Phone"].ToString().Trim(),
          State = oleDbDataReader["State"].ToString().Trim(),
          UserTimeZoneCode = oleDbDataReader["TimeZoneCode"].ToString().Trim(),
          ZIP = oleDbDataReader["ZIP"].ToString().Trim()
        };
        registrationData.Email = registrationData.WindowsLiveUserID;
        registrationData.Subscription = oleDbDataReader["Subscription"].ToString().Trim();
        arrayList.Add((object) registrationData);
      }
    }
    dbConnection.Close();
    return (RegistrationData[]) arrayList.ToArray(typeof (RegistrationData));
  }

  public int GetUsageInventoryBalanceByParentAccountID(int ParentAccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetUsageInventoryBalanceByParentAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter.Value = (object) ParentAccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return Convert.ToInt32(dataSet.Tables[0].Rows[0]["Balance"]);
  }

  public DataSet GetUsageInventoryRecordByAccountIDAndReferenceID(int AccountID, string ReferenceID)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetUsageInventoryRecordByAccountIDAndReferenceID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@accountid", OleDbType.VarChar);
    oleDbParameter2.Value = (object) ReferenceID;
    selectCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public int GetUsageInventoryBalanceByAccountID(int AccountID)
  {
    int balanceByAccountId = 0;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetUsageInventoryBalanceByAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    if (dataSet.Tables[0].Rows.Count > 0)
      balanceByAccountId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Balance"]);
    return balanceByAccountId;
  }

  public DataSet GetEndPointTypeAndQtyOfABatch(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetEndPointTypeAndQtyOfABatch", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter.Value = (object) BatchID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetEndPointTypeAndQtyOfABatchForInboundSMS(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetEndPointTypeAndQtyOfABatchForInboundSMS", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter.Value = (object) BatchID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetUsageInventoryByParentAccountID(int ParentAccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetUsageInventoryByParentAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter.Value = (object) ParentAccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetShipmentConfirmationsWaitingToBeSent(
    int AccountID,
    DateTime TransactionDateTimeStart,
    DateTime TransactionDateTimeEnd)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetShipmentConfirmationsWaitingToBeSent", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@transactiondatetimestart", OleDbType.Date);
    oleDbParameter2.Value = (object) TransactionDateTimeStart;
    selectCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@transactiondatetimeend", OleDbType.Date);
    oleDbParameter3.Value = (object) TransactionDateTimeEnd;
    selectCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetShipmentConfirmationsSent(
    int AccountID,
    DateTime TransactionDateTimeStart,
    DateTime TransactionDateTimeEnd)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetShipmentConfirmationsSent", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@TransactionDateTimeStart", OleDbType.Date);
    oleDbParameter2.Value = (object) TransactionDateTimeStart;
    selectCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@TransactionDateTimeEnd", OleDbType.Date);
    oleDbParameter3.Value = (object) TransactionDateTimeEnd;
    selectCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetUsageInventoryByAccountID(int AccountID)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetUsageInventoryByAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetPagedUsageInventoryByAccountID(int AccountID, int LastIndex)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetPagedUsageInventoryByAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@LastIndex", OleDbType.Integer);
    oleDbParameter2.Value = (object) LastIndex;
    selectCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetFirstPageUsageInventoryByAccountID(int AccountID)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetFirstPageUsageInventoryByAccountID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public RegistrationData GetRegistrationData(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    RegistrationData registrationData = (RegistrationData) null;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetRegistrationRecordByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      registrationData = new RegistrationData()
      {
        AccountID = (int) oleDbDataReader["ID"],
        FullName = oleDbDataReader["Fullname"].ToString(),
        Email = oleDbDataReader["WLID"].ToString(),
        WindowsLiveUserID = oleDbDataReader["WLID"].ToString(),
        WindowsLiveClientID = oleDbDataReader["CLID"].ToString()
      };
      registrationData.FullName = oleDbDataReader["FullName"].ToString();
      registrationData.Phone = oleDbDataReader["Phone"].ToString();
      registrationData.BusinessName = oleDbDataReader["BusinessName"].ToString();
      registrationData.Address1 = oleDbDataReader["Address1"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.City = oleDbDataReader["City"].ToString();
      registrationData.State = oleDbDataReader["State"].ToString();
      registrationData.ZIP = oleDbDataReader["ZIP"].ToString();
      registrationData.Country = oleDbDataReader["Country"].ToString();
      registrationData.UserTimeZoneCode = oleDbDataReader["TimeZoneCode"].ToString();
      registrationData.Subscription = oleDbDataReader["Subscription"].ToString();
      registrationData.ParentAccountID = (int) oleDbDataReader["ParentAccountID"];
      registrationData.Enabled = Convert.ToBoolean(oleDbDataReader["Enabled"]);
    }
    dbConnection.Close();
    return registrationData;
  }

  public RegistrationData GetRegistrationData(string CLID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    RegistrationData registrationData = (RegistrationData) null;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetRegistrationRecordByCLID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      registrationData = new RegistrationData()
      {
        AccountID = (int) oleDbDataReader["ID"],
        FullName = oleDbDataReader["Fullname"].ToString(),
        Email = oleDbDataReader["WLID"].ToString(),
        WindowsLiveUserID = oleDbDataReader["WLID"].ToString(),
        WindowsLiveClientID = oleDbDataReader[nameof (CLID)].ToString()
      };
      registrationData.FullName = oleDbDataReader["FullName"].ToString();
      registrationData.Phone = oleDbDataReader["Phone"].ToString();
      registrationData.BusinessName = oleDbDataReader["BusinessName"].ToString();
      registrationData.Address1 = oleDbDataReader["Address1"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.City = oleDbDataReader["City"].ToString();
      registrationData.State = oleDbDataReader["State"].ToString();
      registrationData.ZIP = oleDbDataReader["ZIP"].ToString();
      registrationData.Country = oleDbDataReader["Country"].ToString();
      registrationData.UserTimeZoneCode = oleDbDataReader["TimeZoneCode"].ToString();
      registrationData.Subscription = oleDbDataReader["Subscription"].ToString();
      registrationData.ParentAccountID = (int) oleDbDataReader["ParentAccountID"];
      registrationData.Enabled = Convert.ToBoolean(oleDbDataReader["Enabled"]);
    }
    dbConnection.Close();
    return registrationData;
  }

  public RegistrationData GetOwnerRegistrationData(string Subscription)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    RegistrationData registrationData = (RegistrationData) null;
    OleDbCommand oleDbCommand = new OleDbCommand("spGetRegistrationRecordBySubscription", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@subscription", OleDbType.VarChar);
    oleDbParameter.Value = (object) Subscription;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      registrationData = new RegistrationData()
      {
        AccountID = (int) oleDbDataReader["ID"],
        ParentAccountID = (int) oleDbDataReader["ParentAccountID"],
        FullName = oleDbDataReader["Fullname"].ToString(),
        Email = oleDbDataReader["WLID"].ToString(),
        WindowsLiveUserID = oleDbDataReader["WLID"].ToString(),
        WindowsLiveClientID = oleDbDataReader["CLID"].ToString()
      };
      registrationData.FullName = oleDbDataReader["FullName"].ToString();
      registrationData.Phone = oleDbDataReader["Phone"].ToString();
      registrationData.BusinessName = oleDbDataReader["BusinessName"].ToString();
      registrationData.Address1 = oleDbDataReader["Address1"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.Address2 = oleDbDataReader["Address2"].ToString();
      registrationData.City = oleDbDataReader["City"].ToString();
      registrationData.State = oleDbDataReader["State"].ToString();
      registrationData.ZIP = oleDbDataReader["ZIP"].ToString();
      registrationData.Country = oleDbDataReader["Country"].ToString();
      registrationData.UserTimeZoneCode = oleDbDataReader["TimeZoneCode"].ToString();
      registrationData.Subscription = oleDbDataReader[nameof (Subscription)].ToString();
    }
    dbConnection.Close();
    return registrationData;
  }

  public void SetOptInStatus(int AccountID, VoiShareOLTypes.OptStatus OptInStatus)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spSetOptInStatus", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@OptInStatus", OleDbType.Integer);
    oleDbParameter2.Value = (object) OptInStatus;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    try
    {
      dbConnection.Open();
      oleDbCommand.ExecuteNonQuery();
    }
    catch
    {
    }
  }

  public void SetOptOutStatus(
    int AccountID,
    int MetaID,
    string EndPointAddress,
    VoiShareOLTypes.EndPointType EndPointType,
    DateTime ActivityDateTime,
    VoiShareOLTypes.OptStatus OptOutStatus)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spSetOptOutStatus", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter2.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@EndPointAddress", OleDbType.VarChar);
    oleDbParameter3.Value = (object) EndPointAddress;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@EndPointType", OleDbType.Integer);
    oleDbParameter4.Value = (object) Convert.ToInt32((object) EndPointType);
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@ActivityDateTime", OleDbType.Date);
    oleDbParameter5.Value = (object) ActivityDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@OptOutStatus", OleDbType.Integer);
    oleDbParameter6.Value = (object) Convert.ToInt32((object) OptOutStatus);
    oleDbCommand.Parameters.Add(oleDbParameter6);
    try
    {
      dbConnection.Open();
      oleDbCommand.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
    }
  }

  public VoiShareOLTypes.OptStatus GetOptInStatus(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetOptInStatus", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter2.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@EndPointType", OleDbType.Integer);
    oleDbParameter3.Value = (object) Convert.ToInt32((object) EndPointType);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@EndPointAddress", OleDbType.VarChar);
    oleDbParameter4.Value = (object) EndPointAddress;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    VoiShareOLTypes.OptStatus optInStatus = dataSet.Tables[0].Rows.Count <= 0 ? VoiShareOLTypes.OptStatus.NOT_OPTEDIN : (VoiShareOLTypes.OptStatus) Convert.ToInt32(dataSet.Tables[0].Rows[0]["OptInStatus"]);
    dbConnection.Close();
    return optInStatus;
  }

  public VoiShareOLTypes.OptStatus GetOptOutStatus(
    int AccountID,
    int MetaID,
    VoiShareOLTypes.EndPointType EndPointType,
    string EndPointAddress)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetOptOutStatus", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter2.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@EndPointType", OleDbType.Integer);
    oleDbParameter3.Value = (object) Convert.ToInt32((object) EndPointType);
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@EndPointAddress", OleDbType.VarChar);
    oleDbParameter4.Value = (object) EndPointAddress;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    VoiShareOLTypes.OptStatus optOutStatus = dataSet.Tables[0].Rows.Count <= 0 ? VoiShareOLTypes.OptStatus.NOT_OPTEDIN : (VoiShareOLTypes.OptStatus) Convert.ToInt32(dataSet.Tables[0].Rows[0]["OptOutStatus"]);
    dbConnection.Close();
    return optOutStatus;
  }

  public int CloneFYIApp(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    dbConnection.Open();
    OleDbCommand oleDbCommand = new OleDbCommand("spCloneApp", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    oleDbCommand.ExecuteNonQuery();
    oleDbCommand.CommandText = "select @@identity";
    oleDbCommand.CommandType = CommandType.Text;
    int int32 = Convert.ToInt32(oleDbCommand.ExecuteScalar());
    dbConnection.Close();
    return int32;
  }

  public int CloneFYIAppToAccount(int MetaID, int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    dbConnection.Open();
    OleDbCommand oleDbCommand = new OleDbCommand("spCloneAppToAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter2.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    oleDbCommand.ExecuteNonQuery();
    oleDbCommand.CommandText = "select @@identity";
    oleDbCommand.CommandType = CommandType.Text;
    int int32 = Convert.ToInt32(oleDbCommand.ExecuteScalar());
    dbConnection.Close();
    return int32;
  }

  public void WritePayPalTransactionHistoryRecord(
    DateTime TransactionDateTime,
    int PayPalPayerContactID,
    double GrossAmount,
    string OriginatorTransactionID,
    string CSVRecord)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("dbo.spUpsertPayPalTransactionHistoryRecord", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@transactiondatetime", OleDbType.DBTimeStamp);
    oleDbParameter1.Value = (object) TransactionDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@PayPalPayerContactID", OleDbType.Integer);
    oleDbParameter2.Value = (object) PayPalPayerContactID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@GrossAmount", OleDbType.Decimal);
    oleDbParameter3.Value = (object) GrossAmount;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@OriginatorTransactionID", OleDbType.VarChar);
    oleDbParameter4.Value = (object) OriginatorTransactionID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@CSVRecord", OleDbType.VarChar);
    oleDbParameter5.Value = (object) CSVRecord;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
  }

  public DataSet GetIPNTransactionByOriginatorTransactionID(string OriginatorTransactionID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("dbo.spGetIPNTransactionsByOriginatorTransactionID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@OriginatorTransactionID", OleDbType.VarChar);
    oleDbParameter.Value = (object) OriginatorTransactionID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetIPNTransactionByIPNTransactionDetailID(int IPNTransactionDetailID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("dbo.spGetIPNTransactionByIPNTransactionDetailID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@IPNTransactionDetailID", OleDbType.Integer);
    oleDbParameter.Value = (object) IPNTransactionDetailID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public int WriteIPNTransactionDetail(
    DateTime IPNDateTime,
    int PayPalPayerContactID,
    double GrossAmount,
    int Transaction2HandlerMapID,
    string OriginatorTransactionID,
    string OriginatorIPNID,
    string RequestString,
    string ResponseString,
    int FYIMessageID)
  {
    if (RequestString.Length > 2048 /*0x0800*/)
      RequestString = RequestString.Substring(0, 2048 /*0x0800*/);
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("dbo.spUpsertIPNTransactionDetail", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@ipndatetime", OleDbType.DBTimeStamp);
    oleDbParameter1.Value = (object) IPNDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@PayPalPayerContactID", OleDbType.Integer);
    oleDbParameter2.Value = (object) PayPalPayerContactID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@GrossAmount", OleDbType.Decimal);
    oleDbParameter3.Value = (object) GrossAmount;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@Transaction2HandlerMapID", OleDbType.Integer);
    oleDbParameter4.Value = (object) Transaction2HandlerMapID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@OriginatorTransactionID", OleDbType.VarChar);
    oleDbParameter5.Value = (object) OriginatorTransactionID;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@OriginatorIPNID", OleDbType.VarChar);
    oleDbParameter6.Value = (object) OriginatorIPNID;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@request", OleDbType.VarChar);
    oleDbParameter7.Value = (object) RequestString;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@response", OleDbType.VarChar);
    oleDbParameter8.Value = (object) ResponseString;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@FYIMessageID", OleDbType.Integer);
    oleDbParameter9.Value = (object) FYIMessageID;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    dbConnection.Open();
    int num1 = oleDbCommand.ExecuteNonQuery();
    int num2 = 0;
    if (num1 > 0)
    {
      oleDbCommand.Parameters.Clear();
      oleDbCommand.CommandText = "spGetIPNTransactionsByOriginatorTransactionID";
      oleDbCommand.Parameters.Add(oleDbParameter5);
      num2 = Convert.ToInt32(oleDbCommand.ExecuteScalar());
    }
    dbConnection.Close();
    return num2;
  }

  public int WriteIPNTransactionDetail2(
    DateTime IPNDateTime,
    int PayPalPayerContactID,
    double GrossAmount,
    int Transaction2HandlerMapID,
    string OriginatorTransactionID,
    string OriginatorIPNID,
    string RequestString,
    string ResponseString,
    int FYIMessageID,
    int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("dbo.spUpsertIPNTransactionDetail2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@ipndatetime", OleDbType.DBTimeStamp);
    oleDbParameter1.Value = (object) IPNDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@PayPalPayerContactID", OleDbType.Integer);
    oleDbParameter2.Value = (object) PayPalPayerContactID;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@GrossAmount", OleDbType.Decimal);
    oleDbParameter3.Value = (object) GrossAmount;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@Transaction2HandlerMapID", OleDbType.Integer);
    oleDbParameter4.Value = (object) Transaction2HandlerMapID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@OriginatorTransactionID", OleDbType.VarChar);
    oleDbParameter5.Value = (object) OriginatorTransactionID;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@OriginatorIPNID", OleDbType.VarChar);
    oleDbParameter6.Value = (object) OriginatorIPNID;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@request", OleDbType.VarChar);
    oleDbParameter7.Value = (object) RequestString;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@response", OleDbType.VarChar);
    oleDbParameter8.Value = (object) ResponseString;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@FYIMessageID", OleDbType.Integer);
    oleDbParameter9.Value = (object) FYIMessageID;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter10.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    dbConnection.Open();
    int num1 = oleDbCommand.ExecuteNonQuery();
    int num2 = 0;
    if (num1 > 0)
    {
      oleDbCommand.Parameters.Clear();
      oleDbCommand.CommandText = "spGetIPNTransactionsByOriginatorTransactionID";
      oleDbCommand.Parameters.Add(oleDbParameter5);
      num2 = Convert.ToInt32(oleDbCommand.ExecuteScalar());
    }
    dbConnection.Close();
    return num2;
  }

  public DataSet GetFYIHandlerSelectorsForDefaultFYIHandler(int DefaultFYIHandler)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetFYIHandlerSelectorsByDefaultFYIHandler", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@defaultfyihandler", OleDbType.Integer);
    oleDbParameter.Value = (object) DefaultFYIHandler;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public int GetPayPalNotificationMessageFlowMode(int HandlerFYIMetaID)
  {
    int notificationMessageFlowMode = 0;
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMessageFlowModeByHandlerFYIMetaID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    oleDbCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@HandlerFYIMetaID", OleDbType.Integer);
    oleDbParameter.Value = (object) HandlerFYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    object obj = oleDbCommand.ExecuteScalar();
    if (obj != null)
      notificationMessageFlowMode = Convert.ToInt32(obj);
    dbConnection.Close();
    return notificationMessageFlowMode;
  }

  public DataSet GetTransaction2HandlerMap(
    string TransactionType,
    string TransactionOriginator,
    string CLID)
  {
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetTransaction2HandlerMap", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@TransactionType", OleDbType.VarChar);
    oleDbParameter1.Value = (object) TransactionType;
    selectCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@TransactionOriginator", OleDbType.VarChar);
    oleDbParameter2.Value = (object) TransactionOriginator;
    selectCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@CLID", OleDbType.VarChar);
    oleDbParameter3.Value = (object) CLID;
    selectCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public int GetAppCategoryByMetaID(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAppcategoryByMetaID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet.Tables[0].Rows.Count > 0 ? Convert.ToInt32(dataSet.Tables[0].Rows[0]["AppCategory"].ToString()) : 0;
  }

  public DataSet GetPayPalBillingAgreementByCLID(string CLID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetPayPalBillingAgreementByCLID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@clid", OleDbType.VarChar);
    oleDbParameter.Value = (object) CLID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public string GetCLIDFromWixInstanceID(string WixInstanceID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetCLIDFromWixInstanceID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@wixinstanceid", OleDbType.VarChar);
    oleDbParameter.Value = (object) WixInstanceID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet.Tables[0].Rows.Count > 0 ? dataSet.Tables[0].Rows[0]["CLID"].ToString() : string.Empty;
  }

  public int GetEndPointSubTypeByMetaID(int MetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetEndPointSubTypeByMetaID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return Convert.ToInt32(dataSet.Tables[0].Rows[0]["EndPointSubType"].ToString());
  }

  public DataSet GetIPNTransactionsByDate(DateTime Date)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetIPNTransactionsByDate", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@date", OleDbType.DBTimeStamp);
    oleDbParameter.Value = (object) Date;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetMostRecentIPNTransactionByPayPalPayerContactID(int PayPalPayerContactID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMostRecentIPNTransactionByPayPalPayerContactID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@PayPalPayerContactID", OleDbType.Integer);
    oleDbParameter.Value = (object) PayPalPayerContactID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetIPNTransactionsByPayPalPayerContactID(int PayPalPayerContactID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetIPNTransactionsByPayPalPayerContactID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@PayPalPayerContactID", OleDbType.Integer);
    oleDbParameter.Value = (object) PayPalPayerContactID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetCustomerContactListNamesByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetCustomerContactListNamesByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetShippingCarriers()
  {
    Hashtable hashtable = new Hashtable();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetShippingCarriers", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public bool IsShippingConfirmationAddOnEnabled(int AccountID)
  {
    bool flag = false;
    DataSet accountAddOnFeatures = this.GetAccountAddOnFeatures(AccountID);
    if (accountAddOnFeatures.Tables[0].Rows.Count > 0 && accountAddOnFeatures.Tables[0].Rows[0]["AddOnFeatures"].ToString().Contains("SHIPMENTCONFIRMATION=1"))
      flag = true;
    return flag;
  }

  public DataSet GetAccountAddOnFeatures(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountAddOnFeatures", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public Hashtable GetCarriers()
  {
    Hashtable carriers = new Hashtable();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetCarriers", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    if (dataSet.Tables.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
        carriers.Add((object) row["gateway"].ToString().Trim(), (object) row["carriername"].ToString().Trim());
    }
    return carriers;
  }

  public DataSet GetAllAccountNames()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAllAccountNames", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAppCategories()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAppCategories", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAppDetailsFromAppBazaarByAppID(int AppID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAppDetailsFromAppBazaarByAppID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@appid", OleDbType.Integer);
    oleDbParameter.Value = (object) AppID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAllAppsFromAppBazaar()
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAllAppsFromAppBazaar", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetCountriesAndCountryCodes()
  {
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetCountriesAndCountryCodes", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetTimeZoneCodesAndOffsetsByCountryCode(int CountryCode)
  {
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetTimeZoneCodesAndOffsetsByCountryCode", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@countrycode", OleDbType.Integer);
    oleDbParameter.Value = (object) CountryCode;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetTimeZoneCodesAndOffsetsByCountryName(int CountryName)
  {
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetTimeZoneCodesAndOffsetsByCountryName", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@countrycode", OleDbType.VarChar);
    oleDbParameter.Value = (object) CountryName;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public RegistrationData.TimeZone[] GetTimeZones()
  {
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetTimeZones", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    if (dataSet.Tables.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
        arrayList.Add((object) new RegistrationData.TimeZone()
        {
          TimeZoneName = row["Name"].ToString(),
          TimeZoneOffset = row["Offset"].ToString(),
          TimeZoneCode = row["TimeZoneCode"].ToString()
        });
    }
    return (RegistrationData.TimeZone[]) arrayList.ToArray(typeof (RegistrationData.TimeZone));
  }

  public RegistrationData.TimeZone[] GetTimeZonesByCountryName(string CountryName)
  {
    ArrayList arrayList = new ArrayList();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetTimeZoneCodesAndOffsetsByCountryName", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@countryname", OleDbType.VarChar);
    oleDbParameter.Value = (object) CountryName;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    if (dataSet.Tables.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
        arrayList.Add((object) new RegistrationData.TimeZone()
        {
          TimeZoneName = row["Name"].ToString(),
          TimeZoneOffset = row["Offset"].ToString(),
          TimeZoneCode = row["TimeZoneCode"].ToString()
        });
    }
    return (RegistrationData.TimeZone[]) arrayList.ToArray(typeof (RegistrationData.TimeZone));
  }

  public string GetCachedQueryStringFromDB(Guid GuidIdentifier)
  {
    string queryStringFromDb = (string) null;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetQueryStringFromCache", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@guididentifier", OleDbType.Guid);
    oleDbParameter.Value = (object) GuidIdentifier;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    if (dataSet.Tables[0].Rows.Count > 0)
      queryStringFromDb = dataSet.Tables[0].Rows[0]["querystring"].ToString();
    dbConnection.Close();
    return queryStringFromDb;
  }

  public int GetInboundSMSOrganizationID(string OrganizationKey, string CalledSMSNumber)
  {
    int smsOrganizationId = 0;
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetInboundSMSOrganizationID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@organizationkey", OleDbType.VarChar);
    oleDbParameter1.Value = (object) OrganizationKey;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@calledsmsnumber", OleDbType.VarChar);
    oleDbParameter2.Value = (object) CalledSMSNumber;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
      smsOrganizationId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ID"]);
    dbConnection.Close();
    return smsOrganizationId;
  }

  public int GetTimeZoneOffset(string TimeZoneCode)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetTimeZoneOffset", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@timezonecode", OleDbType.VarChar);
    oleDbParameter.Value = (object) TimeZoneCode;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    int int32 = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Offset"]);
    dbConnection.Close();
    return int32;
  }

  public DataSet GetMessageByMessageID(int MessageID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetFYIMessageByMessageID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@FYIMessageID", OleDbType.Integer);
    oleDbParameter.Value = (object) MessageID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public string GetEmailMessageHtml(int FYIMessageID)
  {
    string emailMessageHtml = "<html></html>";
    DataSet dataSet = new DataSet();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand("spGetFYIMessageByMessageID", dbConnection);
    selectCommand.CommandType = CommandType.StoredProcedure;
    selectCommand.Connection = dbConnection;
    OleDbParameter oleDbParameter = new OleDbParameter("@fyimessageid", (object) SqlDbType.Int);
    oleDbParameter.Value = (object) FYIMessageID;
    selectCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
    {
      XmlDocument xmlDocument = new XmlDocument();
      string xml = this.DeserializeFYIXMLMessageBody((byte[]) dataSet.Tables[0].Rows[0]["XmlMessageBody"]);
      xmlDocument.LoadXml(xml);
      string str = dataSet.Tables[0].Rows[0]["EndPointType"].ToString();
      if (str == "1" || str == "5")
        emailMessageHtml = xmlDocument.SelectSingleNode("//fyimessage/email/htmlmessage").InnerText.Replace("&lt;", "<").Replace("&gt;", ">");
    }
    return emailMessageHtml;
  }

  public DataSet GetMessageByBatchID(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetFYIMessagesByBatchID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetIVRResponsesByBatchID(Guid BatchID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetIVRResponsesByBatchID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAllEmailBouncesByAccountIDAndDateRange(
    int AccountID,
    DateTime StartDate,
    DateTime EndDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAllBouncesByAccountIDAndDateRange", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@startdate", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) StartDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@enddate", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) EndDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetBatchesAndSentTimesForAccountIDByEndPointType(
    int AccountID,
    VoiShareOLTypes.EndPointType EndPointType)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetBatchesAndSentTimesForAccountIDByEndPointType", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@endpointtype", OleDbType.Integer);
    oleDbParameter2.Value = (object) EndPointType;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetBatchesAndSentTimesByFYIMetaIDAndTimeRange(
    int FYIMetaID,
    DateTime StartTime,
    DateTime EndTime)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetBatchesAndSentTimesByFYIMetaIDAndTimeRange", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimetaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@starttime", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) StartTime;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@endtime", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) EndTime;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetMessageStatusByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMessageStatusByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetMessageStatusByAccountIDByDateRange(
    int AccountID,
    DateTime FromDate,
    DateTime ToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMessageStatusByAccountIDByDateRange", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fromdate", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) FromDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@todate", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) ToDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetEmailMessagesByBatchIDAndSendStatus(Guid BatchID, int SendStatus)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetEmailMessagesByBatchIDAndSendStatus", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@batchid", OleDbType.Guid);
    oleDbParameter1.Value = (object) BatchID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@sendstatus", OleDbType.Integer);
    oleDbParameter2.Value = (object) SendStatus;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetSampleOfIPNTransactionsByAccountIDAndTransactionType(
    int AccountID,
    string IPNTransactionType)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetSampleOfIPNTransactionsByAccountIDAndTransactionType", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@ipn_transactiontype", OleDbType.VarChar);
    oleDbParameter2.Value = (object) $"%{IPNTransactionType}%";
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetIPNTransactionDetailsByAccountIDByDateRange(
    int AccountID,
    DateTime FromDate,
    DateTime ToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetIPNTransactionDetailsByAccountIDByDateRange", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fromdate", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) FromDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@todate", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) ToDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetIPNAndHistoryTransactionDetailsByAccountIDByDateRange(
    int AccountID,
    DateTime FromDate,
    DateTime ToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetIPNAndHistoryTransactionDetailsByAccountIDByDateRange", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fromdate", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) FromDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@todate", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) ToDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetIPNAndHistoryTransactionDetailsByAccountIDByDateRange2(
    int AccountID,
    DateTime FromDate,
    DateTime ToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetIPNAndHistoryTransactionDetailsByAccountIDByDateRange2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fromdate", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) FromDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@todate", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) ToDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetMessageStatusByAccountIDByDateRangeForExpress(
    int AccountID,
    DateTime FromDate,
    DateTime ToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMessageStatusByAccountIDByDateRangeForExpress", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fromdate", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) FromDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@todate", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) ToDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetMessageStatusByParentAccountIDByDateRange(
    int ParentAccountID,
    DateTime FromDate,
    DateTime ToDate)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetMessageStatusByParentAccountIDByDateRange", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter1.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@fromdate", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) FromDate;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@todate", OleDbType.DBTimeStamp);
    oleDbParameter3.Value = (object) ToDate;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAutoSaveData(int FYIMetaID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAutoSaveData", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@fyimetaid", OleDbType.Integer);
    oleDbParameter.Value = (object) FYIMetaID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetCustomerContactDataByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetCustomerContactDataByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAppCountForAppCategoriesByAccountID(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAppCountForAppCategoriesByAccountID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@accountid", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetAppCategoryDetailsByID(int AppCategoryID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAppCategoryDetailsByID", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@AppCategoryID", OleDbType.Integer);
    oleDbParameter.Value = (object) AppCategoryID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public AccountOptions GetAccountOptions(int AccountID)
  {
    AccountOptions accountOptions = new AccountOptions();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountOptions", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@AccountID", OleDbType.Integer);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      accountOptions.FromEmailName = oleDbDataReader["EmailFromName"].ToString();
      accountOptions.IngnoreOptIn = bool.Parse(oleDbDataReader["IgnoreOptIn"].ToString());
      accountOptions.CompanyLogoUrl = oleDbDataReader["CompanyLogoUrl"].ToString();
      accountOptions.FooterText = oleDbDataReader["FooterText"].ToString();
    }
    dbConnection.Close();
    return accountOptions;
  }

  public AccountOptions GetAccountOptionsByParentAccount(int ParentAccountID)
  {
    AccountOptions optionsByParentAccount = new AccountOptions();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAccountOptionsByParentAccount", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@parentaccountid", OleDbType.Integer);
    oleDbParameter.Value = (object) ParentAccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
    if (oleDbDataReader.HasRows)
    {
      oleDbDataReader.Read();
      optionsByParentAccount.FromEmailName = oleDbDataReader["EmailFromName"].ToString();
      optionsByParentAccount.IngnoreOptIn = bool.Parse(oleDbDataReader["IgnoreOptIn"].ToString());
      optionsByParentAccount.CompanyLogoUrl = oleDbDataReader["CompanyLogoUrl"].ToString();
      optionsByParentAccount.FooterText = oleDbDataReader["FooterText"].ToString();
    }
    dbConnection.Close();
    return optionsByParentAccount;
  }

  public DataSet GetSavedFYIsByAccountAsDataSetWithAppCategoryEnabledStateAndFlowMode(int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetSavedFYIsByAccountAsDataSetWithAppCategoryEnabledStateAndFlowMode", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("accountid", (object) SqlDbType.Int);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetSavedFYIsByAccountAsDataSetWithAppCategoryEnabledStateAndFlowModeV2(
    int AccountID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetSavedFYIsByAccountAsDataSetWithAppCategoryEnabledStateAndFlowModeV2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("accountid", (object) SqlDbType.Int);
    oleDbParameter.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  private FYIData DeserializeFYIData(byte[] FYIBinaryDataArray)
  {
    FYIData fyiData = (FYIData) null;
    MemoryStream serializationStream = new MemoryStream(FYIBinaryDataArray);
    if (serializationStream != null && serializationStream.Length > 0L)
    {
      serializationStream.Position = 0L;
      fyiData = (FYIData) new BinaryFormatter().Deserialize((Stream) serializationStream);
      serializationStream.Close();
    }
    if (fyiData.Search.Clauses != null)
    {
      for (int index = 0; index < fyiData.Search.Clauses.Length; ++index)
      {
        if (fyiData.Search.Clauses[index].Value.GetType().Equals(typeof (ArrayList)))
          fyiData.Search.Clauses[index].Value = (object) FYIData.GetListItemCollectionFromArrayList((ArrayList) fyiData.Search.Clauses[index].Value);
      }
    }
    return fyiData;
  }

  private FYIDataExpress DeserializeFYIDataExpress(byte[] FYIBinaryDataArray)
  {
    FYIDataExpress fyiDataExpress = (FYIDataExpress) null;
    MemoryStream serializationStream = new MemoryStream(FYIBinaryDataArray);
    if (serializationStream != null && serializationStream.Length > 0L)
    {
      serializationStream.Position = 0L;
      fyiDataExpress = (FYIDataExpress) new BinaryFormatter().Deserialize((Stream) serializationStream);
      serializationStream.Close();
    }
    return fyiDataExpress;
  }

  private OleDbConnection GetDBConnection()
  {
    if (this.m_Connection == null)
      this.m_Connection = new OleDbConnection(ConfigurationSettings.AppSettings["SIMPLYFYI_ConnectionString"]);
    return this.m_Connection;
  }

  private MemoryStream SerializeAutoSaveData(object AutoSaveData)
  {
    MemoryStream serializationStream = new MemoryStream();
    new BinaryFormatter().Serialize((Stream) serializationStream, AutoSaveData);
    serializationStream.Position = 0L;
    return serializationStream;
  }

  private MemoryStream SerializeFYIData(
    VoiShareOLTypes.EndPointType EndPoint,
    SessionContext SearchContext,
    SessionContext SelectContext,
    object ScribeData,
    object ScheduleData)
  {
    MemoryStream serializationStream = new MemoryStream();
    new BinaryFormatter().Serialize((Stream) serializationStream, (object) new FYIData()
    {
      Search = new FYIData.SearchData(SearchContext),
      Select = new FYIData.SelectData(SelectContext),
      Scribe = new FYIData.ScribeData(EndPoint, ScribeData),
      Schedule = new FYIData.ScheduleData(EndPoint, ScheduleData)
    });
    serializationStream.Position = 0L;
    return serializationStream;
  }

  private MemoryStream SerializeFYIDataForExpress(
    VoiShareOLTypes.EndPointType EndPoint,
    object ScribeData,
    object ScheduleData)
  {
    MemoryStream serializationStream = new MemoryStream();
    FYIDataExpress graph = new FYIDataExpress();
    FYIDataExpress.ScribeData scribeData = new FYIDataExpress.ScribeData(EndPoint, ScribeData);
    graph.EndPointType = EndPoint;
    graph.Scribe = scribeData;
    FYIDataExpress.ScheduleData scheduleData = new FYIDataExpress.ScheduleData(EndPoint, ScheduleData);
    graph.Schedule = scheduleData;
    new BinaryFormatter().Serialize((Stream) serializationStream, (object) graph);
    serializationStream.Position = 0L;
    return serializationStream;
  }

  private MemoryStream SerializeFYIXMLMessageBody(string XmlMessageBody)
  {
    MemoryStream serializationStream = new MemoryStream();
    new BinaryFormatter().Serialize((Stream) serializationStream, (object) XmlMessageBody);
    serializationStream.Position = 0L;
    return serializationStream;
  }

  public string DeserializeFYIXMLMessageBody(byte[] FYIXMLMessageBodyBinaryDataArray)
  {
    string str = (string) null;
    MemoryStream serializationStream = new MemoryStream(FYIXMLMessageBodyBinaryDataArray);
    if (serializationStream != null && serializationStream.Length > 0L)
    {
      serializationStream.Position = 0L;
      str = (string) new BinaryFormatter().Deserialize((Stream) serializationStream);
      serializationStream.Close();
    }
    return str;
  }

  public DataSet GetAutoResponse(int MetaID, DateTime TheTimeNow)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetAutoResponse", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@metaid", OleDbType.Integer);
    oleDbParameter1.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@now", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) TheTimeNow;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public DataSet GetLastAutoResponseLog(int AutoResponseID)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spGetLastAutoResponseLog", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter = new OleDbParameter("@AutoResponseID", OleDbType.Integer);
    oleDbParameter.Value = (object) AutoResponseID;
    oleDbCommand.Parameters.Add(oleDbParameter);
    dbConnection.Open();
    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
    oleDbDataAdapter.SelectCommand = oleDbCommand;
    DataSet dataSet = new DataSet();
    oleDbDataAdapter.Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public Guid WriteAutoResponseLogToDB(
    int AutoResponseID,
    DateTime LogDateTime,
    int ProcessingStatus,
    int SequenceID,
    int TotalItems)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("spWriteAutoResponseLog", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@autoresponseid", OleDbType.Integer);
    oleDbParameter1.Value = (object) AutoResponseID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@datetime", OleDbType.DBTimeStamp);
    oleDbParameter2.Value = (object) LogDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@processingstatus", OleDbType.Integer);
    oleDbParameter3.Value = (object) ProcessingStatus;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@sequenceid", OleDbType.Integer);
    oleDbParameter4.Value = (object) SequenceID;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@totalitems", OleDbType.Integer);
    oleDbParameter5.Value = (object) TotalItems;
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@batchid", OleDbType.Guid);
    Guid db = Guid.NewGuid();
    oleDbParameter6.Value = (object) db;
    oleDbCommand.Parameters.Add(oleDbParameter6);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
    return db;
  }

  public DataSet GetUsageInventoryAllFieldsByAccountID(int AccountID)
  {
    DataSet dataSet = new DataSet();
    string cmdText = "select * from usageinventory where accountid = " + AccountID.ToString();
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand selectCommand = new OleDbCommand(cmdText, dbConnection);
    selectCommand.CommandType = CommandType.Text;
    selectCommand.Connection = dbConnection;
    dbConnection.Open();
    new OleDbDataAdapter(selectCommand).Fill(dataSet);
    dbConnection.Close();
    return dataSet;
  }

  public void WriteUnitsPurchaseIPNResponse(
    DateTime IPNDateTime,
    string RequestString,
    string ResponseString)
  {
    OleDbConnection dbConnection = this.GetDBConnection();
    OleDbCommand oleDbCommand = new OleDbCommand("dbo.spWriteIPNResponse", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@ipndatetime", OleDbType.DBTimeStamp);
    oleDbParameter1.Value = (object) IPNDateTime;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@request", OleDbType.VarChar);
    oleDbParameter2.Value = (object) RequestString;
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@response", OleDbType.VarChar);
    oleDbParameter3.Value = (object) ResponseString;
    oleDbCommand.Parameters.Add(oleDbParameter3);
    dbConnection.Open();
    oleDbCommand.ExecuteNonQuery();
    dbConnection.Close();
  }

  public int UpdateFYIMessageActualSendDateTimePXIDAndStatusByFYIMessageD(
    int FYIMessageID,
    string ActualSendDate,
    string ActualSendTime,
    string PXID,
    int StatusCode)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageActualSendDateTimePXIDAndStatusByFYIMessageD", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@fyimessageid", OleDbType.Integer);
      oleDbParameter1.Value = (object) FYIMessageID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@actualsenddate", OleDbType.VarChar);
      oleDbParameter2.Value = (object) ActualSendDate;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@actualsendtime", OleDbType.VarChar);
      oleDbParameter3.Value = (object) ActualSendTime;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      OleDbParameter oleDbParameter4 = new OleDbParameter("@pxid", OleDbType.VarChar);
      oleDbParameter4.Value = (object) PXID;
      oleDbCommand.Parameters.Add(oleDbParameter4);
      OleDbParameter oleDbParameter5 = new OleDbParameter("@statuscode", OleDbType.Integer);
      oleDbParameter5.Value = (object) StatusCode;
      oleDbCommand.Parameters.Add(oleDbParameter5);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  public int UpdateFYIMessageActualSendDateTimeAndStatusByBatchID(
    Guid BatchID,
    string ActualSendDate,
    string ActualSendTime,
    string PXID,
    int StatusCode)
  {
    int num = 0;
    try
    {
      OleDbConnection dbConnection = this.GetDBConnection();
      OleDbCommand oleDbCommand = new OleDbCommand("spUpdateFYIMessageActualSendDateTimePXIDAndStatusByBatchID", dbConnection);
      oleDbCommand.CommandType = CommandType.StoredProcedure;
      OleDbParameter oleDbParameter1 = new OleDbParameter("@batchid", OleDbType.Guid);
      oleDbParameter1.Value = (object) BatchID;
      oleDbCommand.Parameters.Add(oleDbParameter1);
      OleDbParameter oleDbParameter2 = new OleDbParameter("@actualsenddate", OleDbType.VarChar);
      oleDbParameter2.Value = (object) ActualSendDate;
      oleDbCommand.Parameters.Add(oleDbParameter2);
      OleDbParameter oleDbParameter3 = new OleDbParameter("@actualsendtime", OleDbType.VarChar);
      oleDbParameter3.Value = (object) ActualSendTime;
      oleDbCommand.Parameters.Add(oleDbParameter3);
      OleDbParameter oleDbParameter4 = new OleDbParameter("@pxid", OleDbType.VarChar);
      oleDbParameter4.Value = (object) PXID;
      oleDbCommand.Parameters.Add(oleDbParameter4);
      OleDbParameter oleDbParameter5 = new OleDbParameter("@statuscode", OleDbType.Integer);
      oleDbParameter5.Value = (object) StatusCode;
      oleDbCommand.Parameters.Add(oleDbParameter5);
      if (dbConnection.State == ConnectionState.Closed)
        dbConnection.Open();
      num = oleDbCommand.ExecuteNonQuery();
      dbConnection.Close();
    }
    catch (Exception ex)
    {
      EventLog.WriteEntry("FYI", ex.Message);
    }
    return num;
  }

  private MemoryStream SerializeFYIDataForExperience(string JObjectExperience)
  {
    MemoryStream serializationStream = new MemoryStream();
    new BinaryFormatter().Serialize((Stream) serializationStream, (object) JObjectExperience);
    serializationStream.Position = 0L;
    return serializationStream;
  }

  private string DeserializeFYIDataForExperience(byte[] DataArray)
  {
    string str = string.Empty;
    MemoryStream serializationStream = new MemoryStream(DataArray);
    if (serializationStream != null && serializationStream.Length > 0L)
    {
      serializationStream.Position = 0L;
      str = (string) new BinaryFormatter().Deserialize((Stream) serializationStream);
      serializationStream.Close();
    }
    return str;
  }

  public int WriteFYIDataToDB_V2(
    int AccountID,
    int MetaID,
    string MetaDataName,
    VoiShareOLTypes.EndPointType EndPoint,
    string JObjectExperienceData,
    VoiShareOLTypes.ScheduleType Schedule,
    string ScheduledTime,
    string Version)
  {
    ScheduledTime = DateTime.Parse(ScheduledTime).ToString("HH:mm");
    OleDbConnection dbConnection = this.GetDBConnection();
    MemoryStream memoryStream = this.SerializeFYIDataForExperience(JObjectExperienceData);
    byte[] buffer = new byte[Convert.ToInt32(memoryStream.Length)];
    memoryStream.Read(buffer, 0, Convert.ToInt32(memoryStream.Length));
    memoryStream.Close();
    OleDbCommand oleDbCommand = new OleDbCommand("spUpsertFYIMetaMessage2", dbConnection);
    oleDbCommand.CommandType = CommandType.StoredProcedure;
    OleDbParameter oleDbParameter1 = new OleDbParameter("@FYIAccountID", OleDbType.Integer);
    oleDbParameter1.Value = (object) AccountID;
    oleDbCommand.Parameters.Add(oleDbParameter1);
    OleDbParameter oleDbParameter2 = new OleDbParameter("@DateCreated", OleDbType.Date);
    oleDbParameter2.Value = (object) DateTime.Now.ToUniversalTime();
    oleDbCommand.Parameters.Add(oleDbParameter2);
    OleDbParameter oleDbParameter3 = new OleDbParameter("@Active", OleDbType.Char, 1);
    oleDbParameter3.Value = (object) 'Y';
    oleDbCommand.Parameters.Add(oleDbParameter3);
    OleDbParameter oleDbParameter4 = new OleDbParameter("@Data", OleDbType.LongVarBinary, buffer.Length);
    oleDbParameter4.Value = (object) buffer;
    oleDbCommand.Parameters.Add(oleDbParameter4);
    OleDbParameter oleDbParameter5 = new OleDbParameter("@EndPointType", OleDbType.Integer);
    oleDbParameter5.Value = (object) Convert.ToInt32((object) EndPoint);
    oleDbCommand.Parameters.Add(oleDbParameter5);
    OleDbParameter oleDbParameter6 = new OleDbParameter("@ScheduleType", OleDbType.Integer);
    oleDbParameter6.Value = (object) Convert.ToInt32((object) Schedule);
    oleDbCommand.Parameters.Add(oleDbParameter6);
    OleDbParameter oleDbParameter7 = new OleDbParameter("@ScheduledTime", OleDbType.VarChar);
    oleDbParameter7.Value = (object) ScheduledTime;
    oleDbCommand.Parameters.Add(oleDbParameter7);
    OleDbParameter oleDbParameter8 = new OleDbParameter("@MetaDataName", OleDbType.VarChar);
    oleDbParameter8.Value = (object) MetaDataName;
    oleDbCommand.Parameters.Add(oleDbParameter8);
    OleDbParameter oleDbParameter9 = new OleDbParameter("@MetaID", OleDbType.Integer);
    oleDbParameter9.Value = (object) MetaID;
    oleDbCommand.Parameters.Add(oleDbParameter9);
    OleDbParameter oleDbParameter10 = new OleDbParameter("@Version", OleDbType.VarChar);
    oleDbParameter10.Value = (object) Version;
    oleDbCommand.Parameters.Add(oleDbParameter10);
    dbConnection.Open();
    if (oleDbCommand.ExecuteNonQuery() > 0)
      MetaID = (int) new OleDbCommand($"{$"{"SELECT MetaID from FYIMETAMESSAGES WHERE FYIAccountID=" + (object) AccountID} AND DateCreated='{oleDbParameter2.Value}'"} OR DateCreated='{DateTime.Parse(oleDbParameter2.Value.ToString()).AddSeconds(1.0).ToString()}'", dbConnection).ExecuteScalar();
    dbConnection.Close();
    return MetaID;
  }
}
