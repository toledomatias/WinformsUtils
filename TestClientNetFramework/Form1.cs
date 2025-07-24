using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrasladosCMC.Lineas;
using WinformsUtilsNetFramework;

namespace TestClientNetFramework
{
    public partial class Form1 : Form
    {
        List<LineaConsultaRentabilidadEmpresas> _testData;

        public Form1()
        {
            InitializeComponent();

            _testData = new List<LineaConsultaRentabilidadEmpresas>
{
            new LineaConsultaRentabilidadEmpresas
            {
                ClienteId = 1,
                Nombre = "Transporte Norte",
                CantidadViajes = 120,
                KmPromedio = 8.5m,
                KmsCliente = 1020m,
                TotalCliente = 255000m,
                KmsMovil = 1100m,
                TotalMovil = 230000m,
                MontoRentabilidad = 25000m,
                PorcentajeRentabilidad = .109m,
                Participacion = .205m,
                InterfazEsTotal = false
            },
            new LineaConsultaRentabilidadEmpresas
            {
                ClienteId = 2,
                Nombre = "Remises del Centro",
                CantidadViajes = 95,
                KmPromedio = 6.2m,
                KmsCliente = 589m,
                TotalCliente = 142000m,
                KmsMovil = 620m,
                TotalMovil = 138000m,
                MontoRentabilidad = 4000m,
                PorcentajeRentabilidad = .29m,
                Participacion = .153m,
                InterfazEsTotal = false
            },
            new LineaConsultaRentabilidadEmpresas
            {
                ClienteId = 3,
                Nombre = "Logística Express",
                CantidadViajes = 200,
                KmPromedio = 12.4m,
                KmsCliente = 2480m,
                TotalCliente = 520000m,
                KmsMovil = 2500m,
                TotalMovil = 500000m,
                MontoRentabilidad = 20000m,
                PorcentajeRentabilidad = .40m,
                Participacion = .302m,
                InterfazEsTotal = false
            },
            new LineaConsultaRentabilidadEmpresas
            {
                ClienteId = 0,
                Nombre = "TOTAL",
                CantidadViajes = 415,
                KmPromedio = 9.8m,
                KmsCliente = 4089m,
                TotalCliente = 917000m,
                KmsMovil = 4220m,
                TotalMovil = 868000m,
                MontoRentabilidad = 49000m,
                PorcentajeRentabilidad = .53m,
                Participacion = 1m,
                InterfazEsTotal = true
            }
        };

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gr.Configure<LineaConsultaRentabilidadEmpresas>(GridUtils.ColDefs.New()
                .String("Nombre", "Empresa").Width(20)
                .Integer("CantidadViajes", "Cant. viajes")
                .Decimal("KmsCliente", "Kms. Cliente")
                .Decimal("KmPromedio", "Kms. Promedio")
                .Decimal("TotalCliente", "Total Cliente")
                .Decimal("KmsMovil", "Kms. Móvil")
                .Decimal("TotalMovil", "Total Móvil")
                .Decimal("MontoRentabilidad", "Monto rent.")
                .Percentage("PorcentajeRentabilidad", "% rent.")
                .Percentage("Participacion", "% part. rent."),
                new[] {
                    new GridUtils.GridButton(button1)
                });

            gr.DataSource = _testData;
        }
    }
}
