using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poker : Gesture
{
    [SerializeField] GameObject card1;
    [SerializeField] GameObject card2;
    [SerializeField] GameObject card3;
    [SerializeField] GameObject card4;
    [SerializeField] GameObject card5;

    [SerializeField] float move;

    List<GameObject> card;

    int cardIndex = 0; // 0~4

    // Start is called before the first frame update
    void Start()
    {
        card = new List<GameObject>();
        card.Add(card1);
        card.Add(card2);
        card.Add(card3);
        card.Add(card4);
        card.Add(card5);

        cardIndex = 0;
        Vector3 tempPosition = card[cardIndex].gameObject.transform.position;
        card[cardIndex].gameObject.transform.position.Set(tempPosition.x,
                                    tempPosition.y, tempPosition.z + move);
    }

    // Update is called once per frame
    void Update()
    {
        cardIndex = 0;

    }

    void Select(bool isLeft)
    {
        if (isLeft)
        {
            if (cardIndex == 0) return;

            Vector3 tempPosition = card[cardIndex].gameObject.transform.position;
            card[cardIndex].gameObject.transform.position.Set(tempPosition.x,
                                        tempPosition.y, tempPosition.z - move);

            cardIndex--;
            tempPosition = card[cardIndex].gameObject.transform.position;
            card[cardIndex].gameObject.transform.position.Set(tempPosition.x,
                                        tempPosition.y, tempPosition.z + move);
        }
        else
        {
            if (cardIndex >= 4) return;

            Vector3 tempPosition = card[cardIndex].gameObject.transform.position;
            card[cardIndex].gameObject.transform.position.Set(tempPosition.x,
                                        tempPosition.y, tempPosition.z - move);

            cardIndex++;
            tempPosition = card[cardIndex].gameObject.transform.position;
            card[cardIndex].gameObject.transform.position.Set(tempPosition.x,
                                        tempPosition.y, tempPosition.z + move);
        }
    }
}
