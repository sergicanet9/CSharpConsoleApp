using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("master")]
public class master
{
    public master()
    {
        detail = new HashSet<detail>();
    }

    [Key]
    public int Id { get; set; }

    [StringLength(45)]
    public string Name { get; set; }

    public virtual ICollection<detail> detail { get; set; }

}