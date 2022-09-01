namespace FPS
{
    public interface IShop<T>
    {
        Wallet PlayerWallet { get; }
        void Buy(T type, int cost);
    }
}
