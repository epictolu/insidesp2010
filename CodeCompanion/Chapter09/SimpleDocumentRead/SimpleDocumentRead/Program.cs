using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace SimpleDocumentRead {
  class Program {
    static void Main(string[] args) {
      using (WordprocessingDocument package =
          WordprocessingDocument.Open(args[0], false)) {
        var q = from t in package.MainDocumentPart.Document.Descendants<Text>()
                select t;
        foreach (var i in q) {
          Console.WriteLine(i.Text);
        }
      }
    }
  }
}
