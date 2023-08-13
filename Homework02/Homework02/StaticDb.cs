using Homework02.Models;

namespace Homework02
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Author = "Aleksandar",
                Title = "Volsebnoto samarce"
            },
            new Book
            {
                Id = 2,
                Author = "Darko",
                Title = "Midnight"
            },
            new Book
            {
                Id = 3,
                Author = "Marko",
                Title = "Sunlight"
            }
        };
    }
}
