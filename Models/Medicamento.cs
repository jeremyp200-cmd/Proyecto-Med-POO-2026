namespace ProyectoAPI.Models
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
    }
}