using System;
using System.Collections.Generic;
using System.Text;

// メールキット
using MimeKit;
using MailKit;
using MailKit.Search;
using MailKit.Net.Imap;
using MailKit.Security;

using Microsoft.Data.Sqlite;
using System.IO;

namespace mail_to_plesanter
{
    class Mail
    {

        /// <summary>
        /// メールを取得して、メール内容を引数のActionに渡す
        /// </summary>
        /// <param name="action">メール内容を元に行う処理</param>
        static public void IMAP_SEARCH(Action<ImapClient, MailRecode> action)
        {

            using (var imap4 = new ImapClient())
            {

                imap4.Connect(MailConst.C_IMAP_HOST, MailConst.C_IMAP_PORT, SecureSocketOptions.SslOnConnect);

                // 認証
                imap4.Authenticate(MailConst.C_ACCOUNT_USER, MailConst.C_ACCOUNT_PASS);

                // ReadOnly で Inbox を開きます
                imap4.Inbox.Open(FolderAccess.ReadWrite);

                // 1日分のメール取得
                var uids = imap4.Inbox.Search(
                    SearchQuery.DeliveredAfter(DateTime.Now.AddDays(-1))
                    );

                //メール毎に処理
                foreach (var uid in uids)
                {
                    var mailObj = new MailRecode();
                    var message = imap4.Inbox.GetMessage(uid);
                    mailObj.ToMailAddress = string.Join(", ", message.To);
                    mailObj.CcMailAddress = string.Join(", ", message.Cc);
                    mailObj.FromMailAddress = string.Join(", ", message.From);
                    mailObj.ResentFromMailAddress = string.Join(", ", message.ResentFrom);
                    mailObj.SendDate = String.Format("{0:yyyy-MM-dd H:mm:ss}", message.Date);
                    mailObj.Subject = message.Subject.ToString();
                    mailObj.Body = message.TextBody.ToString();
                    mailObj.Attachments = message.Attachments;

                    //デリゲート実行
                    action(imap4, mailObj);

                    if (MailConst.MAIL_DONE_DELETE)
                    {
                        imap4.Inbox.AddFlags(uid, MessageFlags.Deleted, true);
                        // 削除
                        if (imap4.Capabilities.HasFlag(ImapCapabilities.UidPlus))
                        {
                            imap4.Inbox.Expunge(uids);
                        }

                    }
                }

                imap4.Disconnect(true);
            }
        }

    }
}
