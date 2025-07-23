using System.Globalization;

namespace WinformsUtils
{
    public static class TextBoxUtils
    {
        public static void UseDecimalSeparator(this TextBox textBox, char decimalSeparator = ',')
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    e.KeyChar = decimalSeparator;
                }
            };
        }

        public static decimal GetDecimalValue(this TextBox textBox)
        {
            var texto = textBox.Text.Trim();

            if (decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out var resultado))
            {
                return resultado;
            }

            throw new FormatException($"El valor '{texto}' no es un número decimal válido.");
        }
    }
}
