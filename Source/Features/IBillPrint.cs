namespace CoEdge.Features
{
    public interface IBillPrint : IFeature
    {
        void PrintTitle(string v);

        void PrintRow(short idx, string name, decimal price, short qty);

        void PrintBottomLn();
    }
}