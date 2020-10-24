using System;
using System.IO;

namespace Tickets_Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileTi = "ticket.csv";
            string fileEn = "enhancements.csv";
            string fileTa = "task.csv";
            Int16 choice;
            String watchingChoice;
            TicketFile ticketFile = new TicketFile(fileTi);
            EnhancementsFile enhancementsFile = new EnhancementsFile(fileEn);
            TaskFile taskFile = new TaskFile(fileTa);
            do
            {
                // ask user a question
                Console.WriteLine("1) Show all tickets data.");
                Console.WriteLine("2) Add a Ticket.");
                Console.WriteLine("3) Show all enhancements data.");
                Console.WriteLine("4) Add an enhancements.");
                Console.WriteLine("5) Show all task data.");
                Console.WriteLine("6) Add a task.");
                Console.WriteLine("7) Search based on status.");
                Console.WriteLine("8) Search based on priority.");
                Console.WriteLine("9) Search based on submitter.");
                Console.WriteLine("Enter any other number to exit.");
                // input response
                choice = Int16.Parse(Console.ReadLine());
                switch(choice){
                    case 1:
                        foreach(Ticket t in ticketFile.Ticket){
                            Console.WriteLine(t.Display());
                        }
                        break;
                    case 2:
                        if (File.Exists(fileTi))
                        {
                            Ticket ticket = new Ticket();
                            //get the data
                            Console.WriteLine("Enter the ID of the ticket.");
                            ticket.id = Console.ReadLine();
                            Console.WriteLine("Enter the summary of the ticket.");
                            ticket.summary = Console.ReadLine();
                            Console.WriteLine("Enter the status of the ticket.");
                            ticket.status = Console.ReadLine();
                            Console.WriteLine("Enter the priority of the ticket.");
                            ticket.priority = Console.ReadLine();
                            Console.WriteLine("Enter the submitter of the ticket.");
                            ticket.submitter = Console.ReadLine();
                            Console.WriteLine("Who is assigned the ticket?");
                            ticket.assigned = Console.ReadLine();

                            //loop to get who is watching
                            do
                            {
                                Console.WriteLine("Enter someone who is watching.");
                                ticket.watching.Add(Console.ReadLine());
                                Console.WriteLine("Are there more people watching Y/N.");
                                watchingChoice = Console.ReadLine();
                            } while (watchingChoice.Equals("y")||watchingChoice.Equals("Y"));

                            Console.WriteLine("What is the severity of the ticket?");
                            ticket.severity = Console.ReadLine();
                            ticketFile.AddTicket(ticket);
                        }
                        else
                        {
                            Console.WriteLine("File does not exist");
                        }
                        break;
                    case 3:
                        foreach(Enhancements e in enhancementsFile.Enhancement){
                            Console.WriteLine(e.Display());
                        }
                        break;
                    case 4:
                        if (File.Exists(fileEn))
                        {
                            Enhancements enhancement = new Enhancements();
                            //get the data
                            Console.WriteLine("Enter the ID of the enhancement.");
                            enhancement.id = Console.ReadLine();
                            Console.WriteLine("Enter the summary of the enhancement.");
                            enhancement.summary = Console.ReadLine();
                            Console.WriteLine("Enter the status of the enhancement.");
                            enhancement.status = Console.ReadLine();
                            Console.WriteLine("Enter the priority of the enhancement.");
                            enhancement.priority = Console.ReadLine();
                            Console.WriteLine("Enter the submitter of the enhancement.");
                            enhancement.submitter = Console.ReadLine();
                            Console.WriteLine("Who is assigned the enhancement?");
                            enhancement.assigned= Console.ReadLine();

                            //loop to get who is watching
                            do
                            {
                                Console.WriteLine("Enter someone who is watching.");
                                enhancement.watching.Add(Console.ReadLine());
                                Console.WriteLine("Are there more people watching Y/N.");
                                watchingChoice = Console.ReadLine();
                            } while (watchingChoice.Equals("y")||watchingChoice.Equals("Y"));

                            Console.WriteLine("What is the software fo the enhancement?");
                            enhancement.software = Console.ReadLine();
                            Console.WriteLine("What is the cost of the enhancement?");
                            enhancement.cost = Console.ReadLine();
                            Console.WriteLine("What is the reason for the enhancement?");
                            enhancement.reason = Console.ReadLine();
                            Console.WriteLine("What is the estimate for the enhancement?");
                            enhancement.estimate = Console.ReadLine();


                            enhancementsFile.AddTicket(enhancement);
                        }
                        else
                        {
                            Console.WriteLine("File does not exist");
                        }
                        break;
                    case 5:
                        foreach(Tasks t in taskFile.Task){
                            Console.WriteLine(t.Display());
                        }
                        break;
                    case 6:
                        if (File.Exists(fileTa))
                        {
                            Tasks task = new Tasks();
                            //get the data
                            Console.WriteLine("Enter the ID of the task.");
                            task.id = Console.ReadLine();
                            Console.WriteLine("Enter the summary of the task.");
                            task.summary = Console.ReadLine();
                            Console.WriteLine("Enter the status of the task.");
                            task.status = Console.ReadLine();
                            Console.WriteLine("Enter the priority of the task.");
                            task.priority = Console.ReadLine();
                            Console.WriteLine("Enter the submitter of the task.");
                            task.submitter = Console.ReadLine();
                            Console.WriteLine("Who is assigned the task.");
                            task.assigned= Console.ReadLine();

                            //loop to get who is watching
                            do
                            {
                                Console.WriteLine("Enter someone who is watching.");
                                task.watching.Add(Console.ReadLine());
                                Console.WriteLine("Are there more people watching Y/N.");
                                watchingChoice = Console.ReadLine();
                            } while (watchingChoice.Equals("y")||watchingChoice.Equals("Y"));

                            Console.WriteLine("What is the task's project name?");
                            task.projectName = Console.ReadLine();
                            Console.WriteLine("What is the due date of the Task? (MM dd, yyyy)");
                            task.dueDate = DateTime.Parse(Console.ReadLine());

                            taskFile.AddTicket(task);
                        }
                        else
                        {
                            Console.WriteLine("File does not exist");
                        }
                        break;
                    
                }
            } while (choice> 0 && choice < 7);
        }
    }
}
