namespace ApiSqlServer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime? FechaLlegada { get; set; }

        public int Status { get; set; }
    }
}
