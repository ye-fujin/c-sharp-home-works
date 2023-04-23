using System.Text.RegularExpressions;

namespace CSharpHomeWorksNamespace
{
    /// <summary>
    /// Класс, который отвечает за работу с данными сотрудников
    /// </summary>
    internal class Repository
    {
        private string FilePath { get; set; }

        private string[] columns = new string[] {
            "ID", "Дата и время добавления записи", "Ф.И.О.", "Возраст", "Рост", "Дата рождения", "Место рождения"
        };

        private string format = "{0,-10:N0} {1,-35} {2,-35} {3,-7:N0} {4,-5:N0} {5,-15:N0} {6,-35}";

        public Repository(string FilePath)
        {
            this.FilePath = FilePath;
        }

        /// <summary>
        /// Получение данных всех сотрудников
        /// </summary>
        public List<Worker> GetAllWorkers()
        {
            CheckFileExists();
            List<Worker> workers = new List<Worker>();

            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] record = line.Split("#");
                        if (record.Length == columns.Length)
                        {
                            workers.Add(new Worker(record[0], record[1], record[2], record[3], record[4], record[5], record[6]));
                        }
                        else
                        {
                            Console.WriteLine($"Число колонок не равно {columns.Length}");
                        }
                    }
                    
                }
            }
            return workers;
        }

        /// <summary>
        /// Получение данных сотрудника по Id
        /// </summary>
        public List<Worker> GetWorker()
        {
            CheckFileExists();

            try
            {
                int id = GetWorkerId();
                List<Worker> workers = GetAllWorkers();

                return workers.FindAll(delegate (Worker worker)
                {
                    return worker.Id == id;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");
            }

            return new List<Worker>();
        }

        /// <summary>
        /// Добавление данных сотрудника
        /// </summary>
        public void AddWorker()
        {
            CheckFileExists();

            List<Worker> workers = GetAllWorkers();

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                Console.WriteLine("Ф. И. О.:");
                string fullName = Console.ReadLine() ?? "";

                Console.WriteLine("Возраст:");
                byte age = Convert.ToByte(Console.ReadLine());

                Console.WriteLine("Рост:");
                byte height = Convert.ToByte(Console.ReadLine());

                Console.WriteLine("Дата рождения в формате dd.mm.yyyy:");
                DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine() ?? "");

                Console.WriteLine("Место рождения:");
                string placeOfBirth = Console.ReadLine() ?? "";

                Worker worker = new Worker()
                {
                    Id = workers.Count + 1,
                    Created = DateTime.Now,
                    FullName = fullName,
                    Age = age,
                    Height = height,
                    DateOfBirth = dateOfBirth,
                    PlaceOfBirth = placeOfBirth
                };

                workers.Add(worker);
                
                workers.ForEach(delegate(Worker worker)
                {
                    sw.WriteLine(worker.Format());
                });

                Console.WriteLine("Данные нового сотрудника успешно добавлены");
            }
        }

        /// <summary>
        /// Удаление данных сотрудника по Id
        /// </summary>
        public void DeleteWorker()
        {
            CheckFileExists();

            try
            {
                int id = GetWorkerId();
                List<Worker> workers = GetAllWorkers();

                int removed = workers.RemoveAll(delegate(Worker worker)
                {
                    return worker.Id == id;
                });

                if (removed > 0)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        workers.ForEach(delegate(Worker worker)
                        {
                            sw.WriteLine(worker.Format());
                        });
                    }

                    Console.WriteLine($"Данные сотрудников с Id {id} успешно удалены");
                }
                else
                {
                    Console.WriteLine("Сотрудники не найдены");
                }
            } catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");
            }
        }

        /// <summary>
        /// Получение данных сотрудников по диапазону дат рождения и сортировка по дате рождения
        /// </summary>
        public List<Worker> GetWorkersByDateRange()
        {
            CheckFileExists();

            List<Worker> workers = GetAllWorkers();

            Console.WriteLine("От даты рождения в формате dd.mm.yyyy:");
            DateTime dateOfBirthFrom = Convert.ToDateTime(Console.ReadLine() ?? "");

            Console.WriteLine("До даты рождения в формате dd.mm.yyyy:");
            DateTime dateOfBirthTo = Convert.ToDateTime(Console.ReadLine() ?? "");

            if (dateOfBirthTo < dateOfBirthFrom)
            {
                throw new Exception("Введена некорректная дата");
            }

            workers = workers.FindAll(delegate (Worker worker)
            {
                return worker.DateOfBirth >= dateOfBirthFrom && worker.DateOfBirth <= dateOfBirthTo;
            });

            workers.Sort(delegate(Worker workerA, Worker workerB)
            {
                if (workerA.DateOfBirth > workerB.DateOfBirth) return 1;
                if (workerA.DateOfBirth < workerB.DateOfBirth) return -1;
                return 0;
            });

            return workers;
        }

        /// <summary>
        /// Выводит данные сотрудников в консоль
        /// </summary>
        public void PrintWorkers(List<Worker> workers)
        {
            if (workers.Count > 0)
            {   
                Console.WriteLine(
                    format,
                    columns[0],
                    columns[1],
                    columns[2],
                    columns[3],
                    columns[4],
                    columns[5],
                    columns[6]
                );

                workers.ForEach(delegate(Worker worker)
                {
                    worker.Print(format);
                });
            }
            else
            {
                Console.WriteLine("Сотрудники не найдены");
            }
        }

        private static int GetWorkerId()
        {
            Console.WriteLine("Введите Id сотрудника:");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        private void CheckFileExists()
        {
            if (!File.Exists(FilePath))
            {
                using (FileStream fs = File.Create(FilePath))
                {
                    Console.WriteLine($"Создан файл {FilePath}\n");
                }
            }
        }
    }
}
