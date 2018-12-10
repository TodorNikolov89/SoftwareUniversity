namespace Solid.Logger.Appenders.Factory.Contrats
{
    using Solid.Logger.Appenders.Contracts;
    using Solid.Logger.Layouts.Contracts;

    public interface IAppenderFactory
    {
        IAppender CreateAppender(string type, ILayout layout);
    }
}
