using FirebaseAdmin;
using FirebaseAdmin.Messaging;

using Google.Apis.Auth.OAuth2;

namespace FcmBase;
/// <summary>
/// firebase core
/// </summary>
/// <see href="https://firebase.google.com/docs/admin/setup#c" />
public class FcmCore
{
    // 幾種建立方法

    /// <summary>
    /// 指定檔案位置
    /// </summary>
    /// <param name="path">檔案路徑</param>
    public FcmCore(string path)
    {
        FirebaseApp.Create(path);
    }

    /// <summary>
    /// 使用提供的選項建立
    /// </summary>
    /// <param name="options"></param>
    public FcmCore(AppOptions options)
    {
        FirebaseApp.Create(options);

        // 例如..
        var _options = new AppOptions()
        {
            Credential = GoogleCredential.FromFile("...檔案位置"),
        };
    }

    /// <summary>
    /// 使用預設值
    /// 會使用 GoogleCredential.GetApplicationDefaultAsync
    /// 到系統的環境變數去取得檔案路徑「GOOGLE_APPLICATION_CREDENTIALS」
    /// Windows的話到開始點選又鍵 => 進階系統設定 => 「進階」標籤下的「環境變數(N)」設定
    /// 可以使用工具查看變數設定
    /// CMD -> echo %GOOGLE_APPLICATION_CREDENTIALS%
    /// PowerShellCore -> echo $env:GOOGLE_APPLICATION_CREDENTIALS
    /// </summary>
    /// <see cref="GoogleCredential.GetApplicationDefaultAsync" />
    public FcmCore()
    {
        FirebaseApp.Create();
    }

    /// <summary>
    /// 發送推播訊息
    /// </summary>
    /// <param name="msg">Firebase提供的訊息格式Model</param>
    /// <returns></returns>
    public Task SendMessageAsync(Message msg)
    {
        // FirebaseAdmin套件採用Singleton的方式
        // 只要使用FirebaseAppCreate過後可以從FirebaseMessaging.DefaultInstance取得內容
        return FirebaseMessaging.DefaultInstance.SendAsync(msg);
    }

    /// <summary>
    /// 發送推播訊息 (同步寫法，可選擇是否使用布林值查看是否成功)
    /// </summary>
    /// <param name="msg">Firebase提供的訊息格式Model</param>
    /// <returns></returns>
    public bool SendMessage(Message msg)
    {
        try
        {
            string response = FirebaseMessaging.DefaultInstance.SendAsync(msg).Result;
            return !string.IsNullOrEmpty(response);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 發送自訂推播訊息
    /// </summary>
    /// <param name="token">使用者token</param>
    /// <param name="message">自訂訊息內容</param>
    /// <returns></returns>
    public Task SendMessageAsync(string token, MyMessage message)
    {
        var msg = new Message() {
            Token = token,
            // 訊息類型
            Notification = new Notification() {
                Title = message.Title,
                Body = message.Message,
            },

            // 資料類型
            Data = new Dictionary<string, string>()
        };
        return FirebaseMessaging.DefaultInstance.SendAsync(msg);
    }

    /// <summary>
    /// 發送自訂推播訊息給多個對象
    /// </summary>
    /// <param name="token">使用者token</param>
    /// <param name="message">自訂訊息內容</param>
    /// <returns></returns>
    public Task SendMessageAsync(string[] tokens, MyMessage message)
    {
        var msg = new MulticastMessage() {
            Tokens = tokens,
            // 訊息類型
            Notification = new Notification() {
                Title = message.Title,
                Body = message.Message,
            },

            // 資料類型
            Data = new Dictionary<string, string>()
        };
        return FirebaseMessaging.DefaultInstance.SendMulticastAsync(msg);
    }
}

public class MyMessage
{
    public MyMessage(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
}