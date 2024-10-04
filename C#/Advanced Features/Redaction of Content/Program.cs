using System.Text.RegularExpressions;
using GemBox.Pdf;
using GemBox.Pdf.Content;

Example1();
Example2();
Example3();
Example4();
Example5();

void Example1()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    using var document = PdfDocument.Load("Invoice.pdf");
    PdfPage page = document.Pages[0];

    // Adding and applying redaction annotation to the area with the content we want to redact.
    GemBox.Pdf.Annotations.PdfRedactionAnnotation redaction = page.Annotations.AddRedaction(300, 440, 225, 160);
    redaction.Apply();

    document.Save("Redacted.pdf");
}

void Example2()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    using var document = PdfDocument.Load("Reading.pdf");
    PdfPage page = document.Pages[0];

    // Adding redaction annotation with any non-zero area.
    GemBox.Pdf.Annotations.PdfRedactionAnnotation redaction = page.Annotations.AddRedaction(0, 0, 1, 1);

    // Adding quads for the areas we want to redact
    redaction.Quads.Add(new PdfQuad(0, 0, 100, 100));
    redaction.Quads.Add(new PdfQuad(200, 0, 300, 100));
    redaction.Quads.Add(new PdfQuad(400, 0, 500, 100));
    redaction.Quads.Add(new PdfQuad(100, 100, 200, 200));
    redaction.Quads.Add(new PdfQuad(300, 100, 400, 200));
    redaction.Quads.Add(new PdfQuad(0, 200, 100, 300));
    redaction.Quads.Add(new PdfQuad(200, 200, 300, 300));
    redaction.Quads.Add(new PdfQuad(400, 200, 500, 300));
    redaction.Quads.Add(new PdfQuad(100, 300, 200, 400));
    redaction.Quads.Add(new PdfQuad(300, 300, 400, 400));
    redaction.Quads.Add(new PdfQuad(0, 400, 100, 500));
    redaction.Quads.Add(new PdfQuad(200, 400, 300, 500));
    redaction.Quads.Add(new PdfQuad(400, 400, 500, 500));

    // Applying redaction to remove all content in the area.
    redaction.Apply();

    document.Save("MultipleRedactions.pdf");
}

void Example3()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    using var document = PdfDocument.Load("Document.pdf");
    // Applying all redactions existing in the PDF document
    foreach (PdfPage page in document.Pages)
    {
        page.Annotations.ApplyRedactions();
    }

    document.Save("RedactedOutput.pdf");
}

void Example4()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    using var document = PdfDocument.Load("Invoice.pdf");
    PdfPage page = document.Pages[0];

    // Regex to match with decimal numbers
    var regex = new Regex(@"\d+\.\d+");

    // Redacting everything that matches with the regex
    foreach (PdfText text in page.Content.GetText().Find(regex))
    {
        text.Redact();
    }

    document.Save("RegexRedactedOutput.pdf");
}

void Example5()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    using var document = PdfDocument.Load("Invoice.pdf");
    PdfPage page = document.Pages[0];
    GemBox.Pdf.Annotations.PdfRedactionAnnotation redaction = page.Annotations.AddRedaction(0, 0, 1, 1);
    var regex = new Regex(@"\d+\.\d+");

    // Adding quads for each matching text
    foreach (PdfText text in page.Content.GetText().Find(regex))
    {
        redaction.Quads.Add(text.Bounds);
    }

    // Setting custom fill color for the redacted areas
    redaction.Appearance.RedactedAreaFillColor = PdfColor.FromRgb(0.95, 0.4, 0.14);
    redaction.Apply();

    document.Save("CustomFilledRedactedOutput.pdf");
}
