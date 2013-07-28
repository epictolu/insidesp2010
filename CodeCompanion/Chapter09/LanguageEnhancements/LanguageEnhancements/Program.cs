using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LanguageEnhancements {
  class Program {
    Func<FileInfo, bool> whereMethod = CreatedOnFirstDay;

    static void Main(string[] args) {
      //Generic collections
      List<Customer> customers = new List<Customer>();

      //Extension method
      string email = "administrator@wingtip.com";
      Console.WriteLine(email.IsValidEmailAddress());

      //Where extension
      DirectoryInfo di = new DirectoryInfo("C:\\");
      var q1 = di.GetFiles().Where(CreatedOnFirstDay);
      var q2 = di.GetFiles().Where(delegate(FileInfo info) {
        return info.CreationTime.Day == 1;
      });
      var q3 = di.GetFiles().Where(info => info.CreationTime.Day == 1);
      var q4 = from f in di.GetFiles()
               where f.CreationTime.Day == 1
               select f;
    }

    static bool CreatedOnFirstDay(FileInfo info) {
      return info.CreationTime.Day == 1;
    }
  }

  public class Customer {
    public string Name { get; set; }
    public string Phone { get; set; }
  }

  public static class ExtensionMethod {
    public static bool IsValidEmailAddress(this string s) {
      Regex r = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
      return r.IsMatch(s);
    }
  }
}
