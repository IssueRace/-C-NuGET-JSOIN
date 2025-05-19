using System;
using System.Xml;

public class XmlReaderExample
{
    public static void Example()
    {
        using (XmlReader reader = XmlReader.Create("example.xml"))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    Console.WriteLine($"Element: {reader.Name}");
                }
            }
        }
    }
}
