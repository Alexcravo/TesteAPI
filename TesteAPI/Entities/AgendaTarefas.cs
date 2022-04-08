using System.ComponentModel.DataAnnotations;

namespace TesteAPI.Entities
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
        public Prioridade Prioridade { get; set; }

        [Required]
        public string IsFinalizada { get; set; }
    }

    public enum Prioridade
    {
        Alta = 0,
        Media = 1,
        Baixa = 2
    }
}
