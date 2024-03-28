namespace StorageSystem.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider;

        public NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
