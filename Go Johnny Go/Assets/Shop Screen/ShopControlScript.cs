using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopControlScript : MonoBehaviour {

	public int moneyAmount;
	public bool guitarBought;
	public Text coinsText;
	public Button buyButton;
	public Text amountText;
	public Text buyText;
	public int price;
	public Button backButton;


	// Use this for initialization
	void Start () {
		moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
		guitarBought = PlayerPrefs.GetInt ("GuitarBought") == 1 ? true : false;
		setBought (guitarBought);

		coinsText.text = moneyAmount.ToString ();
		Button btn = buyButton.GetComponent<Button>();
		btn.onClick.AddListener(BuyOnClick);

		Button btn2 = backButton.GetComponent<Button>();
		btn2.onClick.AddListener(mainMenuScene);

		int index = amountText.text.IndexOf (" "); //used to remove " Coins"
		price = int.Parse(amountText.text.Substring(0,index));
	}

	// Update is called once per frame
	void Update () {
		//makes buy button clickable if funds available
		if (price <= moneyAmount) {
			buyButton.interactable = true;
		}
		else {
			buyButton.interactable = false;	
		}
	}

	void BuyOnClick() {		
			moneyAmount -= price;
			PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
			PlayerPrefs.SetInt("GuitarBought", 1);
			coinsText.text = moneyAmount.ToString();
			setBought (true);
	}

	void setBought(bool isBought) {
		if (isBought) {
			amountText.text = "";
			buyText.text = "OWNED";
			buyButton.image.enabled = false;
		} else {
			amountText.text = "500 Coins";
			buyText.text = "BUY";
			buyButton.image.enabled = true;
		}
	}

	void mainMenuScene() {
		PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
		SceneManager.LoadScene ("Main Menu");
	}
}
