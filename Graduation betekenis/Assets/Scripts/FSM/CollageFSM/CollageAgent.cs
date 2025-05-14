using UnityEngine;

public class CollageAgent : MonoBehaviour
{
    private FSM fsm;
    private System.Type startState;
    public enum Collagestaten {CollageInitState, CherryPickState, CollageCreateState}

    Collagestaten StartStaat;
    public Collagestaten huidigeStaat;
    
    void Start()
    {
        /*switch (StartStaat)
        {

        }*/
        //huidigeStaat = StartStaat;
        startState = typeof(CollageInitState);
        fsm = new FSM(startState, GetComponents<BaseState>()); //Starting state, with getcomponentSSS because multiple states are being used
    }

    void Update()
    {
        fsm.OnUpdate();
    }

    void FixedUpdate()
    {
        fsm.OnFixedUpdate();
    }
}
