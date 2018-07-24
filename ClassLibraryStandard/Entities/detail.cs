using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class detail
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //data annotations: autoincrement
    [Key]
    public int Id { get; set; }

    [StringLength(25)]
    public string Name { get; set; }

    [ForeignKey("maestro")]
    public int IdMaster { get; set; } //'int?' nullable, 'int' not nullable

    public virtual master master { get; set; }
}