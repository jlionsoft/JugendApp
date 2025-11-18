using JugendApp.SharedModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace JugendApp.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        //public static IServiceCollection AddJugendAppClient(this IServiceCollection services)
        //{
        //    // BaseUrl zentral definieren
        //    var baseUrl = "https://localhost:7249/"; // später aus Config oder Environment

        //    services.AddHttpClient<IPersonApiClient, PersonApiClient>(c => c.BaseAddress = new Uri(baseUrl));
        //    services.AddHttpClient<IGroupApiClient, GroupApiClient>(c => c.BaseAddress = new Uri(baseUrl));
        //    services.AddHttpClient<IEventApiClient, EventApiClient>(c => c.BaseAddress = new Uri(baseUrl));
        //    services.AddHttpClient<IInstrumentApiClient, InstrumentApiClient>(c => c.BaseAddress = new Uri(baseUrl));
        //    services.AddHttpClient<ILocationApiClient, LocationApiClient>(c => c.BaseAddress = new Uri(baseUrl));
        //    services.AddHttpClient<IAddressApiClient, AddressApiClient>(c => c.BaseAddress = new Uri(baseUrl));

        //    return services;
        //}

    }
}
