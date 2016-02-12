using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Model.General
{
    public class EmailBDto
    {
        private String _to;
        private String _from;
        private String m_DisplayName;
        private String _subject = String.Empty;
        private String _body = String.Empty;
        private String _bodyEncoding = String.Empty;
        private String _attachmentFileUrl = String.Empty;
        private Boolean _isBodyHtml = false;
        private List<System.Net.Mail.Attachment> _Attachment;

        public String To
        {
            get { return _to; }
            set { _to = value; }
        }

        public String From
        {
            get { return _from; }
            set { _from = value; }
        }

        public String CC { get; set; }
        public String Bcc { get; set; }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public string BodyEncoding
        {
            get { return _bodyEncoding; }
            set { _bodyEncoding = value; }
        }

        public string AttachmentFileUrl
        {
            get { return _attachmentFileUrl; }
            set { _attachmentFileUrl = value; }
        }

        public String DisplayName
        {
            get { return m_DisplayName; }
            set { m_DisplayName = value; }
        }

        public List<System.Net.Mail.Attachment> Attachments
        {
            get { return _Attachment; }
        }

        //public System.Net.Mail.AttachmentCollection AttachmentCollection { get; set; }

        //public System.Net.Mail.AttachmentCollection AttachmentCollection {
        //    get { return _AttachmentCollection; }
        //    set { _AttachmentCollection = value; }
        
        //}
    }
}
