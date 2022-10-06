using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace SQLite_lab
{
    delegate void myDelegate(string[] cont);
    class addresBook
    {
        private string[] contactDetails = new string[3];
        private string paht = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AddresBook.txt";
        public string[] Data()
        {
            Console.Write("Введите имя контакта: ");
            contactDetails[0] = Console.ReadLine();
            Console.Write("Введите первый номер телефона: ");
            contactDetails[1] = Console.ReadLine();
            Console.Write("Введите второй номер телефона: ");
            contactDetails[2] = Console.ReadLine();
            return contactDetails;
        }
        public void createDB(string[] empty)
        {
            SQLiteConnection connection = new SQLiteConnection("Data source = test.db; Version = 3; Mode = ReadWriteCreate;");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                command.Connection = connection;
                command.CommandText = "CREATE TABLE contacts(_id INTEGER NOT NULL PRIMARY KEY ASC AUTOINCREMENT, name VARCHAR (2, 100), tel_number001 VARCHAR (4, 50), tel_number002 VARCHAR (4, 50))";
                command.ExecuteNonQuery();
                Console.WriteLine("Таблица contacts создана.\n");
            }
            catch
            {
                Console.WriteLine("Таблица уже создана.\n");
            }
        }
        public void addToDB(string[] _contact)
        {
            //addresBook _ab = new addresBook();
            //string[] _contact = _ab.Data();
            SQLiteConnection connection = new SQLiteConnection("Data source = test.db; Version = 3; Mode = ReadWriteCreate;");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO contacts (tel_number001, tel_number002, name) VALUES ('{_contact[1]}', '{_contact[2]}', '{_contact[0]}');";
            int number = command.ExecuteNonQuery();
            Console.WriteLine($"В таблицу contacts добавлено объектов: {number}\n");
        }
        public void writeToFile(string[] _contact)
        {
            StreamWriter SW = new StreamWriter(paht, true);
            SW.WriteLine($"Имя: {_contact[0]}\nТелефонный номрер 1: {_contact[1]}\nТелефонный номрер 2: {_contact[2]}");
            SW.Close();
        }
        public void showContacts(string[] _contact)
        {           
            Console.WriteLine($"Имя: {_contact[0]}\nТелефонный номрер 1: {_contact[1]}\nТелефонный номрер 2: {_contact[2]}\n");
        }
    }
  
    class Program
    {
        static void Main(string[] args)
        {
            addresBook AD = new addresBook();
            string[] contacts = AD.Data();
            myDelegate Choice;
            Console.Write("Что сделать с записью (1 - добавить в базу данных, 2 - записать в файл, 3 - вывести на экран, 4 - все варианта): ");
            string answer = Console.ReadLine();
            if (answer == "1" || answer == "2" || answer == "3" || answer == "4")
            {
                if (answer == "1")
                {
                    Choice = AD.createDB;
                    Choice += AD.addToDB;
                    Choice(contacts);
                }
                else if (answer == "2")
                {
                    Choice = AD.writeToFile;
                    Choice(contacts);
                }
                else if (answer == "3")
                {
                    Choice = AD.showContacts;
                    Choice(contacts);
                }
                else if (answer == "4")
                {
                    Choice = AD.createDB;
                    Choice += AD.showContacts;
                    Choice += AD.writeToFile;
                    Choice += AD.addToDB;
                    Choice(contacts);
                }
            }
            else
            {
                Console.WriteLine("Такого варианта нет.");
            }
        }
    }
}

