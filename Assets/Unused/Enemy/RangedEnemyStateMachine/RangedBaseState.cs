using UnityEngine;

public abstract class RangedBaseState : MonoBehaviour{
    public GameObject player;
    public Transform pos;
    public Enemy enemy;

    public abstract void OnEnter(RangedStateManager rsm);

    public abstract void OnUpdate(RangedStateManager rsm);

    public abstract void OnExit(RangedStateManager rsm);

}
