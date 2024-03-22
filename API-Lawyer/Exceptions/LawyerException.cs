using System.Net;

namespace API_Lawyer.Exceptions;

public class LawyerException : Exception
{
    public LawyerException(string message) : base(message)
    {
    }

    public LawyerException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public LawyerException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public LawyerException(string message, Exception innerException, HttpStatusCode statusCode) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public LawyerException(string erros, string titulo, string descricao) : base(erros)
    {
        Titulo = titulo;
        Descricao = descricao;
    }
    public HttpStatusCode StatusCode { get; set; }

    public string Titulo { get; }
    public string Descricao { get; }
}
