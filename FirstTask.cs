namespace Library
{
    /// <summary>
    /// Метод one принимает на вход строку inputValue
    /// и возвращает в качестве результата последовательность
    /// символов от 1 до inputValue.
    /// </summary>
    public class ZadanieOne
    {
        private static int _minimalInputValue = 1;

        public static string One(string inputValue)
        {
            try
            {
                int n = Int32.Parse(inputValue);
                if (n < _minimalInputValue) return $"Введённое число меньше {_minimalInputValue}.";
                return string.Join(", ", Enumerable.Range(1, n));
            }
            catch (FormatException)
            {
                return "Неверный формат числа.";
            }
            catch (OverflowException)
            {
                return "Число слишком большое.";
            }
        }
    }

    /// <summary>
    /// Метод Two принимает на вход строку inputValue
    /// и возвращает в качестве результата объект.
    /// В случае ошибки ввода в качестве результата
    /// вернётся строка с ошибкой, иначе вернётся двумерный массив
    /// символов '#' (квадрат) с "дыркой" в центре.
    /// </summary>
    public class ZadanieTwo
    {
        private static int _minimalInputValue = 3;

        public static object Two(string inputValue)
        {
            try
            {
                int n = Int32.Parse(inputValue);

                if (n % 2 == 0) return "Введено чётное число.";
                if (n < _minimalInputValue) return $"Введённое число меньше {_minimalInputValue}.";

                char[,] result = new char[n, n];
                int _center = n / 2;

                for (int row = 0; row < n; row++)
                {
                    for (int col = 0; col < n; col++)
                    {
                        if (row == _center && col == _center) result[row, col] = ' ';
                        else result[row, col] = '#';
                    }
                }

                return result;
            }
            catch (FormatException)
            {
                return "Неверный формат числа.";
            }
            catch (OverflowException)
            {
                return "Число слишком большое.";
            }
        }
    }
}
