using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.Core.Services;

public class PdfService : IPdfService
{
    public void SaveReport(Group group)
    {
        if (GlobalFontSettings.FontResolver is not FailsafeFontResolver)
        {
            GlobalFontSettings.FontResolver = new FailsafeFontResolver();
        }
        
        var document = new Document
        {
            Info =
            {
                Title = "Course Report",
                Subject = "Consist of course -> group -> students",
                Author = "Alex Bell"
            }
        };

        DefineStyles(document);
 
        CreatePage(document, group);

        var pdfDocumentRenderer = new PdfDocumentRenderer
        {
            Document = document
        };

        var fileName = $"{group.Course!.Name}_{group.Name}_{DateTime.Now:dd-MM-yyyyTHH-mm-ss}.pdf";
        
        pdfDocumentRenderer.RenderDocument();
        pdfDocumentRenderer.PdfDocument.Save(fileName);
    }

    private static void DefineStyles(Document document)
    {
        var style = document.Styles["Normal"]!;
        style.Font.Name = "Verdana";
 
        style = document.Styles[StyleNames.Header]!;
        style.ParagraphFormat.AddTabStop("2cm", TabAlignment.Left);
        style.Font.Size = 18;
 
        style = document.Styles[StyleNames.Footer]!;
        style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

        style = document.Styles.AddStyle("SubHeader", "Normal");
        style.ParagraphFormat.TabStops.AddTabStop("8cm", TabAlignment.Left);
        style.Font.Size = 16;
 
        style = document.Styles.AddStyle("Table", "Normal");
        style.Font.Name = "Verdana";
        style.Font.Size = 14;
 
        style = document.Styles.AddStyle("Reference", "Normal");
        style.ParagraphFormat.SpaceBefore = "5mm";
        style.ParagraphFormat.SpaceAfter = "5mm";
        style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
    }

    private static void CreatePage(Document document, Group group)
    {
        var section = document.AddSection();
        
        // Course
        var paragraph = section.Headers.Primary.AddParagraph();
        paragraph.AddText($"Course: {group.Course!.Name}");
        
        // Group
        paragraph = section.AddParagraph();
        paragraph.Style = "SubHeader";
        paragraph.Format.SpaceAfter = "0.5cm";
        paragraph.AddText($"Group: {group.Name}");
        
        paragraph = section.Footers.Primary.AddParagraph();
        paragraph.AddText("Created at: ");
        paragraph.AddDateField("dddd, dd MMMM yyyy HH:mm:ss");
        paragraph.Format.Font.Size = 10;
        paragraph.Format.Alignment = ParagraphAlignment.Center;
 
        // Table
        var table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = Colors.Black;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;
        
        // Add Columns
        var column = table.AddColumn("3cm");
        column.Format.Alignment = ParagraphAlignment.Center;
 
        column = table.AddColumn("5cm");
        column.Format.Alignment = ParagraphAlignment.Right;
 
        column = table.AddColumn("5cm");
        column.Format.Alignment = ParagraphAlignment.Right;
        
        // Header
        var row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = Colors.LightGray;
        
        row.Cells[0].AddParagraph("Number");
        row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
        
        row.Cells[1].AddParagraph("First name");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[1].VerticalAlignment = VerticalAlignment.Bottom;
        
        row.Cells[2].AddParagraph("Last name");
        row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[2].VerticalAlignment = VerticalAlignment.Bottom;
 
        table.SetEdge(0, 0, 3, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        
        FillContent(table, group);
    }

    private static void FillContent(Table table, Group group)
    {
        var students = group.Students.ToList();
        
        for (var i = 0; i < students.Count; i++)
        {
            var row = table.AddRow();
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[0].AddParagraph($"{i + 1}");
            row.Cells[1].AddParagraph($"{students[i].FirstName}");
            row.Cells[2].AddParagraph($"{students[i].LastName}");
        }
    }
}