using Game.Resource;
using Game.View.UI.View;

namespace Game.View.UI.Presenter
{
    public class ResourceListener
    {
        private readonly ResourceStorage _woodStorage;
        private readonly ResourceView _resourceView;

        public ResourceListener(ResourceStorage woodStorage, ResourceView resourceView)
        {
            _woodStorage = woodStorage;
            _resourceView = resourceView;
            _resourceView.Setup(_woodStorage.Resource);
            _woodStorage.OnResourceEarned += _resourceView.Increment;
            _woodStorage.OnResourceSpent += _resourceView.Decrement;
        }

        public void Dispose()
        {
            _woodStorage.OnResourceEarned -= _resourceView.Increment;
            _woodStorage.OnResourceSpent -= _resourceView.Decrement;
        }
    }
}