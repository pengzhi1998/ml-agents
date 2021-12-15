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
        txtMy.text = "Delta Time: " + Time.deltaTime.ToString("0.0000") + ", " +
        "\nCurrent Time: " + Time.time.ToString("0.0000") + ", " + "\nCurrent Pos: " +
        pos_rb[0].ToString("0.0000") + ", " + pos_rb[1].ToString("0.0000") + ", "
        + pos_rb[2].ToString("0.0000") + "\nCurrent Rot: " + rotation.ToString("0.0000") +
         "\nCurrent Goal: " + pos_goal + "\nhorizontal_distance: " + horizontal_distance +
        "\nvertical_distance: " + vertical_distance + "\nangle_rb_2_g: " + angle_rb_2_g
        + "\ntransform:" + transform.up[0].ToString("0.0000") + ", " + transform.up[1].ToString("0.0000")
        + ", " + transform.up[2].ToString("0.0000");
//        txtMy.text = pos.ToString("0.0000");
    }
}
