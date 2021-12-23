using DDtank_Ba_Decompress.Data;
using DDtank_Ba_Decompress.Tools;
using Ionic.Zlib;
using SplushTools.Analyzer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDtank_Ba_Decompress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void loadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            try
            {
                DialogResult dialogResult = dialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {

                    var pathBytes = File.ReadAllBytes(dialog.FileName);
                    var memoryStream = new MemoryStream(pathBytes);

                    ZipArchive zipArchive = new ZipArchive(memoryStream);
                    var barFileInfo = zipArchive.Entries.First();

                    var languageInfo = File.CreateText("Decrypt/" + dialog.SafeFileName.Split('.')[0] + ".txt");

                    using (var stream = barFileInfo.Open())
                    {
                        using (var fileMemoryStream = new MemoryStream())
                        {
                            stream.CopyTo(fileMemoryStream);
                            fileMemoryStream.Position = 0;

                            FileAnalyzer fileAnalyzer = new FileAnalyzer(fileMemoryStream.GetBuffer());
                            string fileName = fileAnalyzer.ReadString();
                            int lineCount = fileAnalyzer.ReadInt();
                            for (int i = 0; i < lineCount; i++)
                            {
                                string paramName = fileAnalyzer.ReadString();
                                string paramText = fileAnalyzer.ReadString();
                                languageInfo.WriteLine("{0}: {1}", paramName, paramText);
                            }
                            languageInfo.Close();
                            MessageBox.Show("Descompactado com sucesso!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void decryptXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            try
            {
                DialogResult dialogResult = dialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    var pathBytes = File.ReadAllBytes(dialog.FileName);
                    var xmlAnalyzer = new FileAnalyzer(ZlibStream.UncompressBuffer(pathBytes));

                    List<ArrayList> arrayLists = new List<ArrayList>();
                    List<StructTableInfo> structTableInfos = new List<StructTableInfo>();

                    string fileName = xmlAnalyzer.ReadString();
                    int columsCount = xmlAnalyzer.ReadInt();

                    for(int i = 0; i < columsCount; i++)
                    {
                        string Colum = xmlAnalyzer.ReadString();
                        string Type = xmlAnalyzer.ReadString();

                        structTableInfos.Add(new StructTableInfo
                        {
                            titleName = Colum,
                            type = Type,
                            value = ""
                        });
                    }

                    int linesCount = xmlAnalyzer.ReadInt();
                    for(int j = 0; j < linesCount; j++)
                    {
                        ArrayList arrayList = new ArrayList();
                        foreach(var structTableInfo in structTableInfos)
                        {
                            switch (structTableInfo.type)
                            {
                                case "INT":
                                    arrayList.Add(xmlAnalyzer.ReadInt());
                                    break;
                                case "STRING":
                                    arrayList.Add(xmlAnalyzer.ReadString());
                                    break;
                                case "SHORT":
                                    arrayList.Add(xmlAnalyzer.ReadShort());
                                    break;
                                case "BOOLEAN":
                                    arrayList.Add(xmlAnalyzer.ReadBoolean());
                                    break;
                                case "DATETIME":
                                    arrayList.Add(xmlAnalyzer.ReadDateTime());
                                    break;
                            }
                        }
                        arrayLists.Add(arrayList);
                    }

                    CreateXmlFile.Decompress(arrayLists, structTableInfos, fileName);
                    MessageBox.Show("Descompactado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
