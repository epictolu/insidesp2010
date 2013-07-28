using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;



namespace WingtipDocuments {
  public class DocumentFactory {

    public static void CreateTextDocument(string name, SPDocumentLibrary library, string contents) {

      string DocumentName = name + ".txt";

      MemoryStream DocumentStream = new MemoryStream();
      StreamWriter writer = new StreamWriter(DocumentStream);
      writer.Write(contents);
      writer.Flush();

      library.RootFolder.Files.Add(DocumentName, DocumentStream, true);

    }

    public static void CreateXmlDocument(string name, SPDocumentLibrary library, string contents) {

      string DocumentName = name + ".xml";

      MemoryStream DocumentStream = new MemoryStream();
      XmlTextWriter writer = new XmlTextWriter(DocumentStream, Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.Indentation = 2;


      var xml =
        new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
          new XElement("WingtipDocument",
            new XElement("DocumentContents", contents)));
     
      xml.WriteTo(writer);
      writer.Flush();
      
      SPFile file = library.RootFolder.Files.Add(DocumentName, DocumentStream, true);

    }

    public static void CreateWordDocument(string name, SPDocumentLibrary library, string contents) {

      string DocumentName = name + ".docx";
      MemoryStream DocumentStream = new MemoryStream();

      WordprocessingDocument wordDocument =
        WordprocessingDocument.Create(DocumentStream, WordprocessingDocumentType.Document);


      MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

      // Create the document structure and add some text.
      mainPart.Document = new Document();
      Body body = mainPart.Document.AppendChild(new Body());
      Paragraph para = body.AppendChild(new Paragraph());
      Run run = para.AppendChild(new Run());
      run.AppendChild(new Text(contents));

      wordDocument.Close();

      library.RootFolder.Files.Add(DocumentName, DocumentStream, true);

    }

    public static bool IsOpenXmlSdkInstalled() {
      try {
        string asmName = "DocumentFormat.OpenXml, Version=2.0.5022.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
        Assembly asm = Assembly.Load(asmName);
        return (asm != null);
      }
      catch {
        return false;
      }
    }

    
  }
}
