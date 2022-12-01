using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float health = 100f;
    public float attack;
    private string move;
    public GameObject self;
    public GameObject levelManager;
    public string idleAnim;
    public string magicAnim;
    public string punchAnim;
    public string blockAnim;
    private float crossFadeTime = 0.1f;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(anim);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0f)
        {
            Death();
        }
        
    }

    public void generateAttack(ref float power, ref string type) 
    {   
        float got = Random.Range(0f, 9f);
        if (got < 3f)
        {
            type = "magic";
            anim.CrossFadeInFixedTime(magicAnim, crossFadeTime);
        }
        else if (got < 6f)
        {
            type = "punch";
            anim.CrossFadeInFixedTime(punchAnim, crossFadeTime);
        }
        else
        {
            type = "block";
            anim.CrossFadeInFixedTime(blockAnim, crossFadeTime);
        }
        move = type;
        //move = "block";
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
        anim.CrossFadeInFixedTime(idleAnim, crossFadeTime);
    }

    public void ConfidenceStatUpdate(ref float cpower, ref string ctype) {
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
        anim.CrossFadeInFixedTime(idleAnim, crossFadeTime);
    }

    public void DesperationStatUpdate(float cpower) {
        health -= cpower;
        anim.CrossFadeInFixedTime(idleAnim, crossFadeTime);
    }

    private void Death()
    {
        levelManager.GetComponent<LevelManagement>().switchLevel = true;
    }
}
