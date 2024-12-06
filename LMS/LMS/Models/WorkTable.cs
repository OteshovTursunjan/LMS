using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class WorkTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("GroupId")]
        public Group? Group { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }
        public int SubjectId { get; set; }
    }
}
