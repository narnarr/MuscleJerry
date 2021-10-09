using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    cat target;
    float smooth = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        target = GameObject.Find("cat01").GetComponent<cat>();
    }

    // Update is called once per frame
    void Update()
    {
        float n = target.currentBarY;

        Vector3 targetPos = new Vector3(0, n + 1, smooth);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
        // transform.position = new Vector3(transform.position.x, n+1, transform.position.z);
    }
}
