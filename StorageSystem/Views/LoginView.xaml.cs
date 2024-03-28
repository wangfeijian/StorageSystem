using StorageSystem.Extensions;
using System.Windows.Controls;

namespace StorageSystem.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView(IEventAggregator aggregator)
        {
            InitializeComponent();

            //注册提示消息
            aggregator.ResgiterMessage(arg =>
            {
                LoginSnakeBar.MessageQueue.Enqueue(arg.Message);
            }, "Login");
        }
    }
}
