using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Buildings[] inventory;
    GameManager _buildingDatabase;

    int randomItemSelector;
    bool isRun;

    private void Start()
    {
        _buildingDatabase = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _buildingDatabase.Running();
        }
    }
}
