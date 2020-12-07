using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OneGate.Backend.Engines.Base.Extensions;
using OneGate.Backend.Engines.Base.OhlcProvider;
using OneGate.Common.Models.Series.Ohlc;

namespace OneGate.Backend.Engines.FakeStaticEngine
{
    public class GaussianRandomOhlcProvider : IOhlcProvider
    {
        private static readonly Random Random = new Random();

        private double _lastPrice;

        private readonly Dictionary<IntervalDto, OhlcDto> _lastCandles =
            new Dictionary<IntervalDto, OhlcDto>();

        private readonly double _gaussianSpread;
        private Timer _timer;

        public event Func<IOhlcProvider, OhlcProviderEventArgs, Task> OnPriceChanged;
        public int AssetId { get; }

        public GaussianRandomOhlcProvider(int assetId, float lastPrice)
        {
            AssetId = assetId;

            _lastPrice = lastPrice;
            _gaussianSpread = _lastPrice * 0.01;
            _timer = new Timer(GenerateNewPrice, null, 0, 1000);
        }

        public GaussianRandomOhlcProvider(int assetId, float lastPrice, float gaussianSpread) : this(assetId, lastPrice)
        {
            _gaussianSpread = gaussianSpread;
        }

        private async void GenerateNewPrice(object state)
        {
            _lastPrice = Math.Sqrt(-2.0 * Math.Log(1 - Random.NextDouble())) *
                Math.Cos(2.0 * Math.PI * (1 - Random.NextDouble())) * _gaussianSpread + _lastPrice;
            await RaiseUpdate();
        }

        private async Task RaiseUpdate()
        {
            var currentDateTime = DateTime.Now;

            foreach (IntervalDto interval in Enum.GetValues(typeof(IntervalDto)))
            {
                if (!_lastCandles.ContainsKey(interval) ||
                    _lastCandles[interval].Timestamp.AddInterval(interval) < currentDateTime)
                {
                    _lastCandles[interval] = new OhlcDto
                    {
                        Open = _lastPrice,
                        Close = _lastPrice,
                        Low = _lastPrice,
                        High = _lastPrice,
                        Timestamp = currentDateTime.NormalizeByInterval(interval)
                    };
                }
                else
                {
                    _lastCandles[interval].Close = _lastPrice;
                    _lastCandles[interval].High = Math.Max(_lastCandles[interval].High, _lastPrice);
                    _lastCandles[interval].Low = Math.Min(_lastCandles[interval].Low, _lastPrice);
                }
            }

            await OnPriceChanged.Raise(this, new OhlcProviderEventArgs(
                _lastCandles.ToDictionary(entry => entry.Key,entry => entry.Value)));
        }
    }
}