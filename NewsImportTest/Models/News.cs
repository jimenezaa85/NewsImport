using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsImportTest.Models
{
    public class News
    {
        public string unid { get; set; }
        public int nr { get; set; }
        public string language { get; set; }

        public string id { get; set; }

        public DateTime date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Categories { get; set; }

        public string Margin1 { get; set; }

        public string Margin2 { get; set; }

        public string Margin3 { get; set; }

        public string Content1 { get; set; }

        public string Content2 { get; set; }

        public string Content3 { get; set; }
        public News()
        {
            this.Categories = new List<string>();
        }
    }
}