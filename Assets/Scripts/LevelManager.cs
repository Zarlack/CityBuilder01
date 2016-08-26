using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelManager : Singleton<LevelManager> {
	//Makes a private GameObject as tilePrefab which is the list that you add the prefabs into, SerializeField makes it so unity can get it even if its private
	[SerializeField]
	private GameObject[] tilePrefabs;

	//Makes a object of the CameraMovement class
	[SerializeField]
	private CameraMovement cameraMovement;

	//Makes a map object from transform which will be used to put the tiles in the map folder
	[SerializeField]
	private Transform map;

	//Makes a getter and setter for the Tiles list
	public Dictionary<Point, TileScript> Tiles { get; set; }

	//Makes a getter of the tilePrefab which is going to tell how big the tile is by giving the size of the first tile
	public float TileSize {
		get {
			return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		}
	}

	// Use this for initialization
	void Start () {
		CreateLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Creates the level at the beginning
	private void CreateLevel() {
		//Makes a dictionary which takes the point, which again is X and Y position, and gives up the TileScript back
		Tiles = new Dictionary<Point, TileScript>();

		//Put the information it gets from reading the Level document in a string array
		string[] mapData = ReadLevelText();

		//Sets mapX which is its width to the length of the first line in Level.txt, and the height of the map to the number of lines split by -
		int mapX = mapData[0].ToCharArray().Length;
		int mapY = mapData.Length;

		//Makes a vector3 and sets it to empty to not get a null referanse
		Vector3 maxTile = Vector3.zero;

		//Find the 0, 0 coordinates
		Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

		//Goes through all the mapY and mapX to draw them in the game
		for (int y = 0; y < mapY; y++) {
			//Splits up the numbers in each line of Level.txt whenever Y increases and puts it into a char array
			char[] newTiles = mapData[y].ToCharArray();

			for (int x = 0; x < mapX; x++) {
				//Calls up the PlaceTile function with the value coresponding in Level.txt and the position of X and Y from the worldStart which is 0, 0
				PlaceTile(newTiles[x].ToString(), x, y, worldStart);
			}
		}

		//After generating the tiles it sets the maxTile to the last tile in the game so the camera can use that to find the max position to scroll
		maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

		//Sets the limits of the camera with the X position and Y position, and since Y axis is strange it's - where X is +
		cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
	}

	//Make the PlaceTile function which places the tiles and takes the type of tile from Level.txt, the x and y position and the start of the map
	private void PlaceTile(string tileType, int x, int y, Vector3 worldStart) {
		//Makes a number of the tileType from Level.txt to give the tile type
		int tileIndex = int.Parse(tileType);

		//Makes a new TileScript object with the tilePrefabs object
		TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

		//Goes to the Setup function in the TileScript class to set up the new tile with the information given
		newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);
	}

	//Function to read the textdocument and return as a string array
	private string[] ReadLevelText() {
		//Loads the Level.txt and puts it into bindData
		TextAsset bindData = Resources.Load("Level") as TextAsset;

		//Makes a string of bindData and removed the new lines, which we use to make it easier to read, but doesn't work with the code
		string data = bindData.text.Replace(Environment.NewLine, string.Empty);

		//Returns the data but makes a new string index with each -
		return data.Split('-');
	}
}
