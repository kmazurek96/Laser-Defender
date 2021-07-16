using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPrice : MonoBehaviour
{
    [SerializeField] int price = 0;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        UpdatePrice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdatePrice()
    {
        text.text = price.ToString() + "$";
    }

    public int GetPrice()
    {
        return price;
    }


}
