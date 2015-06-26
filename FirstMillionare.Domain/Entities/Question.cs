using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstMillionare.Domain.Entities
{
    [Table("tblQuestions")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Column("Question")]
        public string QuestionText { get; set; }
        public int Complexity { get; set; }
    }
}
