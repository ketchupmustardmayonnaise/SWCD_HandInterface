using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private float _speed = 1f;

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
}
