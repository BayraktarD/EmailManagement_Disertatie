using EmailMngmntApi.AES;
using EmailMngmntApi.EntityModels;
using EmailMngmntApi.Interfaces.Repositories;
using EmailMngmntApi.Interfaces.Services;
using EmailMngmntApi.Repositories;
using EmailMngmntApi.RSA;
using EmailMngmntApi.Services;

namespace EmailMngmntApi.Configuration.DependencyInjection
{
    public static class DI_Configuration
    {
        public static void AddDependencies(IServiceCollection services, IConfiguration configuration)
        {

           // string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Proiecte\\GitHub\\DotNet\\Proiect - Catalog Online\\CatalogOnline-ClassLibrary\\Database\\SchoolDatabase.mdf\";Integrated Security=True";

          //  services.AddDbContext<EmailManagementApiContext>(options => options.UseSqlServer(connection, x => x.UseNetTopologySuite()));

            services.SERVICE_DI_Configuration(configuration);
            services.REPOSITORY_DI_Configuration(configuration);
        }

        public static void SERVICE_DI_Configuration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISentEmailsServices, SentEmailsService>();
            services.AddScoped<IRSAHelper, RSAHelper>();
            services.AddScoped<IAESHelper, AESHelper>();
        }

      
        public static void REPOSITORY_DI_Configuration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISentEmailsRepository, SentEmailsRepository>();


        }
    }
}
