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

//    Textchanging.show_position(randomSpawnPos, rotationAngle, randomGoal);

    public void show_position(Vector3 pos_rb, float rotation, Vector3 pos_goal) {
        Text txtMy = GameObject.Find("Canvas/Text").GetComponent<Text>();
        txtMy.text = rotation.ToString("0.0000");
//        txtMy.text = pos.ToString("0.0000");
    }
}
