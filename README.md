# Unity.Wcf

Library for using [Unity](http://unity.codeplex.com/) with WCF.

### Usage

	RouteCollection routes;
	routes.Add(new ServiceRoute("prefix", new UnityDataServiceHostFactory(container), typeof(SampleWcfService)));

Sample Code using WebActivator and Unity.Wcf with asp.net mvc 3

	[assembly: WebActivator.PreApplicationStartMethod(
		typeof(WebApplication.App_Start.AppStart_Unity), "Start")]

	namespace WebApplication.App_Start
	{
		using System.Web.Routing;
		using Microsoft.Practices.Unity;
		using Unity.Wcf;

		public class AppStart_Unity
		{
			public static void Start()
			{
				// Create the unity container.
				var container = new UnityContainer();

				// register services with the container.
				RegisterServices(container, RouteTable.Routes);
				
	#if MVC
				// Tell MVC 3 to use Unity DI container.
				System.Web.Mvc.DependencyResolver.SetResolver(
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
	#endif

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


### License

Unity.Wcf is intended to be used in both open-source and commercial environments.

Unity.Wcf is licensed under Apache License 2.0.
