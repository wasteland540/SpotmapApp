namespace KS21.MvvmCross.Plugins.SimpleTcp
{
    public interface ITcpClientTask
    {
        string OpenClientAndConnectToServer(string ipAddress, int port);
    }
}