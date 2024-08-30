using Data.Model;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("https://localhost:7058"
                                    )
                                  .AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                              });
        });

        //builder.Services.AddControllers();
        builder.Services.AddControllersWithViews()
                          .AddJsonOptions(options =>
                          {
                              options.JsonSerializerOptions.PropertyNamingPolicy = null;
                          });



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        //DMAExamNewDB
        var contextOptions = new DbContextOptionsBuilder<AppDBContext>()
                     .UseSqlServer("Data Source=tcp:commondbserver.database.windows.net,1433;Initial Catalog=DMAExamNewDB;User ID=common@commondbserver;Password=RZarni2022D$$;MultipleActiveResultSets=True")
                     .Options;

        builder.Services.AddScoped(s => new AppDBContext(contextOptions));


        //other servies

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

        app.Run();
    }
}