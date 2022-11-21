using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharState : MonoBehaviour
{
    public float health = 100f;
    public float confidence = 100f;
    public GameObject self;
        // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void Attack(ref float power, ref string type) 
    {
        if (self.name == "SUPER MAXIMUM RANGER") type = "punch";
        else if (self.name == "Sophrona") type = "block";
        else type = "magic";
        power = 20;
        //Debug.Log(self.name + " used "+type+". " + ((int)health).ToString() + " health, "+((int)confidence).ToString() + " confidence.");
    }
}
