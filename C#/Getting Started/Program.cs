using GemBox.Pdf;
using GemBox.Pdf.Content;

// If using the Professional version, put your serial key below.
ComponentInfo.SetLicense("FREE-LIMITED-KEY");

using var document = new PdfDocument();
// Add a page.
PdfPage page = document.Pages.Add();

// Write a text.
using (var formattedText = new PdfFormattedText())
{
    formattedText.Append("Hello World!");

    page.Content.DrawText(formattedText, new PdfPoint(100, 700));
}

document.Save("HelloWorld.pdf");
