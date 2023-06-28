namespace Library
{
    public class ZadanieOne
    {
        public static void One()
        {
            int n;
            Console.Write("Введите число N: ");
            if (!(int.TryParse(Console.ReadLine(), out n)))
            {
                Console.WriteLine("Некорректный ввод."); //If N is not a number, exit the function.
                return;
            }
            else if (n < 1)
            {
                Console.WriteLine("Введённое число меньше единицы."); //If N < 1, exit the function.
                return;
            }
            string result = string.Join(", ", Enumerable.Range(1, n));
            Console.WriteLine(result);
        }
    }
    public class ZadanieTwo
    {
        public static void Two()
        {
            Console.Write("Введите нечетное число N: ");
            int n;
            if (!(int.TryParse(Console.ReadLine(), out n)))
            {
                Console.WriteLine("Некорректный ввод."); //If N is not a number, exit the function.
                return;
            }
            if (n < 3)
            {
                Console.WriteLine("Введённое число меньше трёх."); //If N < 3, exit the function.
                return;
            }
            if (n % 2 == 0) // If N is an even number, exit the function.
            {
                Console.WriteLine("Вы ввели четное N.");
                return;
            }            
            for (int row = 1; row <= n; row++) {
                for (int col = 1; col <= n; col++) {
                    if (row == n / 2 + 1 && col == n / 2 + 1) Console.Write(" ");
                    else Console.Write("#");                    
                }
                Console.WriteLine();
            }
        }
    }
}
