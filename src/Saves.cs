using ConsoleApp1.src.SaveType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1.src
{
    public class Saves
    {
        string EasySavepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EasySave");
        string logsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"EasySave", "logs");
        string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EasySave", "save");
        private List<Save> saves;
        private StreamWriter log;
        private StreamWriter rts;
        private XmlTextReader saveFile;
        private String format;
        private bool crypt;
        private String rtsFilePath;
        private int heavyFileSize;

        private static Saves _instance;

        public static Saves Instance()
        {
            {
                if (_instance == null)
                {
                    _instance = new Saves("json");
                }
                return _instance;
            }
        }
        private Saves(string format)
        {
            this.format = format;
            saves = new List<Save>();


            if(!Directory.Exists(EasySavepath))
            { 
                Directory.CreateDirectory(EasySavepath);
            }
            if (!Directory.Exists(logsPath))
            {
                Directory.CreateDirectory(logsPath);
            }
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string logFilePath = Path.Combine(logsPath, "log." + format);
            rtsFilePath = Path.Combine(logsPath, "rts.json");
            string saveFilePath = Path.Combine(savePath, "save.xml");

            if (!File.Exists(saveFilePath))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(saveFilePath, settings))
                {
                    writer.WriteStartElement("saves");
                    writer.WriteEndElement();
                    writer.Close();
                }
            }

            log = new StreamWriter(logFilePath, true);
            
            new StreamWriter(saveFilePath, true).Close();
            saveFile = new XmlTextReader(saveFilePath);
            createSaveXml();
            saveFile.Close();
        }

        public void changeCrypt(bool crypt)
        {
            this.crypt = crypt;
        }

        public void changeFormat(string format)
        {
            this.format = format;
            log.Close();
            log = new StreamWriter(logsPath + "\\log." + format, true);
        }

        // Create a save with XML file
        private void createSaveXml()
        {

            Console.WriteLine("ici");
            int i = 0;

            String? name = null;
            String? source = null;
            String? destination = null;
            TypeSave? ts = null;

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
            StreamWriter xml = new StreamWriter(savePath + "\\save.xml");
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
                this.saves.Add(new Save(name, source, destination, ts, this));
                return true;
        }

        // Remove a save with the method, only if it exists
        public void removeSave(int num)
        {
            if (this.saves[num] != null)
            {
                this.saves.RemoveAt(num);
            }
        }

        //Remove a save without index
        public void removeSave(Save save)
        {
                this.saves.Remove(save);
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
            rts = new StreamWriter(rtsFilePath, false);
            String res = "[";
            foreach(Save save in this.saves)
            {
                res += save.getSaveState();
                res += "\n";
            }
            res += "]\n";
            this.rts.Write(res);
            rts.Close();
        }

        public List<Save> getSaves()
        {
            return this.saves;
        }

        // Quit the menu and close the app
        public void quit()
        {
            this.log.Close();
            this.writeXmlSave();
        }

        public bool getCrypt()
        {
            return this.crypt;
        }

        public string getFormat()
        {
            return this.format;
        }
    }
}
