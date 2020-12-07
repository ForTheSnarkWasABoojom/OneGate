using System;
using System.Threading.Tasks;

namespace OneGate.Backend.Engines.Base.OhlcProvider
{
    public interface IOhlcProvider
    {
        public event Func<IOhlcProvider, OhlcProviderEventArgs, Task> OnPriceChanged;
        
        public int AssetId { get; }
    }
}