using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    //List to hold characters (when placed should be in the order that characters are set up)
    public List<GameObject> characters;
    private GameObject enemy;
    private VisualElement Buttons;
    private VisualElement Health;
    //variables to hold each of the buttons
    private Button clockwise;
    private Button counterclockwise;
    private Button attack;
    private Button confidenceAttack;
    private VisualElement playerHealth;
    private VisualElement confidence;
    private VisualElement enemyHealth;
    private Button desperationAttack;
    //holds which character is in front
    private int inFrontSlot = 0;
    private GameObject inFront;
    private bool moveState = false;
    public GameObject levelManager;
    private bool isMoving = false;

    public AudioSource buttonPress;
    public AudioSource buttonFail;


    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;
        //create the buttons and attach them to a function when they are clicked
        Buttons = rootVisualElement.Q<VisualElement>("Buttons");
        clockwise = Buttons.Q<Button>("clockwise");
        clockwise.RegisterCallback<ClickEvent>(ev => SwapChar(0));
        counterclockwise = Buttons.Q<Button>("counter-clockwise");
        counterclockwise.RegisterCallback<ClickEvent>(ev => SwapChar(1));
        attack = Buttons.Q<Button>("Attack");
        attack.RegisterCallback<ClickEvent>(ev => Attack());
        confidenceAttack = Buttons.Q<Button>("confidence-attack");
        confidenceAttack.RegisterCallback<ClickEvent>(ev => ConfidenceAttack());
        desperationAttack = Buttons.Q<Button>("DesperationAttack");
        desperationAttack.RegisterCallback<ClickEvent>(ev => DesperationAttack());
        Health = rootVisualElement.Q<VisualElement>("Health");
        playerHealth = Health.Q<VisualElement>("health-progress");
        confidence = Health.Q<VisualElement>("confidence-progress");
        enemyHealth = Health.Q<VisualElement>("enemy-health-progress");
    }

    float enemyAttackPower = 0f;
    string enemyAttackType = "";
    float characterAttackPower = 0f;
    string characterAttackType = "";

    // Start is called before the first frame update
    void Start()
    {
        enemy = levelManager.GetComponent<LevelManagement>().activeEnemy;
        //assign the infront to the first character
        inFront = characters[inFrontSlot];
        enemy.GetComponent<EnemyState>().generateAttack(ref enemyAttackPower, ref enemyAttackType);
        Debug.Log(enemy.name+"\'s gonna use "+enemyAttackType+" next turn!"+"           ignore this number: "+Random.Range(0f, 100f).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        enemy = levelManager.GetComponent<LevelManagement>().activeEnemy;
        playerHealth.style.width = Length.Percent(characters[inFrontSlot].GetComponent<CharState>().health);
        confidence.style.width = Length.Percent(characters[inFrontSlot].GetComponent<CharState>().confidence);
        enemyHealth.style.width = Length.Percent(enemy.GetComponent<EnemyState>().health);
        if (moveState)
        {
            isMoving = true;
            foreach (var character in characters)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, character.GetComponent<CharState>().target, character.GetComponent<CharState>().speed);
                if (Vector3.Distance(character.transform.position, character.GetComponent<CharState>().target) <= 0.1f)
                {
                    moveState = false;
                    isMoving = false;
                }
            }

        }

    }
    //swaps two characters based on the button that was pushed
    private void SwapChar(int data)
    {
        if (!isMoving)
        {
            buttonPress.Play();
            moveState = true;
            int last = characters.Count - 1;

            switch (data)
            {
                case 0:
                    for (int i = 0; i <= last; i++)
                    {
                        if (i == 0)
                        {
                            characters[i].GetComponent<CharState>().target = characters[last].transform.position;
                        }
                        else
                        {
                            characters[i].GetComponent<CharState>().target = characters[i - 1].transform.position;
                        }
                    }
                    inFrontSlot = (inFrontSlot + 1) % characters.Count;
                    break;
                case 1:
                    for (int i = 0; i <= last; i++)
                    {
                        if (i == characters.Count - 1)
                        {
                            characters[i].GetComponent<CharState>().target = characters[0].transform.position;
                        }
                        else
                        {
                            characters[i].GetComponent<CharState>().target = characters[i + 1].transform.position;
                        }
                    }
                    inFrontSlot = Mathf.Abs((inFrontSlot + last) % characters.Count);
                    inFront = characters[inFrontSlot];
                    break;
                default:
                    break;
            }
        }

    }


    //function to handle attack logic (unfinished)
    private void Attack()
    {
        if (enemy.GetComponent<EnemyState>().health <= 0f) return;
        buttonPress.Play();
        //enemy.GetComponent<EnemyState>().generateAttack(ref enemyAttackPower, ref enemyAttackType);
        characters[inFrontSlot].GetComponent<CharState>().generateAttack(ref characterAttackPower, ref characterAttackType);
        Debug.Log(characters[inFrontSlot].name + " used "+ characterAttackType+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(enemy.name + " used "+enemyAttackType+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        characters[inFrontSlot].GetComponent<CharState>().StatUpdate(ref enemyAttackPower, ref enemyAttackType);
        enemy.GetComponent<EnemyState>().StatUpdate(ref characterAttackPower, ref characterAttackType);
        Debug.Log(characters[inFrontSlot].name+": "+characters[inFrontSlot].GetComponent<CharState>().health.ToString()+"/"+characters[inFrontSlot].GetComponent<CharState>().confidence.ToString()+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(enemy.name+": "+enemy.GetComponent<EnemyState>().health.ToString()+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log("============"+"           ignore this number: "+Random.Range(0f, 100f).ToString());
        enemy.GetComponent<EnemyState>().generateAttack(ref enemyAttackPower, ref enemyAttackType);
        Debug.Log(enemy.name+"\'s gonna use "+enemyAttackType+" next turn!"+"           ignore this number: "+Random.Range(0f, 100f).ToString()); 
        
        if (characters[inFrontSlot].GetComponent<CharState>().health <= 0f) Die();
        if (enemy.GetComponent<EnemyState>().health <= 0f) {
            Debug.Log("enemy "+enemy.name+" has been defeated!");
        }
    }

    private void Die() {
        bool everybodyDead = false;
        Debug.Log(characters[inFrontSlot].name+" died!");
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

    private void ConfidenceAttack() {

        if (inFront.GetComponent<CharState>().confidence < 50f || enemy.GetComponent<EnemyState>().health <= 0f)
        {
            buttonFail.Play();
            return;
        }

        inFront.GetComponent<CharState>().generateConfidenceAttack(ref characterAttackPower, ref characterAttackType);
        string ctype = "";  //only used for the Debug Log
        if (characterAttackType == "punch") ctype = "SUPER MAXIMUM KICK";
        else if (characterAttackType == "magic") ctype = "Telekinesis";
        else if (characterAttackType == "block") ctype = "Were Mouse Counter";
        Debug.Log(inFront.name + " used "+ ctype+"!             ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(enemy.name + " used "+enemyAttackType+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        inFront.GetComponent<CharState>().ConfidenceStatUpdate(ref enemyAttackPower, ref enemyAttackType);
        enemy.GetComponent<EnemyState>().ConfidenceStatUpdate(ref characterAttackPower, ref characterAttackType);
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

        public void DesperationAttack() {
        if (enemy.GetComponent<EnemyState>().health <= 0f) return;
        buttonPress.Play();
        int liveTeammates = 0;
        for (int i=0; i<characters.Count; i++) {
            if (characters[i].GetComponent<CharState>().health > 0f) liveTeammates++;
        }
        if (liveTeammates != 1) {
            Debug.Log("Can't use desperation attack.  Still "+liveTeammates.ToString()+" alive."+"            ignore this number: "+Random.Range(0f, 100f).ToString());
            return;
        }

        inFront.GetComponent<CharState>().DesperationStatUpdate(ref characterAttackPower, enemyAttackType, enemyAttackPower);
        enemy.GetComponent<EnemyState>().DesperationStatUpdate(characterAttackPower);
        Debug.Log(inFront.name + " used desperation attack!             ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(enemy.name + " used "+enemyAttackType+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(inFront.name+": "+inFront.GetComponent<CharState>().health.ToString()+"/"+inFront.GetComponent<CharState>().confidence.ToString()+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log(enemy.name+": "+enemy.GetComponent<EnemyState>().health.ToString()+"            ignore this number: "+Random.Range(0f, 100f).ToString());
        Debug.Log("============"+"           ignore this number: "+Random.Range(0f, 100f).ToString());
        enemy.GetComponent<EnemyState>().generateAttack(ref enemyAttackPower, ref enemyAttackType);
        Debug.Log(enemy.name+"\'s gonna use "+enemyAttackType+" next turn!"+"           ignore this number: "+Random.Range(0f, 100f).ToString()); 
    }
}
