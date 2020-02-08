using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buildings
{
    //public GameObject _building;
    public string _name;
    public int _id;
    public string _description;
    //public Sprite _icon;
    public int _steel, _gold, _food, _water, _lumber, _pollution;
    
    
    public Buildings(string name, int id, string description, int steel, int gold, int food, int water, int lubmer, int pollution)
    {
        //this._building = building;
        this._name = name;
        this._id = id;
        this._description = description;
        //this._icon = icon;
        this._steel = steel;
        this._gold = gold;
        this._food = food;
        this._water = water;
        this._lumber = lubmer;
        this._pollution = pollution;
    }
    
}
