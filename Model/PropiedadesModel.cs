namespace WebApplication2.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
[Table("propiedades")]
public class PropiedadesModel
{
    public int id { get; set; }
    public string direccion { get; set; }
    public string tipo { get; set; }

    [ForeignKey("propietario_id")]
    public int propietario_id { get; set; }
}
