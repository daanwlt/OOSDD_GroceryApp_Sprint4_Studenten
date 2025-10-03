using Grocery.Application.Services;
using Grocery.App.ViewModels;
using Grocery.App.Views;
using Grocery.App.Services;
using Microsoft.Extensions.Logging;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Interfaces.Repositories;
using Grocery.Infrastructure.Data.Repositories;
using CommunityToolkit.Maui;

namespace Grocery.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Core Services
            builder.Services.AddSingleton<IGroceryListService, GroceryListService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IClientService, ClientService>();
            builder.Services.AddSingleton<IFileSaverService, Grocery.App.Services.FileSaverService>();
            
            // Specialized Services (following SRP)
            builder.Services.AddSingleton<ProductEnrichmentService>();
            builder.Services.AddSingleton<BestSellingProductsAnalysisService>();
            builder.Services.AddSingleton<BoughtProductsAnalysisService>();
            builder.Services.AddSingleton<IGroceryListItemsService, GroceryListItemsService>();
            builder.Services.AddSingleton<IBoughtProductsService, BoughtProductsService>();
            
            // App Services (following SRP)
            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddSingleton<RoleValidationService>();

            builder.Services.AddSingleton<IGroceryListRepository, GroceryListRepository>();
            builder.Services.AddSingleton<IGroceryListItemsRepository, GroceryListItemsRepository>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<IClientRepository, ClientRepository>();
            builder.Services.AddSingleton<GlobalViewModel>();

            builder.Services.AddTransient<GroceryListsView>().AddTransient<GroceryListViewModel>();
            builder.Services.AddTransient<GroceryListItemsView>().AddTransient<GroceryListItemsViewModel>();
            builder.Services.AddTransient<ProductView>().AddTransient<ProductViewModel>();
            builder.Services.AddTransient<ChangeColorView>().AddTransient<ChangeColorViewModel>();
            builder.Services.AddTransient<LoginView>().AddTransient<LoginViewModel>();
            builder.Services.AddTransient<BestSellingProductsView>().AddTransient<BestSellingProductsViewModel>();
            builder.Services.AddTransient<BoughtProductsView>().AddTransient<BoughtProductsViewModel>();
            return builder.Build();
        }
    }
}
