using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharSelect : MonoBehaviour
{
    //Queue to hold the order of the characters
    private Queue<GameObject> characters = new Queue<GameObject>();

    //Variables to hold the buttons
    private Button clockwise;
    private Button counterclockwise;
    private Button attack;
    private Button confidenceAttack;
    //Variables to hold locations

    // Start is called before the first frame update

    private void OnEnable()
    {
        
    }
    void Start()
    {
        characters.Enqueue(GameObject.Find("Sophrona"));
        characters.Enqueue(GameObject.Find("Josh From I.T."));
        characters.Enqueue(GameObject.Find("SUPER MAXIMUM RANGER"));
        Debug.Log(characters.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void RotateChars(int data)
    //{
    //    switch(data)
    //    {
    //        case 0:

    //    }
    //}
}
