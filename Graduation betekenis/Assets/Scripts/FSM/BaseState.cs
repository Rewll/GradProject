using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected FSM owner;
    public void Initalize(FSM owner)
    {
        this.owner = owner;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}