using UnityEngine;
using StatePattern.StateMachine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;
using static UnityEditor.VersionControl.Asset;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private OnePunchManStateMachine stateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(StateMachine.States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(StateMachine.States.SHOOTING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(StateMachine.States.IDLE);
    }
}