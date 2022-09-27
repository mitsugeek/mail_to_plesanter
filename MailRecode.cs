using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;

namespace mail_to_plesanter
{
    class MailRecode
    {
        public string ToMailAddress;
        public string CcMailAddress;
        public string FromMailAddress;
        public string ResentFromMailAddress;
        public string SendDate;
        public string Subject;
        public string Body;
        public IEnumerable<MimeEntity> Attachments = null;
    }
}
