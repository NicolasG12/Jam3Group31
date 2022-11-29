using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float health = 100f;
    public float attack;
    private string move;
    public GameObject self;
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
        float got = Random.Range(0f, 9f);
        if (got < 3f) type = "magic";
        else if (got < 6f) type = "punch";
        else type = "block";
        move = type;
        power = attack;
        //Debug.Log(self.name + " used "+got.ToString()+". " + ((int)health).ToString() + " health.");
    }

    public void StatUpdate(ref float cpower, ref string ctype) {
        if (move == "punch") {
            if (ctype == "punch") health -= cpower;
            else if (ctype == "magic") {}
            else if (ctype == "block") health -= cpower/8;
        }
        else if (move == "magic") {
            if (ctype == "punch") health -= cpower;
            else if (ctype == "magic") {}
            else if (ctype == "block") {}
        }
        else if (move == "block") {
            if (ctype == "punch") {}
            else if (ctype == "magic") health -= cpower;
            else if (ctype == "block") {}
        }
    }

    public void ConfidenceStatUpdate(ref float cpower, ref string ctype) {
            //unfinished code.  Feel free to delete or replace this code, or even this entire function.  It's mostly just copied from StatUpdate anyway.


        if (move == "punch") {
            if (ctype == "punch") health -= cpower;
            else if (ctype == "magic") {health -= cpower;}
            else if (ctype == "block") health -= cpower*1.5f;
        }
        else if (move == "magic") {
            if (ctype == "punch") health -= cpower;
            else if (ctype == "magic") {health -= cpower;}
            else if (ctype == "block") {health -= cpower*0.6f;}
        }
        else if (move == "block") {
            if (ctype == "punch") {health -= cpower;}
            else if (ctype == "magic") health -= cpower;
            else if (ctype == "block") {}
        }
        
    }
}
