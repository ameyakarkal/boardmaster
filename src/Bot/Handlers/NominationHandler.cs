using Bot.Domain;

namespace Bot.Handlers
{
    public class NominationHandler
    {
        private readonly BoardMaster _boardMaster;
        
        private readonly Messenger _messenger;

        public NominationHandler(BoardMaster boardMaster, Messenger messenger)
        {
            _boardMaster = boardMaster;
            _messenger = messenger;
        }

        public HandlerResponse<Notification> Handle()
        {
            var nominee = _boardMaster.Pick();

            var notification = _messenger.Nudge(nominee);

            return new HandlerResponse<Notification>(notification);
        }
    }
}
