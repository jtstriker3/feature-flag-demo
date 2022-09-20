// See https://aka.ms/new-console-template for more information
using System;
using feature_flag_demo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Welcome to the Launch Darkly feature");
var sp = BuildDi();
var shipmentRepo = sp.GetRequiredService<IShipmentRepo>();

Console.WriteLine(shipmentRepo.GetType().FullName);
var shipment1 = await shipmentRepo.GetShipmentAsync("Shipment1");

Console.WriteLine($"ShipmentCount: {shipmentRepo.ShipmentCount}");

if (shipment1 is null)
{
    throw new Exception("Whoa this is null");
}
else
{
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(shipment1));
}


static IServiceProvider BuildDi()
{
    var services = new ServiceCollection();

    // Utility to manage execution of Logic for 
    services.AddSingleton<FeatureExecutor>();

    // Setup Configuration
    var configBuilder = new ConfigurationBuilder();
    configBuilder.AddJsonFile("appsettings.json");
    var config = configBuilder.Build();
    services.AddSingleton<IConfiguration>(config);


    var featureClient = new FeatureFlagClient(config);
    var shipmentRepoVersion = featureClient.GetFlagValue<string>("ShipmentRepoUseIsDelete");

    // What happens when an app is running and the feature flag is flipped??
    switch (shipmentRepoVersion)
    {
        case "v1":
            services.AddTransient<IShipmentRepo, ShipmentRepoV1>();
            break;
        case "v2":
            services.AddTransient<IShipmentRepo, ShipmentRepoV2>();
            break;
    }

    // Dynamically resolve based off current feature flag value
    services
        .AddTransientVersionedService<IShipmentRepo, ShipmentRepoV1>("ShipmentRepoUseIsDelete", "v1")
        .WithVersion<ShipmentRepoV2>("v2");

    return services.BuildServiceProvider();
}
