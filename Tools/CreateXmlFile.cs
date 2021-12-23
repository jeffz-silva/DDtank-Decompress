using DDtank_Ba_Decompress.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DDtank_Ba_Decompress.Tools
{
    class CreateXmlFile
    {
        public static void Decompress(List<ArrayList> arrayLists, List<StructTableInfo> structTableInfos, string fileName)
        {
            XElement xElement = new XElement("Result");
            XElement xElement2 = new XElement("DataInfo");

            foreach (var arrayList in arrayLists)
            {
                var fileLines = arrayList;
                var elementOfLine = new XElement("Item");
                int colum = 0;
                foreach(var fileLine in fileLines)
                {
                    var titleName = structTableInfos[colum].titleName;
                    elementOfLine.Add(new XAttribute(titleName, fileLine));
                    colum++;
                }
                xElement2.Add(elementOfLine);
            }
            xElement.Add(xElement2);
            xElement.Add(new XAttribute("value", true));
            xElement.Add(new XAttribute("message", "Success!"));
            CreateCompressXml("Decrypt", xElement, fileName, isCompress: false);
        }

        public static string CreateCompressXml(string path, XElement result, string file, bool isCompress)
        {
            try
            {
                file += ".xml";
                path = Path.Combine(path, file);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.Write(result.ToString());
                }
                return "Build:" + file + ",Success!";
            }
            catch (Exception ex)
            {
                return "Build:" + file + ",Fail!";
            }
        }
    }
}
