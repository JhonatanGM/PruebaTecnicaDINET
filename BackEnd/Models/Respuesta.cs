namespace BackEnd.Models
{
    public class Respuesta
    {
        public string messagePrivado { get; set; }
        public string messagePublico { get; set; }
        public object results { get; set; }

        public Respuesta(string messagePrivado, string messagePublico, object results)
        {
            this.messagePrivado = messagePrivado;
            this.messagePublico = messagePublico;
            this.results = results;
        }
        public Respuesta(string messagePrivado, string messagePublico)
        {
            this.messagePrivado = messagePrivado;
            this.messagePublico = messagePublico;
        }
    }
}
