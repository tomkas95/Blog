using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClassLibrary1.Interface;

namespace ClassLibrary1
{
    public class Book: IEntity
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Insert name of the author.")]
        public virtual string Author { get; set; }
        [Required(ErrorMessage = "Insert name of the book.")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "Insert year published between 1800 and 2020.")]
        [Range(1800, 2020)]
        public virtual int Published { get; set; }
       // [Required(ErrorMessage = "Insert number of pages from 1 to 999.")]
       //  [Range(1, 999)]
        // public virtual int Pages { get; set; }
        [AllowHtml]
        public virtual string Description { get; set; }
        public virtual string ImageName { get; set; }
        
        public virtual BookCategory Category { get; set; }

    }
}
