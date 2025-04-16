using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private readonly EnemyAI enemy;

    public EnemyPatrolState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        Debug.Log("Entering Patrol");
        enemy.agent.SetDestination(enemy.GetNextPatrolPoint().position);

    }

    public void UpdateState()
    {
        if (enemy.IsPlayerVisible())
        {
            enemy.SwitchState(enemy.ChaseState);
            return;
        }

        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            enemy.agent.SetDestination(enemy.GetNextPatrolPoint().position);

        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Patrol");
    }
}
