using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private float _speed = 1f;

    int index = 0;

    [SerializeField] OVRCameraRig cameraRig;
    [SerializeField] List<GameObject> cameraPos;

    public bool isTransformed = false;
    Vector3 beforeCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateMovement(float x, float y)
    {
        Vector3 direction = new Vector3(x, 0f, y);
        //Debug.Log("x : " + x + ", z : " + y);
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    public void MoveCameraPos(bool isUp)
    {
        if (isUp && index < cameraPos.Count) index++;
        else if (!isUp && index > 0) index--;

        gameObject.transform.position = cameraPos[index].transform.position;
    }

    public void InitCameraPos()
    { 
        
    }
}
