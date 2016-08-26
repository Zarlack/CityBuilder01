using UnityEngine;
using System.Collections;

//Create a struct called point which we give X and Y variables
public struct Point {
	public int x { get; set; }
	public int y { get; set; }

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}
}