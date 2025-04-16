using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    private readonly EnemyAI enemy;

    public EnemyChaseState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        Debug.Log("Entering Chase");
        enemy.ResetChaseTimer();
    }

    public void UpdateState()
    {
        enemy.agent.SetDestination(enemy.player.position);
        enemy.IncrementChaseTimer();

        if (enemy.IsPlayerInAttackRange())
        {
            enemy.agent.isStopped = true;
            enemy.AttackPlayer();
        }
        else
        {
            enemy.agent.isStopped = false;
        }

        if (!enemy.IsPlayerVisible() && enemy.ChaseTimeExceeded())
        {
            enemy.SwitchState(enemy.PatrolState);
        }
    }

    public void ExitState()
    {
        enemy.agent.isStopped = false;
        Debug.Log("Exiting Chase");
    }
}
