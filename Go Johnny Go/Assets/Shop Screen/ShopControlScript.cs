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
	public int guitar_b_price;
	public Button backButton;
	public Button equipButton1;
	public Button equipButton2;
	public Text equipText1;
	public Text equipText2;


	// Use this for initialization
	void Start () {
		guitar_b_price = 25;
		amountText.text = guitar_b_price.ToString() + " Coins";
		moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
		guitarBought = PlayerPrefs.GetInt ("GuitarB_Bought") == 1 ? true : false;
		setBought (guitarBought);

		coinsText.text = moneyAmount.ToString ();

		buyButton.onClick.AddListener(BuyOnClick);
		backButton.onClick.AddListener(mainMenuScene);
		equipButton1.onClick.AddListener(delegate{changeGuitar(0);});
		equipButton2.onClick.AddListener(delegate{changeGuitar(1);});

		changeGuitar (PlayerPrefs.GetInt ("GuitarType"));
	}

	// Update is called once per frame
	void Update () {
		//makes buy button clickable if funds available
		if (guitar_b_price <= moneyAmount) {
			buyButton.interactable = true;
		}
		else {
			buyButton.interactable = false;	
		}
	}

	void BuyOnClick() {		
			moneyAmount -= guitar_b_price;
			PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
			PlayerPrefs.SetInt("GuitarB_Bought", 1); //indicates that 2nd guitar has been purchased
			coinsText.text = moneyAmount.ToString();
			setBought (true);
			changeGuitar(1);
	}

	void setBought(bool isBought) {
		if (isBought) {
			guitarBought = true;
			amountText.enabled = false;
			buyText.text = "OWNED";
			buyButton.image.enabled = false;
		} else {
			guitarBought = false;
			amountText.enabled = true;
			buyText.text = "BUY";
			buyButton.image.enabled = true;
		}
	}

	void mainMenuScene() {
		PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
		SceneManager.LoadScene ("Main Menu");
	}


	void changeGuitar(int guitarNum) {
		PlayerPrefs.SetInt("GuitarType", guitarNum);
		if (guitarNum == 0) {
			equipButton1.gameObject.SetActive (false);
			equipText1.enabled = true;
			equipButton2.gameObject.SetActive (true);
			equipText2.enabled = false;
		} else {
			equipButton1.gameObject.SetActive (true);
			equipText1.enabled = false;
			equipButton2.gameObject.SetActive (false);
			equipText2.enabled = true;
		}

		if (!guitarBought) {
			equipButton2.gameObject.SetActive (false);
			equipText2.enabled = false;
		}
	}
}
