using estatedocflow.api.RabbitMQ;
using estatedocflow.Data.Infrastructure;
using estatedocflow.Data.Repositories.IRepository.Base;
using estatedocflow.Data.Repositories.IRepository.House;
using estatedocflow.Data.Repositories.Repository.Base;
using estatedocflow.Data.Repositories.Repository.House;
using estatedocflow.Data.Services.Iservice;
using estatedocflow.Data.Services.Service;

namespace estatedocflow.api.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureRepository(this WebApplicationBuilder app)
        {
            app.Services.AddScoped<RealEstateDbContext>();
            app.Services.AddScoped<IHouseRepository, HouseRepository>();
            app.Services.AddScoped<IHousePhotoRepository, HousePhotoRepository>();
        }
        public static void ConfigureService(this WebApplicationBuilder app)
        {

            app.Services.AddScoped<IRabbitMQService, RabbitMQService>();
            app.Services.AddScoped<IHouseService, HouseService>();
        }
    }
}
