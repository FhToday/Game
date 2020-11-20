using System;
using System.Collections;
using System.Collections.Generic;


public class serverCreater
{
    public List<cardData> dataset = new List<cardData>();
    public Queue<cardData> dealcards = new Queue<cardData>();
    public void CreateCard()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                dataset.Add(new cardData(j, i));
            }
        }
        dataset.Add(new cardData(14, 5));
        dataset.Add(new cardData(15, 5));
    }
    public void Shuffle()
    {
        List<cardData> shuffle = new List<cardData>();
        foreach(cardData crd in dataset)
        {
            // int index = Random.Range(0, shuffle.Count + 1);
            Random r = new Random();
            int index = r.Next(0, shuffle.Count + 1);
            shuffle.Insert(index, crd);
        }
        foreach (cardData crd in shuffle)
        {
            dealcards.Enqueue(crd);
        }
        shuffle.Clear();
        dataset.Clear();
    }
    // Start is called before the first frame update
    // void Start()
    // {
    //     dataset.Clear();
    //     dealcards.Clear();
    //     CreateCard();
    //     Shuffle();
    // }
}
