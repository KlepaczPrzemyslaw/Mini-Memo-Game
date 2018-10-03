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
	public int TileColumns = 6;
	public bool CanMove = false;

	//// Unity ////

	IEnumerator Start()
	{
		InstantiateBoard();
		ShuffleTailes();
		PlateTiles();

		CanMove = false;
		yield return new WaitForSeconds(3f);

		HideTailes();
		CanMove = true;
	}

	void Update()
	{

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
			_tiles[i] = CreateTile(TileImages[i/2]);
		}
	}

	private void ShuffleTailes()
	{
		for (int i = 0; i < 1000; i++)
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
}
