﻿using estatedocflow.api.Data.Repositories;
using estatedocflow.api.Data.Repositories.Interfaces;
using estatedocflow.api.Data.Services;
using estatedocflow.api.Data.Services.Interfaces;
using estatedocflow.api.RabbitMQ;

namespace estatedocflow.api.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureRepository(this WebApplicationBuilder app)
        {
            app.Services.AddScoped<IHouseRepository, HouseRepository>();
            app.Services.AddScoped<IHouseAttachmentRepository, HouseAttachmentRepository>();
        }
        public static void ConfigureService(this WebApplicationBuilder app)
        {
            app.Services.AddScoped<IRabbitMqService, RabbitMqService>();
            app.Services.AddScoped<IHouseService, HouseService>();
            app.Services.AddScoped<FileStore>();
        }        
    }
}
