﻿namespace Store.Web.App
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Isbn { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }
    }
}
