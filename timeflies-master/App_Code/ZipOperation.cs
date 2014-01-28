using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using System.Collections.Specialized;
using System.Collections;

namespace TimeFliesBy.WebUI
{
  public class ZipOperation
  {
    public static void CreateZipFile(string[] filenames, string outputFile)
    {
      // Zip up the files - From SharpZipLib Demo Code
      using (ZipOutputStream s = new ZipOutputStream(File.Create(outputFile)))
      {
        s.SetLevel(9); // 0-9, 9 being the highest level of compression
        byte[] buffer = new byte[4096];
        foreach (string file in filenames)
        {
          ZipEntry entry = new ZipEntry(Path.GetFileName(file));
          entry.DateTime = DateTime.Now;
          s.PutNextEntry(entry);

          using (FileStream fs = File.OpenRead(file))
          {
            int sourceBytes;
            do
            {
              sourceBytes = fs.Read(buffer, 0, buffer.Length);
              s.Write(buffer, 0, sourceBytes);

            } 
            while (sourceBytes > 0);
          }
        }
        s.Finish();
        s.Close();
      }
    }
  }
}