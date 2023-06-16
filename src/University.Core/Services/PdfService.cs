using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Snippets.Font;
using University.Core.Interfaces;

namespace University.Core.Services;

public class PdfService : IPdfService
{
    public void SaveReport()
    {
        GlobalFontSettings.FontResolver = new FailsafeFontResolver();
        
        var document = new Document
        {
            Info =
            {
                Title = "Course Report",
                Subject = "Consist of course -> group -> students",
                Author = "Stefan Lange"
            }
        };

        DefineStyles(document);
 
        CreatePage(document);
 
        FillContent();

        var pdfDocumentRenderer = new PdfDocumentRenderer
        {
            Document = document
        };
        
        pdfDocumentRenderer.RenderDocument();
        pdfDocumentRenderer.PdfDocument.Save("test.pdf");
    }

    private static void DefineStyles(Document document)
    {
        var style = document.Styles["Normal"]!;
        style.Font.Name = "Verdana";
 
        style = document.Styles[StyleNames.Header]!;
        style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);
 
        style = document.Styles[StyleNames.Footer]!;
        style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
 
        style = document.Styles.AddStyle("Table", "Normal");
        style.Font.Name = "Verdana";
        style.Font.Size = 14;
 
        style = document.Styles.AddStyle("Reference", "Normal");
        style.ParagraphFormat.SpaceBefore = "5mm";
        style.ParagraphFormat.SpaceAfter = "5mm";
        style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
    }

    private void CreatePage(Document document)
    {
        var section = document.AddSection();
        
        var paragraph = section.Footers.Primary.AddParagraph();
        paragraph.AddText("PowerBooks Inc · Sample Street 42 · 56789 Cologne · Germany");
        paragraph.Format.Font.Size = 9;
        paragraph.Format.Alignment = ParagraphAlignment.Center;
        
        var addressFrame = section.AddTextFrame();
        addressFrame.Height = "3.0cm";
        addressFrame.Width = "7.0cm";
        addressFrame.Left = ShapePosition.Left;
        addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        addressFrame.Top = "5.0cm";
        addressFrame.RelativeVertical = RelativeVertical.Page;
        
        // Put sender in address frame
        paragraph = addressFrame.AddParagraph("PowerBooks Inc · Sample Street 42 · 56789 Cologne");
        paragraph.Format.Font.Name = "Times New Roman";
        paragraph.Format.Font.Size = 7;
        paragraph.Format.SpaceAfter = 3;
 
        // Add the print date field
        paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = "8cm";
        paragraph.Style = "Reference";
        paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
        paragraph.AddTab();
        paragraph.AddText("Cologne, ");
        paragraph.AddDateField("dd.MM.yyyy");
 
        // Create the item table
        var table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = Colors.Black;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;
        
        // Before you can add a row, you must define the columns
        var column = table.AddColumn("1cm");
        column.Format.Alignment = ParagraphAlignment.Center;
 
        column = table.AddColumn("2.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;
 
        column = table.AddColumn("3cm");
        column.Format.Alignment = ParagraphAlignment.Right;
 
        column = table.AddColumn("3.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;
 
        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Center;
 
        column = table.AddColumn("4cm");
        column.Format.Alignment = ParagraphAlignment.Right;
        
        // Create the header of the table
        var row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = Colors.LightBlue;
        row.Cells[0].AddParagraph("Number");
        row.Cells[0].Format.Font.Bold = true;
        row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[1].AddParagraph("First name");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[1].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[2].AddParagraph("Last name");
        row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[2].VerticalAlignment = VerticalAlignment.Bottom;
 
        table.SetEdge(0, 0, 3, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
    }

    private void FillContent()
    {
    }
}