using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class textchanging : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void show_position(float pos) {
        Text txtMy = GameObject.Find("Canvas/Text").GetComponent<Text>();
        txtMy.text = "The text has been changed";
//        txtMy.text = pos.ToString("0.0000");
    }
}
