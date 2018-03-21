using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {

	public Text coinsText;
	public Button buyButton;
	public Text amountText;
	public Text buyText;
	public int coins;
	public int price;

	// Use this for initialization
	void Start () {
		Button btn = buyButton.GetComponent<Button>();
		btn.onClick.AddListener(BuyOnClick);

		coins = int.Parse(coinsText.text); //should be in playerprefs
		int index = amountText.text.IndexOf (" "); //used to remove " Coins"
		price = int.Parse(amountText.text.Substring(0,index));
	}

	// Update is called once per frame
	void Update () {
		//makes buy button clickable if funds available
		if (price <= coins) {
			buyButton.interactable = true;
		}
		else {
			buyButton.interactable = false;	
		}
	}

	void BuyOnClick()
	{		
			coins -= price;
			coinsText.text = coins.ToString();
			amountText.text = "";
			buyText.text = "OWNED";
			buyButton.image.enabled = false;
	}
}
