using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBomb : MonoBehaviour
{

    [SerializeField]
    GameObject ParticleFXExplosion;

    [SerializeField]
    GameObject explosionPosition;

    float bombTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Burst()
    {
        yield return new WaitForSeconds(bombTime);
        Instantiate(ParticleFXExplosion, explosionPosition.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor")) StartCoroutine(Burst());
    }

    public void SetBombTime(float time)
    { 
        bombTime = time;
    }
}
