using System;
using Serilog;
using Serilog.Events;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using MailKit.Net.Imap;


namespace mail_to_plesanter
{
    class Program
    {
        private static HttpClient http;
        private static string TempPath = "TEMP";
        static void Main(string[] args)
        {

            //エンコーディングおまじない
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //ログ出力おまじない
            SetupLoggerConfig();

            try
            {
                //添付ファイル保存場所の確認
                if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);

                //httpクライアント
                http = new HttpClient();

                Database db = new Database();

                using (var tran = db.BeginTransaction())
                {

                    //メールを受信
                    //メールを受信
                    Mail.IMAP_SEARCH((ImapClient imap4, MailRecode mail) =>
                    {

                        //同じ件名のレコードが存在する場合は処理しない
                        if (db.MainDataIsExistsBySubject(mail.Subject))
                        {
                            return;
                        }

                        //存在しない場合INSERT
                        long id = db.InsertMainData(mail);

                        foreach (var attachment in mail.Attachments)
                        {
                            string idpath = System.IO.Path.Combine(TempPath, id.ToString());
                            if (!Directory.Exists(idpath)) Directory.CreateDirectory(idpath);

                            var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;

                            byte[] filenamebyte = System.Text.Encoding.GetEncoding("iso-2022-jp").GetBytes(fileName);
                            string filenamestring = Encoding.GetEncoding("iso-2022-jp").GetString(filenamebyte);

                            string attachtemppath = System.IO.Path.Combine(idpath, filenamestring);
                            using (var stream = File.Create(attachtemppath))
                            {
                                if (attachment is MessagePart)
                                {
                                    var rfc822 = (MessagePart)attachment;

                                    rfc822.Message.WriteTo(stream);
                                }
                                else
                                {
                                    var part = (MimePart)attachment;

                                    part.Content.DecodeTo(stream);
                                }
                            }

                        }

                    });



                    tran.Commit();
                }

                SubMain(db);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
            }
        }


        static private void SubMain(Database db)
        {
            var list = db.GetMainDataByDoneflgIsNull();
            foreach (var mail in list)
            {
                {
                    string mailDataKey = MailToPleasanterSave(mail);

                    db.UpdateDoneflg(mail.id, mailDataKey);
                }
            }
        }

        static private string MailToPleasanterSave(MainRecord mail)
        {

            Dictionary<string, object> dc = new Dictionary<string, object>();
            dc["ApiVersion"] = 1.1;
            dc["ApiKey"] = MailConst.PLEASANTER_API_KEY;
            dc["Title"] = mail.subject;
            dc["Body"] = mail.body;
            dc["CompletionTime"] = DateTime.Now.ToString("yyyy/MM/dd");
            var dateHash = new Dictionary<string, object>();
            dateHash["DateA"] = mail.date;
            dc["DateHash"] = dateHash;
            var AttachmentsHash = new Dictionary<string, object>();
            var classHash = new Dictionary<string, Object>();
            classHash["ClassA"] = mail.frommail;
            classHash["ClassB"] = mail.tomail;
            classHash["ClassC"] = mail.ccmail;
            classHash["ClassD"] = mail.resentfrommail;
            dc["ClassHash"] = classHash;

            string idpath = System.IO.Path.Combine(TempPath, mail.id.ToString());
            if (Directory.Exists(idpath))
            {
                //添付ファイルは一つの項目に10個までしか添付できない為、A～Jを使って最大１００個まで添付できるようにする
                var AttachmentsAList = new List<Dictionary<string, object>>();
                var AttachmentsBList = new List<Dictionary<string, object>>();
                var AttachmentsCList = new List<Dictionary<string, object>>();
                var AttachmentsDList = new List<Dictionary<string, object>>();
                var AttachmentsEList = new List<Dictionary<string, object>>();
                var AttachmentsFList = new List<Dictionary<string, object>>();
                var AttachmentsGList = new List<Dictionary<string, object>>();
                var AttachmentsHList = new List<Dictionary<string, object>>();
                var AttachmentsIList = new List<Dictionary<string, object>>();
                var AttachmentsJList = new List<Dictionary<string, object>>();
                foreach (var filename in Directory.GetFiles(idpath))
                {
                    if (AttachmentsHash.Count < 10) { SetAttachments(ref AttachmentsAList, filename); }
                    else if (10 <= AttachmentsHash.Count && AttachmentsHash.Count < 20) { SetAttachments(ref AttachmentsBList, filename); }
                    else if (20 <= AttachmentsHash.Count && AttachmentsHash.Count < 30) { SetAttachments(ref AttachmentsCList, filename); }
                    else if (30 <= AttachmentsHash.Count && AttachmentsHash.Count < 40) { SetAttachments(ref AttachmentsDList, filename); }
                    else if (40 <= AttachmentsHash.Count && AttachmentsHash.Count < 50) { SetAttachments(ref AttachmentsEList, filename); }
                    else if (50 <= AttachmentsHash.Count && AttachmentsHash.Count < 60) { SetAttachments(ref AttachmentsFList, filename); }
                    else if (60 <= AttachmentsHash.Count && AttachmentsHash.Count < 70) { SetAttachments(ref AttachmentsGList, filename); }
                    else if (70 <= AttachmentsHash.Count && AttachmentsHash.Count < 80) { SetAttachments(ref AttachmentsHList, filename); }
                    else if (80 <= AttachmentsHash.Count && AttachmentsHash.Count < 90) { SetAttachments(ref AttachmentsIList, filename); }
                    else if (90 <= AttachmentsHash.Count && AttachmentsHash.Count < 100) { SetAttachments(ref AttachmentsJList, filename); }
                    Log.Logger.Information(filename);
                }

                if (AttachmentsAList.Count > 0) AttachmentsHash["AttachmentsA"] = AttachmentsAList;
                if (AttachmentsBList.Count > 0) AttachmentsHash["AttachmentsB"] = AttachmentsBList;
                if (AttachmentsCList.Count > 0) AttachmentsHash["AttachmentsC"] = AttachmentsCList;
                if (AttachmentsDList.Count > 0) AttachmentsHash["AttachmentsD"] = AttachmentsDList;
                if (AttachmentsEList.Count > 0) AttachmentsHash["AttachmentsE"] = AttachmentsEList;
                if (AttachmentsFList.Count > 0) AttachmentsHash["AttachmentsF"] = AttachmentsFList;
                if (AttachmentsGList.Count > 0) AttachmentsHash["AttachmentsG"] = AttachmentsGList;
                if (AttachmentsHList.Count > 0) AttachmentsHash["AttachmentsH"] = AttachmentsHList;
                if (AttachmentsIList.Count > 0) AttachmentsHash["AttachmentsI"] = AttachmentsIList;
                if (AttachmentsJList.Count > 0) AttachmentsHash["AttachmentsJ"] = AttachmentsJList;

            }

            if (AttachmentsHash.Count > 0)
            {
                dc["AttachmentsHash"] = AttachmentsHash;
            }

            //jsonに変換する
            string json = JsonSerializer.Serialize(dc);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var apiURL = String.Format("{0}/api/items/{1}/create", MailConst.PLEASANTER_SERVER_URL, MailConst.PLEASANTER_SITE_ID);
            var response = http.PostAsync(apiURL, content).Result;
            var responseContentString = response?.Content?.ReadAsStringAsync().Result;
            var responseContent = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContentString);
            return responseContent["Id"].ToString();

        }

        private static void SetAttachments(ref List<Dictionary<string, object>> AttachmentsList, string filename)
        {

            var fileinfo = new FileInfo(filename);
            var AttachmentsA = new Dictionary<string, object>();

            var provider = new FileExtensionContentTypeProvider();

            provider.TryGetContentType(filename, out string contentType);
            AttachmentsA["ContentType"] = contentType;
            AttachmentsA["Name"] = fileinfo.Name;
            AttachmentsA["Base64"] = Convert.ToBase64String(File.ReadAllBytes(filename));
            AttachmentsList.Add(AttachmentsA);
        }

        private static void SetupLoggerConfig()
        {
            string template = " |{Timestamp:yyyy/MM/dd HH:mm:ss.fff} | {Level:u4} | {Message:j} | {NewLine}{Exception}";
            string logFilePathHead = @"logs\";

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Verbose()
                            //.WriteTo.Console(outputTemplate: template)
                            //.WriteTo.Debug(outputTemplate: template)
                            .WriteTo.File($"{logFilePathHead}.txt", LogEventLevel.Information, outputTemplate: template, rollingInterval: RollingInterval.Day)
                            .CreateLogger();
        }
    }
}
