using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonFlip : MonoBehaviour
{
    public GameObject catob;
    public int jumpPower;
    cat catScript;

    public float to = 1.5f;

    public void Start()
    {
        catScript = GameObject.Find("cat01").GetComponent<cat>();
    }

    /*public void OnClickButton()
    {
        to *= -1;
        catScript.toward *= -1;
        catScript.isLeft =! catScript.isLeft;
        catob.transform.localScale = new Vector3(catScript.toward, 1.5f, 1.5f);
    }*/
}
