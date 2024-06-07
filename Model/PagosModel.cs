namespace WebApplication2.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pagos")]
public class PagosModel
{
    public int id { get; set; }
    public double monto { get; set; }
    public DateTime fecha_pago { get; set; }
    public string descripcion { get; set; }

    [ForeignKey("usuario_id")]
    public int usuario_id { get; set; }

    [ForeignKey("propiedad_id")]
    public int propiedad_id { get; set; }
}