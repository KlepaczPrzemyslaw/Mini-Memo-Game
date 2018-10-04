using UnityEngine;

public class Tile : MonoBehaviour
{
	public bool IsActive = true;
	public bool IsOnBoard = true;
	public Sprite FrontRune;

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

	Quaternion GetTargetRotation()
	{
		return Quaternion.Euler(IsActive ? Vector3.zero : Vector3.up * 180f);
	}

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
