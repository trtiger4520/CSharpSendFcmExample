string token = "";
var fcm = new FcmBase.FcmCore();

Console.WriteLine("發送推播訊息...");

try
{
    await fcm.SendMessageAsync(token, new FcmBase.MyMessage(
        title: "測試喔",
        message: "測試內容測試內容測試內容"
    ));

    Console.WriteLine("發送成功");
}
catch (Exception ex)
{
    Console.WriteLine($"發送失敗:{ex.Message}");
}

Console.WriteLine("推播訊息發送結束");
