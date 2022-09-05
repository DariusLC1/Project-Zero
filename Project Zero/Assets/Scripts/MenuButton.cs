using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameObject subMenu;


    public void optionsButton()
    {
        if (subMenu.activeInHierarchy == true)
        {
            subMenu.SetActive(false);
        }
        else if (subMenu.activeInHierarchy == false)
        {
            subMenu.SetActive(true);
        }
    }
}
