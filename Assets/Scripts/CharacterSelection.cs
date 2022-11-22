using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    //List to hold characters (when placed should be in the order that characters are set up)
    public List<GameObject> characters;
    public GameObject enemy;
    private VisualElement frame;
    //variables to hold each of the buttons
    private Button mecha;
    private Button magical_girl;
    private Button super_sentai;
    private Button attack;
    //Vectors to hold the location of the front and swapping character
    private Vector3 front;
    private Vector3 target;
    //holds which character is in front
    private GameObject inFront;
    private int moveState = -1;
    private int currentChar = -1;

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

    float enemyAttackPower = 0f;
    string enemyAttackType = "";
    float characterAttackPower = 0f;
    string characterAttackType = "";

    // Start is called before the first frame update
    void Start()
    {
        //assign the infront to the first character
        front = characters[0].transform.position;
        inFront = characters[0];
        enemy.GetComponent<EnemyState>().generateAttack(ref enemyAttackPower, ref enemyAttackType);
        Debug.Log(enemy.name+"\'s gonna use "+enemyAttackType+" next turn!"+"           ignore this number: "+Random.Range(0f, 100f).ToString());
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
        currentChar = data;
        if (moveState == -1 && characters[data].GetComponent<CharState>().health > 0) {
        //set the target to whichever character is chosen
            target = characters[data].transform.position;
            speed = (Vector3.Distance(target, front)) * 0.01f;
            moveState = data;
        }
        //moves the characters to their respective positions
        /*while (Vector3.Distance(inFront.transform.position, target) > 0.1f)
        {
            inFront.transform.position = Vector3.MoveTowards(inFront.transform.position, target, speed);
        }
        while (Vector3.Distance(characters[data].transform.position, front) > 0.1f) {
            characters[data].transform.position = Vector3.MoveTowards(characters[data].transform.position, front, speed);
        }*/
        //moving = true;
        //update the infront character
    }


    //function to handle attack logic (unfinished)
    private void Attack()
    {
        if (enemy.GetComponent<EnemyState>().health <= 0f) return;
        //enemy.GetComponent<EnemyState>().generateAttack(ref enemyAttackPower, ref enemyAttackType);
        inFront.GetComponent<CharState>().generateAttack(ref characterAttackPower, ref characterAttackType);
        Debug.Log(inFront.name + " used "+ characterAttackType+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(enemy.name + " used "+enemyAttackType+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        inFront.GetComponent<CharState>().StatUpdate(ref enemyAttackPower, ref enemyAttackType);
        enemy.GetComponent<EnemyState>().StatUpdate(ref characterAttackPower, ref characterAttackType);
        Debug.Log(inFront.name+": "+inFront.GetComponent<CharState>().health.ToString()+"/"+inFront.GetComponent<CharState>().confidence.ToString()+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(enemy.name+": "+enemy.GetComponent<EnemyState>().health.ToString()+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log("============"+"           ignore this number: "+Random.Range(0f, 100f).ToString());
        enemy.GetComponent<EnemyState>().generateAttack(ref enemyAttackPower, ref enemyAttackType);
        Debug.Log(enemy.name+"\'s gonna use "+enemyAttackType+" next turn!"+"           ignore this number: "+Random.Range(0f, 100f).ToString()); 
        
        if (inFront.GetComponent<CharState>().health <= 0f) Die();
        if (enemy.GetComponent<EnemyState>().health <= 0f) {
            Debug.Log("enemy "+enemy.name+" has been defeated!");
        }
    }

    private void Die() {
        bool everybodyDead = false;
        Debug.Log(inFront.name+" died!");
        int nextChar = -1;
        if (characters[0].GetComponent<CharState>().health > 0) nextChar = 0;
        else if (characters[1].GetComponent<CharState>().health > 0) nextChar = 1;
        else if (characters[2].GetComponent<CharState>().health > 0) nextChar = 2;
        else {
            everybodyDead = true;
            Debug.Log("All teammates are dead!");
        }
        if (!everybodyDead) SwapChar(nextChar);
    }
}
