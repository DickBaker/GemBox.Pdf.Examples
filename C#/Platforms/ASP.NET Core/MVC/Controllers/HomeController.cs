﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GemBox.Pdf;
using GemBox.Pdf.Content;
using Microsoft.AspNetCore.Mvc;
using PdfCoreMvc.Models;

namespace PdfCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        // If using the Professional version, put your serial key below.
        static HomeController() => ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        public IActionResult Index() => View(new FileModel());

        public FileStreamResult Download(FileModel model)
        {
            // Create new document.
            var document = new PdfDocument();

            // Add page.
            PdfPage page = document.Pages.Add();

            // Write text.
            using (var formattedText = new PdfFormattedText())
            {
                // Write header.
                formattedText.TextAlignment = PdfTextAlignment.Center;
                formattedText.FontSize = 18;
                formattedText.MaxTextWidth = 400;

                _ = formattedText.Append(model.Header);
                page.Content.DrawText(formattedText, new PdfPoint(90, 750));

                // Write body.
                _ = formattedText.Clear();
                formattedText.TextAlignment = PdfTextAlignment.Justify;
                formattedText.FontSize = 14;

                _ = formattedText.Append(model.Body);
                page.Content.DrawText(formattedText, new PdfPoint(90, 400));

                // Write footer.
                _ = formattedText.Clear();
                formattedText.TextAlignment = PdfTextAlignment.Right;
                formattedText.FontSize = 10;
                formattedText.MaxTextWidth = 100;

                _ = formattedText.Append(model.Footer);
                page.Content.DrawText(formattedText, new PdfPoint(450, 40));
            }

            // Save PDF file.
            var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            // Download file.
            return File(stream, "application/pdf", "OutputFromView.pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

namespace PdfCoreMvc.Models
{
    public class FileModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Header { get; set; } = "Header text from ASP.NET Core MVC";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Body { get; set; } = string.Join(" ",
            Enumerable.Repeat("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", 4));
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Footer { get; set; } = "Page 1 of 1";
    }
}
