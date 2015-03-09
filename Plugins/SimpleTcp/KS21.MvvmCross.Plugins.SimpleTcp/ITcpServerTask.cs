namespace KS21.MvvmCross.Plugins.SimpleTcp
{
    public interface ITcpServerTask
    {
        void StartServerAndWaitingForClient(int port);
    }
}