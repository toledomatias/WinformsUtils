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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gr.Configure<LineaConsultaRentabilidadEmpresas>(GridUtils.ColDefs.New()
                .String("Empresa", "Nombre").Fill()
                .Integer("Cant. viajes", "CantidadViajes")
                .Decimal("Kms. Cliente", "KmsCliente")
                .Decimal("Kms. Promedio", "KmsPromedio")
                .Decimal("Total Cliente", "TotalCliente")
                .Decimal("Kms. Móvil", "KmsMovil")
                .Decimal("Total Móvil", "TotalMovil")
                .Decimal("Monto rent.", "MontoRentabilidad")
                .Decimal("% rent.", "PorcentajeRentabilidad")
                .Decimal("% part. rent.", "Participacion"),
                new[] {
                    new GridUtils.GridButton(button1)
                });

        }
    }
}
