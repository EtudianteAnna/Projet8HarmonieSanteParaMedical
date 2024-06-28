using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjet8
{
    internal class ModelResponses
    {
       public string Token { get; set; }
}

public class RegisterResponse
{
    public string Message { get; set; }
}

public class ForgotPasswordResponse
{
    public string Message { get; set; }
}

public class ResetPasswordResponse
{
    public string Message { get; set; }
}



public class ErrorResponse
{
    public string Message { get; set; }
}
}
