using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Rpc.Contracts.Asset.GetAsset;
using OneGate.Frontend.ClientLibrary.Utils;
using OneGate.Shared.Models.Account;
using OneGate.Shared.Models.Asset;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Exchange;
using OneGate.Shared.Models.Order;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Frontend.ClientLibrary
{
    public class OneGateApi
    {
        private Uri BaseEndpoint { get; }
        private string AccessToken { get; }

        public OneGateApi(Uri baseEndpoint, string accessToken)
        {
            BaseEndpoint = baseEndpoint;
            AccessToken = accessToken;
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(string methodName, TRequest bodyModel,
            object queryModel = null)
        {
            return await HttpUtils.PostAsync<TRequest, TResponse>(BaseEndpoint, AccessToken, methodName, bodyModel,
                queryModel);
        }

        private async Task<TResponse> GetAsync<TResponse>(string methodName, object queryModel = null)
        {
            return await HttpUtils.GetAsync<TResponse>(BaseEndpoint, AccessToken, methodName, queryModel);
        }

        private async Task DeleteAsync(string methodName, object queryModel = null)
        {
            await HttpUtils.DeleteAsync(BaseEndpoint, AccessToken, methodName, queryModel);
        }

        #region Account controller

        public static async Task<AccessTokenDto> CreateTokenAsync(Uri baseEndpoint, OAuthDto model,
            ClientKeyDto clientKey)
        {
            return await HttpUtils.PostAsync<OAuthDto, AccessTokenDto>(baseEndpoint, null, "account/auth", model,
                clientKey);
        }

        public static async Task<CreatedResourceDto> CreateAccountAsync(Uri baseEndpoint, CreateAccountDto model,
            ClientKeyDto clientKey)
        {
            return await HttpUtils.PostAsync<CreateAccountDto, CreatedResourceDto>(baseEndpoint, null, "account", model,
                clientKey);
        }

        public async Task<AccountDto> GetMyAccountAsync()
        {
            return await GetAsync<AccountDto>("account/me");
        }

        public async Task<AccountDto> GetAccountAsync(int id)
        {
            return await GetAsync<AccountDto>($"account/{id}");
        }

        public async Task<List<AccountDto>> GetAccountsByFilterAsync(AccountFilterDto model)
        {
            return await GetAsync<List<AccountDto>>("account", model);
        }

        public async Task DeleteAccountAsync(int id)
        {
            await DeleteAsync($"account/{id}");
        }

        #endregion

        #region Asset controller

        public async Task<CreatedResourceDto> CreateAssetAsync(CreateAssetBaseDto model)
        {
            return await PostAsync<CreateAssetBaseDto, CreatedResourceDto>("asset", model);
        }

        public async Task<AssetBaseDto> GetAssetAsync(int id)
        {
            return await GetAsync<AssetBaseDto>($"asset/{id}");
        }

        public async Task<List<AssetBaseDto>> GetAssetsByFilterAsync(AssetBaseFilterDto model)
        {
            return await GetAsync<List<AssetBaseDto>>("asset", model);
        }

        public async Task DeleteAssetAsync(int id)
        {
            await DeleteAsync($"asset/{id}");
        }

        #endregion
        
        #region Exchange controller
        
        public async Task<CreatedResourceDto> CreateExchangeAsync(CreateExchangeDto model)
        {
            return await PostAsync<CreateExchangeDto, CreatedResourceDto>("exchange", model);
        }
        
        public async Task<ExchangeDto> GetExchangeAsync(int id)
        {
            return await GetAsync<ExchangeDto>($"exchange/{id}");
        }

        public async Task<List<ExchangeDto>> GetExchangeByFilterAsync(ExchangeFilterDto model)
        {
            return await GetAsync<List<ExchangeDto>>("exchange", model);
        }

        public async Task DeleteExchangeAsync(int id)
        {
            await DeleteAsync($"exchange/{id}");
        }
        
        #endregion
        
        #region Order controller
        
        public async Task<CreatedResourceDto> CreateOrderAsync(CreateOrderBaseDto model)
        {
            return await PostAsync<CreateOrderBaseDto, CreatedResourceDto>("order", model);
        }

        public async Task<OrderBaseDto> GetOrderAsync(int id)
        {
            return await GetAsync<OrderBaseDto>($"order/{id}");
        }

        public async Task<List<OrderBaseDto>> GetOrdersByFilterAsync(OrderBaseFilterDto model)
        {
            return await GetAsync<List<OrderBaseDto>>("order", model);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await DeleteAsync($"order/{id}");
        }
        
        #endregion

        #region Timeseries controller

        public async Task<CreatedResourceDto> CreateOhlcTimeseriesAsync(CreateOhlcTimeseriesDto model)
        {
            return await PostAsync<CreateOhlcTimeseriesDto, CreatedResourceDto>("ohlc", model);
        }

        public async Task<OhlcTimeseriesDto> GetOhlcTimeseriesAsync(int id)
        {
            return await GetAsync<OhlcTimeseriesDto>($"ohlc/{id}");
        }

        public async Task<List<OhlcTimeseriesDto>> GetOhlcTimeseriesByFilterAsync(OhlcTimeseriesFilterDto model)
        {
            return await GetAsync<List<OhlcTimeseriesDto>>("ohlc", model);
        }

        public async Task DeleteOhlcTimeseriesAsync(int id)
        {
            await DeleteAsync($"ohlc/{id}");
        }

        public async Task<CreatedResourceDto> CreateValueTimeseriesAsync(CreateValueTimeseriesDto model)
        {
            return await PostAsync<CreateValueTimeseriesDto, CreatedResourceDto>("value", model);
        }

        public async Task<ValueTimeseriesDto> GetValueTimeseriesAsync(int id)
        {
            return await GetAsync<ValueTimeseriesDto>($"value/{id}");
        }

        public async Task<List<ValueTimeseriesDto>> GetValueTimeseriesByFilterAsync(ValueTimeseriesFilterDto model)
        {
            return await GetAsync<List<ValueTimeseriesDto>>("value", model);
        }

        public async Task DeleteValueTimeseriesAsync(int id)
        {
            await DeleteAsync($"value/{id}");
        }

        #endregion
    }
}