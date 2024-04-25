using System;

namespace ConcursModel.domain.validator;

public class ValidationException : ApplicationException
{
    public ValidationException(string message): base(message)
    {
        
    }
}