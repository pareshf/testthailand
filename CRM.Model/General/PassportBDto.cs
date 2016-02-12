using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Model.General
{
    public class PassportBDto
    {

        /// <summary>
        /// Gets or sets a value indicating PassportNo.
        /// </summary>
        public String PassportNo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating PassportIssueDate.
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating IssuePlace.
        /// </summary>
        public String IssuePlace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating EntryDate.
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating PrintName.
        /// </summary>
        public String PrintName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating IssueCountry.
        /// </summary>
        public String IssueCountry { get; set; }
    }
}
