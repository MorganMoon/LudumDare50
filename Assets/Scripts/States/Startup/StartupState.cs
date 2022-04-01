using Cerberus;

namespace LudumDare50.Client.States.Startup
{
    public enum StartupStateEvent
    {
        PlayGame
    }

    public enum StartupStateSubState
    {
        MainMenu,
        SettingsMenu,
        CreditsMenu
    }

    public class StartupState : State
    {
        
    }
}