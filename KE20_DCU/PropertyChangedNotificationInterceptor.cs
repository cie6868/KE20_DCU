using System;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace KE20_DCU
{
    /**
     *  Tells Fody.PropertyChanged to use UI thread.
     */
    public static class PropertyChangedNotificationInterceptor
    {
        public static async void Intercept(object target, Action onPropertyChangedAction, string propertyName)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                onPropertyChangedAction();
            });
        }
    }
}
