namespace TeamBuilder.App.Core.Commands.Constracts
{
    public interface ICommand
    {
        string Execute(params string[] args);
    }
}
