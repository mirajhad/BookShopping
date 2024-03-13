using BlazorBookShop.Client.StringHelpers;
using BlazorBookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Pages
{
    public partial class BookList
    {
        [Inject]
        public IJSRuntime js { get; set; }
        [Parameter]
        public List<Book> bookList { get; set; }
        //[Parameter] public RenderFragment ChildContent{ get; set; }

        public bool isShow = false;

        private async Task DeleteBook(Book book)
        {
            await js.WelComeMessage("Tarun");
            await js.InvokeVoidAsync("StaticMethodCall");
            await js.InvokeVoidAsync("NonStaticMethodCall",                
                DotNetObjectReference.Create(this));
            var confirmed = await js.Confirm("Are your sure you want to delete this book");
            if (confirmed)
            {
                bookList.Remove(book);

            }


        }
        [JSInvokable]
        public string CallFromJavaScriptNonStatic()
        {
            return "Hello World";
        }



        [JSInvokable]
        public static Task<string> CallFromJavaScript()
        {
            return Task.FromResult("Hello World");
        }
       

        Book Books = new Book
        {
            Id = 1,
            Title = "Ëcommerce App with ASP.NET CORE"
        };

        protected override void OnInitialized()
        {
            Console.WriteLine("OnInitialized method :  Total Books" + bookList.Count.ToString());
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            Console.WriteLine("OnParametersSet method :  Total Books" + bookList.Count.ToString());
            base.OnParametersSet();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine("OnAfterRender method :" + firstRender.ToString());
            base.OnAfterRender(firstRender);
        }
        protected override bool ShouldRender()
        {
            return true;
        }


    }
}
