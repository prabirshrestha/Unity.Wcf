# Unity.Wcf

Library for using unity with wcf.

### Usage

	RouteCollection routes;
	routes.Add(new ServiceRoute("prefix", new UnityDataServiceHostFactory(container), typeof(SampleWcfService)));

Sample Code using WebActivator and Unity.Wcf with asp.net mvc 3

	[assembly: WebActivator.PreApplicationStartMethod(
		typeof(WebApplication.App_Start.UnityWcfAppStart), "Start")]

	namespace WebApplication.App_Start
	{
		using System.Web.Routing;
		using Microsoft.Practices.Unity;
		using Unity.Wcf;

		public class UnityWcfAppStart
		{
			public static void Start()
			{
				// Create the unity container.
				var container = new UnityContainer();

				// register services with the container.
				RegisterServices(container, RouteTable.Routes);
				
				// Tell MVC 3 to use the Unity DI container.
				DependencyResolver.SetResolver(
					serviceType =>
					{
						try { return container.Resolve(serviceType); }
						catch { return null; }
					},
					serviceType =>
					{
						try { return container.ResolveAll(serviceType); }
						catch { return null; }
					});

				// set the unity container as the service locator.
				Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
			}

			public static void RegisterServices(IUnityContainer container, RouteCollection routes)
			{
				// register unity types here
				container.RegisterType<ISampleRepo, SqlSampleRepo>();

				// register wcf services
				routes.Add(new ServiceRoute("prefix", new UnityDataServiceHostFactory(container), typeof(SampleWcfService)));
			}
		}
	}

