using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Buildings> buildingDatabase = new List<Buildings>();
    public List<float> happinessLogs = new List<float>();
    public List<float> wealthLogs = new List<float>();
    public List<float> populationLogs = new List<float>();
    public List<float> pollutionLogs = new List<float>();
    public List<float> famineLogs = new List<float>();
    public List<float> employmentLogs = new List<float>();
    
    //These are the kinds of buildings, resources, etc,. currently players can have. 
    //If you compare this code with the GameManager's Inspector field, you will notice what this means very easily.
    [Header("[Buildings you want to run]")]
    public int _smithy;
    public int _mine;
    public int _farm;
    public int _park;

    [Header("[Current Resources]")]
    public int _steel;
    public int _gold;
    public int _food;
    public int _water;
    public int _lumber;
    
    [Header("[Indicators]")]
    public float _happiness_f;
    public float _wealth_f;
    public float _population_f;
    public float _pollution_f;
    public float _famine_f;     //(invisible to players)
    public float _employment_f; //(invisible to players)

    //These will not be visible to players. These are variables for the algorithms to calculate indicators.
    [Header("[Algorithm]")]
    public float _happiness;
    public float _wealth;
    public float _population;
    public float _pollution;
    public float _famine;
    public float _employment;

    void Start()
    {
        //These are the lines that contain information about the resources needed to run the buildings.
        //For example, line 48 contains information regarding Smithy. It will cost 10 food and produce 30 steel each turn. (refer the comment line 47)
        //If it is deemed necessary to balance the cost(-values) and produce(+values) of specific buildings, the last six integers can be modified.
        //BUT DO NOT CHANGE the first three values(name, id, description)
        //                                ( name,  id,  description,  steel,  gold,  food,  water,  lubmer,  pollution)
        buildingDatabase.Add(new Buildings("Smithy", 101, "This can be built at any normal tiles.", 30, 0, -10, 0, 0, 2));
        buildingDatabase.Add(new Buildings("Mine", 201, "This can be built at gold land tiles.", -15, 40, 0, 0, 0, 2));
        buildingDatabase.Add(new Buildings("Farm", 301, "This can be built at fertile land tiles.", 0, -10, 25, 0, 0, 1));
        buildingDatabase.Add(new Buildings("Park", 401, "This can be built at any normal tiles.", 0, -15, 0, 0, 0, -3));

        // Here, you can modify the seed resources. Player can start the game with the resources that you wrote here.
        // For now, ignore water and lumber.
        _steel = 30; _gold = 30; _food = 30; _water = 0; _lumber = 0;

        // These are default indicators(factors).
        _pollution_f = 0; _happiness_f = 3; _wealth_f = 3; _population_f = 3; _famine_f = 1;
    }

    // No need to read this method.
    public void Running()
    {
        int doNotCal = 0;
        foreach (var building in buildingDatabase)
        {
            if (building._id == 101)
            {
                if (_food >= _smithy * 10 && _mine + _park + _smithy + _farm <= _population_f)
                {
                    Debug.Log("Your city can run " + _smithy + " Smithy.");
                    _steel += _smithy * building._steel;
                    _gold += _smithy * building._gold;
                    _food += _smithy * building._food;                    
                }
                else
                {
                    Debug.Log("You don't have enough resources to run a: " + building._name);
                    doNotCal++;
                }
            }
            else if (building._id == 201)
            {
                if (_steel >= _mine * 15 && _mine + _park + _smithy + _farm <= _population_f)
                {
                    Debug.Log("Your city can run " + _mine + " Mine.");
                    _steel += _mine * building._steel;
                    _gold += _mine * building._gold;
                    _food += _mine * building._food;
                }
                else
                {
                    Debug.Log("You don't have enough resources to run a: " + building._name);
                    doNotCal++;
                }

            }
            else if (building._id == 301)
            {
                if (_gold >= _farm * 10 && _mine + _park + _smithy + _farm <= _population_f)
                {
                    Debug.Log("Your city can run " + _farm + " Farm.");
                    _steel += _farm * building._steel;
                    _gold += _farm * building._gold;
                    _food += _farm * building._food;
                }
                else
                {
                    Debug.Log("You don't have enough resources to run a: " + building._name);
                    doNotCal++;
                }
            }
            else if (building._id == 401)
            {
                if (_gold >= _park * 15 && _mine + _park + _smithy + _farm <= _population_f)
                {
                    Debug.Log("Your city can run " + _park + " Park.");
                    _steel += _park * building._steel;
                    _gold += _park * building._gold;
                    _food += _park * building._food;
                }
                else
                {
                    Debug.Log("You don't have enough resources to run a: " + building._name);
                    doNotCal++;
                }
            }
        }
        if (doNotCal < _mine + _park + _smithy + _farm) { FactorsUpdate(); }          
        else doNotCal = 0;        
    }

    // ★★★ Key algorithm of how this game calculate the indicators. ★★★
    // You may want to change these algorithms and calculating methods.
    public void FactorsUpdate()
    {
        // algorithm
        foreach (var building in buildingDatabase)
        {
            _pollution += building._pollution;
        }        
        _wealth = ((_gold / 15) + (_steel / 20) + (_food / 25)) / 4;
        _population = (_food / 2) / (_pollution * 5);
        _famine = (_population * 7) / _food + 1;
        _happiness = (_wealth + _employment) / (_famine + _pollution);
        _employment = _population_f / (_mine + _park + _smithy + _farm);
               
        // calculate indicators
        if (_wealth < 0.3)
            _wealth_f -= 1;
        else if (_wealth >= 0.3 && _wealth < 1.5)
            _wealth_f -= 0;
        else if (_wealth >= 1.5 && _wealth < 3)
            _wealth_f += 1;
        else _wealth_f += 2;

        if (_population < 0.4)
            _population_f -= 2;
        else if (_population >= 0.4 && _population < 1)
            _population_f -= 1;
        else if (_population >= 1 && _population < 3)
            _population_f += 1;
        else _population_f += 2;

        if (_famine < 0.4)
            _famine_f -= 2;
        else if (_famine >= 0.4 && _famine < 1)
            _famine_f -= 1;
        else if (_famine >= 1 && _famine < 3)
            _famine_f += 1;
        else _famine_f += 2;

        if (_happiness < 0.4)
            _happiness_f -= 2;
        else if (_happiness >= 0.4 && _happiness < 1)
            _happiness_f -= 1;
        else if (_happiness >= 1 && _happiness < 3)
            _happiness_f += 1;
        else _happiness_f += 2;

        if (_employment < 0.4)
            _employment_f -= 2;
        else if (_employment >= 0.4 && _employment < 1)
            _employment_f -= 1;
        else if (_employment >= 1 && _employment < 3)
            _employment_f += 1;
        else _employment_f += 2;

        // sync pollution
        _pollution_f = _pollution;

        // store log data
        happinessLogs.Add(_happiness);
        wealthLogs.Add(_wealth);
        populationLogs.Add(_population);
        pollutionLogs.Add(_pollution);
        famineLogs.Add(_famine);
        employmentLogs.Add(_employment);

        // print log data
        //foreach (float _happy in happinessLogs)
        //{
        //    int happy = (int)_happy;
        //    print(happinessLogs[happy]);
        //}

    }

    
}


// --------------------------------- The End ----------------------------------------

/*
[Trash]
old algorithms
        _wealth = ((_gold / 15) + (_mine / 20) + (_food / 25)) / 5;        
        _population = (_happiness + _wealth) / (_famine + (_pollution / 5));
        _famine = (_population * 10) / _food;
        _happiness = _wealth / (_famine + (_pollution / 5));


    
        //Buildings(name, id, description, steel, gold, food, water, lubmer, pollution)
        buildingDatabase.Add(new Buildings("Smithy_b", 1, "This can be built at any normal tiles.", 0, 0, 20, 0, 0, 0));
        buildingDatabase.Add(new Buildings("Mine_b", 2, "This can be built at gold land tiles.", 15, 0, 0, 0, 0, 0));
        buildingDatabase.Add(new Buildings("Farm_b", 3, "This can be built at fertile land tiles.", 20, 0, 0, 0, 0, 0));
        buildingDatabase.Add(new Buildings("Park_b", 4, "This can be built at any normal tiles.", 0, 27, 0, 0, 0, 0));


        public void AddBuilding (int buildingID)
    {
        //check if building matched something in DB list
        foreach(var building in buildingDatabase)
        {
            if (buildingID == 1 && building._id == buildingID)
            {
                if (_food >= building._food && _lumber >= building._lumber && _mine + _park + _smithy + _farm < _population_f)
                {
                    Debug.Log(building._name);
                    _smithy++;
                    _food -= building._food;
                    _lumber -= building._lumber;
                    FactorsUpdate();
                    return;
                }
                else
                {
                    Debug.Log("You don't have enough resources to build a: " + building._name);
                    return;
                }
            }
            else if (buildingID == 2 && building._id == buildingID)
            {
                if(_steel >= building._steel && _lumber >= building._lumber && _mine + _park + _smithy + _farm < _population_f)
                {
                    Debug.Log(building._name);
                    _mine++;
                    _steel -= building._steel;
                    _lumber -= building._lumber;
                    FactorsUpdate();
                    return;
                }
                else
                {
                    Debug.Log("You don't have enough resources to build a: " + building._name);
                    return;
                }
            }
            else if (buildingID == 3 && building._id == buildingID)
            {
                if(_steel >= building._steel && _lumber >= building._lumber && _mine + _park + _smithy + _farm < _population_f)
                {
                    Debug.Log(building._name);
                    _farm++;
                    _steel -= building._steel;
                    _lumber -= building._lumber;
                    FactorsUpdate();
                    return;
                }
                else
                {
                    Debug.Log("You don't have enough resources to build a: " + building._name);
                    return;
                }
            }
            else if (buildingID == 4 && building._id == buildingID)
            {
                if(_gold >= building._gold && _mine + _park + _smithy + _farm < _population_f)
                {
                    Debug.Log(building._name);
                    _park++;
                    _gold -= building._gold;
                    FactorsUpdate();
                    return;
                }
                else
                {
                    Debug.Log("You don't have enough resources to build a: " + building._name);
                    return;
                }
            }
        }        
    }


 */
