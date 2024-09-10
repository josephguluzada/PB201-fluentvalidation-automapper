﻿using Microsoft.Extensions.DependencyInjection;
using PB201MovieApp.Business.Services.Implementations;
using PB201MovieApp.Business.Services.Interfaces;

namespace PB201MovieApp.Business;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
    }
}
