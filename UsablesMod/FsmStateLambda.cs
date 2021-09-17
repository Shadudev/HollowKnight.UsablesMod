using HutongGames.PlayMaker;
using System;

namespace UsablesMod
{
    class FsmStateLambda : FsmStateAction
    {
        private readonly Action method;

        public FsmStateLambda(Action method)
        {
            this.method = method;
        }

        public override void OnEnter()
        {
            try
            {
                method();
            }
            catch (Exception e)
            {
                LogHelper.LogError("Error in FsmStateLambda:" + e);
                LogHelper.LogError(e.StackTrace);
            }

            Finish();
        }
    }
}
