using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TesteMVC.Entities
{
    public class AgendaTarefas
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public DateTime Data { get; set; }

        [Required]
        public DateTime HoraInicio { get; set; }

        [Required]
        public DateTime HoraFim { get; set; }

        [Required]
        public PrioridadeEnum Prioridade { get; set; }

        [Required]
        public bool IsFinalizada { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PrioridadeEnum
    {
        Alta = 0,
        Media = 1,
        Baixa = 2
    }
}