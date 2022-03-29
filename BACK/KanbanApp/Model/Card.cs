using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanApp.Model
{
    public class Card
    {
        public Guid Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Conteudo { get; set; }
        [Required]
        public string Lista { get; set; }
    }
}
