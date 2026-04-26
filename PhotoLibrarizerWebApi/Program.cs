var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages().AddRazorPagesOptions(options => { options.RootDirectory = "/wwwroot"; });
//builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "wwwroot"; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.UseStaticFiles(); // For the wwwroot folder to find the other files
app.UseHttpsRedirection();

app.MapFallbackToFile("index.html");

app.Run();