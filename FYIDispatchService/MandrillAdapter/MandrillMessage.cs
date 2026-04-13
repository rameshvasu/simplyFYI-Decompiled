// Decompiled with JetBrains decompiler
// Type: RameshInnovation.VoiShare.Services.Adapters.MandrillMessage
// Assembly: MandrillAdapter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03B45742-E072-49C4-8BB0-818A9396681F
// Assembly location: C:\Users\rames\OneDrive\Desktop\simplyFYI-Decompiled-Code\simplyfyi.services\FYIDispatchService\MandrillAdapter.dll

using System.Text;

#nullable disable
namespace RameshInnovation.VoiShare.Services.Adapters;

public class MandrillMessage
{
  public _message message;

  public string key { get; set; }

  public string SerializeMandrillMessage(MandrillMessage MandrillMessage)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("{");
    stringBuilder.Append("\"key\":");
    stringBuilder.Append($"\"{MandrillMessage.key}\",");
    stringBuilder.Append("\"message\": {");
    stringBuilder.Append("\"html\":");
    stringBuilder.Append($"\"{MandrillMessage.message.html}\",");
    stringBuilder.Append("\"text\":null,");
    stringBuilder.Append("\"subject\":");
    stringBuilder.Append($"\"{MandrillMessage.message.subject}\",");
    stringBuilder.Append("\"from_email\":");
    stringBuilder.Append($"\"{MandrillMessage.message.from_email}\",");
    stringBuilder.Append("\"from_name\":");
    stringBuilder.Append($"\"{MandrillMessage.message.from_name}\",");
    stringBuilder.Append("\"to\": [");
    stringBuilder.Append("{");
    stringBuilder.Append("\"email\":");
    stringBuilder.Append($"\"{MandrillMessage.message.to[0].email}\",");
    stringBuilder.Append("\"name\":");
    stringBuilder.Append($"\"{MandrillMessage.message.to[0].name}\",");
    stringBuilder.Append("\"type\":");
    stringBuilder.Append($"\"{MandrillMessage.message.to[0].type}\"");
    stringBuilder.Append("}");
    stringBuilder.Append("],");
    stringBuilder.Append("\"important\":");
    stringBuilder.Append($"\"{MandrillMessage.message.important}\",");
    stringBuilder.Append("\"track_opens\":");
    stringBuilder.Append($"\"{MandrillMessage.message.track_opens}\",");
    stringBuilder.Append("\"track_clicks\":");
    stringBuilder.Append($"\"{MandrillMessage.message.track_clicks}\"");
    stringBuilder.Append(",\"auto_text\":true");
    stringBuilder.Append("}");
    stringBuilder.Append("}");
    return stringBuilder.ToString();
  }
}
