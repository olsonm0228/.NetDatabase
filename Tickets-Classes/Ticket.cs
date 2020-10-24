using System;
using System.Collections.Generic;
/// <summary>
/// this isnt needed delete later
/// </summary>
namespace Tickets
{
    public class Tickets
    {
        /*
            ticketId
            TicketID, Summary, Status, Priority, Submitter, Assigned, Watching, Severity
        */
        public String ticketId{get;set;}
        public String summary{get;set;}
        public String status{get;set;}
        public String priority{get;set;}
        public String submitter{get;set;}
        public String assigned{get;set;}
        public List<String> watching{get;set;}
        public String severity{get;set;}


        public Tickets(){
            watching = new List<string>();
        }


        public String Display(){
            string outputWatching = "";
            for(int i = 0; i<watching.Count;i++){
                if(i == watching.Count - 1){
                    outputWatching += watching[i];
                }else{
                    outputWatching += watching[i] + ", ";
                }
            }
            return $"Id: {ticketId}\nSummary: {summary}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {outputWatching}\nSeverity: {severity}";
        }
    }
}