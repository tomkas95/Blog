using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Interface;

namespace ClassLibrary1
{
    public class BookCategory : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}
