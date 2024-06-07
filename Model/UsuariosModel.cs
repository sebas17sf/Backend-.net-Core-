using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Model
{
    [Table("usuarios")]
    public class UsuariosModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        
        [ForeignKey("role_id")]
        public int role_id { get; set; } 
 
          
    }
}