using Exam_Prep_Assignment___API.View_Models;

namespace Exam_Prep_Assignment___API.EmailServices
{
    public interface IEmailService
    {
        bool SendEmail(Email email);
    }
}
