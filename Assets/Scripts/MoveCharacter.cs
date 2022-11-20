using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : MonoBehaviour//test casey-branch commit
{
    public int slot;
    private bool endTurn = false;
    public float speed = 1.0f;
    public Vector3 diff = new Vector3(1.85f, -2.14f, 0);
    public Vector3[] playerLocations;

    // Start is called before the first frame update
    void Start()
    {
        //origin = transform.position;
        //EventManager.OnClick += Move;
    }

    // Update is called once per frame
    void Update()
    {
        if(endTurn) {
            Debug.Log(name + " " + slot);
            transform.position = Vector3.MoveTowards(transform.position, playerLocations[slot], speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, playerLocations[slot]) < 0.1f) {
                endTurn = false;
            }
        }
    }

    //void Move() {
    //    endTurn = true;
    //    slot--;
    //    if(slot < 0) {
    //        slot = 2;
    //    }
    //}
}
