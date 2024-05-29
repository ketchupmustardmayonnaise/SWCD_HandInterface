using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Gesture
{
    [SerializeField]
    ThrowableHand hand;

    [SerializeField]
    InstantiateBomb bombInst;

    [SerializeField]
    float velocityThreshold = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw()
    {
        if (gameObject.activeSelf == true)
        {
            if (hand.GetVelocity().magnitude > velocityThreshold)
            {
                Instantiate(bombInst, gameObject.transform);

                bombInst.GetComponent<Rigidbody>().useGravity = true;
                bombInst.GetComponent<Rigidbody>().isKinematic = true;
                bombInst.GetComponent<Rigidbody>().AddForce(hand.GetVelocity());
            }
        }
    }
}
