using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Books
    {
        private static List<Book> books=null;
        public static int counter=4;
        public static int Counter()
        {
            ++counter;
            return counter;
        }
        public static List<Book> GetBooks
        {
            get
            {
                if (books == null)
                {
                    books = new List<Book>();
                    Book one = new Book() { Id = 1, Author = "J. K. Rowling", Name = "Harry Potter", Published = 2000 };
                    books.Add(one);
                    books.Add(new Book() { Id = 2, Author = "K. H. Mácha", Name = "Máj", Published = 1939 });

                    books.Add(new Book() { Id = 3, Author = "C. S. Lewis", Name = "Narnia Chronicles", Published = 1978 });
                    books.Add(new Book() { Id=4, Author="E. Tolle", Name="The power of Now", Published=1939});
                }

                return books;
            }
        }
       
    }
}
