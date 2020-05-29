using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    Text pointsText;
    // Start is called before the first frame update
    void Start()
    {
        pointsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "" + GameManager.instance.points;   
    }
}
