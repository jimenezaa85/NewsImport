using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NewsImportTest.Repositories
{
    public class NewsImportRepository
    {

        public NewsImportRepository()
        {

        }

        public bool GetNewsByName(string name)
        {
            Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            Item parentItem = masterDb.Items["/sitecore/content/Lanxess/Intranet/Home/archive"];
            List<Item> listItems = parentItem.Axes.GetDescendants().Where(x => x.Name == ItemUtil.ProposeValidItemName(name).ToString()).ToList();
            if (listItems.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void CreateNewsItem(XElement element)
        {
            using (new SecurityDisabler())
            {
                Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
                Item parentItem = masterDb.Items["/sitecore/content/Lanxess/Intranet/Home/archive"];
                TemplateItem template = masterDb.Items["/sitecore/templates/Project/Lanxess/Articles/News"];

                Item newItem = parentItem.Add(ItemUtil.ProposeValidItemName(element.Element("id").Value).ToString(), template);
                newItem.Editing.BeginEdit();
                try
                {
                    newItem.Fields["Main Title"].Value = ItemUtil.ProposeValidItemName(element.Element("headline").Value).ToString();
                    newItem.Fields["Abstract"].Value = ItemUtil.ProposeValidItemName(element.Element("abstract").Value).ToString();
                    string[] date = element.Element("date").Value.ToString().Split('.');
                    newItem.Fields["Publish Date"].Value = date[2].ToString() + date[1].ToString() + date[0].ToString();

                    newItem.Editing.EndEdit();
                }
                catch (System.Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Could not update item " + newItem.Paths.FullPath + ": " + ex.Message, this);
                    newItem.Editing.CancelEdit();
                }
            }
        }

        public bool GetTagByName(string name)
        {
            Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            Item parentTagItem = masterDb.Items["/sitecore/content/Lanxess/Intranet/Data/Tags"];
            List<Item> tagItem = parentTagItem.Axes.GetDescendants().Where(x => x.Name == ItemUtil.ProposeValidItemName(name)).ToList();
            if (tagItem.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateNewTag(string name)
        {
            using (new SecurityDisabler())
            {
                Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
                Item parentTagItem = masterDb.Items["/sitecore/content/Lanxess/Intranet/Data/Tags"];
                TemplateItem template = masterDb.Items["/sitecore/templates/Feature/Experience Accelerator/Taxonomy/Datasource/Tag"];

                Item newTagItem = parentTagItem.Add(ItemUtil.ProposeValidItemName(name).ToString(), template);
                newTagItem.Editing.BeginEdit();
                try
                {
                    newTagItem.Fields["Title"].Value = ItemUtil.ProposeValidItemName(name).ToString();
                    newTagItem.Editing.EndEdit();
                }
                catch (System.Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Could not update item " + newTagItem.Paths.FullPath + ": " + ex.Message, this);
                    newTagItem.Editing.CancelEdit();
                }
            }
        }
    }
}