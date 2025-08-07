using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    [Table("Comment")]
    public class Comment
    {
        public int? Stockid { get; set; }
        public Stock? Stock { get; set; } = null;

        //can be written in: (EFNameID will recongnise it automatically)
        //public int Stockid { get; set; }
        //public Stock Stock { get; set; } = null;

        //can be written: (better in case having multiple fkeys)
        // public int Stockid { get; set; }
        // [ForeignKey("Stockid")]
        // public Stock Stock { get; set; } = null;
        public int id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}