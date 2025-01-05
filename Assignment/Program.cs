var builder = WebApplication.CreateBuilder(args);

// Register services in the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProductRepository>(); // Register ProductRepository here

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"); // Use a valid controller name like "ProductController"

app.Run();