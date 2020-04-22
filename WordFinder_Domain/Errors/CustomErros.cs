using System;

namespace WordFinder_Domain.Errors
{
    [Serializable]
    public class InvalidLoginException : Exception
    {
        public string Login { get; }
        
        public InvalidLoginException() {}
        
        public InvalidLoginException(string message) : base(message) {}
        
        public InvalidLoginException(string message, Exception inner) : base(message, inner) {}

        public InvalidLoginException(string message, string login) : this(message)
        {
            Login = login;
        }
    }
    
    [Serializable]
    public class InvalidEmailException : Exception
    {
        public string Email { get; }
        
        public InvalidEmailException() {}
        
        public InvalidEmailException(string message) : base(message) {}
        
        public InvalidEmailException(string message, Exception inner) : base(message, inner) {}

        public InvalidEmailException(string message, string email) : this(message)
        {
            Email = email;
        }
    }
}