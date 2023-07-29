using System.Xml.Serialization;

namespace StandUpKitV2.Migrations.Models;

[XmlRoot("CustomTransform")]
public class CustomTransform
{
    [XmlElement("ClassFile")]
    public CustomFile ClassFile { get; set; }
}

public class CustomFile
{
    [XmlAttribute("path")]
    public string Path { get; set; }

    [XmlText]
    public string CodeContent { get; set; }
}