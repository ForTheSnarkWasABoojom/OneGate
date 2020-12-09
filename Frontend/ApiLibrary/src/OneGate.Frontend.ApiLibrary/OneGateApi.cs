using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Common.Models.Account;
using OneGate.Common.Models.Asset;
using OneGate.Common.Models.Common;
using OneGate.Common.Models.Exchange;
using OneGate.Common.Models.Layout;
using OneGate.Common.Models.Order;
using OneGate.Common.Models.Portfolio;
using OneGate.Common.Models.Series.Ohlc;
using OneGate.Common.Models.Series.Point;
using OneGate.Frontend.ApiLibrary.Utils;

namespace OneGate.Frontend.ApiLibrary
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

        public static async Task<ResourceDto> CreateAccountAsync(Uri baseEndpoint, CreateAccountDto model,
            ClientKeyDto clientKey)
        {
            return await HttpUtils.PostAsync<CreateAccountDto, ResourceDto>(baseEndpoint, null, "account", model,
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

        public async Task<ResourceDto> CreateAssetAsync(CreateAssetDto model)
        {
            return await PostAsync<CreateAssetDto, ResourceDto>("asset", model);
        }

        public async Task<AssetDto> GetAssetAsync(int id)
        {
            return await GetAsync<AssetDto>($"asset/{id}");
        }

        public async Task<List<AssetDto>> GetAssetsByFilterAsync(AssetFilterDto model)
        {
            return await GetAsync<List<AssetDto>>("asset", model);
        }

        public async Task DeleteAssetAsync(int id)
        {
            await DeleteAsync($"asset/{id}");
        }

        #endregion
        
        #region Exchange controller
        
        public async Task<ResourceDto> CreateExchangeAsync(CreateExchangeDto model)
        {
            return await PostAsync<CreateExchangeDto, ResourceDto>("exchange", model);
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
        
        #region Layout controller
        
        public async Task<ResourceDto> CreateLayoutAsync(CreateLayoutDto model)
        {
            return await PostAsync<CreateLayoutDto, ResourceDto>("layout", model);
        }
        
        public async Task<LayoutDto> GetLayoutAsync(int id)
        {
            return await GetAsync<LayoutDto>($"layout/{id}");
        }

        public async Task<List<LayoutDto>> GetLayoutByFilterAsync(LayoutFilterDto model)
        {
            return await GetAsync<List<LayoutDto>>("layout", model);
        }

        public async Task DeleteLayoutAsync(int id)
        {
            await DeleteAsync($"layout/{id}");
        }
        
        #endregion
        
        #region Order controller
        
        public async Task<ResourceDto> CreateOrderAsync(CreateOrderDto model)
        {
            return await PostAsync<CreateOrderDto, ResourceDto>("order", model);
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            return await GetAsync<OrderDto>($"order/{id}");
        }

        public async Task<List<OrderDto>> GetOrdersByFilterAsync(OrderFilterDto model)
        {
            return await GetAsync<List<OrderDto>>("order", model);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await DeleteAsync($"order/{id}");
        }
        
        #endregion

        #region Ohlc series controller

        public async Task CreateOhlcSeriesAsync(OhlcSeriesDto model)
        {
            await PostAsync<OhlcSeriesDto, ResourceDto>("ohlcseries", model);
        }

        public async Task<OhlcSeriesDto> GetOhlcSeriesByFilterAsync(OhlcSeriesFilterDto model)
        {
            return await GetAsync<OhlcSeriesDto>("ohlcseries", model);
        }

        public async Task DeleteOhlcSeriesAsync(OhlcSeriesFilterDto model)
        {
            await DeleteAsync("ohlcseries", model);
        }

        #endregion

        #region Point series controller

        public async Task CreatePointSeriesAsync(PointSeriesDto model)
        {
            await PostAsync<PointSeriesDto, ResourceDto>("pointseries", model);
        }

        public async Task<PointSeriesDto> GetPointSeriesByFilterAsync(PointSeriesFilterDto model)
        {
            return await GetAsync<PointSeriesDto>("pointseries", model);
        }

        public async Task DeletePointSeriesAsync(PointSeriesFilterDto model)
        {
            await DeleteAsync("pointseries", model);
        }

        #endregion
        
        #region Portfolio controller
        
        public async Task<ResourceDto> CreatePortfolioAsync(CreatePortfolioDto model)
        {
            return await PostAsync<CreatePortfolioDto, ResourceDto>("portfolio", model);
        }

        public async Task<PortfolioDto> GetPortfolioAsync(int id)
        {
            return await GetAsync<PortfolioDto>($"portfolio/{id}");
        }

        public async Task<List<PortfolioDto>> GetPortfoliosByFilterAsync(PortfolioDto model)
        {
            return await GetAsync<List<PortfolioDto>>("portfolio", model);
        }

        public async Task DeletePortfolioAsync(int id)
        {
            await DeleteAsync($"portfolio/{id}");
        }
        
        #endregion
    }
}