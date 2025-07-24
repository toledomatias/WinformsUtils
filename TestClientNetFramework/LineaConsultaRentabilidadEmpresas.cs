namespace TrasladosCMC.Lineas
{
    public class LineaConsultaRentabilidadEmpresas
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public int CantidadViajes { get; set; }
        public decimal KmPromedio { get; set; }
        public decimal KmsCliente { get; set; }
        public decimal TotalCliente { get; set; }
        public decimal KmsMovil { get; set; }
        public decimal TotalMovil { get; set; }
        public decimal MontoRentabilidad { get; set; }
        public decimal PorcentajeRentabilidad { get; set; }
        public decimal Participacion { get; set; }
        public bool InterfazEsTotal { get; set; }
    }
}
