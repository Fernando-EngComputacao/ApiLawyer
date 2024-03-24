namespace API_Lawyer.Core;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class PathWay : Attribute
{
    public string XmlFilePath { get; }

    public PathWay(string xmlFilePath)
    {
        XmlFilePath = xmlFilePath;
    }
}