﻿using Sensing.SDK.Contract;
using SensingStoreCloud.Devices.Dto;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sensing.SDK
{
    partial class SensingWebClient
    {
        private const string GroupInfoQuery = "api/services/app/SensingDevice/GetTenantAndOrganizationUnitInfo";
        private const string GetLastUpdateTimeQuery = "api/services/app/SensingDevice/GetLastUpdateTime";


        public async Task<TenantAndOrganizationUnitOutput> GetTenantAndOrganizationUnitInfo()
        {
            var absolutePath = $"{SApiHost}{GroupInfoQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var groupResult = await SendRequestAsync<string, AjaxResponse<TenantAndOrganizationUnitOutput>>(HttpMethod.Get, absolutePath, null);
                return groupResult.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetGroupInfo:" + ex.InnerException);
            }
            return null;
        }

        public async Task<TableLastTimeDto> GetLastUpdateTime()
        {
            var absolutePath = $"{SApiHost}{GetLastUpdateTimeQuery}?{GetBasicNameValuesQueryString()}";
            try
            {
                var pagedList = await SendRequestAsync<string, AjaxResponse<TableLastTimeDto>>(HttpMethod.Get, absolutePath, null);
                return pagedList.Result;
            }
            catch (Exception ex)
            {
                //logger.Error("PostBehaviorRecordsAsync", ex);
                Console.WriteLine("GetDeviceInfo:" + ex.InnerException);
            }
            return null;
        }


    }
}
