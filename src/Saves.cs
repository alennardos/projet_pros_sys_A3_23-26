using ConsoleApp1.src.SaveType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1.src
{
    internal class Saves
    {

        private List<Save> saves;
        private StreamWriter log;
        private StreamWriter rts;
        private XmlTextReader saveFile;

        public Saves()
        {
            saves = new List<Save>();
            var path = GetThisFilePath();
            
            if (!new DirectoryInfo(path + "\\..\\..\\logs").Exists)
            {
                Directory.CreateDirectory(path + "\\..\\..\\logs");
            }

            log = new StreamWriter(path + "\\..\\..\\logs\\log.txt", true);
            rts = new StreamWriter(path + "\\..\\..\\logs\\rts.txt");
            saveFile = new XmlTextReader(path + "\\..\\..\\save\\save.xml");
            createSaveXml();
            saveFile.Close();
        }

        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }

        private void createSaveXml()
        {

            Console.WriteLine("ici");
            int i = 0;

            String name = null;
            String src = null;
            String dest = null;
            TypeSave ts = null;

            bool cree = false;
            while (saveFile.Read())
            {

                if (saveFile.NodeType == XmlNodeType.Text)
                {
      
                    switch (i)
                    {
                        case 0:
                            name = saveFile.Value;
                            
                            break;
                        case 1:
                            src = saveFile.Value;
                            break;
                        case 2:
                            dest = saveFile.Value;
                            break;
                        case 3:
                            if (saveFile.Value == "comp")
                            {
                                ts = new SaveComplete();
                            }
                            else
                            {
                                ts = new SaveDif();
                            }
                            cree = true;
                            break;
                    }

                    if (cree)
                    {
                        createSave(name, src, dest, ts);
                        cree = false;
                    }
                    i++;
                    i = i % 4;
                }
            }
        }

        public void writeXmlSave()
        {
            var path = GetThisFilePath();
            StreamWriter xml = new StreamWriter(path + "\\..\\..\\save\\save.xml");
            xml.Write("<saves>");
            foreach (Save s in this.saves)
            {
                xml.Write("<save>");

                xml.Write("<name>" + s.GetName() + "</name>");
                xml.Write("<src>" + s.GetSrc() + "</src>");
                xml.Write("<dst>" + s.GetDest() + "</dst>");
                xml.Write("<type>" + s.getTs().ToString() + "</type>");

                xml.Write("</save>");
            }
            xml.Write("</saves>");

            xml.Close();
        }

        public bool createSave(String nom, String src, String dest, TypeSave ts)
        {
            if(this.saves.Count < 5) {
                this.saves.Add(new Save(nom, src, dest, ts, this));
                return true;
            }
            return false;
        }

        public void removeSave(int num)
        {
            if (this.saves[num] != null)
            {
                this.saves.RemoveAt(num);
            }
        }

        public void changeSaveParam(int num, TypeSave ts)
        {
            this.saves[num].setTs(ts);
        }

        public void save(int num)
        {
            this.writeLog(this.saves[num].save());
        }


        public void writeLog(String str)
        {
            this.log.Write(str);
        }

        public void writeRts()
        {
            String res = "[";
            foreach(Save save in this.saves)
            {
                res += save.getSaveState();
                res += "\n";
            }
            res += "]\n";
            this.rts.Write(res);
        }

        public List<Save> getSaves()
        {
            return this.saves;
        }

        public void quit()
        {
            this.log.Close();
            this.rts.Close();
            this.writeXmlSave();
        }
    }
}
