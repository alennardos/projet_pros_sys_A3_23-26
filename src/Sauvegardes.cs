﻿using ConsoleApp1.src.SaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src
{
    internal class Sauvegardes
    {

        private List<Save> saves;
        private StreamWriter log;
        private StreamWriter rts;

        public Sauvegardes()
        {
            saves = new List<Save>();
            if (!new DirectoryInfo(@"C:\SaveLogs").Exists)
            {
                Directory.CreateDirectory(@"C:\SaveLogs");
            }
            log = new StreamWriter(@"C:\SaveLogs\log.txt");
            rts = new StreamWriter(@"C:\SaveLogs\rts.txt");
        }

        public bool createSave(String nom, String src, String dest, TypeSave ts)
        {
            if(this.saves.Count < 5) {
                this.saves.Append(new Save(nom, src, dest, ts, this));
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
            res += "]";
            this.rts.Write(res);
        }
    }
}
