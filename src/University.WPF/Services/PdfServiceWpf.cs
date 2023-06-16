using PdfSharp.Drawing;
using PdfSharp.Pdf;
using University.Core.Interfaces;

namespace University.WPF.Services;

public class PdfServiceWpf : IPdfService
{
    public void SaveReport()
    {
        // PdfDocument document = new PdfDocument();
        // document.Info.Title = "Created with PDFsharp";
        //
        // PdfPage page = document.AddPage();
        //
        // XGraphics xGraphics = XGraphics.FromPdfPage(page);
        //
        // XFont xFont = new XFont("Verdana", 20, XFontStyle.BoldItalic);
        //
        // xGraphics.DrawString("Hello, World!", xFont, XBrushes.Black,
        //     new XRect(0, 0, page.Width, page.Height),
        //     XStringFormats.Center);
        //
        // document.Save("test.pdf");
    }
}