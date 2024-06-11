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
        // �� �̰� ���� setactive ���� ������ �� �ƴ� �ű���
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
