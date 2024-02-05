using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.src.SaveType;

namespace ConsoleApp1.src
{
    internal class Save
    {

        private String name;
        private String src;
        private String target;
        private bool isComplet;
        private bool isActive;
        private TypeSave ts;

        public Save(String name, String src, String target, TypeSave ts)
        {
            this.name = name;
            this.src = src;
            this.target = target;
            this.isComplet = isComplet;
            this.isActive = false;
            this.ts = ts;
        }

        public String log(String src, String target, int size, double time)
        {
            String res = "{\n";
            res += "\"Name\": \"" + name+"\",";
            res += "\n\"FileSource\": \"" + src + "\",";
            res += "\n\"FileTarget\": \"" + target + "\",";
            res += "\n\"FileSize\": " + size + ",";
            res += "\n\"FileTransferTime\": " + time + ",";
            res += "\n \"time\": \"" + DateTime.Now.ToString() + "\"";
            res += "\n},\n";
            return res;
        }

        public void setIsComplet(bool isComplet)
        {
            this.isComplet=isComplet;
        }

        public bool getIsComplet()
        {
            return this.isComplet;
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
                throw new DirectoryNotFoundException();
            }

            if(!new DirectoryInfo(this.target).Exists)
            {
                Directory.CreateDirectory(this.target);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(target, file.Name);
                var watch = System.Diagnostics.Stopwatch.StartNew();


                ts.save(file, targetFilePath);

                watch.Stop();
                double temps = (double)watch.ElapsedMilliseconds/1000;
                res+=(log(file.FullName, target+"\\"+file.Name, ((int)file.Length), temps));
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (DirectoryInfo subDir in dirs)
            {
                Save s = new Save(name, subDir.FullName, target+"\\"+subDir.Name, ts);
                res += s.save();
            }

            return res;
        }

    }
}
