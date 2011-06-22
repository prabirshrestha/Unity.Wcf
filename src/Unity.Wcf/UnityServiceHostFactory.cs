
namespace Unity.Wcf
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using Microsoft.Practices.Unity;

    public class UnityServiceHostFactory : ServiceHostFactory
    {
        private readonly IUnityContainer _container;

        public UnityServiceHostFactory(IUnityContainer container)
        {
            _container = container;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(_container, serviceType, baseAddresses);
        }
    }
}