using System.IO;
using GemBox.Pdf;

Example1();
Example2();

void Example1()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    // List of source file names.
    var fileNames = new string[]
    {
            "MergeFile01.pdf",
            "MergeFile02.pdf",
            "MergeFile03.pdf"
    };

    using var document = new PdfDocument();
    // Merge multiple PDF files into single PDF by loading source documents
    // and cloning all their pages to destination document.
    foreach (var fileName in fileNames)
    {
        using var source = PdfDocument.Load(fileName);
        document.Pages.Kids.AddClone(source.Pages);
    }

    document.Save("Merge Files.pdf");
}

void Example2()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    System.Collections.Generic.IEnumerable<string> files = Directory.EnumerateFiles("Merge Many Pdfs");

    var fileCounter = 0;
    const int chunkSize = 50;

    using var document = new PdfDocument();
    // Create output PDF file that will have large number of PDF files merged into it.
    document.Save("Merged Files.pdf");

    foreach (var file in files)
    {
        using (var source = PdfDocument.Load(file))
        {
            document.Pages.Kids.AddClone(source.Pages);
        }

        ++fileCounter;
        if (fileCounter % chunkSize == 0)
        {
            // Save the new pages that were added after the document was last saved.
            document.Save();

            // Clear previously parsed pages and thus free memory necessary for merging additional pages.
            document.Unload();
        }
    }

    // Save the last chunk of merged files.
    document.Save();
}
