using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace mail_to_plesanter
{
    class MailConst
    {
        private static MailConst _singleInstance = new MailConst();
        private static MailConst GetInstance()
        {
            return _singleInstance;
        }

        private readonly string _C_IMAP_HOST;
        private readonly int _C_IMAP_PORT;
        private readonly string _C_ACCOUNT_USER;
        private readonly string _C_ACCOUNT_PASS;
        private readonly string _DB_FILE;
        private readonly string _PLEASANTER_API_KEY;
        private readonly string _PLEASANTER_SERVER_URL;
        private readonly string _PLEASANTER_SITE_ID;
        private readonly string _SB_SITE_ID;
        private readonly string _SB_MAIL_SITE_ID;
        private readonly bool _MAIL_DONE_DELETE;

        private MailConst()
        {
            try
            {

                // iniファイル読み込み
                var mail_setting = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("mail.json").Build();

                this._C_IMAP_HOST = mail_setting["C_IMAP_HOST"];
                this._C_IMAP_PORT = int.Parse(mail_setting["C_IMAP_PORT"]);
                this._C_ACCOUNT_USER = mail_setting["C_ACCOUNT_USER"];
                this._C_ACCOUNT_PASS = mail_setting["C_ACCOUNT_PASS"];
                this._DB_FILE = mail_setting["DB_FILE"];
                this._PLEASANTER_API_KEY = mail_setting["PLEASANTER_API_KEY"];
                this._PLEASANTER_SERVER_URL = mail_setting["PLEASANTER_SERVER_URL"];
                this._PLEASANTER_SITE_ID = mail_setting["PLEASANTER_SITE_ID"];
                this._SB_SITE_ID = mail_setting["SB_SITE_ID"];
                this._SB_MAIL_SITE_ID = mail_setting["SB_MAIL_SITE_ID"];
                this._MAIL_DONE_DELETE = mail_setting["MAIL_DONE_DELETE"].ToLower() == "true" ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Logger.Error(ex.Message);
                Serilog.Log.Logger.Error(ex.StackTrace);
            }
        }

        public static string C_IMAP_HOST { get { return GetInstance()._C_IMAP_HOST; } }
        public static int C_IMAP_PORT { get { return GetInstance()._C_IMAP_PORT; } }
        public static string C_ACCOUNT_USER { get { return GetInstance()._C_ACCOUNT_USER; } }
        public static string C_ACCOUNT_PASS { get { return GetInstance()._C_ACCOUNT_PASS; } }
        public static string DB_FILE { get { return GetInstance()._DB_FILE; } }
        public static string PLEASANTER_API_KEY { get { return GetInstance()._PLEASANTER_API_KEY; } }
        public static string PLEASANTER_SERVER_URL { get { return GetInstance()._PLEASANTER_SERVER_URL; } }
        public static string PLEASANTER_SITE_ID { get { return GetInstance()._PLEASANTER_SITE_ID; } }
        public static bool MAIL_DONE_DELETE { get { return GetInstance()._MAIL_DONE_DELETE; } }

    }
}
