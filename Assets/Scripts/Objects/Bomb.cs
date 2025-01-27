using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static OVRPlugin;
using UnityEngine.UIElements;

public class Bomb : Gesture
{
    [SerializeField]
    ThrowableHand hand;

    [SerializeField]
    InstantiateBomb bombInst;

    [SerializeField]
    float bombTime = 5.0f;

    [SerializeField]
    TMP_Text text;

    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    OVRCameraRig player;

    float jumpPower = 4.0f;
    public bool isJumpReady = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        //lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        setText();

        if (gameObject.activeSelf == true && isJumpReady)
        {
            PredictTrajectory(transform.position, player.transform.forward * 3f + Vector3.up * jumpPower);
        }
    }

    public void Throw()
    {
        if (isJumpReady)
        {
            InstantiateBomb bombtemp = Instantiate(bombInst, gameObject.transform.position, gameObject.transform.rotation);
            bombtemp.SetBombTime(bombTime);
            bombtemp.gameObject.GetComponent<Rigidbody>().velocity = player.transform.forward * 3f + Vector3.up * jumpPower;
            
            List<Vector3> vectorlist = new List<Vector3>();
            for (int i = 0; i < 60; i++)
            {
                Vector3 tempVec = new Vector3(0, 0, 0);
                vectorlist.Add(tempVec);
            }
            lineRenderer.SetPositions(vectorlist.ToArray());
            isJumpReady = false;
        }
    }

    void setText()
    {
        text.text = "Time: " + bombTime.ToString();
    }

    public void setBombTime(bool isUp)
    {
        if (isUp) bombTime += 1.0f;
        else bombTime -= 1.0f;
    }

    void PredictTrajectory(Vector3 startPos, Vector3 vel)
    {
        int step = 60;
        float deltaTime = Time.fixedDeltaTime;
        Vector3 gravity = Physics.gravity;

        Vector3 position = startPos;
        Vector3 velocity = vel;

        List<Vector3> vectorlist = new List<Vector3>();
        for (int i = 0; i < step; i++)
        {
            position += velocity * deltaTime + 0.5f * gravity * deltaTime * deltaTime;
            velocity += gravity * deltaTime;
            vectorlist.Add(position);
        }
        lineRenderer.SetPositions(vectorlist.ToArray());
    }
}
