using NewsImportTest.Models;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using NewsImportTest.Repositories;
using NewsImportTest.Services;

namespace NewsImportTest.sitecore.admin
{
    public partial class NewsImport : System.Web.UI.Page
    {
        private readonly NewsImportRepository db = new NewsImportRepository();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoadXml_Click(object sender, EventArgs e)
        {
            StringBuilder log = new StringBuilder();
            var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath + "\\export_en.xml";
            XDocument xmlFile = XDocument.Load(path);

            Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            Item tag = masterDb.Items["/sitecore/content/Lanxess/Intranet/Home/archive/2018/09/8f8egp-de_virtuelle-bauarbeiten"];
            log.Append(tag.Fields["Tags"].Value.ToString());
           /* MultilistField multilist = parentItem.Fields["Tags"];
            foreach(Item prueba in multilist.GetItems())
            {
                log.Append(prueba.ToString());


            }*/
            

            /*IEnumerable<XElement> NewsElements =
                from el in xmlFile.Descendants("doc")
                where DateTime.ParseExact(el.Element("date").Value.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture) >= startDate.SelectedDate
                && DateTime.ParseExact(el.Element("date").Value.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture) <= endDate.SelectedDate
                select el;*/

            /*IEnumerable<XElement> NewsElements =
                from el in xmlFile.Descendants("doc")
                select el;*/

            
            /*foreach (XElement el in NewsElements)
            {
                foreach (XElement category in el.Element("categories").Descendants("category"))
                {
                    bool tagExists = db.GetTagByName(category.Value.ToString());
                    if (!tagExists)
                    {
                        db.CreateNewTag(category.Value.ToString());
                    }
                }

                bool newsExists = db.GetNewsByName(el.Element("id").Value.ToString());
                if (!newsExists)
                {
                    db.CreateNewsItem(el);
                }
            }*/
            lblLog.Text = log.ToString();
        }

        protected void btnLocal_Click(object sender, EventArgs e)
        {
            string sFilePath = @"C:\Imagenes\";
            FileServiceLocal servicio = new FileServiceLocal(sFilePath);
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(sFilePath + "lanxess.jpg"));
            lblLog.Text=servicio.Create("lanxess.jpg", ms);

            
        }
    }
}