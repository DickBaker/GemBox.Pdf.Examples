using GemBox.Pdf;
using GemBox.Pdf.Forms;
using GemBox.Pdf.Security;

namespace DigitalSignaturePAdES;

static class Program
{
    static void Main()
    {
        Example1();
        Example2();
        Example3();
        Example4();
    }

    static void Example1()
    {
        // If using the Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        using var document = PdfDocument.Load("Reading.pdf");
        // Add a visible signature field to the first page of the PDF document.
        PdfSignatureField signatureField = document.Form.Fields.AddSignature(document.Pages[0], 300, 500, 250, 50);

        // Get a digital ID from PKCS#12/PFX file.
        var digitalId = new PdfDigitalId("GemBoxECDsa521.pfx", "GemBoxPassword");

        // Create a PDF signer that will create PAdES B-B level signature.
        var signer = new PdfSigner(digitalId)
        {
            // PdfSigner should create CAdES-equivalent signature.
            SignatureFormat = PdfSignatureFormat.CAdES,

            // Adobe Acrobat Reader currently doesn't download certificate chain
            // so we will also embed certificate of intermediate Certificate Authority in the signature.
            // (see https://community.adobe.com/t5/acrobat/signature-validation-using-aia-extension-not-enabled-by-default/td-p/10729647)
            ValidationInfo = new PdfSignatureValidationInfo([new("GemBoxECDsa.crt")], null, null),

            // Make sure that all properties specified on PdfSigner are according to PAdES B-B level.
            SignatureLevel = PdfSignatureLevel.PAdES_B_B
        };

        // Initiate signing of a PDF file with the specified signer.
        signatureField.Sign(signer);

        // Finish signing of a PDF file.
        document.Save("PAdES B-B.pdf");
    }

    static void Example2()
    {
        // If using the Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        using var document = PdfDocument.Load("Reading.pdf");
        // Add a visible signature field to the first page of the PDF document.
        PdfSignatureField signatureField = document.Form.Fields.AddSignature(document.Pages[0], 300, 500, 250, 50);

        // Get a digital ID from PKCS#12/PFX file.
        var digitalId = new PdfDigitalId("GemBoxECDsa521.pfx", "GemBoxPassword");

        // Create a PDF signer that will create PAdES B-T level signature.
        var signer = new PdfSigner(digitalId)
        {
            // PdfSigner should create CAdES-equivalent signature.
            SignatureFormat = PdfSignatureFormat.CAdES,

            // PdfSigner will embed a timestamp created by freeTSA.org Time Stamp Authority in the signature.
            Timestamper = new PdfTimestamper("https://freetsa.org/tsr"),

            // Adobe Acrobat Reader currently doesn't download certificate chain
            // so we will also embed certificate of intermediate Certificate Authority in the signature.
            // (see https://community.adobe.com/t5/acrobat/signature-validation-using-aia-extension-not-enabled-by-default/td-p/10729647)
            ValidationInfo = new PdfSignatureValidationInfo([new("GemBoxECDsa.crt")], null, null),

            // Make sure that all properties specified on PdfSigner are according to PAdES B-T level.
            SignatureLevel = PdfSignatureLevel.PAdES_B_T
        };

        // Initiate signing of a PDF file with the specified signer.
        signatureField.Sign(signer);

        // Finish signing of a PDF file.
        document.Save("PAdES B-T.pdf");
    }

    static void Example3()
    {
        // If using the Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        using var document = PdfDocument.Load("Reading.pdf");
        // Add a visible signature field to the first page of the PDF document.
        PdfSignatureField signatureField = document.Form.Fields.AddSignature(document.Pages[0], 300, 500, 250, 50);

        // Get a digital ID from PKCS#12/PFX file.
        var digitalId = new PdfDigitalId("GemBoxECDsa521.pfx", "GemBoxPassword");

        // Create a PDF signer that will create PAdES B-LT level signature.
        var signer = new PdfSigner(digitalId)
        {
            // PdfSigner should create CAdES-equivalent signature.
            SignatureFormat = PdfSignatureFormat.CAdES,

            // PdfSigner will embed a timestamp created by freeTSA.org Time Stamp Authority in the signature.
            Timestamper = new PdfTimestamper("https://freetsa.org/tsr"),

            // Make sure that all properties specified on PdfSigner are according to PAdES B-LT level.
            SignatureLevel = PdfSignatureLevel.PAdES_B_LT
        };

        // Initiate signing of a PDF file with the specified signer.
        signatureField.Sign(signer);

        // Finish signing of a PDF file.
        document.Save("PAdES B-LT.pdf");

        // Download validation-related information for the signer's certificate.
        PdfSignatureValidationInfo signerValidationInfo = document.SecurityStore.GetValidationInfo(digitalId.Certificate);

        // Embed validation-related information for the signer's certificate in the PDF file.
        // This will make the signature "LTV enabled".
        document.SecurityStore.AddValidationInfo(signerValidationInfo);

        // Save any changes done to the PDF file that were done since the last time Save was called.
        document.Save();
    }

    static void Example4()
    {
        // If using the Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        using var document = PdfDocument.Load("Reading.pdf");
        // Add a visible signature field to the first page of the PDF document.
        PdfSignatureField signatureField = document.Form.Fields.AddSignature(document.Pages[0], 300, 500, 250, 50);

        // Get a digital ID from PKCS#12/PFX file.
        var digitalId = new PdfDigitalId("GemBoxECDsa521.pfx", "GemBoxPassword");

        // Create a PDF signer that will create PAdES B-LTA level signature.
        var signer = new PdfSigner(digitalId)
        {
            // PdfSigner should create CAdES-equivalent signature.
            SignatureFormat = PdfSignatureFormat.CAdES,

            // PdfSigner will embed a timestamp created by freeTSA.org Time Stamp Authority in the signature.
            Timestamper = new PdfTimestamper("https://freetsa.org/tsr"),

            // Make sure that all properties specified on PdfSigner are according to PAdES B-LTA level.
            SignatureLevel = PdfSignatureLevel.PAdES_B_LTA
        };

        // Initiate signing of a PDF file with the specified signer.
        signatureField.Sign(signer);

        // Finish signing of a PDF file.
        document.Save("PAdES B-LTA.pdf");

        // Download validation-related information for the signature and the signature's timestamp and embed it in the PDF file.
        // This will make the signature "LTV enabled".
        document.SecurityStore.AddValidationInfo(signatureField.Value);

        // Add an invisible signature field to the PDF document that will hold the document timestamp.
        PdfSignatureField timestampField = document.Form.Fields.AddSignature();

        // Initiate timestamping of a PDF file with the specified timestamper.
        timestampField.Timestamp(signer.Timestamper);

        // Save any changes done to the PDF file that were done since the last time Save was called and
        // finish timestamping of a PDF file.
        document.Save();
    }
}