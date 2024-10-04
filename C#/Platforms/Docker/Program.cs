using GemBox.Pdf;
using GemBox.Pdf.Content;

ComponentInfo.SetLicense("FREE-LIMITED-KEY");

// Create new document.
using var document = new PdfDocument();
// Add a page.
PdfPage page = document.Pages.Add();

// Write a text.
using (var formattedText = new PdfFormattedText())
{
    _ = formattedText.Append("Hello World!");
    page.Content.DrawText(formattedText, new PdfPoint(100, 700));
}

// Save as PDF file.
document.Save("output.pdf");
