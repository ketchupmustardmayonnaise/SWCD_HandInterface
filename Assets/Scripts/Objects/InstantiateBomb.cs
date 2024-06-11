using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBomb : MonoBehaviour
{

    [SerializeField]
    GameObject ParticleFXExplosion;

    [SerializeField]
    GameObject explosionPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burst()
    {
        // 아 이게 먼저 setactive 꺼서 폭발이 안 됐던 거구나
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
            Instantiate(ParticleFXExplosion, explosionPosition.transform.position, gameObject.transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("floor")) Burst();
    }
}
