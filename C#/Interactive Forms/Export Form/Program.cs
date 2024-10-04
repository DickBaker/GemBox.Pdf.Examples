using GemBox.Pdf;

// If using the Professional version, put your serial key below.
ComponentInfo.SetLicense("FREE-LIMITED-KEY");

using var document = PdfDocument.Load("FormFilled.pdf");
document.Form.ExportData("Form Data.fdf");
