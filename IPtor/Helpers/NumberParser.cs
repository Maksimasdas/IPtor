using System.Globalization;

namespace IPtor.Helpers
{
    public static class NumberParser
    {
        //Парсит float из строки. Поддерживает запятую и точку как разделитель.
        public static float ParseFloat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            string normalized = input.Replace(',', '.');

            if (float.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
                return result;

            return 0;
        }

        // Парсит int из строки. Возвращает 0 если не число.
        public static int ParseInt(string input)
        {
            return int.TryParse(input, out int result) ? result : 0;
        }
    }
}
