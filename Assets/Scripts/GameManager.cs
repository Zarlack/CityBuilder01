using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	//Makes a public object of BuildingButton used in GameManager
	public BuildingButton ClickedBuildingBtn { get; private set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Makes a function that is used when picking a button in the building panel
	public void PickBuilding(BuildingButton buildingButton) {
		//Sets the clickedBuildingBtn to the same as the input buildingButton
		this.ClickedBuildingBtn = buildingButton;
	}

	//A function for when buying buildings, that is when placing the building after clicking the button
	public void BuyBuilding() {
		//Only sets it to null if not pressing shift, so if pressing shift can place more than one
		if (!Input.GetKey(KeyCode.LeftShift)) {
			//Sets it to null so you have to choose building each time
			ClickedBuildingBtn = null;
		}
	}
}
