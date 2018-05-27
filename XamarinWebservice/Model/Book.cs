using System;
using System.Collections.Generic;

namespace XamarinWebservice.Model
{
        public class Book
        {
            public string isbn { get; set; }
            public string title { get; set; }
            public List<string> authors { get; set; }
            public DateTime publishDate { get; set; } = DateTime.Now;
            public string genre { get; set; }
        }
}
