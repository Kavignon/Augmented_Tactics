﻿using Assets.Map;
using Assets.Map.Creator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, ICharacterObserver
{
    private int _indexOfCharacters;
    public List<CharacterObservable> GameCharacters;
    public CharacterObservable ActivePlayer;
    public GameMap GameMap;
    public GameObject CellPrefab;
    public List<List<Cell>> map;
    public int mapSize = 32;
    private Transform mapTransform;

    public static GameManager instance;

    public void GoToNextCharacter()
    {
        _indexOfCharacters = _indexOfCharacters + 1 < GameCharacters.Count ? _indexOfCharacters + 1 : 0;
        ActivePlayer = GameCharacters[_indexOfCharacters];
    }

    public void UpdateObserver(CharacterObservable character)
    {
        throw new NotImplementedException();
    }

    private void CreateMap()
    {
        CellMapXMLContainer container = MapSaveLoad.Load("map.xml");

        mapSize = container.size;

        //initially remove all children
        // for (int i = 0; i < mapTransform.childCount; i++)
        //{
        //    Destroy(mapTransform.GetChild(i).gameObject);
        //}

        map = new List<List<Cell>>();
        for (int i = 0; i < mapSize; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < mapSize; j++)
            {
                Cell cell = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Cell>();
                Debug.Log(cell == null);
                Debug.Log(mapTransform == null);

                cell.transform.parent = mapTransform;
                cell.Coordinates = new Vector2(i, j);
                Debug.Log(cell == null);
                Debug.Log(cell.Coordinates == null);
                cell.Type = (CellType)(container.cells.Where(x => x.locX == i && x.locY == j).First().id);
                row.Add(cell);
            }
            map.Add(row);
        }
    }

    private void InitializaGameMap()
    {
        map = new List<List<Cell>>();
        for (int i = 0; i < 32; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < 32; j++)
            {
                Cell tile = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Cell>();
                tile.Coordinates = new Vector2(i, j);
                row.Add(tile);
            }
            map.Add(row);
        }
    }

    public void Awake()
    {
        instance = this;

        mapTransform = transform.FindChild("Map");
    }

    // Use this for initialization
    public void Start()
    {
        CreateMap();
        //_indexOfCharacters = 0;
        //ActivePlayer = GameCharacters [_indexOfCharacters];
    }

    // Update is called once per frame
    private void Update()
    {
    }
}