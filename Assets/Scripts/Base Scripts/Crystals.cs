using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crystals : HoverableInteractables
{
    protected BuffManager buffManager;
    public TextMeshProUGUI percentages; //uhhhhhhh :)
    public ShopKeeper shopKeeper;
    private void Awake()
    {
        buffManager = FindAnyObjectByType<BuffManager>();
    }
}
