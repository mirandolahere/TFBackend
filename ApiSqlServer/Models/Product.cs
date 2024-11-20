namespace ApiSqlServer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string ImagenBase64 { get; set; }
        public int Disponibilidad { get; set; }
        public DateTime? fechaExpiracion { get; set;}
    }
}
