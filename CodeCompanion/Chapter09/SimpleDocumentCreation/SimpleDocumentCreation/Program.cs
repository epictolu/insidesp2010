using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace SimpleDocumentCreation {
  class Program {
    static void Main(string[] args) {

      using (WordprocessingDocument package =
               WordprocessingDocument.Create(
                  "C:\\HelloOpenXML.docx",
                  WordprocessingDocumentType.Document)) {

        //Create content
        Body body = new Body(
            new Paragraph(
                new Run(
                    new Text("Hello, Open XML SDK!"))));

        //Create package
        package.AddMainDocumentPart();
        package.MainDocumentPart.Document = new Document(body);
        package.MainDocumentPart.Document.Save();
        package.Close();
      }
    }
  }
}
