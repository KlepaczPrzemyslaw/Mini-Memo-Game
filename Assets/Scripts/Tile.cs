using UnityEngine;

public class Tile : MonoBehaviour
{
	//// Variables ////

	public bool IsActive = true;
	public bool IsOnBoard = true;
	public Sprite FrontRune;

	//// Unity ////

	void Start()
	{
		transform.rotation = GetTargetRotation();

		var actualFrontObjectWithRune = transform.Find("TileFront").transform.GetComponent<SpriteRenderer>();
		actualFrontObjectWithRune.sprite = FrontRune;
	}

	void Update()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, GetTargetRotation(), Time.deltaTime * 5f);

		if (IsOnBoard == false)
			Destroy(gameObject);
	}

	//// Private ////

	Quaternion GetTargetRotation()
	{
		return Quaternion.Euler(IsActive ? Vector3.zero : Vector3.up * 180f);
	}

	//// Events ////

	private void OnMouseDown()
	{
		var board = FindObjectOfType<Board>();
		if (board.CanMove == false)
			return;

		if (IsActive == true)
			return;

		IsActive = !IsActive;
		board.CheckPair();
	}
}
