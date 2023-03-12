// Задание 1. Случайная матрица

// Console.WriteLine("Введите количество строк матрицы:");
// int rows = Convert.ToInt32(Console.ReadLine());

// Console.WriteLine("Введите количество столбцов матрицы:");
// int columns = Convert.ToInt32(Console.ReadLine());

// var random = new Random();

// Console.WriteLine("Матрица:");

// int sum = 0;

// for (int i = 0; i < rows; i++)
// {
//     string line = "";

//     for (int j = 0; j < columns; j++)
//     {   
//         int value = random.Next(0, 9);
//         line += $"{value} ";
//         sum += value;
//     }

//     Console.WriteLine(line);
// }

// Console.WriteLine($"Сумма всех элементов:\n{sum}");

// Задание 2. Сложение матриц

Console.WriteLine("Введите количество строк матрицы:");
int rows = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Введите количество столбцов матрицы:");
int columns = Convert.ToInt32(Console.ReadLine());

var random = new Random();

int[,] matrix1 = new int[rows, columns];
int[,] matrix2 = new int[rows, columns];

static void CreateMatrix(
    string title,
    int rows,
    int columns,
    Random random,
    int[,] matrix
)
{
    Console.WriteLine(title);

    for (int i = 0; i < rows; i++)
    {
        string line = "";

        for (int j = 0; j < columns; j++)
        {   
            int value = random.Next(0, 9);
            line += $"{value} ";
            matrix[i, j] = value;
        }

        Console.WriteLine(line);
    }
}

CreateMatrix("Матрица 1:", rows, columns, random, matrix1);
CreateMatrix("Матрица 2:", rows, columns, random, matrix2);

Console.WriteLine("Сумма матриц:");

for (int i = 0; i < rows; i++)
{
    string line = "";

    for (int j = 0; j < columns; j++)
    {   
        int value = matrix1[i, j] + matrix2[i, j];
        line += $"{value} ";
    }

    Console.WriteLine(line);
}
