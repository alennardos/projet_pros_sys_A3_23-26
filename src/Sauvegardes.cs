using ConsoleApp1.src.SaveType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1.src
{
    internal class Sauvegardes
    {

        private List<Save> saves;
        private StreamWriter log;
        private StreamWriter rts;
        private XmlDocument saveFile;

        public Sauvegardes()
        {
            saves = new List<Save>();
            var path = GetThisFilePath();
            

            if (!new DirectoryInfo(path + "\\..\\..\\logs").Exists)
            {
                Directory.CreateDirectory(path + "\\..\\..\\logs");
            }
            log = new StreamWriter(path + "\\..\\..\\logs\\log.txt", true);
            rts = new StreamWriter(path + "\\..\\..\\logs\\rts.txt");
            this.saveFile = new XmlDocument();
            this.saveFile.Load(path + "\\..\\..\\save\\save.xml");
            this.createSaveXml();

        }

        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }

        private void createSaveXml()
        {
            Console.Write(this.saveFile.Attributes["saves"].Value);
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
        }
    }
}
