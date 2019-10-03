using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private CharacterType characterType = CharacterType.Banana;

    [HideInInspector] protected string monsterName;
    
    [SerializeField] protected string monsterDescription;


    public CharacterType GetCharacterType()
    {
        return characterType;
    }
}

public enum CharacterType
{
    Banana,
    Robot
}

