using GemBox.Pdf;

// If using the Professional version, put your serial key below.
ComponentInfo.SetLicense("FREE-LIMITED-KEY");

using var document = new PdfDocument();
// Add a first empty page.
_ = document.Pages.Add();

// Add a second empty page.
_ = document.Pages.Add();

document.Save("Output.pdf");
