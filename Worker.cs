using System.Text.RegularExpressions;

namespace CSharpHomeWorksNamespace
{
    /// <summary>
    /// Структура, описывающая данные сотрудника
    /// </summary> 
    struct Worker
    {
        /// <summary>
        /// ID сотрудника
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Дата и время добавления данных сотрудника
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Ф.И.О. сотрудника
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Рост сотрудника
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Дата рождения сотрудника
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Место рождения сотрудника
        /// </summary>
        public string? PlaceOfBirth { get; set; }

        /// <summary>
        /// Вывод данных сотрудника в консоль
        /// </summary>
        public void Print(string format)
        {
            Console.WriteLine(
                format,
                this.Id,
                GetCreated(),
                this.FullName,
                this.Age,
                this.Height,
                this.DateOfBirth?.ToShortDateString(),
                this.PlaceOfBirth
            );
        }

        /// <summary>
        /// Форматирование данных сотрудника
        /// </summary>
        public string Format()
        {
            return
                $"{this.Id}#" +
                $"{GetCreated()}#" +
                $"{this.FullName}#" +
                $"{this.Age}#" +
                $"{this.Height}#" +
                $"{this.DateOfBirth?.ToShortDateString()}#" +
                $"{this.PlaceOfBirth}";
        }

        /// <summary>
        /// Чтение данных сотрудника
        /// </summary>
        public Worker(string Id, string Created, string FullName, string Age, string Height, string DateOfBirth, string PlaceOfBirth)
        {
            this.Id = Convert.ToInt32(Id);
            this.Created = GetDateTime(Created);
            this.FullName = FullName;
            this.Age = Convert.ToByte(Age);
            this.Height = Convert.ToByte(Height);
            this.DateOfBirth = Convert.ToDateTime(DateOfBirth);
            this.PlaceOfBirth = PlaceOfBirth;
        }

        /// <summary>
        /// Добавление данных сотрудника
        /// </summary>
        public Worker(int Id, DateTime Created, string FullName, int Age, int Height, DateTime DateOfBirth, string PlaceOfBirth)
        {
            this.Id = Id;
            this.Created = Created;
            this.FullName = FullName;
            this.Age = Age;
            this.Height = Height;
            this.DateOfBirth = DateOfBirth;
            this.PlaceOfBirth = PlaceOfBirth;
        }

        private Nullable<DateTime> GetDateTime(string dateTime)
        {
            if (dateTime.Length > 0 && new Regex(@"^\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}$").IsMatch(dateTime))
            {
                string[] dateTimeArray = dateTime.Split(" ");
                string[] dateArray = dateTimeArray[0].Split(".");
                string[] timeArray = dateTimeArray[1].Split(":");
                return new DateTime(
                    Convert.ToInt32(dateArray[2]),
                    Convert.ToInt32(dateArray[1]),
                    Convert.ToInt32(dateArray[0]),
                    Convert.ToInt32(timeArray[0]),
                    Convert.ToInt32(timeArray[1]),
                    0
                );
            }
            return null;
        }

        private string GetCreated()
        {
            return $"{this.Created?.ToShortDateString()} {this.Created?.ToShortTimeString()}";
        }
    }
}
