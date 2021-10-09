using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cat : MonoBehaviour
{
    private Vector3 originPos;          // 최초 클릭 위치벡터
    private Vector3 currentPos;         // 현재 클릭 위치벡터
    private bool isMouseClicked;        // 마우스 클릭 여부
    private Vector3 diffVector;         // 마우스 드래그 벡터
    private Vector3 diffNormalVector;   // 마우스 드래그 방향벡터
    private float diffVectorManitude;   // 마우스 드래그 세기
    private float currentDegree;        // 마우스 드래그 벡터 z축과의 각도
    public bool shootConfirm;          // 고양이 발사 가능 여부
    public bool isHolding = true;       // 고양이의 철봉 매달리고있는 중인지 여부
    private float rotateSpeed = 25f;    // 철봉 위 회전 속도

    public float toward = 1.5f;
    public bool isPlaying = true;
    public float currentBarY = -1f;

    float tempScore = 2;

    // Update is called once per frame
    void Update()
    {
        if (isPlaying) work();

        if (gameManager.I.gameEnd == true) isPlaying = false;
    }

    private void work()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            // 마우스 최초 클릭 지점 계산
            originPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMouseClicked = true;
        }
        else if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            isMouseClicked = false;
            transform.localScale = new Vector3(toward, 1.5f, 0.0f);

            if (shootConfirm)
            {
                //transform.Translate(1.5f*diffVector);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + diffVector, diffVectorManitude);
                isHolding = false;
            }
        }

        // 마우스 누르는 동안
        if (isMouseClicked)
        {
            // 철봉 위 회전
            transform.Rotate(Vector3.back * rotateSpeed);
            // 드래그된 지점 계산
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 최초 클릭 지점과 드래그된 지점 간의 거리
            diffVector = (currentPos - originPos) * (-1);
            // 드래그 세기
            diffVectorManitude = diffVector.magnitude;
            diffNormalVector = diffVector.normalized;

            // 드래그 세기만큼 고양이 몸 잡아 땡김
            transform.localScale = new Vector3(toward, 1.5f + 0.05f * diffVector.y, 0);

            float rot_z = Mathf.Atan2(diffNormalVector.y, diffNormalVector.x) * Mathf.Rad2Deg;

            if (diffVectorManitude > 1 && rot_z > 0)
            {
                // 드래그 각도 제한
                currentDegree = Mathf.Clamp(rot_z - 90, -60, 60);

                transform.rotation = Quaternion.AngleAxis(currentDegree, Vector3.forward);
                shootConfirm = true;

            }
            else
            {
                shootConfirm = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!isHolding && coll.gameObject.tag == "bar")
        {
            currentBarY = coll.transform.position.y;
            gameManager.I.addScore(currentBarY-tempScore);
            isHolding = true;

            coll.gameObject.tag = "touched";
        }
        tempScore = currentBarY;
    }
}
