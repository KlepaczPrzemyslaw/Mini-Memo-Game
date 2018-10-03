using UnityEngine;

public class Tile : MonoBehaviour
{
	public bool IsActive = false;
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
	}

	Quaternion GetTargetRotation()
	{
		return Quaternion.Euler(IsActive ? Vector3.zero : Vector3.up * 180f);
	}

	private void OnMouseDown()
	{
		if (FindObjectOfType<Board>().CanMove == false)
			return;

		IsActive = !IsActive;
	}
}
