using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempsphere : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer;
    float jumpPower = 5.0f;
    public bool isJumpReady = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PredictTrajectory(transform.position, Vector3.right * 3f + Vector3.up * jumpPower);
        }
    }

    void PredictTrajectory(Vector3 startPos, Vector3 vel)
    {
        int step = 60;
        float deltaTime = Time.fixedDeltaTime;
        Vector3 gravity = Physics.gravity;
        //Debug.Log(gravity);

        Vector3 position = startPos;
        Vector3 velocity = vel;

        List<Vector3> vectorlist = new List<Vector3>();
        for (int i = 0; i < step; i++)
        {
            position += velocity * deltaTime + 0.5f * gravity * deltaTime * deltaTime;
            velocity += gravity * deltaTime;
            vectorlist.Add(position);

            //Debug.Log(position);
        }
        Debug.Log(vectorlist.Count);
        lineRenderer.SetPositions(vectorlist.ToArray());
    }
}
