using System;
using System.ComponentModel.DataAnnotations.Schema; 
namespace WebApplication2.Model;
[Table("roles")]
public class RolesModel
{
public int id { get; set; }
public string nombre { get; set; }
public string descripcion { get; set; }
  
}