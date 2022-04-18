using System.Windows;

namespace CustomerDetails
{
    public interface IViewFactory
    {
        FrameworkElement? ResolveView(object viewModel);
    }
}
