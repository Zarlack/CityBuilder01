using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	//How fast the camera moves
	[SerializeField]
	private float cameraSpeed = 0;

	private float xMax;
	private float yMin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
		GetInput();
	}

	//Gets the users key input
	private void GetInput() {
		//If the user press the X key the camera will move a direction with the cameraspeed and update speed
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))		transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))		transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))		transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))	transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);

		//Make sure the camera posistion from above can't move under 0 coordinates in X to the left, xMax to the right, and Y can't move lower than yMin and above 0
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);
	}

	//Sets the limits of how far the camera can move
	public void SetLimits(Vector3 maxTile) {
		//Sets how far the camera can move to 1, 0 which is the bottom right, since left X is always 0 and top Y is always 0 so we always knows those values anyway. 
		Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

		xMax = maxTile.x - wp.x;
		yMin = maxTile.y - wp.y;
	}
}
