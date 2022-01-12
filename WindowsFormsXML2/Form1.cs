using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace WindowsFormsXML2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string path = @".\test.xml"; //relative path where the executable is

        private void button1_Click(object sender, EventArgs e)
        {
            // Creating the XmlWriterSettings object
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; settings.IndentChars = ("  ");


            // Creating the XmlWriter object 
            XmlWriter xmlOut = XmlWriter.Create(path, settings);


            // Writing the start of the document
            xmlOut.WriteStartDocument();


            xmlOut.WriteStartElement("Root");
            xmlOut.WriteComment("This is a comment.");


            // Writing Name into the XML document
            for(int i = 1; i<6; i++){

                xmlOut.WriteStartElement("UserName"); //open child element <UserName>
                xmlOut.WriteAttributeString("attribute1", "123");

                xmlOut.WriteElementString("FName", textBox1.Text.Trim() + i); //<FName> .... </FName>
                xmlOut.WriteElementString("LName", textBox2.Text.Trim() + (i * 2));

                // write the end tag for the root element
                xmlOut.WriteEndElement();  //closing child element </UserName)

            }

            // Writing the end tag for the root element 
            xmlOut.WriteEndElement();  //close <Root> tag
            // Closing the XmlWriter object
            xmlOut.Close();

            MessageBox.Show("Created/Wrote in XML file");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Creating the XmlReaderSettings object
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            // Creating the XmlReader object
            XmlReader xmlIn = XmlReader.Create(path, settings); 

            string tempStr = "", FN = "", LN = "";

            // Reading past all nodes to the first UseName node
            if (xmlIn.ReadToDescendant("UserName"))
            {
                // Creating FirstName and LastName string for each UserName node
                do{
                    xmlIn.ReadStartElement("UserName");
                    FN = xmlIn.ReadElementContentAsString();
                    LN = xmlIn.ReadElementContentAsString();
                    tempStr += FN + ", " + LN + "\n";
                }while(xmlIn.ReadToNextSibling("UserName"));
            }

             // close the XmlReader object
            xmlIn.Close();
            MessageBox.Show(tempStr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Code for exit button
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo).ToString().ToLower() == "yes")
            {
                this.Close();
            }
        }
    }
}
