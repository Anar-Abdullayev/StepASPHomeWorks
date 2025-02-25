using CityManagerApp1.Data;
using CityManagerApp1.Entities;
using CityManagerApp1.Repository.Abstract;
using CityManagerApp1.Repository.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace CityManagerApp1
{
    public class Program
    {
        static BackgroundWorker backgroundWorker = new BackgroundWorker();
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);


            var conn = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<AppDataContext>(options =>
            {
                options.UseSqlServer(conn);
            });

            builder.Services.AddScoped<IAppRepository, AppRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IArrayGeneratorRepository, ArrayGeneratorRepository>();
            builder.Services.AddScoped<IMovieSearchRepository, MovieSearchRepository>();
            builder.Services.AddScoped<IArrayFilterRepository, ArrayFilterRepository>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();


            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            // Start the background worker
            backgroundWorker.RunWorkerAsync(argument: app);
            app.Run();

        }
        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WebApplication app = (WebApplication)e.Argument;
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var arrayGeneratorRepository = services.GetService<IArrayGeneratorRepository>();
                var arrayFilterRepository = services.GetService<IArrayFilterRepository>();
                var movieRepository = services.GetService<IMovieRepository>();
                var movieSearchRepository = services.GetService<IMovieSearchRepository>();
                var searchPattern = arrayGeneratorRepository.GenerateArrayFromFile("./Notes.txt");



                while (true)
                {
                    var randomSearchPattern = arrayFilterRepository.GenerateRandomArrayFrom(searchPattern, 5);

                    for (int i = 0; i < randomSearchPattern.Length; i++)
                    {
                        dynamic result = JsonConvert.DeserializeObject(movieSearchRepository.GetMovieList(randomSearchPattern[i]));
                        if (result.Search != null)
                        {
                            string movieTitle = result.Search[0].Title;
                            string movieYear = result.Search[0].Year;
                            Movie newMovie = new Movie
                            {
                                Name = movieTitle,
                                Year = movieYear
                            };
                            movieRepository.AddMovie(newMovie);
                        }
                    }
                    Thread.Sleep(5000);
                }
            }
        }

        // RunWorkerCompleted event handler
        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine($"Error: {e.Error.Message}");
            }
            else
            {
                Console.WriteLine($"Background worker completed: {e.Result}");
            }
        }
    }
}
