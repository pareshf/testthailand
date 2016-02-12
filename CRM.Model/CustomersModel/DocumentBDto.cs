using System;

namespace CRM.Model.CustomersModel
{
    public class DocumentBDto
    {
        public Int32 SerialNo { get; set; }

        public Int32 CustomerId { get; set; }

        public String DocumentName { get; set; }

        public String DocumentDescription { get; set; }

        public DateTime DocumentDate { get; set; }

        public String DocumentFileName { get; set; }

        public Byte[] DocumentFile { get; set; }

        public String DocumentType { get; set; }

        public Boolean IsDocumentChanged { get; set; }
    }
}