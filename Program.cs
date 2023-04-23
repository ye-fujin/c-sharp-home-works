using System.Globalization;

// Улучшите программу, которую разработали в модуле 6. Создайте структуру Worker со следующими свойствами:

// ID
// Дата и время добавления записи
// Ф.И.О.
// Возраст
// Рост
// Дата рождения
// Место рождения


// Структура будет выглядеть примерно так:

//     struct Worker
//     {
//         public int Id { get; set; }
//         public string FIO { get; set; }
//     … другие свойства
//     }


// Создайте класс Repository, который будет отвечать за работу с экземплярами Worker.

// В репозитории должны быть реализованы следующие функции:

// Просмотр всех записей.
// Просмотр одной записи. Функция должна на вход принимать параметр ID записи, которую необходимо вывести на экран. 
// Создание записи.
// Удаление записи.
// Загрузка записей в выбранном диапазоне дат.


// Структура класса Repository примерно такая:

// class Repository
//     {
//         public Worker[] GetAllWorkers()
//         {
//             // здесь происходит чтение из файла
//             // и возврат массива считанных экземпляров
//         }

//         public Worker GetWorkerById(int id)
//         {
//             // происходит чтение из файла, возвращается Worker
//             // с запрашиваемым ID
//         }

//         public void DeleteWorker(int id)
//         {
//             // считывается файл, находится нужный Worker
//             // происходит запись в файл всех Worker,
//             // кроме удаляемого
//         }

//         public void AddWorker(Worker worker)
//         {
//             // присваиваем worker уникальный ID,
//             // дописываем нового worker в файл
//         }

//         public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
//         {
//             // здесь происходит чтение из файла
//             // фильтрация нужных записей
//             // и возврат массива считанных экземпляров
//         }
//     }


// Имя файла должно храниться в приватном поле Repository. Файл, который использует репозиторий, должен выглядеть примерно так:

// 1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва
// 2#15.12.2021 03:12#Алексеев Алексей Иванович#24#176#05.11.1980#город Томск
// …


// Используя структуру Worker и класс Repository, в основном методе Main реализуйте программу для работы с записями. Также предоставьте пользователю возможность сортировать данные по разным полям.

namespace CSharpHomeWorksNamespace
{

    class CSharpHomeWorks
    {
        static void Main()
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Repository repository = new Repository(@"records.csv");

            byte selected = 255;
            string menu = "Введите:\n" +
                "0 - Выход\n" +
                "1 - Просмотр всех записей\n" +
                "2 - Просмотр одной записи\n" +
                "3 - Создание записи\n" +
                "4 - Удаление записи\n" +
                "5 - Просмотр записей в выбранном диапазоне дат рождения";

            do
            {
                Console.WriteLine(menu);
                
                try
                {
                    selected = Convert.ToByte(Console.ReadLine());
                    List<Worker> workers;
                    switch (selected)
                    {
                        case 1:
                            workers = repository.GetAllWorkers();
                            repository.PrintWorkers(workers);
                            break;
                        case 2:
                            workers = repository.GetWorker();
                            repository.PrintWorkers(workers);
                            break;
                        case 3:
                            repository.AddWorker();
                            break;
                        case 4:
                            repository.DeleteWorker();
                            break;
                        case 5:
                            workers = repository.GetWorkersByDateRange();
                            repository.PrintWorkers(workers);
                            break;
                        default:
                            break;
                    }
                } catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e}");
                }

            } while (selected != 0);
        }
    }
}
