string token = "fcm-push-token=feqgGptKTmWHwpNaQ3tn5l:APA91bGUZsmugkPDfG0aBJ05UheTUEkoX8O9L1UDcD9N3lA6wWvWASc8ZXAW_lK4s4f4ltlcQWQGFA_ffLlMJCIRUiO6up91x6BOakXbgoID7Zg-FezBpzs8tBzlxxFox50f-SxpECck";
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
