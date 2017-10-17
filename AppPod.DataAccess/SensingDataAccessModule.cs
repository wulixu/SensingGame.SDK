﻿using AppPod.DataAccess;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using System.Threading;

namespace ProductProvider
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

        public async  void Initialize()
        {
            SensingDataAccess sensingDataAccess = new SensingDataAccess();
            //mEventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = "加载数据" });
            await sensingDataAccess.Intialize();
            //ShareData.Instance.ProductProvider = sensingDataAccess;
            mUnityContainer.RegisterInstance<ILocalSensingDataAccess>(sensingDataAccess);
        }
    }
}