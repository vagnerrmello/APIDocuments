using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIDocuments.Models
{
    [Table("Documentos")]
    public class Documento
    {
        [Key]
        public int DocumentoId { get; set; }

        [Required(ErrorMessage = "Tipo é obrigatório!")]
        public int Tipo{ get; set; }

        [Required(ErrorMessage = "Vencimento é obrigatório!")]
        public DateTime Vencimento { get; set; }

        public DateTime Pagamento { get; set; }

        [Required(ErrorMessage = "Path é obrigatório!")]
        public string Path { get; set; }

        public int Status { get; set; }

        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
    }
}
