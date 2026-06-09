namespace Ocorrencias.DTO
{
    public class Ocorrencia
    {
        public int Id { get; set; }

        public string Tipo { get; set; } = "";

        public string Descricao { get; set; } = "";

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Status { get; set; } = "Aberta";

        public DateTime DataAbertura { get; set; } = DateTime.Now;
    }
}