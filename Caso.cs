namespace MinimalAPICasosIVR
{
    public class Caso
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? NroEnvio { get; set; }
        public int Cuit { get; set; }
        public string? Motivo { get; set; }
        public string? EmailContacto { get; set; }
        public string? EmailReclamante { get; set; }
    }
}
