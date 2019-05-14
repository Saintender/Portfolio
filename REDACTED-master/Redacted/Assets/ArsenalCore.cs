using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArsenalCore : MonoBehaviour {
    //Member declarations
    private GameObject card;
    private GameObject cardPrefab;
    private CameraRaycaster raycast;
	private ArrayList arsenalList = new ArrayList();
	private System.Random rand;
	private int nextCardIndex;
	
	//method that will eventually fill the arsenal with card keys
	void fillArsenal() {
		//this is where the database will be accessed to fill the arsenal with card keys
		
		//for now this loop will fill the arrayList with keys 0 - 59
		for(int i = 0; i < 60; i++){
			arsenalList.Add(i);
		}
	}
	
	//initial shuffling mechanic - hold a random number, if shuffle is required or card is drawn, re-declare that number
	void shuffle() {
		nextCardIndex = rand.Next(0, arsenalList.Count);
	}

	// Use this for initialization
	void Start () {
        raycast = Camera.main.GetComponent<CameraRaycaster>();
        cardPrefab = Resources.Load("CardPrefab") as GameObject;
		rand = new System.Random();
		fillArsenal();
		shuffle();
		
		//opening deal
		for(int i = 0; i < 7; i++)
			drawCard();
	}
	
	// Update is called once per frame
	void Update () {
		//monitor for draw card command
        bool bDrawCard = Input.GetMouseButtonDown(0) && raycast.layerHit == Layer.Stack;
        if (bDrawCard)
			drawCard();
		
		if(Input.GetMouseButtonDown(1))
			intentionalShuffle();
	}
	
	//leyman draw implementation
	void drawCard() {
        if(arsenalList.Count == 0)
        {
            print("Your arsental is empty.");
            return;
        }
		createCardInstance();
		shuffle(); //card was drawn so shuffle
        print("Arsenal count: " + arsenalList.Count);
	}
	
	//quick method for checking
	void intentionalShuffle() {
		print("The next card would have been " + arsenalList[nextCardIndex]);
		shuffle();
		print("Now the next card is " + arsenalList[nextCardIndex]);
	}

    private void createCardInstance()
    {
        card = Instantiate(cardPrefab) as GameObject;
		print("the card drawn was number " + arsenalList[nextCardIndex]);
        arsenalList.RemoveAt(nextCardIndex);
    }
}
