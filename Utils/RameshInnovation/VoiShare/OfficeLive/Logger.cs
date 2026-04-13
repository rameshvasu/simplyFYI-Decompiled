// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.OfficeLive.Logger
// Assembly: Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E78752C-B53F-47A3-9F8C-1D1416E9B663
// Assembly location: C:\Users\rames\OneDrive\Desktop\PayPalIPNHandler\Utils.dll

using System;
using System.Configuration;
using System.IO;

#nullable disable
namespace RameshInnovation.VoiShare.OfficeLive;

public class Logger
{
  public static void Write(string Message)
  {
    string newValue = DateTime.Now.ToString("MMddyyyy");
    string path = ConfigurationManager.AppSettings.Get("LogFile").Replace("MMDDYYYY", newValue);
    StreamWriter streamWriter = new StreamWriter(File.Exists(path) ? (Stream) File.Open(path, FileMode.Append, FileAccess.Write) : (Stream) File.Create(path));
    streamWriter.WriteLine($"[{DateTime.Now.ToString()}] {Message}");
    streamWriter.Close();
  }
}
