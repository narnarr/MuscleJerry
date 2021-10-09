using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cat : MonoBehaviour
{
    private Vector3 originPos;          // ���� Ŭ�� ��ġ����
    private Vector3 currentPos;         // ���� Ŭ�� ��ġ����
    private bool isMouseClicked;        // ���콺 Ŭ�� ����
    private Vector3 diffVector;         // ���콺 �巡�� ����
    private Vector3 diffNormalVector;   // ���콺 �巡�� ���⺤��
    private float diffVectorManitude;   // ���콺 �巡�� ����
    private float currentDegree;        // ���콺 �巡�� ���� z����� ����
    public bool shootConfirm;          // ����� �߻� ���� ����
    public bool isHolding = true;       // ������� ö�� �Ŵ޸����ִ� ������ ����
    private float rotateSpeed = 25f;    // ö�� �� ȸ�� �ӵ�

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
            // ���콺 ���� Ŭ�� ���� ���
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

        // ���콺 ������ ����
        if (isMouseClicked)
        {
            // ö�� �� ȸ��
            transform.Rotate(Vector3.back * rotateSpeed);
            // �巡�׵� ���� ���
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���� Ŭ�� ������ �巡�׵� ���� ���� �Ÿ�
            diffVector = (currentPos - originPos) * (-1);
            // �巡�� ����
            diffVectorManitude = diffVector.magnitude;
            diffNormalVector = diffVector.normalized;

            // �巡�� ���⸸ŭ ����� �� ��� ����
            transform.localScale = new Vector3(toward, 1.5f + 0.05f * diffVector.y, 0);

            float rot_z = Mathf.Atan2(diffNormalVector.y, diffNormalVector.x) * Mathf.Rad2Deg;

            if (diffVectorManitude > 1 && rot_z > 0)
            {
                // �巡�� ���� ����
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
