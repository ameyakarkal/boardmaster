using System.Threading.Tasks;
using Bot.Domain;

namespace Bot.Handlers
{
    public class NominationHandler
    {
        private readonly IPersistence _persistence;
        private readonly Messenger _messenger;

        public NominationHandler(IPersistence persistence, Messenger messenger)
        {
            _persistence = persistence;
            _messenger = messenger;
        }

        public async Task<HandlerResponse<Notification>> Handle()
        {
            var boardMaster = await BoardMaster.Get(_persistence);

            var nominee = boardMaster.Pick();

            var notification = _messenger.Nudge(nominee);

            await boardMaster.Save(_persistence);

            return new HandlerResponse<Notification>(notification);
        }
    }
}
