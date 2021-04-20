using System.Security.Cryptography.X509Certificates;
using Autofac;
using AzureProject.Business.Abstract.Services;
using AzureProject.Business.Concrete;
using AzureProject.Core.Repositories.AzureRepository;
using AzureProject.Entities.Entities.Azure;

namespace AzureProject.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AzureRepositoryBase<Product>>().As<ITableStorage<Product>>();
            builder.RegisterType<ProductManager>().As<IProductService>();

            builder.RegisterType<PictureManager>().As<IPictureService>();
            builder.RegisterType<BlobStorage>().As<IBlobStorage>();

        }
    }
}