namespace WebApplication2.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[Table("mantenimiento")]
public class MantenimientoModel
{
    public int id { get; set; }
    public string descripcion { get; set; }
    public DateTime fecha_solicitud { get; set; }
    public DateTime fecha_completado { get; set; }
    public string estado { get; set; }

    [ForeignKey("propiedad_id")]
    public int propiedad_id { get; set; }
}