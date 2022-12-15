using UnityEngine;

public abstract class SlimeBaseState : MonoBehaviour{
    public GameObject player;
    public Transform pos;
    public Enemy enemy;

    public abstract void OnEnter(SlimeStateManager ssm);

    public abstract void OnUpdate(SlimeStateManager ssm);

    public abstract void OnExit(SlimeStateManager ssm);
}
