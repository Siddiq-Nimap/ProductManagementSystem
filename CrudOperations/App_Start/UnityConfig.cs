using CrudOperations.Business_Layer;
using CrudOperations.Business_Layer.Activation;
using CrudOperations.Business_Layer.CategoryCrudOperation;
using CrudOperations.Business_Layer.Insertion;
using CrudOperations.Business_Layer.Modification;
using CrudOperations.Business_Layer.ProductCrudOperation;
using CrudOperations.Business_Layer.ProductCrudOperation.Insertion;
using CrudOperations.Business_Layer.ProductCrudOperation.Modification;
using CrudOperations.Interfaces;
using CrudOperations.Interfaces.ProductInterfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

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

            //Paging 
            container.RegisterType<IPaging, ProductPaging>();

            //File
            container.RegisterType<IFileSaving, FileUploadClass>();

            //Logins
            container.RegisterType<ILogin,LoginCredentialClass>();

            //Product
            container.RegisterType<IProduct, ProductCrud>();
            container.RegisterType<IProductInsertion,ProductInsertion>();
            container.RegisterType<IProductModification,ProductModification>();

            //Category
            container.RegisterType<ICategory,Categorys>();
            container.RegisterType<ICategoryInsertion,CategoryInsertion>();
            container.RegisterType<ICategoryActivation,Activation>();
            container.RegisterType<ICategoryModification,CategoryModification>();

            //Authentication
            container.RegisterType<IAuthenticationManager,AuthenticationClass>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}