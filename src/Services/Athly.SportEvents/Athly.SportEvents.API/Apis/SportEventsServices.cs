using MediatR;

namespace Athly.SportEvents.API.Apis
{
    public class SportEventsServices(IMediator mediator, ILogger<SportEventsServices> logger)
    {
        public IMediator Mediator { get; } = mediator;
        public ILogger<SportEventsServices> Logger { get; } = logger;
    }
}
