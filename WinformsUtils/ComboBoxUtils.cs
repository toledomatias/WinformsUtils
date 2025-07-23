using System.Collections;
using System.ComponentModel;

namespace WinformsUtils
{
    public static class ComboBoxUtils
    {
        public static void LoadData(
            this ComboBox comboBox,
            IEnumerable dataSource,
            string displayMember = "",
            string valueMember = "Id"
        )
        {
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.DataSource = null;
            comboBox.DataSource = dataSource;
        }

        public static void InsertEmpty<T>(this List<T> lista) where T : class, new()
        {
            var @object = new T();
            lista.Insert(0, @object);
        }

        public static T GetSelectedObject<T>(this ComboBox comboBox) where T : class
        {
            return comboBox.SelectedItem as T;
        }

        public static int GetSelectedId(this ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null || comboBox.SelectedValue == null)
                throw new InvalidOperationException("No hay un elemento seleccionado.");

            if (comboBox.SelectedValue is int id)
                return id;

            // Si ValueMember no está bien seteado
            throw new InvalidCastException("El valor seleccionado no es un entero.");
        }

        public static int? GetSelectedIdOrNull(this ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null || comboBox.SelectedValue == null)
                return null;

            return comboBox.SelectedValue as int?;
        }

        public static void SelectById(this ComboBox comboBox, int id, string valueMember = "Id")
        {
            if (comboBox.DataSource == null)
                throw new InvalidOperationException("El ComboBox no tiene un DataSource asignado.");

            comboBox.ValueMember = valueMember;
            comboBox.SelectedValue = id;
        }

        public static void Unselectable(this ComboBox comboBox)
        {
            comboBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    comboBox.SelectedIndex = -1;
                    e.Handled = true;
                }
            };
        }

        public static void OnSelectionOrTextCleared(this ComboBox comboBox, Action<ComboBox> action)
        {
            if (comboBox == null) throw new ArgumentNullException(nameof(comboBox));
            if (action == null) throw new ArgumentNullException(nameof(action));

            comboBox.SelectedIndexChanged += (sender, e) =>
            {
                action(comboBox);
            };

            comboBox.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(comboBox.Text))
                {
                    comboBox.SelectedIndex = -1;
                    action(comboBox);
                }
            };
        }

        public static void SetDataSource(this ComboBox comboBox, object dataSource, string displayMember = "", string valueMember = "Id")
        {
            var selectedIndexChangedHandlers = comboBox
                .GetInvocationList(nameof(comboBox.SelectedIndexChanged));

            var selectedValueChangedHandlers = comboBox
                .GetInvocationList(nameof(comboBox.SelectedValueChanged));

            foreach (var h in selectedIndexChangedHandlers)
                comboBox.SelectedIndexChanged -= (EventHandler)h;

            foreach (var h in selectedValueChangedHandlers)
                comboBox.SelectedValueChanged -= (EventHandler)h;

            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.DataSource = dataSource;

            comboBox.SelectedIndex = -1;

            foreach (var h in selectedIndexChangedHandlers)
                comboBox.SelectedIndexChanged += (EventHandler)h;

            foreach (var h in selectedValueChangedHandlers)
                comboBox.SelectedValueChanged += (EventHandler)h;
        }

        private static Delegate[] GetInvocationList(this Control control, string eventName)
        {
            var field = typeof(Control).GetField("Events", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var eventHandlerList = (EventHandlerList)field?.GetValue(control);
            var keyField = typeof(ComboBox).GetField("Event" + eventName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var key = keyField?.GetValue(null);
            var del = eventHandlerList?[key];
            return del?.GetInvocationList() ?? Array.Empty<Delegate>();
        }
    }
}
