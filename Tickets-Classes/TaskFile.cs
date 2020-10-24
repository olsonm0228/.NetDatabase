using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tickets_Classes
{
    public class TaskFile
    {
        public String filePath {get;set;}
        public List<Tasks> Task {get;set;}

        public TaskFile(string tasksFilePath){
            filePath = tasksFilePath;
            Task = new List<Tasks>();
            //ProjectName, DueDate
            try{
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while(!sr.EndOfStream){
                    Tasks task = new Tasks();
                    String line = sr.ReadLine();

                    String[] ticketSplit = line.Split(',');
                    String[] watching = ticketSplit[6].Split('|');
                    task.id = ticketSplit[0];
                    task.summary = ticketSplit[1];
                    task.status = ticketSplit[2];
                    task.priority = ticketSplit[3];
                    task.submitter = ticketSplit[4];
                    task.assigned = ticketSplit[5];
                    task.projectName = ticketSplit[7];
                    task.dueDate = DateTime.Parse(ticketSplit[8]);

                    for(int i = 0; i<watching.Length; i++){
                        task.watching.Add(watching[i]);
                    }
                    Task.Add(task);
                }
                sr.Close();
            } catch {

            }
        }
        public void AddTicket(Tasks task){
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine($"{task.id},{task.summary},{task.status},{task.priority},{task.submitter},{task.assigned},{String.Join("|",task.watching)},{task.projectName},{task.dueDate}");
            Task.Add(task);

            sw.Close();
        }
    }
}
