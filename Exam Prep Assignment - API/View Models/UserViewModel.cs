using System;

namespace Exam_Prep_Assignment___API.View_Models
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string username { get; set; }
        public string Password { get; set; }
        public bool valid { get; set; }
        public bool usernameValid { get; set; }
        public bool passwordValid { get; set; }
        public int otp { get; set; }
    }
}
