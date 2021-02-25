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

            int MaxCommands = 20;
            int ComID = 0;
            string UserPath = null;

            string[,] command = new string[MaxCommands, 2]; //Названия и описание

            command[0, 0] = "exit";

            // Работа с каталогами и дисками
            command[1, 0] = "Cd";
            command[1, 1] = "Возвращение в домашний каталог и выведение информации в консоль о логических дисках, именах, метке тома, размере типе файловой системы.";

            command[2, 0] = "Ls";
            command[2, 1] = "Получение списка файлов и подкаталогов в текущем каталоге, для перехода в нужный - введите адрес через пробел, начиная с текущего положения (для возвращения используется cd => ls и путь, куда необходимо попасть).";

            command[3, 0] = "Mkdir";
            command[3, 1] = "Создаёт новый каталог в текущем каталоге. Имя каталога указывается через пробел.";

            command[4, 0] = "Pwd";
            command[4, 1] = "Получение информации о текущем каталоге. Имя каталога указывается через пробел.";

            command[5, 0] = "Rmdir";
            command[5, 1] = "Удаление каталога. Имя каталога указывается через пробел.";

            command[6, 0] = "Mvdir";
            command[6, 1] = "Перемещение каталога. Имя каталога указывается через пробел.";

            //Работа с файлами. Классы File и FileInfo
            command[7, 0] = "Stat";
            command[7, 1] = "Получение информации о файле. Имя файла указывается через пробел.";

            command[8, 0] = "Rm";
            command[8, 1] = "Удаление файла. Имя файла указывается через пробел.";

            command[9, 0] = "Mv";
            command[9, 1] = "Перемещение файла. Имя файла указывается через пробел.";
            
            command[10, 0] = "Cp";
            command[10, 1] = "Копирование файла. Имя файла указывается через пробел.";

            //FileStream.Чтение и запись файла
            command[11, 0] = "FSWrite";
            command[11, 1] = "Запись в файл. Имя файла указывается через пробел.";

            command[12, 0] = "FSRead";
            command[12, 1] = "Чтение из файла. Имя файла указывается через пробел.";




            command[18, 0] = "Bash";
            command[19, 0] = "Fuck";
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
                        Console.WriteLine("Неизвестная команда '" + UserCommand + "'. Для обзора доступных команд введите Bash");
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
                        UserPath = Ls(UserPath, UserVar);
                        Console.WriteLine("Каталог " + UserVar + " удалён.");
                        break;

                    case 6:
                        UserPath = Mvdir(UserPath, UserVar);
                        UserPath = Ls(UserPath, UserVar);
                        Console.WriteLine("Новый файл " + UserVar + " создан.");
                        break;

                    case 7:
                        Stat(UserPath, UserVar);
                        break;

                    case 8:
                        Rm(UserPath, UserVar);
                        UserPath = Ls(UserPath, UserVar);
                        Console.WriteLine("Файл " + UserVar + " удалён.");
                        break;

                    case 9:
                        UserPath = Mv(UserPath, UserVar);
                        UserPath = Ls(UserPath, UserVar);
                        Console.WriteLine("Файл " + UserVar + " перемещён.");
                        break;

                    case 10:
                        UserPath = Cp(UserPath, UserVar);
                        UserPath = Ls(UserPath, UserVar);
                        Console.WriteLine("Файл " + UserVar + " скопирован.");
                        break;

                    case 11:
                        FSWrite(UserPath, UserVar);
                        FSRead(UserPath, UserVar);
                        break;

                    case 12:
                        FSRead(UserPath, UserVar);
                        break;

                    case 18:
                        for (int i = 1; i < (MaxCommands-1); i++)
                        {
                            Console.WriteLine(" " + command[i, 0] + " - " + command[i, 1]);
                        }
                        break;

                    case 19:
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
        public static string AddBackSlash(string UserPath, string UserVar)
        {
            if (UserPath == null)
                return UserVar;
            
            string BackSlash = @"\";
            var chars = UserVar.ToCharArray();
            for (int i = 0; i < 1; i++)
            {
                string f = null;
                f += chars[i];
                if (string.Compare(f, BackSlash, true) != 0)
                    UserVar = BackSlash + UserVar;
            }         
            return UserVar;
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
            UserVar = AddBackSlash(UserPath, UserVar);
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
            UserVar = AddBackSlash(UserPath, UserVar);
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
            UserVar = AddBackSlash(UserPath, UserVar);
            UserPath += UserVar;
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(UserPath);
                dirInfo.Delete(true);
                Console.WriteLine("Каталог" + UserVar + "удален.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string Mvdir (string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserPath, UserVar);
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

        static void Stat(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserPath, UserVar);
            UserPath += UserVar;
            FileInfo fileInf = new FileInfo(UserPath);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
        }

        static void Rm(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserPath, UserVar);
            UserPath += UserVar;
            FileInfo fileInf = new FileInfo(UserPath);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                // альтернатива с помощью класса File
                // File.Delete(path);
            }
        }

        static string Mv(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserPath, UserVar);
            Console.WriteLine("Старый путь до папки: " + UserPath + " , введите новый путь: ");
            UserPath += UserVar;
            string NewPath = Console.ReadLine() + UserVar;
            FileInfo fileInf = new FileInfo(UserPath);
            if (fileInf.Exists)
            {
                fileInf.MoveTo(NewPath);
                // альтернатива с помощью класса File
                // File.Move(path, newPath);
            }
            return UserPath;
        }

        static string Cp(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserPath, UserVar);
            Console.WriteLine("Введите путь, куда скопировать папку: ");
            UserPath += UserVar;
            string NewPath = Console.ReadLine() + UserVar;
            FileInfo fileInf = new FileInfo(UserPath);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(NewPath, true);
                // альтернатива с помощью класса File
                // File.Copy(path, newPath, true);
            }
            return UserPath;
        }

        static void FSWrite(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserPath, UserVar);
            UserPath += UserVar;
            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();
            // запись в файл
            using (FileStream fstream = new FileStream($"{UserPath}", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл.");
            }
        }

        static void FSRead(string UserPath, string UserVar)
        {
            UserVar = AddBackSlash(UserPath, UserVar);
            UserPath += UserVar;
            // чтение из файла
            using (FileStream fstream = File.OpenRead($"{UserPath}"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
        }
    }
}
