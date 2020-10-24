using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tickets_Classes
{
    public class TicketFile
    {
        public String filePath {get;set;}
        public List<Ticket> Ticket {get;set;}

        public TicketFile(string ticketFilePath){
            filePath = ticketFilePath;
            Ticket = new List<Ticket>();

            try{
                StreamReader sr = new StreamReader(filePath);
                sr.ReadLine();
                while(!sr.EndOfStream){
                    Ticket ticket = new Ticket();
                    String line = sr.ReadLine();

                    String[] ticketSplit = line.Split(',');
                    String[] watching = ticketSplit[6].Split('|');
                    ticket.id = ticketSplit[0];
                    ticket.summary = ticketSplit[1];
                    ticket.status = ticketSplit[2];
                    ticket.priority = ticketSplit[3];
                    ticket.submitter = ticketSplit[4];
                    ticket.assigned = ticketSplit[5];
                    ticket.severity = ticketSplit[7];
                    for(int i = 0; i<watching.Length; i++){
                        ticket.watching.Add(watching[i]);
                    }
                    Ticket.Add(ticket);
                }
                sr.Close();
            } catch {

            }
        }


        public void AddTicket(Ticket ticket){
            StreamWriter sw = new StreamWriter(filePath, true);

            sw.WriteLine($"{ticket.id}, {ticket.summary}, {ticket.status}, {ticket.priority}, {ticket.submitter}, {ticket.assigned}, {String.Join("|",ticket.watching)}, {ticket.severity}");
            Ticket.Add(ticket);

            sw.Close();
        }
    }
}
