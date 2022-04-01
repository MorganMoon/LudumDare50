using LudumDare50.Client.ViewModels;

namespace LudumDare50.Client.Infrastructure
{
    public interface IScreenService
    {
        IScreenBuilder SetActiveScreen();
        T SetActiveScreen<T>() where T : ViewModel;
        T SetActiveScreen<T, TParameter>(TParameter parameter) where T : ViewModel<TParameter>;
        T AddToScreen<T, TParameter>(TParameter parameter) where T : ViewModel<TParameter>;
        T AddToScreen<T>() where T : ViewModel;

        void RemoveFromScreen<T>() where T : ViewModel;
        void ClearScreen();

        bool TryGetViewModel<T>(out T viewModel) where T : ViewModel;
    }

    public interface IScreenBuilder
    {
        IScreenBuilder With<T>() where T : ViewModel;
        IScreenBuilder With<T, TParameter>(TParameter parameter) where T : ViewModel<TParameter>;
        void Build();
    }
}