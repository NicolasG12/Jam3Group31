using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characters;
    private VisualElement frame;
    private Button mecha;
    private Button magical_girl;
    private Button super_sentai;
    private Button attack;
    public Vector3 front;
    private GameObject inFront;
    private Vector3 target;
    public float speed = 1.0f;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;
        frame = rootVisualElement.Q<VisualElement>("Frame");
        mecha = frame.Q<Button>("MechaGirl");
        mecha.RegisterCallback<ClickEvent>(ev => SwapChar(0));
        magical_girl = frame.Q<Button>("MagicalGirl");
        magical_girl.RegisterCallback<ClickEvent>(ev => SwapChar(1));
        super_sentai = frame.Q<Button>("SuperSentai");
        super_sentai.RegisterCallback<ClickEvent>(ev => SwapChar(2));
        attack = frame.Q<Button>("Attack");
        attack.RegisterCallback<ClickEvent>(ev => Attack());
    }

    // Start is called before the first frame update
    void Start()
    {
        inFront = characters[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SwapChar(int data)
    {
        target = characters[data].transform.position;
        while (Vector3.Distance(inFront.transform.position, target) > 0.1f)
        {
            inFront.transform.position = Vector3.MoveTowards(inFront.transform.position, target, speed);
        }
        while (Vector3.Distance(characters[data].transform.position, front) > 0.1f) {
            characters[data].transform.position = Vector3.MoveTowards(characters[data].transform.position, front, speed);
        }
        inFront = characters[data];
    }

    private void Attack()
    {
        Debug.Log(inFront + " attacked");
        
    }
}
