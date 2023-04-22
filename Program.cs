using System.Globalization;
using System.Text.RegularExpressions;

// Создайте справочник «Сотрудники».

// Разработайте для предполагаемой компании программу, которая будет добавлять записи новых сотрудников в файл. Файл должен содержать следующие данные:

// ID
// Дату и время добавления записи
// Ф. И. О.
// Возраст
// Рост
// Дату рождения
// Место рождения
// Для этого необходим ввод данных с клавиатуры. После ввода данных:

// если файла не существует, его необходимо создать; 
// если файл существует, то необходимо записать данные сотрудника в конец файла. 
// При запуске программы должен быть выбор:

// введём 1 — вывести данные на экран;
// введём 2 — заполнить данные и добавить новую запись в конец файла.


// Файл должен иметь следующую структуру:

// 1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва
// 2#15.12.2021 03:12#Алексеев Алексей Иванович#24#176#05.11.1980#город Томск
// …


namespace CSharpHomeWorksNamespace
{
    class CSharpHomeWorks
    {
        static string filePath = @"records.csv";

        static void Main()
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

            byte selected = 255;
            string menu = "Введите:\n0 - выход\n1 - вывести данные на экран\n2 - добавить новую запись";

            do
            {
                Console.WriteLine(menu);
                
                try
                {
                    selected = Convert.ToByte(Console.ReadLine());

                    if (selected == 1)
                    {
                        GetRecords();
                    }

                    if (selected == 2)
                    {
                        AddRecord();
                    }
                } catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e}");
                }

            } while (selected != 0);
        }

        private static void GetRecords()
        {
            CheckFileExists();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string[] inputs = FormatInputs(sr);
                    Console.WriteLine(string.Join(" ", inputs));
                }
            }
        }

        private static string[] FormatInputs(StreamReader sr)
        {
            string[] inputs = (sr.ReadLine() ?? "").Split("#");
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == "")
                {
                    inputs[i] = "Н/д";
                }
            }

            return inputs;
        }

        private static void CheckFileExists()
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    Console.WriteLine($"Создан файл {filePath}\n");
                }
            }
        }

        private static void AddRecord()
        {
            CheckFileExists();

            using (StreamWriter sr = File.AppendText(filePath))
            {
                string[] records = File.ReadAllLines(filePath);
                string newRecord = "";

                Console.WriteLine(("Ф. И. О.:"));
                newRecord = GetInputData(newRecord);

                Console.WriteLine(("Возраст:"));
                newRecord = GetInputData(newRecord);

                Console.WriteLine(("Рост:"));
                newRecord = GetInputData(newRecord);

                Console.WriteLine(("Дата рождения:"));
                string dateOfBirth = Console.ReadLine() ?? "";
                newRecord += new Regex(@"^\d{2}\.\d{2}\.\d{4}$").IsMatch(dateOfBirth) ? $"{dateOfBirth}#" : "#";

                Console.WriteLine(("Место рождения:"));
                newRecord = GetInputData(newRecord, true);

                DateTime now = DateTime.Now;
                newRecord = $"{records.Length + 1}#{now.ToShortDateString()} {now.ToShortTimeString()}#" + newRecord;
                sr.WriteLine(newRecord);

                Console.WriteLine(("Новая запись успешно добавлена"));
            }
        }

        private static string GetInputData(string newRecord, bool lastLine = false)
        {
            newRecord += $"{Console.ReadLine()}";
            if (!lastLine)
            {
                newRecord += "#";
            }
            return newRecord;
        }
    }
}
