using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src{
    internal class Fichier{

        private string name;
        private string path;
        private FileInfo info;

        public Fichier(string path)
        {
            this.path = path;
            this.name = Path.GetFileName(path);
            this.info = new FileInfo(path);
        }

        public String infos()
        {
            string res = "";
            res += this.name;
            res += "\n" + this.info.Length;

            return res;
        }

    }
}
