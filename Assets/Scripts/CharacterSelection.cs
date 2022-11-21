using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    //List to hold characters (when placed should be in the order that characters are set up)
    public List<GameObject> characters;
    private VisualElement frame;
    //variables to hold each of the buttons
    private Button mecha;
    private Button magical_girl;
    private Button super_sentai;
    private Button attack;
    //Vectors to hold the location of the front and swapping character
    public Vector3 front;
    private Vector3 target;
    //holds which character is in front
    private GameObject inFront;
    private int moveState = -1;

    public float speed = 0.01f;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;
        //create the buttons and attach them to a function when they are clicked
        frame = rootVisualElement.Q<VisualElement>("Frame");
        mecha = frame.Q<Button>("MechaGirl");
        mecha.RegisterCallback<ClickEvent>(ev => SwapChar(0));
        //mecha.RegisterCallback<ClickEvent>(ev => {moveState = 0});
        magical_girl = frame.Q<Button>("MagicalGirl");
        magical_girl.RegisterCallback<ClickEvent>(ev => SwapChar(1));
        //magical_girl.RegisterCallback<ClickEvent>(ev => {moveState = 1});
        super_sentai = frame.Q<Button>("SuperSentai");
        super_sentai.RegisterCallback<ClickEvent>(ev => SwapChar(2));
        //super_sentai.RegisterCallback<ClickEvent>(ev => {moveState = 2});
        attack = frame.Q<Button>("Attack");
        attack.RegisterCallback<ClickEvent>(ev => Attack());
    }

    // Start is called before the first frame update
    void Start()
    {
        //assign the infront to the first character
        inFront = characters[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (moveState != -1) {
            //moves the characters to their respective positions
            //while (Vector3.Distance(inFront.transform.position, target) > 0f)
            //{
                inFront.transform.position = Vector3.MoveTowards(inFront.transform.position, target, speed);
            //}
            //while (Vector3.Distance(characters[moveState].transform.position, front) > 0f) {
                characters[moveState].transform.position = Vector3.MoveTowards(characters[moveState].transform.position, front, speed);
            //}
            if ((Vector3.Distance(inFront.transform.position, target) == 0f) && (Vector3.Distance(characters[moveState].transform.position, front)) == 0f) {
                inFront = characters[moveState];
                moveState = -1;
            }
            //update the infront character
        }

    }
    //swaps two characters based on the button that was pushed
    private void SwapChar(int data)
    {
        //set the target to whichever character is chosen
        target = characters[data].transform.position;
        speed = (Vector3.Distance(target, front)) * 0.01f;
        //moves the characters to their respective positions
        /*while (Vector3.Distance(inFront.transform.position, target) > 0.1f)
        {
            inFront.transform.position = Vector3.MoveTowards(inFront.transform.position, target, speed);
        }
        while (Vector3.Distance(characters[data].transform.position, front) > 0.1f) {
            characters[data].transform.position = Vector3.MoveTowards(characters[data].transform.position, front, speed);
        }*/
        moveState = data;
        //moving = true;
        //update the infront character
    }
    //function to handle attack logic
    private void Attack()
    {
        Debug.Log(inFront + " attacked");
        
    }
}
