using Groupdocs.Data;
using System.Collections.Generic;
namespace MVCMultipleSigners.Models
{
    public class SignDocument
    {
        public string DocumentGuid { get; set; }

        public string RecipientGuid { get; set; }

        public ICollection<SignatureDocumentRecipient> Recipients { get; set; }
    }
}