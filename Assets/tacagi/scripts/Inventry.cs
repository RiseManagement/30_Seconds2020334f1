using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Inventry :MonoBehaviour
{
    public static Inventry instance;
    InventryUI InventryUI;
    [SerializeField] Text explanationText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        //gameObject.SetActive(false);
        InventryUI = GetComponent<InventryUI>();
        InventryUI.UpdateUI();

    }

    public List<int> itemsid = new List<int>();

    public void Add(int itemid)
    {

        itemsid.Add(itemid);
        InventryUI.UpdateUI();
    }

    public void Removed(int itemid)
    {
        itemsid.Remove(itemid);
        InventryUI.UpdateUI();
    }

    public void SetExplanationText(string _explanationText)
    {
        explanationText.text = _explanationText;
    }
}
