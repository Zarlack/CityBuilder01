using UnityEngine;
using System.Collections;

public class BuildingButton : MonoBehaviour {

	//Makes a GameObject called buildingPrefab which has the information the the building chosen for the button
	[SerializeField]
	private GameObject buildingPrefab;

	//Makes a getter for the BuildingPrefab
	public GameObject BuildingPrefab {
		get { return buildingPrefab; }
	}
}
