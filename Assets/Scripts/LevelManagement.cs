using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public List<GameObject> backgrounds;
    private Queue<GameObject> backgroundQueue = new Queue<GameObject>();
    public List<GameObject> enemies;
    private Queue<GameObject> enemiesQueue = new Queue<GameObject>();
    public GameObject activeEnemy;
    private GameObject activeBackground;
    public bool switchLevel = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(false);
            enemiesQueue.Enqueue(enemy);
        }
        foreach(GameObject background in backgrounds)
        {
            background.SetActive(false);
            backgroundQueue.Enqueue(background);
        }
        activeBackground = backgroundQueue.Dequeue();
        activeBackground.SetActive(true);
        activeEnemy = enemiesQueue.Dequeue();
        activeEnemy.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (switchLevel)
        {
            if (enemiesQueue.Count > 0)
            {
                activeEnemy.SetActive(false);
                activeEnemy = enemiesQueue.Dequeue();
                activeEnemy.SetActive(true);
                if (backgroundQueue.Count > 0)
                {
                    activeBackground.SetActive(false);
                    activeBackground = backgroundQueue.Dequeue();
                    activeBackground.SetActive(true);
                }
                switchLevel = false;
            }
            else
            {

            }
        }
    }
}
