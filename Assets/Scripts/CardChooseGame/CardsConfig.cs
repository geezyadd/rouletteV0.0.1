using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CardsConfig), menuName = nameof(CardsConfig))]
public class CardsConfig : ScriptableObject
{
    [SerializeField] private List<CardHolder> _cards;
    public List<CardHolder> Cards =>
        _cards;
}

[Serializable]
public class CardHolder
{
    public Sprite Sprite;
    public CardType CardType;
}
