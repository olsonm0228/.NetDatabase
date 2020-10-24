using System;
using System.Collections.Generic;

namespace Tickets_Classes
{
    public abstract class TicketTypes
    {
        public String id{get;set;}
        public String summary{get;set;}
        public String status{get;set;}
        public String priority{get;set;}
        public String submitter{get;set;}
        public String assigned{get;set;}
        public List<String> watching{get;set;}

        public TicketTypes(){
            watching = new List<String>();
        }

        public virtual String Display(){return "";}
    }

    public class Ticket : TicketTypes{
        public String severity{get;set;}

        public override String Display(){
            string outputWatching = "";
            for(int i = 0; i<watching.Count;i++){
                if(i == watching.Count - 1){
                    outputWatching += watching[i];
                }else{
                    outputWatching += watching[i] + ", ";
                }
            }
            return $"Id: {id}\nSummary: {summary}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {outputWatching}\nSeverity: {severity}";
        }
    }

    public class Enhancements : TicketTypes{
        public String software{get;set;}
        public String cost{get;set;}
        public String reason{get;set;}
        public String estimate{get;set;}

        public override string Display()
        {
            string outputWatching = "";
            for(int i = 0; i<watching.Count;i++){
                if(i == watching.Count - 1){
                    outputWatching += watching[i];
                }else{
                    outputWatching += watching[i] + ", ";
                }
            }
            return $"Id: {id}\nSummary: {summary}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {outputWatching}\nSoftware: {software}\nCost: {cost}\nReason: {reason}\nEstimate: {estimate}";
        }
    }

    public class Tasks : TicketTypes{
        public String projectName;
        public DateTime dueDate;

        public override string Display()
        {
            string outputWatching = "";
            for(int i = 0; i<watching.Count;i++){
                if(i == watching.Count - 1){
                    outputWatching += watching[i];
                }else{
                    outputWatching += watching[i] + ", ";
                }
            }
            return $"Id: {id}\nSummary: {summary}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {outputWatching}\nProject Name: {projectName}\nDue Date: {dueDate.ToString()} ";
        }
    }
}