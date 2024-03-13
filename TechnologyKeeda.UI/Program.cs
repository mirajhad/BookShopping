using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TechnologyKeeda.Repositories;
using TechnologyKeeda.Repositories.Implementations;
using TechnologyKeeda.Repositories.Interfaces;
using TechnologyKeeda.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//DI :  Tightly Coupling---Convert  - -  loosly coupled 
// class A {  public A(){ n
// } }
// DI -  Achieve IOC 
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
});


// ICityRepo cityRepo =  new CityRepo();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Sync ---  1,2,3,4,5,6
//async   ---  1(10ml) Task.completed,2(15 ms),3(20ms),4(5ms)   Task, async , await
//Validation : Client Side / Server Side, AntiForgery :  XSS Cross Side Scripting Attack


//[Required(ErrorMessage = "The field is required.")]

//[StringLength(50, MinimumLength = 2, ErrorMessage = "The field must be between 2 and 50 characters.")]

//[Range(1, 100, ErrorMessage = "The field must be between 1 and 100.")]

//[RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "The field must contain only alphanumeric characters.")]

//[EmailAddress(ErrorMessage = "Invalid email address.")]

//[Compare("OtherPropertyName", ErrorMessage = "The field does not match.")]

//DataType.Date: Represents a date value without a time component.
//DataType.Time: Represents a time value without a date component.
//DataType.DateTime: Represents a date and time value.
//DataType.Duration: Represents a time interval or duration.
//DataType.PhoneNumber: Represents a phone number.
//DataType.Currency: Represents a currency value.
//DataType.Text: Represents a plain text value.
//DataType.Html: Represents an HTML-encoded string.
//DataType.MultilineText: Represents a multi-line text value.
//DataType.EmailAddress: Represents an email address.
//DataType.Password: Represents a password.
//DataType.Url: Represents a URL.
//DataType.ImageUrl: Represents an image URL.
//DataType.CreditCard: Represents a credit card number.
//DataType.PostalCode: Represents a postal code.
