using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstMillionare.Domain.Entities
{
    [Table("tblOptions")]
    public class Option
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
    }
}
