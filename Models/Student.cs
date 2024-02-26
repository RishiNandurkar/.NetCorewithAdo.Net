using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADODemo.Models
{
    public class Student
    {     
            [Key]
            public int Id { get; set; }
            [Required]
            [Column(TypeName = "nvarchar(250)")]
            public string Name { get; set; }

            public int ContactNumber { get; set; }
            public int Age { get; set; }     
    }
}
