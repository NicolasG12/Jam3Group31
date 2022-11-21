using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float health = 100f;
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
        float got = Random.Range(0f, 9f);
        if (got < 3f) type = "magic";
        else if (got < 6f) type = "punch";
        else type = "block";
        power = 15;
        //Debug.Log(self.name + " used "+got.ToString()+". " + ((int)health).ToString() + " health.");
    }
}
