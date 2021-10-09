using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    float direction = 0.2f;
    int x, y, birdHeight;

    cat target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("cat01").GetComponent<cat>();
        birdHeight = (int) target.transform.position.y;
        x = Random.Range(-3, 3);
        y = Random.Range(birdHeight+2, birdHeight+4);
        transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (x > 0)
        {
            direction = -0.02f;
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        } 
        else
        {
            direction = 0.02f;
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        transform.position += new Vector3(direction, 0, 0);

        if (transform.position.x < -3 || transform.position.x > 3)
        {
            Destroy(gameObject);
        }

    }

}
