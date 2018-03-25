﻿using UnityEngine.UI;
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
	/*public Button equipButton1;
	public Button equipButton2;
	public Text equipText1;
	public Text equipText2;*/


	// Use this for initialization
	void Start () {
		guitar_b_price = 2;
		amountText.text = guitar_b_price.ToString() + " Coins";
		moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
		guitarBought = PlayerPrefs.GetInt ("GuitarB_Bought") == 1 ? true : false;
		setBought (guitarBought);
		//changeGuitar (PlayerPrefs.GetInt ("GuitarType"));

		coinsText.text = moneyAmount.ToString ();
		Button btn = buyButton.GetComponent<Button>();
		btn.onClick.AddListener(BuyOnClick);

		Button btn2 = backButton.GetComponent<Button>();
		btn2.onClick.AddListener(mainMenuScene);
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
			//changeGuitar(1);	
			coinsText.text = moneyAmount.ToString();
			setBought (true);
	}

	void setBought(bool isBought) {
		if (isBought) {
			amountText.enabled = false;
			buyText.text = "OWNED";
			buyButton.image.enabled = false;
		} else {
			amountText.enabled = true;
			buyText.text = "BUY";
			buyButton.image.enabled = true;
		}
	}

	void mainMenuScene() {
		PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
		SceneManager.LoadScene ("Main Menu");
	}

	/*
	void changeGuitar(int guitarNum) {
		PlayerPrefs.SetInt("GuitarType", guitarNum);
		equipButton1.enabled = guitarNum == 0 ? false : true;
		equipText1.enabled = guitarNum == 0 ? true : false;
		equipButton2.enabled = guitarNum == 0 ? true : false;
		equipText2.enabled = guitarNum == 0 ? false : true;
	}*/
}
