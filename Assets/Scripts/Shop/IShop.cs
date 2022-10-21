namespace FPS
{
    public interface IShop
    {
        Wallet PlayerWallet { get; }
        void Buy();
    }
}
