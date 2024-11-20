namespace ApiSqlServer.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProductoId { get; set; }
        public string Numero { get; set; }
        public string Email { get; set; }

        public string EnCamino { get; set; }
    }
}
