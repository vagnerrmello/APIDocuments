using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIDocuments.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public Usuario()
        {
            Documentos = new Collection<Documento>();
        }

        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caractere")]
        [MinLength(10, ErrorMessage = "Deve ter o mínimo de 10 Caractere")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caractere")]
        [MinLength(5, ErrorMessage = "Deve ter o mínimo de 5 Caractere")]
        public string email { get; set; }

        [Required(ErrorMessage = "password é obrigatório!")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caractere")]
        [MinLength(8, ErrorMessage = "Deve ter o mínimo de 8 Caractere")]
        public string password { get; set; }

        [Required]
        public bool ativo { get; set; }

        public ICollection<Documento> Documentos { get; set; }

    }

    

}
