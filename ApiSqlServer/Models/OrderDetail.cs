namespace ApiSqlServer.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }

        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime? FechaLlegada { get; set; }

        public string Status { get; set; }
    }
}
