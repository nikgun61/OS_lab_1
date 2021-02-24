// https://hackmd.io/@0x41/OS_Lab_1
using System;
using System.IO;
using System.Threading.Tasks;

namespace OS_lab_1
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase; //для сравнения строк без учёта регистра
            string Input;
            string UserCommand;
            string UserVar;

            int MaxCommands = 3;
            string[,] command = new string[MaxCommands, 3]; //Массив для команд
            // 0 - имя команды, 1 - вызов функции, 2 - описание для help
            command[0, 0] = "exit";

            void (*message) ();
            message = LogicalDrivesInfo;


            command[1, 0] = "LogicalDrivesInfo";
           // command[1, 1] = ;

            command[2, 0] = "fuck";
            MaxCommands--; //убираю одно значение для цикла for (костыль (^__^'))


            //Алгоритм для введения команд (до exit)
            Input = Console.ReadLine();
            UserCommand = GetUserCommand(Input);
            UserVar = GetUserVar(Input, UserCommand);
            while (string.Compare(UserCommand, command[0, 0], true) != 0)
            {
                for (int i = 1; i <= MaxCommands; i++)
                {
                    if (string.Compare(UserCommand, command[i, 0], true) == 0)
                    {
                        Console.WriteLine(UserVar);
                        //вызов функции command[i, 1]
                        break;
                    }
                    else if (i == MaxCommands)
                    {
                        Console.WriteLine("Неизвестная команда. Для обзора доступных команд введите help");
                    }
                }

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
            string Output = Input.Remove(0, ++length);
            return Output;
        }

        //1. Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.
        void LogicalDrivesInfo()
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
        }
    }
}
