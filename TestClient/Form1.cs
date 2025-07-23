using WinformsUtils;

namespace TestClient
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }


    public partial class Form1 : Form
    {
        private List<Persona> personas;

        public Form1()
        {
            InitializeComponent();
            CargarDatos();
            ConfigurarGrilla();
        }

        private void CargarDatos()
        {
            personas = new List<Persona>
            {
                new Persona { Id = 1, Nombre = "Ana", Saldo = 1500.50m, FechaNacimiento = new DateTime(1990, 5, 12) },
                new Persona { Id = 2, Nombre = "Carlos", Saldo = 200.75m, FechaNacimiento = new DateTime(1985, 3, 25) },
                new Persona { Id = 3, Nombre = "Lucía", Saldo = 3200m, FechaNacimiento = new DateTime(1995, 9, 1) }
            };
        }

        private void ConfigurarGrilla()
        {
            // Definición de columnas
            var cols = GridUtils.ColDefs.New()
                .Hidden(nameof(Persona.Id))
                .String(nameof(Persona.Nombre), "Nombre completo").Width(25)
                .Decimal(nameof(Persona.Saldo), "Saldo").Right()
                .Date(nameof(Persona.FechaNacimiento), "Nacimiento");

            // Botón que depende de la selección
            var btnVer = new Button { Text = "Ver detalle", Left = 10, Top = 250, Width = 100 };
            btnVer.Click += (s, e) =>
            {
                if (gr.CurrentRow?.DataBoundItem is Persona p)
                    MessageBox.Show($"Nombre: {p.Nombre}\nSaldo: {p.Saldo:N2}", "Detalle");
            };
            this.Controls.Add(btnVer);

            // Configurar grilla con botones
            gr.Configure<Persona>(
                cols,
                buttons: new List<GridUtils.GridButton> { new GridUtils.GridButton(btnVer) }
            );

            // Cargar los datos en el DataSource
            gr.DataSource = personas;
        }
    }
}
