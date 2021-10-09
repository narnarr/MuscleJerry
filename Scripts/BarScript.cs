using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour
{
    public GameObject barPrefab;
    int start;

    // Start is called before the first frame update
    void Start()
    {
        start = 1;
            //(int)Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;

        loadBar();
    }

    //7

    public void loadBar()
    {
        GameObject clone = Instantiate(barPrefab);

        for (int i = 0; i < 12; i++)
        {
            float spawnY = Random.Range(start, (int)Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + 10) * 1.5f;
            float spawnX = Random.Range(-1, 2) * 1.8f;

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            GameObject c = Instantiate(clone, spawnPosition, Quaternion.identity);
        }
        start = (int)Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + 10;

        Destroy(clone);
    }
}
