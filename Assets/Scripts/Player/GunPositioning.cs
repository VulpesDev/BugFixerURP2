using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPositioning : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] GameObject dot;
    Vector3 screenCenter;

    public float rotationSpeedBase, rotationAcc;
    float rotationSpeed, positionSpeed;

    [Header("Positioning")]
    [SerializeField] GameObject dot2;
    public float positionSpeedBase, positionAcc;
    Vector3 positionCenter;
    GameObject pistolHolder;


    void Start()
    {

        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
        dot.transform.position = screenCenter;
        rotationSpeed = rotationSpeedBase;
        positionSpeed = positionSpeedBase;

        pistolHolder = transform.parent.GetChild(2).gameObject;
        dot2.transform.position = pistolHolder.transform.position;


    }
    void FixedUpdate()
    {
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
        rotationSpeed = rotationSpeedBase * (Mathf.Abs(Vector3.Distance(dot.transform.position, screenCenter))+1)
            *rotationAcc;
        dot.transform.position = Vector3.Lerp(dot.transform.position, screenCenter,
            Time.fixedDeltaTime + rotationSpeed);
        transform.LookAt(dot.transform.position);


        positionCenter = pistolHolder.transform.position;
        positionSpeed = positionSpeedBase * ((Mathf.Abs(Vector3.Distance(dot2.transform.position, positionCenter))
            + 1)*positionAcc);
        dot2.transform.position = Vector3.Lerp(dot2.transform.position, positionCenter,
            Time.fixedDeltaTime + positionSpeed);
        transform.position = dot2.transform.position;

    }
}
