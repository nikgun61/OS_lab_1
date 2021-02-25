//https://hackmd.io/@0x41/OS_Lab_1
using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace OS_lab_1
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase; //для сравнения строк без учёта регистра
            
            int MaxCommands = 3;
            int ComID = 0;
            string UserPath = null;

            string[,] command = new string[MaxCommands, 2]; //Названия и описание
            
            command[0, 0] = "exit";

            command[1, 0] = "LogicalDrivesInfo";
            
            command[2, 0] = "fuck";
            MaxCommands--; //убираю одно значение для цикла for (костыль (^__^'))


            //Алгоритм для введения команд (до exit)
            string Input = Console.ReadLine();
            string UserCommand = GetUserCommand(Input);
            string UserVar = GetUserVar(Input, UserCommand);
            while (string.Compare(UserCommand, command[0, 0], true) != 0)
            {
                for (int i = 1; i <= MaxCommands; i++)
                {
                    if (string.Compare(UserCommand, command[i, 0], true) == 0)
                    {
                        ComID = i;
                        break;
                    }    
                }
                switch (ComID)
                {
                    case 0:
                        Console.WriteLine("Неизвестная команда'" + UserCommand + "'. Для обзора доступных команд введите help");
                        break;

                    case 1:
                        UserPath = LogicalDrivesInfo();
                        break;

                    case 2:
                        Console.WriteLine("hey you, mazafuka");
                        break;
                }
                ComID = 0;
                
                Input = Console.ReadLine();
                UserCommand = GetUserCommand(Input);
                UserVar = GetUserVar(Input, UserCommand);
            }
            Console.WriteLine(UserVar);
            Environment.Exit(0);
        }

        
        public static string GetUserCommand(string Input)
        {
            int length = Input.Length;
            string Output = null;
            var chars = Input.ToCharArray();
            for (int i = 0; i < length; i++)
            {
                if (chars[i] != ' ')
                    Output += chars[i];
                else
                    if (Output != null)
                    break;
            }
            return Output;
        }

        public static string GetUserVar(string Input, string UserCommand)
        {
            int length = UserCommand.Length;
            if (Input.Length == length)
                return null;
            else
            {
                string Output = Input.Remove(0, ++length);
                return Output;
            }
        }

        //1. Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.
        static string LogicalDrivesInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
            }

            return null;
        }
        static string NewCatalog(string UserPath, string Uservar)
        {
            string path = @"C:\SomeDir";
            int length = UserPath.Length;
            int WrightPath1 = 0;
            var chars = UserPath.ToCharArray();
            for (int i = 0; i <= length; i++)
            {
                if (chars[i] == ':')
                    WrightPath1++;
                else if (chars[i] == '\' && WrightPath1 == 1)


            }
            string subpath = @"program\avalon";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);

            return null;
        }
    }
}
