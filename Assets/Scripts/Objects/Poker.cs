using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Poker : Gesture
{
    [SerializeField] GameObject card1;
    [SerializeField] GameObject card2;
    [SerializeField] GameObject card3;
    [SerializeField] GameObject card4;
    [SerializeField] GameObject card5;

    [SerializeField] Vector3 cardPosition;
    [SerializeField] Quaternion cardRotation;
    [SerializeField] float move;

    List<GameObject> card;
    Dictionary<GameObject, Vector3> cardTransform;

    int cardIndex = 0; // 0~4

    // Start is called before the first frame update
    void Start()
    {
        cardPosition = new Vector3((float)(-0.0049), (float)0.001600001, (float)0.1221);
        cardRotation = new Quaternion(0,0,0,1);

        card = new List<GameObject>();
        card.Add(card1);
        card.Add(card2);
        card.Add(card3);
        card.Add(card4);
        card.Add(card5);

        cardTransform = new Dictionary<GameObject, Vector3>();
        Vector3 tempPosition;
        Quaternion tempQuat;
        
        card1.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardTransform.Add(card1, tempPosition);
        
        card2.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardTransform.Add(card2, tempPosition);
        
        card3.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardTransform.Add(card3, tempPosition);
        
        card4.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardTransform.Add(card4, tempPosition);
        
        card4.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardTransform.Add(card5, tempPosition);

        gameObject.SetActive(false);

        cardIndex = 0;

        /*
        Vector3 tempPosition = new Vector3();
        Quaternion tempQuat = new Quaternion();

        card[cardIndex].gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        tempPosition.z += move;
        */
        card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(cardPosition, cardRotation);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Select(bool isLeft)
    {
        if (isLeft)
        {
            Debug.Log(cardIndex);
            if (cardIndex == 0) return;

            card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(
                cardTransform[card[cardIndex]],
                card[cardIndex].gameObject.transform.localRotation);

            cardIndex++;
            card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(cardPosition, cardRotation);
        }
        else
        {
            Debug.Log(cardIndex);
            if (cardIndex >= 4) return;

            card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(
                cardTransform[card[cardIndex]],
                card[cardIndex].gameObject.transform.localRotation);

            cardIndex++;
            card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(cardPosition, cardRotation);
        }
    }
}
