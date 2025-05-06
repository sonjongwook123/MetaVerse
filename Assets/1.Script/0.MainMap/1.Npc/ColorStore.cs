using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStore : MonoBehaviour
{
    public List<Color> colors;
    public GameObject popup;

    public void OpenPopup()
    {
        popup.SetActive(true);
    }

    public void SelectColor(int num)
    {
        GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color = colors[num];
        popup.SetActive(false);
    }

}
