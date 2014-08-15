using System.Web.Mvc;
using Groupdocs.Data.Signature;
using Groupdocs.Web.UI.Signature;
using Groupdocs.Web.UI.Signature.Services;
using MVCMultipleSigners.Models;

namespace MVCMultipleSigners.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var document = GroupdocsSignature.CreateDocumentForSigning("sample.pdf", "sample_signed.pdf");

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

            var signDocument = new SignDocument {DocumentGuid = document.Guid, RecipientGuid = recipient1.Guid};
            return View(signDocument);
        }

    }
}
