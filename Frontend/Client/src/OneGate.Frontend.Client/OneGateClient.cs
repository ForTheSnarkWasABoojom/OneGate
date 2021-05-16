using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OneGate.Backend.Gateway.User.Api.Client;
using OneGate.Backend.Gateway.User.Api.Contracts.Account;
using OneGate.Backend.Gateway.User.Api.Contracts.Credentials;
using OneGate.Backend.Gateway.User.Api.Contracts.Timeseries;

namespace OneGate.Frontend.Client
{
    public class OneGateClient : IOneGateClient
    {
        private readonly IUserGatewayClient _gateway;

        public OneGateClient(IOptions<OneGateClientOptions> options)
        {
            var userGatewayClientOptions = new OptionsWrapper<UserGatewayClientOptions>(
                new UserGatewayClientOptions
                {
                    BaseUri = options.Value.BaseUri
                });
            _gateway = new UserGatewayClient(userGatewayClientOptions);
        }

        public async Task CreateAccountAsync(CreateAccountRequest model)
            => await _gateway.CreateAccountAsync(model);

        public async Task<TokenResponse> SignInAsync(AuthRequest model)
            => await _gateway.CreateTokenAsync(model);

        /// <summary>
        /// To do! The appropriate server method is expected.
        /// </summary>
        public async Task<ObservableCollection<OhlcModel>> GetOhlcData()
            => GenerateOhlcData();

        public async Task<ObservableCollection<GraphLayer>> GetGraphLayers()
        {
            var layers = new ObservableCollection<GraphLayer>();
            var pointLayer = new GraphLayer("Point graph 0");
            var ohlcData = await GetOhlcData();
            pointLayer.Data = CalculateMovingAverage(ohlcData);
            layers.Add(pointLayer);
            GenerateOhlcData();
            var pointLayer1 = new GraphLayer("Point graph 1");
            pointLayer1.Data = CalculateMovingAverage(ohlcData);
            layers.Add(pointLayer1);
            GenerateOhlcData();
            var pointLayer2 = new GraphLayer("Moving curve");
            pointLayer2.Data = CalculateMovingAverage(ohlcData);
            layers.Add(pointLayer2);
            return layers;
        }


        /// <summary>
        /// This is a temporary code that is needed to test the drawing of the graph.
        /// </summary>
        private static ObservableCollection<OhlcModel> GenerateOhlcData()
        {
            var ohlcData = new ObservableCollection<OhlcModel>();
            var date = DateTime.Now;
            for (int i = 0; i < 100; ++i)
            {
                var random = new Random();
                ohlcData.Add(new OhlcModel()
                {
                    Open = random.NextDouble() * random.Next(1, 100),
                    High = random.NextDouble() * random.Next(1, 100),
                    Low = random.NextDouble() * random.Next(1, 100),
                    Close = random.NextDouble() * random.Next(1, 100),
                    Timestamp = date
                });
                date = date.AddDays(1);
            }
            return ohlcData;
        }

        /// <summary>
        /// This is a temporary code that is needed to test the drawing of the graph.
        /// </summary>
        public static double[] ConvertToArrayOfX(ObservableCollection<OhlcModel> data)
        {
            var xS = new double[data.Count];
            for (int i = 0; i < data.Count; ++i)
            {
                xS[i] = data[i].Timestamp.ToOADate();
            }
            return xS;
        }

        /// <summary>
        /// Calculates the coordinates of the points of the moving line.
        /// Temporary code.
        /// </summary>
        private static double[][] CalculateMovingAverage(ObservableCollection<OhlcModel> data)
        {
            int period = (int)(data[data.Count - 1].Timestamp - data[0].Timestamp).TotalDays;
            double[] buffer = new double[period];
            double[] yS = new double[data.Count];
            yS[0] = data[0].Close;
            int currentIndex = 0;
            for (int i = 0; i < data.Count; ++i)
            {
                buffer[currentIndex] = data[i].Close / period;
                double ma = 0.0;
                for (int j = 0; j < period; ++j)
                {
                    ma += buffer[j];
                }
                yS[i] = ma;
                currentIndex = (currentIndex + 1) % period;
            }
            return new double[][] { ConvertToArrayOfX(data), yS };
        }
    }
}