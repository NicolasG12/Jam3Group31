using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharState : MonoBehaviour
{
    public float health = 100f;
    public float confidence = 100f;
    public string move; //this variable keeps track of what attack the character can use.
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
        //this code would break if we changed any names, so I changed to using the public "move" variable
        //if (self.name == "SUPER MAXIMUM RANGER") type = "punch";
        //else if (self.name == "Sophrona") type = "block";
        //else type = "magic";
        type = move;
        float cmultiplier = 100f/confidence;
        power = 20f * cmultiplier;
        //Debug.Log(self.name + " used "+type+". " + ((int)health).ToString() + " health, "+((int)confidence).ToString() + " confidence.");
    }

    public void StatUpdate(ref float power, ref string etype)
    {
        //unfinished logic system
        if (move == "punch") {
            if (etype == "punch") {
                health -= power;
                confidence -= power/2;
            }
            else if (etype == "magic") {
                
            }
        }
        else if (move == "magic") {

        }
        else if (move == "block") {

        }
    }
}
