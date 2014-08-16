using System.Web.Mvc;
using Groupdocs.Data.Signature;
using Groupdocs.Web.UI.Signature;
using Groupdocs.Web.UI.Signature.Services;
using MVCMultipleSigners.Models;
using Groupdocs.Data;

namespace MVCMultipleSigners.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var docGuid = Request.QueryString["docGuid"];
            string recGuid = Request.QueryString["recGuid"];

            SignDocument signDocument;
            if (!string.IsNullOrEmpty(docGuid) && !string.IsNullOrEmpty(recGuid))
            {
                signDocument = new SignDocument { DocumentGuid = docGuid, RecipientGuid = recGuid };
                return View(signDocument);
            }

            SignatureDocument document;
            if (!string.IsNullOrEmpty(docGuid))
            {
                document = GroupdocsSignature.GetDocument(docGuid);
                foreach(SignatureDocumentRecipient recip in document.SignatureDocumentRecipients)
                {
                    recGuid = recip.Guid;
                    if (!recip.Signed) break;
                }
                signDocument = new SignDocument {
                    DocumentGuid = docGuid,
                    RecipientGuid = recGuid, // to show the doc, because it remembers last signed recipient ID
                                             // and show popup warning to wait all signers
                    Recipients = document.SignatureDocumentRecipients };
                return View(signDocument);
            }

            document = GroupdocsSignature.CreateDocumentForSigning("sample.pdf", "sample_signed.pdf");

            var recipient = document.AddRecipient("John", "Doe");
            var recipient1 = document.AddRecipient("Richard", "Roe");

            var field = document.AddField(SignatureFieldType.Signature, true, "Signature1");
            field.AddLocation(1, (decimal)421.44, (decimal)120.4, (decimal)0.591397849462366, (decimal)0.800456100342075, false, false, false, null, 0,
                null);
            document.AssignFieldToRecipient(field.Guid, recipient.Guid);

            var field1 = document.AddField(SignatureFieldType.Signature, true, "Signature2");
            field1.AddLocation(1, (decimal)421.44, (decimal)120.4, (decimal)0.095397849462366, (decimal)0.800456100342075, false, false, false, null, 0,
                null);
            document.AssignFieldToRecipient(field1.Guid, recipient1.Guid);

            signDocument = new SignDocument
            {
                DocumentGuid = document.Guid,
                RecipientGuid = recipient.Guid,
                Recipients = document.SignatureDocumentRecipients
            };
            return View(signDocument);
        }

    }
}
