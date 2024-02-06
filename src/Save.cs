﻿using System;
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
        private Sauvegardes sauvegardes;
        private int nbfiles;
        private int fileSize;
        private int nbLeft;
        private int leftSize;
        private String actualFile;
        private String actualFileTarget;

        public Save(String name, String src, String target, TypeSave ts, Sauvegardes s)
        {
            this.name = name;
            this.src = src;
            this.target = target;
            this.isActive = false;
            this.ts = ts;
            this.sauvegardes = s;
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

        public void setTs(TypeSave ts)
        {
            this.ts = ts;
        }

        public TypeSave getTs()
        {
            return this.ts;
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

            int fichierTraitee = 0;
            int tailleTraitee = 0;

            this.nbfiles = dir.GetFiles().Length;
            this.fileSize = this.calculerTailleRep(dir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(target, file.Name);
                var watch = System.Diagnostics.Stopwatch.StartNew();

                this.setState(fichierTraitee, tailleTraitee, file.FullName, target + "\\" + file.Name);
                this.sauvegardes.writeRts();

                ts.save(file, targetFilePath);


                watch.Stop();
                double temps = (double)watch.ElapsedMilliseconds/1000;
                res+=(log(file.FullName, target+"\\"+file.Name, ((int)file.Length), temps));

                fichierTraitee++;
                tailleTraitee += (int)file.Length;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (DirectoryInfo subDir in dirs)
            {
                Save s = new Save(name, subDir.FullName, target+"\\"+subDir.Name, ts, this.sauvegardes);
                res += s.save();
            }

            return res;
        }

        public String getSaveState()
        {
            String res = "{";
            res += "\n\"Name\": \"" + name + "\",";
            res += "\n\"SourceFilePath\": \"" + this.src + "\",";
            res += "\n\"TargetFilePath\": \"" + this.target + "\",";
            if (this.isActive)res += "\n\"State\": \"ACTIVE\",";
            if (!this.isActive) res += "\n\"State\": \"END\",";
            res += "\n\"TotalFilesToCopy\": \"" + this.nbfiles + "\",";
            res += "\n\"TotalFilesSize\": \"" + this.fileSize + "\",";
            res += "\n\"NbFilesLeftToDo\": \"" + this.nbLeft + "\",";
            res += "\n\"LeftFilesSize\": \"" + this.leftSize + "\",";
            res += "\n\"ActualFileSource\": \"" + this.actualFile + "\",";
            res += "\n\"ActualFileTarget\": \"" + this.actualFileTarget + "\",";
            res += "}";
            return res;
        }


        public void setState(int nbLeft = 0, int leftSize = 0, String actualFile = "", String actualFileTarget = "")
        {
            this.nbLeft = nbLeft;
            this.leftSize = leftSize;
            this.actualFile = actualFile;
            this.actualFileTarget = actualFileTarget;
        }

        public int calculerTailleRep(DirectoryInfo dir)
        {
            int res = 0;
            
            foreach(FileInfo f in dir.GetFiles())
            {
                res += (int)f.Length;
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                res+=calculerTailleRep(subDir);
            }

            return res;
        }
    }
}
