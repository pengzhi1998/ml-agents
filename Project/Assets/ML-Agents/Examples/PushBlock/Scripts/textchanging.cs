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

    public void show_position(Vector3 pos_rb, float rotation, Vector3 pos_goal,
        float horizontal_distance, float vertical_distance, float angle_rb_2_g) {
        Text txtMy = GameObject.Find("Canvas/Text").GetComponent<Text>();
        txtMy.text = "Current Pos: " + pos_rb + "\nCurrent Rot: " + rotation.ToString("0.0000") +
         "\nCurrent Goal: " + pos_goal + "\nhorizontal_distance: " + horizontal_distance +
        "\nvertical_distance: " + vertical_distance + "\nangle_rb_2_g" + angle_rb_2_g;
//        txtMy.text = pos.ToString("0.0000");
    }
}
