using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnRotation : MonoBehaviour
{
   public GameObject[] player;
   public GameObject[] enemies;
   public Vector2 currentPlayerCoord;
   public Vector2 lastPlayerCoord;

   public float speed = 1.0f;
   private bool endTurn = false;
   // public Vector2 currentEnemyCoord;
   // public Vector2 lastEnemyCoord;
   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (endTurn)
      {
         Debug.Log(endTurn);
         foreach (GameObject character in player)
         {
            character.transform.position += new Vector3(speed * Time.deltaTime, -speed *Time.deltaTime, 0);
            Vector3 pos = character.transform.position;
            if(pos.x > currentPlayerCoord.x) {
               character.transform.position += new Vector3(-speed * Time.deltaTime, speed * Time.deltaTime, 0);
            }
         }
      }
   }


}
