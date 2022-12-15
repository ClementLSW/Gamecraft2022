using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour{
    public abstract void OnEnter(EnemyStateManager esm);

    public abstract void OnUpdate(EnemyStateManager esm);

    public abstract void OnExit(EnemyStateManager esm);

    public abstract void OnCollide(EnemyStateManager esm, Collision2D col);
}
