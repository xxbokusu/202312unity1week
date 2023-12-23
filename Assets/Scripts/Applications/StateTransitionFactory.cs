using System.Threading;

namespace unity1week202312.State
{
    public class StateTransitionFactory
    {
        CancellationToken _token;
        public StateTransitionFactory(CancellationToken token)
        {
            _token = token;
        }

        public TransitionCondition Create()
        {
            return new TransitionCondition(_token);
        }
    }
}