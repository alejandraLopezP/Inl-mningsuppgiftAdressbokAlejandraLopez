using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace InlämningsuppgiftAdressbokAlejandraLopez
{
    public class Person
    {
        //public string name, adress, telefon, email;
        public string name;
        public string adress;
        public string telefon;
        public string email;

        public Person()
        {
        }

        public Person(string Name, string Adress, string Telefon, string Email)
        {
            this.name = Name;
            this.adress = Adress;
            this.telefon = Telefon;
            this.email = Email;
        }



    }
    class Program
    {
        static void Main(string[] args)
        {

            Person objPerson = new Person();

            List<Person> listPerson = new List<Person>();
            //string fileName = @"‪C:\Users\allop\adressbook.txt";
            string fileName = @"./adressbook.txt";
            

            //using (StreamReader file = new StreamReader(fileName))
            //{
            //    string line;

            //    while((line = file.ReadLine()) != null)
            //    {
            //        string[] infoPerson = line.Split(' ');
            //        listPerson.Add(new Person(infoPerson[0], infoPerson[1], infoPerson[2], infoPerson[3]));
            //    }
            //    file.Close();
            //}

            //WriteLine("{0,-15}{1,-40}{2,-50}{3,-70}","Name","Adress","Telefon","Email");
            //WriteLine("---------------------------------" +
            //    "----------------------------------------------------------------");
            //for (int i = 0; i < listPerson.Count(); i++)
            //{
            //    if (listPerson[i] != null)
            //    {
            //        WriteLine("{0,-15}{1,-40}{2,-50}{3,-70}",
            //            listPerson[i].name,listPerson[i].adress,listPerson[i].telefon,listPerson[i].email );

            //    }
            //}
            //WriteLine("---------------------------------" +
            //    "----------------------------------------------------------------");

            WriteLine("Welcome to the ADRESSBOOK!");
            WriteLine("--------------------------------MENU-------------------------------");
            WriteLine("Introduce 'exit' for to finish the program");
            WriteLine("Introduce 'introduce' for to create a contact");
            WriteLine("Introduce 'save' for to save contact information");
            WriteLine("Introduce 'update' for to edit an existens contact");
            WriteLine("Introduce 'show' for to see all the contacts");
            WriteLine("Introduce 'delete' for delete a contact");

            string command;
            Random rand = new Random();
            do
            {
                Write("> ");
                command = ReadLine().ToLower();
                if (command == "sluta")
                {
                    WriteLine("Bye bye!");
                }
                else if (command == "introduce")
                {
                    using (StreamWriter writerDoc = new StreamWriter(fileName))
                    {
                        Write("Introduce a contact NAME: ");
                        string name = ReadLine().ToUpper();
                        Write("Introduce a contact ADDRESS: ");
                        string adress = ReadLine().ToUpper();
                        Write("Introduce a contact TELEFON: ");
                        string telefon = ReadLine().ToUpper();
                        Write("Introduce a contact EMAIL: ");
                        string email = ReadLine().ToUpper();
                        WriteLine($"{name} {adress} {telefon} {email}");
                        listPerson.Add(new Person(name, adress, telefon, email));
                    }
                }
                else if (command == "save")
                {
                    using (StreamWriter writerDoc = new StreamWriter(fileName))
                    {
                        for (int i = 0; i < listPerson.Count(); i++)
                        {
                            writerDoc.WriteLine($"{listPerson[i].name} {listPerson[i].adress} {listPerson[i].telefon} {listPerson[i].email}");
                            
                        }
                    }
                    
                }
                else if (command == "delete")
                {
                    
                    StreamReader toRead = File.OpenText("adessbook.txt");
                    StreamReader toWrite = File.CreateText("tmp.txt");
                    string name, line;
                    bool founded = false;
                    Write("Enter the NAME of the contact to delete: ");
                    name = ReadLine().ToUpper();
                    line = toWrite.ReadLine();
                    try
                    {
                        toRead = File.OpenText("adressbook.txt");
                        toWrite = File.CreateText("tmp.txt");
                        while (name != null && founded == false)
                        {
                            listPerson = line.Split(" ");
                            if (listPerson[0].Trim().Equals(name))
                            {
                                WriteLine("Name: " + listPerson[0].name.Trim());
                                WriteLine("Address: " + listPerson[1].adress.Trim());
                                WriteLine("Telefon: " + listPerson[2].telefon.Trim());
                                WriteLine("Email: " + listPerson[3].email.Trim());
                                founded = true;
                            }
                            else
                            {
                                name = toRead.ReadLine();
                            }


                        }
                    catch (Exception ex)
                    {

                        ;
                    }
                   
                    }
                    //for (int i = 0; i < listPerson.Count(); i++)
                    //{
                    //    if (name == listPerson[i].name)
                    //    {
                    //        WriteLine($"Founded {name} in Adressbook {i}");
                    //       listPerson.Remove();
                    //        WriteLine($"{name} has been delete of Adressbook!");
                    //       // listPerson.Remove(objPerson);//como le digo que borre todos los datos de esa persona con ese nombre

                    //    }
                    //}
                }
                else if (command == "show")
                {
                    WriteLine("{0,-10}{1,-15}{2,-20}{3,-25}", "Name", "Adress", "Telefon", "Email");
                    WriteLine("---------------------------------" +
                        "----------------------------------------------------------------");
                    for (int i = 0; i < listPerson.Count(); i++)
                    {
                        if (listPerson[i] != null)
                        {
                            WriteLine("{0,-10}{1,-15}{2,-20}{3,-25}",
                        listPerson[i].name, listPerson[i].adress, listPerson[i].telefon, listPerson[i].email);
                        }
                    }
                    WriteLine("---------------------------------" +
                        "----------------------------------------------------------------");
                }
                else if (command == "update")
                {
                    WriteLine("Enter the NAME person to update: ");
                    string name = ReadLine().ToUpper();
                    for (int i = 0; i < listPerson.Count(); i++)
                    {
                        if (listPerson[i].name == name)
                        {
                            using (StreamWriter openDoc = File.AppendText("./adressbook.txt"))
                            {
                                
                                    openDoc.WriteLine("{0,-15}{1,-40}{2,-50}{3,-70}",
                                     listPerson[i].name, listPerson[i].adress, listPerson[i].telefon, listPerson[i].email);
                                
                            }
                            
                        }

                    }

                }
                else
                {
                    WriteLine($"Wrong command: {command}");
                }

            } while (command != "exit");


        }

    }
    
    
    
}
