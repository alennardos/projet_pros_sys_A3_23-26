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

        // Create a XML save
        private void createSaveXml()
        {

            Console.WriteLine("ici");
            int i = 0;

            String name = null;
            String source = null;
            String destination = null;
            TypeSave ts = null;

            bool create = false;
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
                            source = saveFile.Value;
                            break;
                        case 2:
                            destination = saveFile.Value;
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
                            create = true;
                            break;
                    }

                    if (create)
                    {
                        createSave(name, source, destination, ts);
                        create = false;
                    }
                    i++;
                    i = i % 4;
                }
            }
        }

        // Write a XML save
        public void writeXmlSave()
        {
            var path = GetThisFilePath();
            StreamWriter xml = new StreamWriter(path + "\\..\\..\\save\\save.xml");
            xml.Write("<saves>");
            foreach (Save save in this.saves)
            {
                xml.Write("<save>");

                xml.Write("<name>" + save.GetName() + "</name>");
                xml.Write("<source>" + save.GetSource() + "</source>");
                xml.Write("<destination>" + save.GetDestination() + "</destination>");
                xml.Write("<type>" + save.getTs().ToString() + "</type>");

                xml.Write("</save>");
            }
            xml.Write("</saves>");

            xml.Close();
        }

        // Create a save only if list isn't full
        public bool createSave(String name, String source, String destination, TypeSave ts)
        {
            if(this.saves.Count < 5) {
                this.saves.Add(new Save(name, source, destination, ts, this));
                return true;
            }
            return false;
        }

        // Remove a save with the method, only if it exists
        public void removeSave(int num)
        {
            if (this.saves[num] != null)
            {
                this.saves.RemoveAt(num);
            }
        }

        // Change a save parameter about typeSave
        public void changeSaveParam(int num, TypeSave ts)
        {
            this.saves[num].setTs(ts);
        }

        // Write a save into a Log File
        public void save(int num)
        {
            this.writeLog(this.saves[num].save());
        }

        // Write a Log File
        public void writeLog(String log)
        {
            this.log.Write(log);
        }

        // Write a RTS File
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

        // Quit the menu and close the app
        public void quit()
        {
            this.log.Close();
            this.rts.Close();
            this.writeXmlSave();
        }
    }
}
