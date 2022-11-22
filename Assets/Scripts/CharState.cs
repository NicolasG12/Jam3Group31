using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharState : MonoBehaviour
{
    public float health = 100f;
    public float confidence = 50f;
    public string move; //this variable keeps track of what attack the character can use.
    public float power;
        // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void generateAttack(ref float power, ref string type) 
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

    //e for enemy
    public void StatUpdate(ref float epower, ref string etype)
    {
        if (move == "punch") {
            if (etype == "punch") { //punch vs punch, both take damage
                health -= epower;
            }
            else if (etype == "magic")  confidence += epower/2; //punch vs magic
            else if (etype == "block") { //punch vs block
                confidence -= epower/3;
                health -= epower/8;
            }
        }
        else if (move == "magic") {
            if (etype == "punch") { 
                health -= epower;
                confidence -= epower/2; 
            }
            else if (etype == "magic") {}//magic vs magic; do nothing
            else if (etype == "block") confidence += power/2; //magic breaks block
        }
        else if (move == "block") {
            if (etype == "punch") confidence += epower;
            else if (etype == "magic") {
                health -= epower;
                confidence -= epower/2;
            }
            else if (etype == "block") {}
        }
    }
}
