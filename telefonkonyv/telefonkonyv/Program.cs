using System;
using System.IO;

class Program
{
    struct Person
    {
        public string Name;
        public string Address;
        public string FatherName;
        public string MotherName;
        public long MobileNo;
        public string Sex;
        public string Mail;
        public string CitizenNo;
    }

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.Clear();
        Start();
    }

    static void Start()
    {
        Menu();
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("\t\t**********WELCOME TO PHONEBOOK*************");
        Console.WriteLine("\n\n\t\t\t MENU\t\t\n\n");
        Console.WriteLine("\t1.Add New \t2.List \t3.Exit \n\t4.Modify \t5.Search\t6.Delete");
        switch (Console.ReadKey().KeyChar)
        {
            case '1':
                AddRecord();
                break;
            case '2':
                ListRecord();
                break;
            case '3':
                Environment.Exit(0);
                break;
            case '4':
                ModifyRecord();
                break;
            case '5':
                SearchRecord();
                break;
            case '6':
                DeleteRecord();
                break;
            default:
                Console.Clear();
                Console.WriteLine("\nEnter 1 to 6 only");
                Console.WriteLine("\n Enter any key");
                Console.ReadKey();
                Menu();
                break;
        }
    }

    static void AddRecord()
    {
        Console.Clear();
        Person p;
        using (FileStream fs = new FileStream("project.dat", FileMode.Append))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            Console.Write("\n Enter name: ");
            p.Name = Console.ReadLine();
            Console.Write("\nEnter the address: ");
            p.Address = Console.ReadLine();
            Console.Write("\nEnter father name: ");
            p.FatherName = Console.ReadLine();
            Console.Write("\nEnter mother name: ");
            p.MotherName = Console.ReadLine();
            Console.Write("\nEnter phone no.: ");
            p.MobileNo = long.Parse(Console.ReadLine());
            Console.Write("Enter sex: ");
            p.Sex = Console.ReadLine();
            Console.Write("\nEnter e-mail: ");
            p.Mail = Console.ReadLine();
            Console.Write("\nEnter citizen no: ");
            p.CitizenNo = Console.ReadLine();

            writer.Write(p.Name);
            writer.Write(p.Address);
            writer.Write(p.FatherName);
            writer.Write(p.MotherName);
            writer.Write(p.MobileNo);
            writer.Write(p.Sex);
            writer.Write(p.Mail);
            writer.Write(p.CitizenNo);

            Console.WriteLine("\nrecord saved");
        }
        Console.WriteLine("\n\nEnter any key");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }

    static void ListRecord()
    {
        Console.Clear();
        Person p;
        using (FileStream fs = new FileStream("project.dat", FileMode.OpenOrCreate))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            while (fs.Position < fs.Length)
            {
                p.Name = reader.ReadString();
                p.Address = reader.ReadString();
                p.FatherName = reader.ReadString();
                p.MotherName = reader.ReadString();
                p.MobileNo = reader.ReadInt64();
                p.Sex = reader.ReadString();
                p.Mail = reader.ReadString();
                p.CitizenNo = reader.ReadString();

                Console.WriteLine("\n\n\n YOUR RECORD IS\n\n ");
                Console.WriteLine($"Name={p.Name}\nAdress={p.Address}\nFather name={p.FatherName}\nMother name={p.MotherName}\nMobile no={p.MobileNo}\nSex={p.Sex}\nE-mail={p.Mail}\nCitizen no={p.CitizenNo}");
                Console.ReadKey();
                Console.Clear();
            }
        }
        Console.WriteLine("\n Enter any key");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }

    static void SearchRecord()
    {
        Console.Clear();
        Person p;
        using (FileStream fs = new FileStream("project.dat", FileMode.OpenOrCreate))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            Console.Write("\nEnter name of person to search\n");
            string name = Console.ReadLine();

            bool recordFound = false;
            while (fs.Position < fs.Length)
            {
                p.Name = reader.ReadString();
                p.Address = reader.ReadString();
                p.FatherName = reader.ReadString();
                p.MotherName = reader.ReadString();
                p.MobileNo = reader.ReadInt64();
                p.Sex = reader.ReadString();
                p.Mail = reader.ReadString();
                p.CitizenNo = reader.ReadString();

                if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\n\tDetail Information About {name}");
                    Console.WriteLine($"Name={p.Name}\nAdress={p.Address}\nFather name={p.FatherName}\nMother name={p.MotherName}\nMobile no={p.MobileNo}\nSex={p.Sex}\nE-mail={p.Mail}\nCitizen no={p.CitizenNo}");
                    recordFound = true;
                    break;
                }
            }

            if (!recordFound)
            {
                Console.WriteLine("File not found");
            }
        }
        Console.WriteLine("\n Enter any key");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }

    static void DeleteRecord()
    {
        Console.Clear();
        Person p;
        bool flag = false;

        using (FileStream fs = new FileStream("project.dat", FileMode.OpenOrCreate))
        using (BinaryReader reader = new BinaryReader(fs))
        using (FileStream tempFs = new FileStream("temp.dat", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(tempFs))
        {
            Console.Write("Enter CONTACT'S NAME:");
            string name = Console.ReadLine();

            while (fs.Position < fs.Length)
            {
                p.Name = reader.ReadString();
                p.Address = reader.ReadString();
                p.FatherName = reader.ReadString();
                p.MotherName = reader.ReadString();
                p.MobileNo = reader.ReadInt64();
                p.Sex = reader.ReadString();
                p.Mail = reader.ReadString();
                p.CitizenNo = reader.ReadString();

                if (!p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    writer.Write(p.Name);
                    writer.Write(p.Address);
                    writer.Write(p.FatherName);
                    writer.Write(p.MotherName);
                    writer.Write(p.MobileNo);
                    writer.Write(p.Sex);
                    writer.Write(p.Mail);
                    writer.Write(p.CitizenNo);
                }
                else
                {
                    flag = true;
                }
            }
        }

        if (flag)
        {
            File.Delete("project.dat");
            File.Move("temp.dat", "project.dat");
            Console.WriteLine("RECORD DELETED SUCCESSFULLY.");
        }
        else
        {
            Console.WriteLine("NO CONTACT'S RECORD TO DELETE.");
            File.Delete("temp.dat");
        }

        Console.WriteLine("\n Enter any key");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }

    static void ModifyRecord()
    {
        Console.Clear();
        Person p;
        Person s;
        bool flag = false;

        using (FileStream fs = new FileStream("project.dat", FileMode.OpenOrCreate))
        using (BinaryReader reader = new BinaryReader(fs))
        using (FileStream tempFs = new FileStream("temp.dat", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(tempFs))
        {
            Console.Write("\nEnter CONTACT'S NAME TO MODIFY:\n");
            string name = Console.ReadLine();

            while (fs.Position < fs.Length)
            {
                p.Name = reader.ReadString();
                p.Address = reader.ReadString();
                p.FatherName = reader.ReadString();
                p.MotherName = reader.ReadString();
                p.MobileNo = reader.ReadInt64();
                p.Sex = reader.ReadString();
                p.Mail = reader.ReadString();
                p.CitizenNo = reader.ReadString();

                if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("\n Enter name: ");
                    s.Name = Console.ReadLine();
                    Console.Write("\nEnter the address: ");
                    s.Address = Console.ReadLine();
                    Console.Write("\nEnter father name: ");
                    s.FatherName = Console.ReadLine();
                    Console.Write("\nEnter mother name: ");
                    s.MotherName = Console.ReadLine();
                    Console.Write("\nEnter phone no: ");
                    s.MobileNo = long.Parse(Console.ReadLine());
                    Console.Write("\nEnter sex: ");
                    s.Sex = Console.ReadLine();
                    Console.Write("\nEnter e-mail: ");
                    s.Mail = Console.ReadLine();
                    Console.Write("\nEnter citizen no: ");
                    s.CitizenNo = Console.ReadLine();

                    writer.Write(s.Name);
                    writer.Write(s.Address);
                    writer.Write(s.FatherName);
                    writer.Write(s.MotherName);
                    writer.Write(s.MobileNo);
                    writer.Write(s.Sex);
                    writer.Write(s.Mail);
                    writer.Write(s.CitizenNo);

                    flag = true;
                }
                else
                {
                    writer.Write(p.Name);
                    writer.Write(p.Address);
                    writer.Write(p.FatherName);
                    writer.Write(p.MotherName);
                    writer.Write(p.MobileNo);
                    writer.Write(p.Sex);
                    writer.Write(p.Mail);
                    writer.Write(p.CitizenNo);
                }
            }
        }

        if (flag)
        {
            File.Delete("project.dat");
            File.Move("temp.dat", "project.dat");
            Console.WriteLine("\n Record updated successfully");
        }
        else
        {
            Console.WriteLine("\n Record not found");
            File.Delete("temp.dat");
        }

        Console.WriteLine("\n Enter any key");
        Console.ReadKey();
        Console.Clear();
        Menu();
    }
}
