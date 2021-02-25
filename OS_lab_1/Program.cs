//https://hackmd.io/@0x41/OS_Lab_1
using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace OS_lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase; //для сравнения строк без учёта регистра

            int MaxCommands = 10;
            int ComID = 0;
            string UserPath = null;

            string[,] command = new string[MaxCommands, 2]; //Названия и описание

            command[0, 0] = "exit";


            command[1, 0] = "cd";
            command[1, 1] = "Возвращает в домашний каталог и выводит информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы.";

            command[2, 0] = "ls";
            command[2, 1] = "Получение списка файлов и подкаталогов в текущем каталоге, для перехода в нужный - введите адрес через пробел (для возвращения используется cd => ls и путь, куда необходимо попасть).";

            command[3, 0] = "mkdir";
            command[3, 1] = "Создаёт новый каталог в текущем каталоге.";

            command[4, 0] = "pwd";
            command[4, 1] = "Получение информации о текущем каталоге";

            command[5, 0] = "rmdir";
            command[5, 1] = "Удаление каталога";

            command[6, 0] = "mv";

            command[9, 0] = "fuck";
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
                        Console.WriteLine("Неизвестная команда '" + UserCommand + "'. Для обзора доступных команд введите bash");
                        break;

                    case 1:
                        UserPath = Cd();
                        break;

                    case 2:
                        UserPath = Ls(UserPath, UserVar);
                        break;

                    case 3:
                        UserVar = Mkdir(UserPath, UserVar);
                        UserPath = Ls(UserPath, UserVar);
                        Console.WriteLine("Новый каталог " + UserVar + " создан.");
                        break;

                    case 4:
                        Pwd(UserPath);
                        break;

                    case 5:
                        Rmdir(UserPath, UserVar);
                        break;

                    case 6:
                        UserPath = Mv(UserPath, UserVar);
                        break;

                    case 9:
                        Console.WriteLine("hey you, mazafaka, tired enough? Its time for D oh double G");
                        break;
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
            if (Input.Length == length)
                return null;
            else
            {
                string Output = Input.Remove(0, ++length);
                return Output;
            }
        }

        //ебать копать костыль, я с ним возился больше часа, если кто-то знает вариант попроще, как проверить и, если что, добавить "\" - подскажите пожалуйста
        public static string AddBackSlash(string Input)
        {
            string BackSlash = @"\";
            var chars = Input.ToCharArray();
            for (int i = 0; i < 1; i++)
            {
                string f = null;
                f += chars[i];
                if (string.Compare(f, BackSlash, true) != 0)
                    Input = BackSlash + Input;
            }         
            return Input;
        }

        static string Cd()
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

        static string Ls(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserVar);
            UserPath += UserVar;

            if (Directory.Exists(UserPath))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(UserPath);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(UserPath);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                }
            }
            return UserPath;
        }

        static string Mkdir(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserVar);
            DirectoryInfo dirInfo = new DirectoryInfo(UserPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(UserVar);
            return null;
        }

        static void Pwd(string UserPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(UserPath);

            Console.WriteLine($"Название каталога: {dirInfo.Name}");
            Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
            Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
            Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
        }

        static void Rmdir(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserVar);
            UserPath += UserVar;
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(UserPath);
                dirInfo.Delete(true);
                Console.WriteLine("Каталог удален");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string Mv (string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserVar);
            Console.WriteLine("Старый путь до папки: " + UserPath + " , введите новый путь: ");
            UserPath += UserVar;
            string NewPath = Console.ReadLine() + UserVar;
            DirectoryInfo dirInfo = new DirectoryInfo(UserPath);
            if (dirInfo.Exists && Directory.Exists(NewPath) == false)
            {
                dirInfo.MoveTo(NewPath);
            }
            return UserPath;
        }
    }
}
