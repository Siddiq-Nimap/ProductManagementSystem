using System.Web.Mvc;
using Unity;
using System.Web.Http;
using CrudOperations.Models;
using CrudOperations.Business_Layer.CategoryCrudOperation;
using CrudOperations.Interfaces;
using CrudOperations.Business_Layer.Activation;
using CrudOperations.Business_Layer.Modification;
using CrudOperations.Business_Layer.Insertion;
using CrudOperations.Business_Layer.ProductCrudOperation;
using CrudOperations.Interfaces.ProductInterfaces;
using CrudOperations.Business_Layer.ProductCrudOperation.Modification;
using CrudOperations.Business_Layer.ProductCrudOperation.Insertion;
using CrudOperations.Business_Layer;

namespace CrudOperations
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();


            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //catagory resolver
            container.RegisterType<ICategory, Categorys>();
            container.RegisterType<ICategoryActivation, Activation>();
            container.RegisterType<ICategoryModification, CategoryModification>();
            container.RegisterType<ICategoryInsertion, CategoryInsertion>();

            //Product Resolver
            container.RegisterType<IProduct, ProductCrud>();
            container.RegisterType<IProductInsertion, ProductInsertion>();
            container.RegisterType<IProductModification, ProductModification>();

            //Login Resolver
            container.RegisterType<ILogin, LoginCredentialClass>();

            //File Resolver
            container.RegisterType<IFileSaving, FileUploadClass>();

            //Paging Resolver
            container.RegisterType<IPaging, ProductPaging>();

            //Authentication Resolver
            container.RegisterType<IAuthenticationManager, AuthenticationClass>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}