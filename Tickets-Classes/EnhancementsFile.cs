using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tickets_Classes
{
    public class EnhancementsFile
    {
        public String filePath {get;set;}
        public List<Enhancements> Enhancement {get;set;}

        public EnhancementsFile(string enhancementsFilePath){
            filePath = enhancementsFilePath;
            Enhancement = new List<Enhancements>();

            try{
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while(!sr.EndOfStream){
                    Enhancements enhancement = new Enhancements();
                    String line = sr.ReadLine();

                    String[] ticketSplit = line.Split(',');
                    String[] watching = ticketSplit[6].Split('|');
                    enhancement.id = ticketSplit[0];
                    enhancement.summary = ticketSplit[1];
                    enhancement.status = ticketSplit[2];
                    enhancement.priority = ticketSplit[3];
                    enhancement.submitter = ticketSplit[4];
                    enhancement.assigned = ticketSplit[5];
                    enhancement.software = ticketSplit[7];
                    enhancement.cost = ticketSplit[8];
                    enhancement.reason = ticketSplit[9];
                    enhancement.estimate = ticketSplit[10];
                    for(int i = 0; i<watching.Length; i++){
                        enhancement.watching.Add(watching[i]);
                    }
                    Enhancement.Add(enhancement);
                }
                sr.Close();
            } catch {

            }
        }
        public void AddTicket(Enhancements enhancement){
            StreamWriter sw = new StreamWriter(filePath, true);

            sw.WriteLine($"{enhancement.id},{enhancement.summary},{enhancement.status},{enhancement.priority},{enhancement.submitter},{enhancement.assigned},{String.Join("|",enhancement.watching)},{enhancement.software},{enhancement.cost},{enhancement.reason},{enhancement.estimate}");
            Enhancement.Add(enhancement);

            sw.Close();
        }
    }
}
