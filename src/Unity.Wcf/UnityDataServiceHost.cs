
namespace Unity.Wcf
{
    using System;
    using System.Data.Services;
    using Microsoft.Practices.Unity;

    public class UnityDataServiceHost : DataServiceHost
    {
        private readonly IUnityContainer _container;

        public UnityDataServiceHost(IUnityContainer container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            _container = container;
        }

        protected override void OnOpening()
        {
            Description.Behaviors.Add(new UnityInstanceProviderServiceBehavior(_container));
            base.OnOpening();
        }
    }
}