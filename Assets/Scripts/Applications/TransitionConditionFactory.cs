using System.Threading;

namespace unity1week202312.State
{
    public class TransitionConditionFactory
    {
        CancellationToken _token;
        public TransitionConditionFactory(CancellationToken token)
        {
            _token = token;
        }

        public TransitionCondition Create()
        {
            return new TransitionCondition(_token);
        }
    }
}