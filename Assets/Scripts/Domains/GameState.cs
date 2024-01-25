using System;
using Cysharp.Threading.Tasks;

namespace unity1week202312.State
{
    /**
     * ゲーム内状態一覧
     */
    public enum GameState {
        Initializing,
        TitleLoading,
        TitleShowing,
        MainLoading,
        MainPlaying,
        ResultShowing,
    }

    /**
    * 遷移経路一覧
    * @param 
    * - 遷移元
    * - 遷移先
    * - 条件
    * - 呼び出し関数
    */
    public struct StatePath {
        public GameState From { get; }
        public GameState To { get; }
        public Func<UniTask> Checker { get; }
        public Func<UniTask> Call { get; }
        public StatePath(GameState from, GameState to, Func<UniTask> checker, Func<UniTask> call) {
            From = from;
            To = to;
            Checker = checker;
            Call = call;
        }
    }

}