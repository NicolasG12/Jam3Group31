using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharState : MonoBehaviour
{
    public float health = 100f;
    public float confidence = 50f;
    public string move; //this variable keeps track of what attack the character can use.
    public float power;
    public int slot;
    public Vector3 target;
    public float speed;
    public string attackAnim;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        speed = (Vector3.Distance(transform.position, target) * 0.01f);
        
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
        anim.Play(attackAnim);
        //Debug.Log(self.name + " used "+type+". " + ((int)health).ToString() + " health, "+((int)confidence).ToString() + " confidence.");
    }

    public void generateConfidenceAttack(ref float power, ref string type) 
    {
        type = move;
        power = 20f;
        confidence -= 50;
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
                health -= epower/5;
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
        if (health < 0f) health = 0f;
        if (confidence < 0f) confidence = 0f;
        if (confidence > 100f) confidence = 100f;
    }


    public void ConfidenceStatUpdate(ref float epower, ref string etype)
    {
        this.confidence -= 50;
        if (move == "punch") {
            if (etype == "punch") health -= epower;
            else if (etype == "magic")  {}
            else if (etype == "block") {}
        }
        else if (move == "magic") {
            if (etype == "punch") {}
            else if (etype == "magic") {}
            else if (etype == "block") confidence += power/2; //magic breaks block
        }
        else if (move == "block") {
            if (etype == "punch") {}
            else if (etype == "magic") {this.confidence += 25;}
            else if (etype == "block") {}
        }
        if (health < 0f) health = 0f;
        if (confidence < 0f) confidence = 0f;
        else if (confidence > 100f) confidence = 100f;
    }

    public void DesperationStatUpdate(ref float cpower, string etype, float epower) {
        cpower = power / 2;
        float drainRate = 20f;
        if (etype != "block") {
            health -= epower;
            if (health < 0f) {
                health = 0f;
                return;
            }
        };
        if (confidence >= drainRate) {
            //Debug.Log("confidence greater"+"            ignore this number: "+Random.Range(0f, 100f).ToString());
            confidence -= drainRate;
            return;
        } else if (confidence < drainRate) {
            float remainder = drainRate - confidence;
            confidence = 0f;
            remainder /= 2;
            health -= remainder;
            //Debug.Log("confidence less "+remainder.ToString()+"            ignore this number: "+Random.Range(0f, 100f).ToString());
            if (health < 0f) health = 0f;
            return;
        }
    }

}
