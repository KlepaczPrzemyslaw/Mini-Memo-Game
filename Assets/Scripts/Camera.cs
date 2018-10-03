using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	void Start()
	{
		var board = FindObjectOfType<Board>();
		transform.position = board.transform.position + (Vector3.up * 3f) + (Vector3.back * 10f) + (Vector3.right * board.TileColumns * 0.4f);
	}

	void Update()
	{ }
}
