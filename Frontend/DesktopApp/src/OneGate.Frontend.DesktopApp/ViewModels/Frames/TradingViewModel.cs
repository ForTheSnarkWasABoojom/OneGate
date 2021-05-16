using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OneGate.Backend.Gateway.User.Api.Contracts.Timeseries;
using OneGate.Frontend.Client;
using ReactiveUI;

namespace OneGate.Frontend.DesktopApp.ViewModels.Frames
{
    public class TradingViewModel : ViewModelBase
    {
        private const string CheckMark = "✓";

        #region Exchanges binding.

        /*private ObservableCollection<ExchangeDto> _exchanges;
        public ObservableCollection<ExchangeDto> Exchanges
        {
            get => _exchanges;
            set => this.RaiseAndSetIfChanged(ref _exchanges, value);
        }

        private ExchangeDto _curExchange;
        public ExchangeDto CurExchange
        {
            get => _curExchange;
            set => this.RaiseAndSetIfChanged(ref _curExchange, value);
        }

        #endregion 

        #region Asset types binding.

        private ObservableCollection<AssetTypeDto> _assetTypes;
        public ObservableCollection<AssetTypeDto> AssetTypes
        {
            get => _assetTypes;
            set => this.RaiseAndSetIfChanged(ref _assetTypes, value);
        }

        private AssetTypeDto _curAssetType;
        public AssetTypeDto CurAssetType
        {
            get
            {
                return _curAssetType;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _curAssetType, value);
            }
        }

        #endregion

        #region Ticker binding.

        private ObservableCollection<AssetDto> _tickers;
        public ObservableCollection<AssetDto> Tickers
        {
            get
            {
                return _tickers;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _tickers, value);
            }
        }

        private AssetDto _curTicker;
        public AssetDto CurTicker
        {
            get
            {
                return _curTicker;

            }
            set
            {
                this.RaiseAndSetIfChanged(ref _curTicker, value);
            }
        }*/


        private ObservableCollection<GraphLayer> _layers;
        public ObservableCollection<GraphLayer> Layers
        {
            get => _layers;
            set => this.RaiseAndSetIfChanged(ref _layers, value);
        }

        private ObservableCollection<GraphLayer> _currentLayers;
        private GraphLayer _currentLayer;
        public GraphLayer CurrentLayer
        {
            get => _currentLayer;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentLayer, value);
                this.RaiseAndSetIfChanged(ref _currentLayer, null);
                if (value == null)
                {
                    if (Layers.Count > 0)
                    {
                        this.RaiseAndSetIfChanged(ref _currentLayer, Layers[0]);
                        this.RaiseAndSetIfChanged(ref _currentLayer, null);
                    }
                    return;
                }
                int index = Layers.IndexOf(value);
                if (_currentLayers.Contains(_layers[index]))
                {
                    _graph.RemoveLayer(_layers[index]);
                    _currentLayers.Remove(_layers[index]);
                    Layers[index].Name = Layers[index].Name.Substring(1);
                }
                else
                {
                    _graph.AddLayer(_layers[index]);
                    _currentLayers.Add(_layers[index]);
                    Layers[index].Name = CheckMark + Layers[index].Name;
                }
                CurrentLayer = null;
            }
        }

        private ObservableCollection<OhlcModel> _ohlcData;
        public ObservableCollection<OhlcModel> OhlcData
        {
            get => _ohlcData;
            set => this.RaiseAndSetIfChanged(ref _ohlcData, value);
        }
        #endregion

        private readonly GraphViewModel _graph;

        private ViewModelBase _content;
        public ViewModelBase Content
        {
            get => _content;
            set
            {
                _content = this;
                this.RaiseAndSetIfChanged(ref _content, value);
            }
        }

        private ViewModelBase _linesContent;

        public ViewModelBase LinesContent
        {
            get => _linesContent;
            set
            {
                _linesContent = this;
                this.RaiseAndSetIfChanged(ref _linesContent, value);
            }
        }

        public TradingViewModel(IOptions<OneGateClientOptions> options, 
            OneGateClientSession session)
        {
            ConnectionOptions = options;
            ClientSession = session;
            InitializeCollectionsAsync();
            _graph = new GraphViewModel(OhlcData);
            Content = _graph;
        }

        private async Task InitializeCollectionsAsync()
        {
            /*Exchanges = new ObservableCollection<ExchangeDto>();
            AssetTypes = new ObservableCollection<AssetTypeDto>();
            Tickers = new ObservableCollection<AssetDto>();
            //var exchanges = await ServerApi.GetExchangeByFilterAsync(new ExchangeFilterDto());
            var exchanges = new ExchangeDto[0];
            foreach (var exchange in exchanges)
            {
                Exchanges.Add(exchange);
            }
            var assetTypes = AssetTypeDto.GetValues(typeof(AssetTypeDto));
            foreach (AssetTypeDto assetType in assetTypes)
            {
                AssetTypes.Add(assetType);
            }
            //var tickers = await ServerApi.GetAssetsByFilterAsync(new AssetFilterDto());
            var tickers = new AssetDto[0];
            foreach (var ticker in tickers)
            {
                Tickers.Add(ticker);
            }*/
            try
            {
                var client = new OneGateClient(ConnectionOptions);
                OhlcData = await client.GetOhlcData();
                Layers = await client.GetGraphLayers();
                _currentLayers = new ObservableCollection<GraphLayer>();
            }
            catch (Exception e)
            {
                // To do.
            }
        }
    }
}