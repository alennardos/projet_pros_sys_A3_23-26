using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src
{
    internal class Save
    {

        private String name;
        private String src;
        private String target;
        private bool isComplet;
        private bool sousDossier;
        private bool isActive;

        public Save(String name, String src, String target, bool isComplet, bool sousDossier = false)
        {
            this.name = name;
            this.src = src;
            this.target = target;
            this.isComplet = isComplet;
            this.sousDossier = sousDossier;
            this.isActive = false;
        }

        public String log(String src, String target, int size, float time)
        {
            String res = "{\n";
            res += "\"Name\": \"" + name+"\",";
            res += "\n\"FileSource\": \"" + src + "\",";
            res += "\n\"FileTarget\": \"" + target + "\",";
            res += "\n\"FileSize\": " + size + ",";
            res += "\n\"FileTransferTime\": " + time + ",";
            res += "\n \"time\": \"" + DateTime.Now.ToString() + "\"";
            res += "\n},";
            return res;
        }

        public void setIsComplet(bool isComplet)
        {
            this.isComplet=isComplet;
        }

        public bool getIsActive()
        {
            return this.isActive;
        }

        public void setIsActive(bool isActive)
        {
            this.isActive = isActive;
        }

        public String save()
        {
            String res = "";

            var dir = new DirectoryInfo(this.src);
            if (!dir.Exists)
            {
                throw new Exception("Directory does not exist");
            }
            if(!new DirectoryInfo(this.target).Exists)
            {
                Directory.CreateDirectory(this.target);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(target, file.Name);
                //file.CopyTo(targetFilePath);
                Console.WriteLine(log(file.FullName, target+"\\"+file.Name, ((int)file.Length), 0));
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (DirectoryInfo subDir in dirs)
            {
                //TODO
            }

            return res;
        }

    }
}
