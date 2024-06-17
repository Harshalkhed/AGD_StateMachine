using StatePattern.Player;
using StatePattern.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

namespace StatePattern.Enemy
{
    public class PatrolManController : EnemyController
    {
        private PatrolManStateMachine stateMachine;

        public PatrolManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(StateMachine.States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new PatrolManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(StateMachine.States.CHASING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(StateMachine.States.IDLE);
    }
}