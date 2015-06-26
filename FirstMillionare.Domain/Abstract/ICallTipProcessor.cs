using FirstMillionare.Domain.Entities;

namespace FirstMillionare.Domain.Abstract
{
    public interface ICallTipProcessor {

        void ProcessQuestion(QuestionItem question);
    }
}