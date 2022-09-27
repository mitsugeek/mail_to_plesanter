using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Data.Sqlite;
using System.IO;
using System.Data;

namespace mail_to_plesanter
{
    class Database
    {
        public SqliteConnection connection { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Database()
        {
            //dbファイルが無ければ作成
            if (!File.Exists(MailConst.DB_FILE)) File.WriteAllBytes(MailConst.DB_FILE, new byte[0]);

            //dbコネクション生成
            this.connection = new SqliteConnection("Data Source=" + MailConst.DB_FILE);

            //dbオープン
            this.connection.Open();

            //定義
            this.Init();
        }

        /// <summary>
        /// データベース初期化処理
        /// </summary>
        /// <param name="connection"></param>
        private void Init()
        {
            using (var cmd = this.connection.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS [MAIN_DATA](" +
                                  "[id] INTEGER PRIMARY KEY," +
                                  "[date] TEXT NOT NULL," +
                                  "[subject] TEXT NOT NULL," +
                                  "[body] TEXT NOT NULL," +
                                  "[tomail] TEXT NOT NULL," +
                                  "[ccmail] TEXT NOT NULL," +
                                  "[frommail] TEXT NOT NULL," +
                                  "[resentfrommail] TEXT NOT NULL," +
                                  "[doneflg] TEXT NULL," +
                                  "[key] TEXT NULL" +
                                  ")";
                cmd.ExecuteNonQuery();
            }

            using (var cmd = this.connection.CreateCommand())
            {
                cmd.CommandText = "CREATE INDEX IF NOT EXISTS [MAIN_DATA_IDX] ON [MAIN_DATA]([subject])";
                cmd.ExecuteNonQuery();
            }

        }

        public SqliteTransaction BeginTransaction()
        {
            return this.connection.BeginTransaction();
        }

        /// <summary>
        /// INSERT処理
        /// </summary>
        /// <param name="date"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public long InsertMainData(MailRecode mail)
        {

            using (var cmd = this.connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO [MAIN_DATA]([date],[subject],[body],[tomail],[ccmail],[frommail],[resentfrommail]) VALUES (@date, @subject,@body,@tomail,@ccmail,@frommail,@resentfrommail);SELECT last_insert_rowid();";
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@date", Value = mail.SendDate });
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@subject", Value = mail.Subject });
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@body", Value = mail.Body });
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@tomail", Value = mail.ToMailAddress });
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@ccmail", Value = mail.CcMailAddress });
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@frommail", Value = mail.FromMailAddress });
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@resentfrommail", Value = mail.ResentFromMailAddress });
                return (long)cmd.ExecuteScalar();
            }
        }

        public void UpdateDoneflg(long id, string doneflg)
        {
            using (var cmd = this.connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE [MAIN_DATA] SET [doneflg] = @doneflg WHERE [id] = @id";
                if (string.IsNullOrEmpty(doneflg))
                {
                    cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@doneflg", Value = DBNull.Value });
                }
                else
                {
                    cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@doneflg", Value = doneflg });
                }
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@id", Value = id });
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 存在チェック
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public bool MainDataIsExistsBySubject(string subject)
        {
            using (var cmd = this.connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM [MAIN_DATA] WHERE subject = @subject";
                cmd.Parameters.Add(new Microsoft.Data.Sqlite.SqliteParameter() { ParameterName = "@subject", Value = subject });
                var reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
        }

        /// <summary>
        /// 未処理のメールデータ取得
        /// </summary>
        /// <returns></returns>
        public List<MainRecord> GetMainDataByDoneflgIsNull()
        {
            List<MainRecord> result = new List<MainRecord>();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT id, date, subject, body, doneflg, tomail,ccmail,frommail,resentfrommail FROM [MAIN_DATA] WHERE doneflg IS NULL";
                var reader = cmd.ExecuteReader();
                while (reader.Read() == true)
                {
                    MainRecord recode = new MainRecord();
                    recode.id = reader.GetInt64("id");
                    recode.subject = reader.GetString("subject");
                    recode.body = reader.GetString("body");
                    recode.date = reader.GetString("date");
                    recode.doneflg = reader.IsDBNull("doneflg") ? null : reader.GetString("doneflg");

                    recode.tomail = reader.GetString("tomail");
                    recode.ccmail = reader.GetString("ccmail");
                    recode.frommail = reader.GetString("frommail");
                    recode.resentfrommail = reader.GetString("resentfrommail");


                    result.Add(recode);
                }
            }
            return result;
        }

    }
}
