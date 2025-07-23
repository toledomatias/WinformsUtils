using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace WinformsUtilsNetFramework
{
    public class Validator
    {
        private readonly List<Func<bool>> _rules = new List<Func<bool>>();
        private readonly ErrorProvider _errorProvider;

        public Validator(ErrorProvider errorProvider = null)
        {
            _errorProvider = errorProvider;
        }

        public void Add(Func<bool> rule)
        {
            _rules.Add(rule);
        }

        public bool Validate()
        {
            bool allOk = true;

            foreach (var rule in _rules)
            {
                if (!rule())
                    allOk = false;
            }

            return allOk;
        }

        public void ClearErrors()
        {
            _errorProvider?.Clear();
        }

        public void TextBoxHasValue(TextBox textBox)
        {
            _rules.Add(() =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    _errorProvider.SetError(textBox, "Este campo es obligatorio.");
                    return false;
                }

                _errorProvider.SetError(textBox, "");
                return true;
            });
        }

        public void TextBoxIsInteger(TextBox textBox)
        {
            _rules.Add(() =>
            {
                if (!int.TryParse(textBox.Text.Trim(), out _))
                {
                    _errorProvider.SetError(textBox, "Debe ingresar un número entero válido.");
                    return false;
                }

                _errorProvider.SetError(textBox, "");
                return true;
            });
        }

        public void TextBoxIsDecimal(TextBox textBox)
        {
            _rules.Add(() =>
            {
                if (!decimal.TryParse(textBox.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out _))
                {
                    _errorProvider.SetError(textBox, "Debe ingresar un número decimal válido.");
                    return false;
                }

                _errorProvider.SetError(textBox, "");
                return true;
            });
        }
    }
}
