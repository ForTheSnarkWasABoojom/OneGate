//using OneGate.Frontend.ApiLibrary;
using ReactiveUI;

namespace OneGate.Frontend.DesktopApp.ViewModels.Frames
{
    public class TradingViewModel : ViewModelBase
    {
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

        #endregion

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

        private async void InitializeCollections()
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
        }

        public TradingViewModel(/*OneGateApi serverApi*/)
        {
            //ServerApi = serverApi;
            InitializeCollections();
            Content = new GraphViewModel();
        }
    }
}