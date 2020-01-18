using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;


namespace SoapWebService.asmx
{
    public partial class Default : Page
    {
        string filename;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //what extensios will we allow users to upload
                string[] allowedExtension = { ".xml" };
                string fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
                //compare what was uploaded to what was permitted
                if (fileExtension == allowedExtension[0])
                {
                    //save file
                    //a path where we will store uploaded file (Server)
                    try
                    {
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Data/") + FileUpload1.FileName);
                        LblStatus.Text = "Upload successful.";
                        filename = Server.MapPath("~/Data/") + FileUpload1.FileName;
                    }
                    catch (Exception ex)
                    {
                        LblStatus.Text = "FIle cannot be uploaded. Message: " + ex.Message;
                    }
                }
                else
                {
                    LblStatus.Text = "File extension " + fileExtension + " is not allowed. ";
                }
            }
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (filename != null && filename.Length > 0)
            {
                ValidateXML(filename);
            }
        }

        private void ValidateXML(string filename)
        {
            //something that will read XML file
            XmlReader xmlReader = null;
            try
            {
                //define the settings that will use whilre reading xml file
                XmlReaderSettings settings = new XmlReaderSettings();
                //xsd
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationFlags |= System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandle);
                //validate the file with the given settings
                xmlReader = XmlReader.Create(filename, settings);
                //iterate over a xml
                while (xmlReader.Read()) { }
                LblStatus.Text = "Validation passed.";
            }
            catch (Exception ex)
            {
                LblStatus.Text = "Error validating: " + ex.Message;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }

        }
        private void ValidationEventHandle(object sender, ValidationEventArgs args)
        {
            //if we are here, it means something is wrong with our XML.
            Console.WriteLine("\r\n\tValidation Error: " + args.Message);
            LblStatus.Text = "Validation failed. message: " + args.Message;
            //Throw an exception. 
            throw new Exception("Validation failed. message: " + args.Message);
        }
    }
}