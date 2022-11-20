using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characters;
    //public VisualTreeAsset mainUI;
    private VisualElement frame;
    private Button mecha;
    private Button magical_girl;
    private Button super_sentai;
    private List<Vector3> locations = new List<Vector3>();
    private bool swap = false;
    private int toFront;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject character in characters)
        {
            locations.Add(character.transform.position);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //if(swap)
        //{
        //    Vector3 character_loc = characters[toFront].transform.position;
        //    character_loc = Vector3.MoveTowards(character_loc, locations[0], speed * Time.deltaTime);
        //    if(Vector3.Distance(character_loc, locations[0]) < 0.1f)
        //    {
        //        swap = false;
        //    }
        //}
    }
    private void SwapChar(int data)
    {
        for (int i = 0; i < 1000;  i++) {
            characters[data].transform.position = Vector3.MoveTowards(characters[data].transform.position, locations[0], speed * Time.deltaTime);
        }
        //toFront = data;
        //swap = true;
    }
}
