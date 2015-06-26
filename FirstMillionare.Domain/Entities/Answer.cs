using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstMillionare.Domain.Entities
{
    [Table("tblAnswers")]
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int OptionId { get; set; }
    }
}
