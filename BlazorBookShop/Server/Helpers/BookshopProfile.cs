using AutoMapper;
using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Server.Helpers
{
    public class BookshopProfile :  Profile
    {
        public BookshopProfile()
        {
            CreateMap<Author, Author>()
              .ForMember(x => x.AuthorImage, opt => opt.Ignore());

            CreateMap<Book, Book>()
  .ForMember(x => x.BookImage, opt => opt.Ignore());

        }

    }
}
