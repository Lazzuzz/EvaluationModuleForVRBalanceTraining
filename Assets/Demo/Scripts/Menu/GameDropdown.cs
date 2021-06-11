using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Dropdown))]
public class GameDropdown : MonoBehaviour
{

    public Dropdown dropdown;

    public gameType gameTyp;

    // Start is called before the first frame update
    void Start()
    {
        GameTypList();

        dropdown = GetComponent<Dropdown>();


    }

    void GameTypList()
    { 
        string[] enumGameTypes = Enum.GetNames(typeof(gameType));
        List<String> gametyps = new List<string>(enumGameTypes);
        dropdown.AddOptions(gametyps);
    }


}


public enum gameType
{
    LONGEST,
    TOTAL,
    COLLIDER,
    ABSOLUTE_DISTANSE
}
