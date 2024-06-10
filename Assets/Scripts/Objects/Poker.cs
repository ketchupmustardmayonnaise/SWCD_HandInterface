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
    Dictionary<GameObject, Vector3> cardPositionList;
    Dictionary<GameObject, Quaternion> cardRotationList;

    int cardIndex = 0; // 0~4
    int cardNum = 5;

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

        cardPositionList = new Dictionary<GameObject, Vector3>();
        cardRotationList = new Dictionary<GameObject, Quaternion>();
        Vector3 tempPosition;
        Quaternion tempQuat;
        
        card1.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardPositionList.Add(card1, tempPosition);
        cardRotationList.Add(card1, tempQuat);
        
        card2.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardPositionList.Add(card2, tempPosition);
        cardRotationList.Add(card2, tempQuat);

        card3.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardPositionList.Add(card3, tempPosition);
        cardRotationList.Add(card3, tempQuat);

        card4.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardPositionList.Add(card4, tempPosition);
        cardRotationList.Add(card4, tempQuat);

        card4.gameObject.transform.GetLocalPositionAndRotation(out tempPosition, out tempQuat);
        cardPositionList.Add(card5, tempPosition);
        cardRotationList.Add(card5, tempQuat);

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
                cardPositionList[card[cardIndex]],
                cardRotationList[card[cardIndex]]);

            cardIndex--;
            card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(cardPosition, cardRotation);
        }
        else
        {
            Debug.Log(cardIndex);
            if (cardIndex >= cardNum) return;

            card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(
                cardPositionList[card[cardIndex]],
                cardRotationList[card[cardIndex]]);

            cardIndex++;
            card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(cardPosition, cardRotation);
        }
    }

    public IEnumerator Throw()
    {
        GameObject cardTemp = card[cardIndex];
        cardPositionList.Remove(card[cardIndex]);
        cardRotationList.Remove(card[cardIndex]);

        card.Remove(card[cardIndex]);
        cardNum--;

        cardTemp.transform.Translate(Vector3.forward * 0.06f);
        yield return new WaitForSeconds(1.0f);
        Destroy(cardTemp);

        if (cardIndex >= cardNum) cardIndex--;
        card[cardIndex].gameObject.transform.SetLocalPositionAndRotation(cardPosition, cardRotation);
        yield return null;
    }
}
