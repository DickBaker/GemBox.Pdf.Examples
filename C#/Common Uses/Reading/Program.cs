using System;
using GemBox.Pdf;
using GemBox.Pdf.Content;

Example1();
Example2();
Example3();

void Example1()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    // Iterate through PDF pages and extract each page's Unicode text content.
    using var document = PdfDocument.Load("Reading.pdf");
    foreach (PdfPage page in document.Pages)
    {
        Console.WriteLine(page.Content.ToString());
    }
}

void Example2()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    // Iterate through all PDF pages and through each page's content elements,
    // and retrieve only the text content elements.
    using var document = PdfDocument.Load("TextContent.pdf");
    foreach (PdfPage page in document.Pages)
    {
        PdfContentElementCollection.AllEnumerator contentEnumerator = page.Content.Elements.All(page.Transform).GetEnumerator();
        while (contentEnumerator.MoveNext())
        {
            if (contentEnumerator.Current.ElementType == PdfContentElementType.Text)
            {
                var textElement = (PdfTextContent)contentEnumerator.Current;

                var text = textElement.ToString();
                PdfFont font = textElement.Format.Text.Font;
                PdfColor color = textElement.Format.Fill.Color;
                PdfQuad bounds = textElement.Bounds;

                contentEnumerator.Transform.Transform(ref bounds);

                // Read the text content element's additional information.
                Console.WriteLine($"Unicode text: {text}");
                Console.WriteLine($"Font name: {font.Face.Family.Name}");
                Console.WriteLine($"Font size: {font.Size}");
                Console.WriteLine($"Font style: {font.Face.Style}");
                Console.WriteLine($"Font weight: {font.Face.Weight}");

                if (color.TryGetRgb(out var red, out var green, out var blue))
                {
                    var colaka = color.ToString();
                    if (string.IsNullOrWhiteSpace(colaka))
                    {
                        Console.WriteLine($"Color: Red={red}, Green={green}, Blue={blue}");
                    }
                    else
                    {
                        Console.WriteLine($"Color: Red={red}, Green={green}, Blue={blue} [{colaka}]");
                    }
                }
                else { Console.WriteLine(   "funny colour??"); }

                Console.WriteLine($"Bounds: Left={bounds.Left:0.00}, Bottom={bounds.Bottom:0.00}, Right={bounds.Right:0.00}, Top={bounds.Top:0.00}");
                Console.WriteLine();
            }
        }
    }
}

void Example3()
{
    // If using the Professional version, put your serial key below.
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");

    const int pageIndex = 0;
    const double areaLeft = 400, areaRight = 550, areaBottom = 680, areaTop = 720;

    using var document = PdfDocument.Load("TextContent.pdf");
    // Retrieve first page object.
    PdfPage page = document.Pages[pageIndex];

    // Retrieve text content elements that are inside specified area on the first page.
    PdfContentElementCollection.AllEnumerator contentEnumerator = page.Content.Elements.All(page.Transform).GetEnumerator();
    while (contentEnumerator.MoveNext())
    {
        if (contentEnumerator.Current.ElementType == PdfContentElementType.Text)
        {
            var textElement = (PdfTextContent)contentEnumerator.Current;

            PdfQuad bounds = textElement.Bounds;

            contentEnumerator.Transform.Transform(ref bounds);

            if (bounds.Left > areaLeft && bounds.Right < areaRight &&
                bounds.Bottom > areaBottom && bounds.Top < areaTop)
            {
                Console.Write(textElement.ToString());
            }
        }
    }
}
