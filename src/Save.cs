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
        private String source;
        private String destination;
        private String actualFile;
        private String actualFileTarget;
        private int nbfiles;
        private int fileSize;
        private int nbLeft;
        private int leftSize;
        private int sizeTreated;
        private int fileTreated;
        private bool isActive;
        private TypeSave ts;
        private Saves saves;

        public Save(String name, String source, String target, TypeSave ts, Saves save)
        {
            this.name = name;
            this.source = source;
            this.destination = target;
            this.isActive = false;
            this.ts = ts;
            this.saves = save;
            nbfiles = 0;
            fileSize = 0;
            nbLeft = 0;
            leftSize = 0;
            actualFile = "";
            actualFileTarget = "";
        }

        // Return a log
        public String log(String source, String target, int size, double time)
        {
            String res = "\n{\n";
            res += "\"Name\": \"" + name + "\",";
            res += "\n\"FileSource\": \"" + source + "\",";
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

        // Call the method save with arguments
        public String save()
        {
            setIsActive(true);

            var directory = new DirectoryInfo(this.source);
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException();
            }

            this.fileTreated = 0;

            this.sizeTreated = 0;

            this.nbfiles = this.calculNumberFiles(directory);

            this.fileSize = this.calculDirectorySize(directory);

            String res = this.save(directory, destination);

            setIsActive(false);

            this.setState(0, 0, "", "");

            return res;
        }

        // Save the files
        private String save(DirectoryInfo directory, String destination)
        {
            String res = "";

            if (!new DirectoryInfo(destination).Exists)
            {
                Directory.CreateDirectory(destination);
            }

            foreach (FileInfo file in directory.GetFiles())
            {


                string targetFilePath = Path.Combine(destination, file.Name);
                var watch = System.Diagnostics.Stopwatch.StartNew();

                this.setState(nbfiles - fileTreated, fileSize - sizeTreated, file.FullName, destination + @"\" + file.Name);

                this.saves.writeRts();

                ts.save(file, targetFilePath);

                watch.Stop();

                double time = (double)watch.ElapsedMilliseconds / 1000;

                res += (log(file.FullName, destination + @"\" + file.Name, ((int)file.Length), time));

                fileTreated++;
                sizeTreated += (int)file.Length;
            }

            DirectoryInfo[] directorys = directory.GetDirectories();

            foreach (DirectoryInfo subDirectory in directorys)
            {
                res += this.save(subDirectory, destination + @"\" + subDirectory.Name);
            }

            return res;
        }

        // Get the save state
        public String getSaveState()
        {
            String res = "{";
            res += "\n\"Name\": \"" + name + "\",";
            res += "\n\"SourceFilePath\": \"" + this.source + "\",";
            res += "\n\"TargetFilePath\": \"" + this.destination + "\",";
            if (this.isActive) res += "\n\"State\": \"ACTIVE\",";
            if (!this.isActive) res += "\n\"State\": \"END\",";
            res += "\n\"TotalFilesToCopy\": \"" + this.nbfiles + "\",";
            res += "\n\"TotalFilesSize\": \"" + this.fileSize + "\",";
            res += "\n\"NbFilesLeftToDo\": \"" + this.nbLeft + "\",";
            res += "\n\"LeftFilesSize\": \"" + this.leftSize + "\",";
            res += "\n\"ActualFileSource\": \"" + this.actualFile + "\",";
            res += "\n\"ActualFileTarget\": \"" + this.actualFileTarget + "\"";
            res += "\n}";
            return res;
        }

        public void setState(int nbLeft = 0, int leftSize = 0, String actualFile = "", String actualFileTarget = "")
        {
            this.nbLeft = nbLeft;
            this.leftSize = leftSize;
            this.actualFile = actualFile;
            this.actualFileTarget = actualFileTarget;
        }

        // Return the number of files of a directory
        public int calculNumberFiles(DirectoryInfo directory)
        {
            int res = 0;

            res += directory.GetFiles().Length;

            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                res += calculNumberFiles(subDirectory);
            }

            return res;
        }

        // Return the size of a directory
        public int calculDirectorySize(DirectoryInfo directory)
        {
            int res = 0;

            foreach (FileInfo f in directory.GetFiles())
            {
                res += (int)f.Length;
            }

            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                res += calculDirectorySize(subDirectory);
            }

            return res;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetSource(string src)
        {
            this.source = src;
        }

        public void SetDestination(string destination)
        {
            this.destination = destination;
        }

        public String GetSource()
        {
            return this.source;
        }

        public String GetDestination()
        {
            return this.destination;
        }
   
    }
}
