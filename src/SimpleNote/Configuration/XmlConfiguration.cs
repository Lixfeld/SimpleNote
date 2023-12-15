using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleNote.Configuration
{
    public class XmlConfiguration
    {
        public const string Filename = "Config.xml";

        public double WindowTop { get; set; }
        public double WindowLeft { get; set; }
        public double WindowHeight { get; set; }
        public double WindowWidth { get; set; }
        public string NoteText { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private XmlConfiguration()
        {
            // Only for XmlSerialzer
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public XmlConfiguration(double windowTop, double windowLeft, double windowHeight, double windowWidth, string noteText)
        {
            WindowTop = windowTop;
            WindowLeft = windowLeft;
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
            NoteText = noteText;
        }

        public static XmlConfiguration Load()
        {
            string text = File.ReadAllText(Filename, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlConfiguration));
            return xmlSerializer.DeserializeObject<XmlConfiguration>(text);
        }

        public void Save()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlConfiguration));
            string text = xmlSerializer.SerializeObject(this, indent: true);
            File.WriteAllText(Filename, text, Encoding.UTF8);
        }
    }
}
