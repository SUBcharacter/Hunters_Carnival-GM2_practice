using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void show()
    {
        rect.localScale = Vector3.one;
    }
    
    public void hide()
    {
        rect.localScale = Vector3.zero;
    }

    public void Select(int index)
    {
        items[index].OnClick();

    }
}
