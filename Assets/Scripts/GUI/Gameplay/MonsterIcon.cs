using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterIcon : MonoBehaviour
{
    [SerializeField] private Image _MonsterIcon = null;

    private void Awake()
    {
        _MonsterIcon = GetComponent<Image>();
    }

    public void Icon(Sprite icon)
    {
        _MonsterIcon.sprite = icon;
    }
}
