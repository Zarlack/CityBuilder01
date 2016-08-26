using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

	//Make a point object
	public Point GridPosition { get; private set; }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Make a function which takes the gridPos and worldPos and parent as in folder
	public void Setup(Point gridPos, Vector3 worldPos, Transform parent) {
		//Sets the GridPosition point to the gridPos parameter
		this.GridPosition = gridPos;

		//Set the tile to the worldPos input
		transform.position = worldPos;

		//Places the tile under parent when created, which is the folder in hierarchi
		transform.SetParent(parent);

		//Adds the tile to the dictionary as a point to find the tiletype
		LevelManager.Instance.Tiles.Add(gridPos, this);
	}

	//Runs when mousing over a tile with a collision and trigger
	private void OnMouseOver() {
		//Checking if the mouse is over a object or button so it doesn't place a building when clicking the button at once
		//Or that it is not null so you wont get an error when clicking on map before clicking on a tower
		if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBuildingBtn != null) {
			if (Input.GetMouseButtonDown(0)) {
				PlaceBuilding();
			}
		}
	}

	//Function for placing buildings
	private void PlaceBuilding() {
		//Creates the object BuildingPrefab with the position of the mouse without rotating it
		GameObject building = (GameObject)Instantiate(GameManager.Instance.ClickedBuildingBtn.BuildingPrefab, transform.position, Quaternion.identity);

		//Sets the sorting order of sprite rendering when placing a tower to the value of Y on the tile
		building.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.y;

		//Makes the tower placed a child of the tile it is placed on in the hirarchy
		building.transform.SetParent(transform);

		//Calls the buybuilding from gamemanager when placing the building
		GameManager.Instance.BuyBuilding();
	}
}
