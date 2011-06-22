
namespace Unity.Wcf
{
    using System;
    using System.Data.Services;
    using System.ServiceModel;
    using Microsoft.Practices.Unity;

    public class UnityDataServiceHostFactory : DataServiceHostFactory
    {
        private readonly IUnityContainer _container;

        public UnityDataServiceHostFactory(IUnityContainer container)
        {
            _container = container;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UnityDataServiceHost(_container, serviceType, baseAddresses);
        }
    }
}