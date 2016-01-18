//-----------------------------------------------------------------------
// <copyright file="DisposeController.cs" company="Cobon Tech">
//     Copyright © Cobon Tech. All rights reserved.
// </copyright>
// <author>William Wu</author>
// <email>wulixu@cobontech.com</email>
// <date>2012-10-15</date>
// <summary>no summary</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using log4net;

namespace TronCell.Game.Loader
{
    public class DisposeController : IDisposable
    {
        #region logger
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        #region singleton
        private DisposeController()
        {
        }
        private static readonly DisposeController _instance = new DisposeController();
        public static DisposeController Instance
        {
            get { return _instance; }
        }
        #endregion
        private List<IDisposable> disposeList = new List<IDisposable>();

        public void AddDisposeClass(IDisposable control)
        {
            this.disposeList.Add(control);
        }
        #region IDisposable Members

        public void Dispose()
        {
            logger.Debug(" disposeList Count = " + this.disposeList.Count);
            int count = 0;
            foreach(var i in this.disposeList)
            {
                try
                {
                    logger.Debug("Dispose Count......" + count.ToString());
                    count++;
                    i.Dispose();
                }
                catch (System.Exception ex)
                {
                    logger.Error("DisPose Helper ", ex);
                }

            }
            this.disposeList.Clear();
            disposeList = null;
          
        }

        #endregion
    }
}