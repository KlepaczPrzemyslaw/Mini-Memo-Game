using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
	//// Variables ////

	private Tile[] _tiles;

	public GameObject TilePrefab;
	public Sprite[] TileImages;
	public Vector2 TilesOffset = Vector2.one;
	public int TileRows = 4;
	public int TileColumns = 8;
	public bool CanMove = false;
	public TextMesh WinText;

	//// Unity ////

	IEnumerator Start()
	{
		WinText.GetComponent<Renderer>().enabled = false;
		InstantiateBoard();
		ShuffleTailes();
		PlateTiles();

		CanMove = false;
		yield return new WaitForSeconds(3f);

		HideTailes();
		CanMove = true;
	}

	//// Private ////

	private Tile CreateTile(Sprite faceSprite)
	{
		var gameobject = Instantiate(TilePrefab);
		gameobject.transform.parent = transform;

		var actualTile = gameobject.GetComponent<Tile>();
		actualTile.FrontRune = faceSprite;
		actualTile.IsActive = true;
		return actualTile;
	}

	private void PlateTiles()
	{
		for (int i = 0; i < (TileRows * TileColumns); i++)
		{
			int x = i % TileRows;
			int y = i / TileRows;

			_tiles[i].transform.localPosition = new Vector3(y * TilesOffset.y, x * TilesOffset.x, 0);
		}
	}

	private void InstantiateBoard()
	{
		int tilesCount = TileRows * TileColumns;
		_tiles = new Tile[tilesCount];

		for (int i = 0; i < tilesCount; i++)
		{
			_tiles[i] = CreateTile(TileImages[i / 2]);
		}
	}

	private void ShuffleTailes()
	{
		for (int i = 0; i < 2000; i++)
		{
			int index1 = Random.Range(0, _tiles.Length);
			int index2 = Random.Range(0, _tiles.Length);

			var tile1 = _tiles[index1];
			var tile2 = _tiles[index2];

			_tiles[index1] = tile2;
			_tiles[index2] = tile1;
		}
	}

	private void HideTailes()
	{
		_tiles.ToList().ForEach(tile => tile.IsActive = false);
	}

	private bool CheckIfEnd()
	{
		return _tiles.All(tile => tile.IsOnBoard == false);
	}

	private IEnumerator CheckPairCorutine()
	{
		var tilesUncovered = _tiles
			.Where(tile => tile.IsOnBoard)
			.Where(tile => tile.IsActive)
			.ToArray();

		if (tilesUncovered.Length != 2)
			yield break;

		var tile1 = tilesUncovered[0];
		var tile2 = tilesUncovered[1];

		CanMove = false;
		yield return new WaitForSeconds(1f);
		CanMove = true;

		if (tile1.FrontRune == tile2.FrontRune)
		{
			tile1.IsOnBoard = false;
			tile2.IsOnBoard = false;
		}
		else
		{
			tile1.IsActive = false;
			tile2.IsActive = false;
		}

		if (CheckIfEnd() == false) yield break;

		CanMove = false;
		WinText.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(5f);
		Application.Quit();
	}

	//// Public ////

	public void CheckPair()
	{
		StartCoroutine(CheckPairCorutine());
	}
}
