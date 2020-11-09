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
    //LINK REFERENCES:https://www.youtube.com/watch?v=NgQCVrCiPp4&pbjreload=101
    //https://www.youtube.com/watch?v=4Oh71Pmlz7E
    //https://docs.microsoft.com/en-us/dotnet/api/system.io.file.replace?view=netcore-3.1
    //https://es.stackoverflow.com/questions/127148/como-hacer-una-list-de-un-objeto-y-guardarlo-en-un-txt-separado-por-tabulacione
    //https://www.youtube.com/watch?v=NgQCVrCiPp4&feature=youtu.be
    //Private programer lesson
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
    }

    public class BookContac
    {
        private readonly string _filePath;
        //CONSTRUCTOR FOR TO GET THE PATH OF THE FILE
        public BookContac(string filePath)
        {
            //INITIALIZATION OF LIST OF CONTACT
            _filePath = filePath;
            Peoples = new List<Person>();
            ConvertFileToListPeoples(_filePath);
        }

        // Declaration of people list
        public List<Person> Peoples { get; set; }
        //FILL LIST OF CONTACT WITH THE INFO
        public void ConvertFileToListPeoples(string path)
        {
            using StreamReader file = new StreamReader(path);
            string line;
            int numLine = 1;
            while ((line = file.ReadLine()) != null)
            {
                if (line != "")
                {
                    string[] infoPerson = line.Split(" | ");
                    Peoples.Add(
                        new Person
                        {
                            Id = Guid.Parse(infoPerson[0]),
                            Name = infoPerson[1],
                            Adress = infoPerson[2],
                            Telefon = infoPerson[3],
                            Email = infoPerson[4]
                        }
                    );
                }

                numLine++;
            }
            file.Close();
        }

        public string ConvertPersonToLineFile(Person person)
        {
            return $"{ person.Id.ToString() } | { person.Name } | { person.Adress } | { person.Telefon } | { person.Email }";
        }

        public void Add(Person person)
        {
            Peoples.Add(person);
            var line = ConvertPersonToLineFile(person);
            using (StreamWriter sw = File.AppendText(_filePath))
            {
                sw.WriteLine(line);
            }
        }

        public List<Person> FindByName(string name)
        {
            return Peoples.Where(p => p.Name.ToUpper().Contains(name.ToUpper())).ToList();
        }

        public List<Person> GetAll()
        {
            return Peoples.Select(p => p).ToList();
        }

        public Person FindByCodePerson(string code)
        {
            return Peoples.Single(p => p.Id.ToString().Substring(0, 8).ToUpper() == code.ToUpper());
        }

        public void Delete(string code)
        {
            var person = FindByCodePerson(code);
            Peoples.Remove(person);
            SyncFile(Peoples);
        }

        public void Update(Person per)
        {
            var personOLd = Peoples.Single(p => p.Id == per.Id);
            personOLd.Name = per.Name;
            personOLd.Adress = per.Adress;
            personOLd.Telefon = per.Telefon;
            personOLd.Email = per.Email;
            SyncFile(Peoples);
        }

        public void SyncFile(List<Person> personList)
        {
            string pathCopy = @".\adressbookCopy.txt";
            foreach (Person person in personList)
            {
                var persoLine = ConvertPersonToLineFile(person);
                File.AppendAllText(pathCopy, persoLine);
            }

            File.Delete(_filePath);
            File.Move(pathCopy, _filePath);
        }

    };


    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @".\adressbook.txt";
            BookContac bookContac = new BookContac(fileName);
            WriteLine("Welcome to the ADRESSBOOK!");
            WriteLine("--------------------------------MENU-------------------------------");
            WriteLine("Introduce 'exit' for to finish the program"); // finish the program 
            WriteLine("Introduce 'introduce' for to create a contact"); // create and SAVE new Person
            WriteLine("Introduce 'update' for to edit an existens contact by Id"); // edit the Contact information
                                                                                   //For to UPDATE you must know the person Id so: number 1: use 'find' then 2: use 'update'
            WriteLine("Introduce 'show' for to see all the contacts"); // show all the Persons in the Adressbook
            WriteLine("Introduce 'delete' for delete a contact by Id"); //delete a Contact
            WriteLine("Introduce 'find' for to search a contact by Name"); //Search contact in the Adressbook

            string command;

            do
            {
                Write("> ");
                command = ReadLine().ToLower();
                if (command == "exit")
                {
                    WriteLine("Bye bye!");
                }
                else if (command == "introduce")
                {
                    Person person = new Person();
                    person.Id = Guid.NewGuid();
                    Write("Introduce a contact NAME: ");
                    person.Name = ReadLine().ToUpper();
                    Write("Introduce a contact ADDRESS: ");
                    person.Adress = ReadLine().ToUpper();
                    Write("Introduce a contact TELEFON: ");
                    person.Telefon = ReadLine().ToUpper();
                    Write("Introduce a contact EMAIL: ");
                    person.Email = ReadLine().ToUpper();
                    WriteLine("Press Enter to Save the contact");
                    ReadKey();
                    bookContac.Add(person);

                }
                else if (command == "delete")
                {
                    WriteLine("Introduce the Code(Id) of the contact that you want delete, if you dont know the Id use 'find' option first: ");
                    var code = ReadLine();
                    bookContac.Delete(code);
                }
                else if (command == "show")
                {
                    WriteLine("{0,-10}{1,-20}{2,-25}{3,-30}{4,-35}", "Id", "Name", "Adress", "Telefon", "Email");
                    WriteLine("---------------------------------" +
                        "----------------------------------------------------------------");
                    var listPerson = bookContac.GetAll();
                    foreach (var person in listPerson)
                    {
                        WriteLine("{0,-10}{1,-20}{2,-25}{3,-30}{4,-35}",
                        person.Id.ToString().Substring(0, 8).ToUpper(), person.Name, person.Adress, person.Telefon, person.Email);
                    }
                    WriteLine("---------------------------------" +
                        "----------------------------------------------------------------");
                }
                else if (command == "find")
                {
                    WriteLine("Introduce the NAME of the contact that you want find: ");
                    var name = ReadLine();

                    WriteLine("{0,-10}{1,-20}{2,-25}{3,-30}{4,-35}", "Id", "Name", "Adress", "Telefon", "Email");
                    WriteLine("---------------------------------" +
                        "----------------------------------------------------------------");
                    var listPerson = bookContac.FindByName(name);
                    foreach (var person in listPerson)
                    {
                        WriteLine("{0,-10}{1,-20}{2,-25}{3,-30}{4,-35}",
                        person.Id.ToString().Substring(0, 8).ToUpper(), person.Name, person.Adress, person.Telefon, person.Email);
                    }
                    WriteLine("---------------------------------" +
                        "----------------------------------------------------------------");
                }
                else if (command == "update")
                {
                    WriteLine("Introduce the Code(Id)XXXXXXXX of the contact that you want update, if you dont know the Id use 'find' option first: ");
                    var code = ReadLine();
                    var person = bookContac.FindByCodePerson(code);
                    var personOld = bookContac.FindByCodePerson(code);

                    WriteLine($"This is the contact to edit:{personOld.Name} {personOld.Adress} {personOld.Telefon} {personOld.Email} ");
                    Write("Introduce a contact NAME: ");
                    person.Name = ReadLine().ToUpper();
                    Write("Introduce a contact ADDRESS: ");
                    person.Adress = ReadLine().ToUpper();
                    Write("Introduce a contact TELEFON: ");
                    person.Telefon = ReadLine().ToUpper();
                    Write("Introduce a contact EMAIL: ");
                    person.Email = ReadLine().ToUpper();
                    bookContac.Update(person);
                }
                else
                {
                    WriteLine($"Wrong command: {command}");
                }

            } while (command != "exit");


        }

    }



}