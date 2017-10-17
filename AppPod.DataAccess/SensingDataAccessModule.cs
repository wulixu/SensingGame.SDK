using AppPod.DataAccess;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using System.Threading;

namespace AppPod.DataAccess
{
    public class SensingDataAccessModule : IModule
    {
        private IUnityContainer mUnityContainer;
        IEventAggregator mEventAggregator;
        public SensingDataAccessModule(IUnityContainer unityContainer, IEventAggregator eventAggregator)
        {
            mUnityContainer = unityContainer;
            mEventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            SensingDataAccess sensingDataAccess = new SensingDataAccess();
            //mEventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = "加载数据" });
            sensingDataAccess.Intialize();
            //ShareData.Instance.ProductProvider = sensingDataAccess;
            mUnityContainer.RegisterInstance<ILocalSensingDataAccess>(sensingDataAccess);

        }
    }
}
