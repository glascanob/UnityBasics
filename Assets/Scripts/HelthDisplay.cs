using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthDisplay : MonoBehaviour
{
    Image barImage;
    // Start is called before the first frame update
    void Start()
    {
        barImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        barImage.fillAmount = GameManager.instance.player.health / GameManager.instance.player.maxHealth;
    }
}
