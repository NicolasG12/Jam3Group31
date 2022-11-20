using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    public delegate void EndTurn();
    public static event EndTurn OnClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTest(InputValue change) {
        //Debug.Log("testing");
        if(OnClick != null) {
            OnClick();
        }
    }
}
