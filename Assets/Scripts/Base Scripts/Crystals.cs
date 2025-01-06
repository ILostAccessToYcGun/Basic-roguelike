using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crystals : MonoBehaviour
{
    public GameObject CrystalUI;
    public TextMeshProUGUI percentages;
    protected BuffManager buffManager;

    private void Awake()
    {
        buffManager = FindAnyObjectByType<BuffManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            CrystalUI.gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            CrystalUI.gameObject.SetActive(false);
    }
}
