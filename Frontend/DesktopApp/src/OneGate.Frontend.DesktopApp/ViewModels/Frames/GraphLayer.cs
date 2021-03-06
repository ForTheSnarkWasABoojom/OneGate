namespace OneGate.Frontend.DesktopApp.ViewModels.Frames
{
    public class GraphLayer
    {
        public string Name { get; set; }

        public double[][] Data { get; set; }

        public GraphLayer(string name)
            => Name = name;

        public override string ToString()
            => Name;
    }
}