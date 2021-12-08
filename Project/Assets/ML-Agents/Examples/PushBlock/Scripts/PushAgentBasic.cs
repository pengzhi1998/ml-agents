//Put this script on your blue cube.
using static System.Math;
using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.SideChannels;
using Unity.MLAgents.Sensors;

public class PushAgentBasic : Agent
{
//    public GameObject ground;

    [HideInInspector]

    public bool useVectorObs;

    Rigidbody m_AgentRb;  //cached on initialization
                          //    Material m_GroundMaterial; //cached on Awake()

    ObsSideChannel obsSideChannel;
    public textchanging Textchanging;
    RayPerceptionSensorComponent3D RayInput;
    CameraSensorComponent CameraInput;

    void Awake()
    {
        obsSideChannel = new ObsSideChannel();
        SideChannelManager.RegisterSideChannel(obsSideChannel);
    }

    public override void Initialize()
    {
        // Cache the agent rigidbody
        m_AgentRb = GetComponent<Rigidbody>();
        SetResetParameters();
    }

    public void OnDestroy()
    {
        SideChannelManager.UnregisterSideChannel(obsSideChannel);
    }

    public static float randomGoalX = 0f;
    public static float randomGoalY = 0f;
    public static float randomGoalZ = 0f;

    /// randomize the initial position
    public (Vector3, float, Vector3) GetRandomSpawnPos()
    {
        var randomSpawnPos = Vector3.zero;
        var randomGoal = Vector3.zero;

        float chance_Robot = Random.Range(0f, 1f);
        if (chance_Robot < 0.3f) {
            var randomPosX = Random.Range(-1f, 1f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(3f, 4.5f);

            float rotationAngle = Random.Range(90f, 270f);

            float chance_Goal = Random.Range(0f, 1f);
            if (chance_Goal < 0.5f) {
                randomGoalX = Random.Range(-4f, -3f);
                randomGoalY = Random.Range(-2f, -1f);
                randomGoalZ = Random.Range(-4.5f, -3.5f);
            }
            else {
                randomGoalX = Random.Range(3f, 4f);
                randomGoalY = Random.Range(-2f, -1f);
                randomGoalZ = Random.Range(-4.5f, -3.5f);
            }
            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else if (chance_Robot < 0.55f) {
            var randomPosX = Random.Range(-4f, -3f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(-4.5f, -3.5f);

            float rotationAngle = Random.Range(-45f, 135f);

            float chance_Goal = Random.Range(0f, 1f);
            if (chance_Goal < 0.5f) {
                randomGoalX = Random.Range(-4f, -3f);
                randomGoalY = Random.Range(-1.5f, -1f);
                randomGoalZ = Random.Range(3.5f, 4f);
            }
            else {
                randomGoalX = Random.Range(3f, 4f);
                randomGoalY = Random.Range(-2f, -1.5f);
                randomGoalZ = Random.Range(3.5f, 4f);
            }
            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else if (chance_Robot < 0.8f) {
            var randomPosX = Random.Range(3f, 4f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(-4.5f, -3.5f);

            float rotationAngle = Random.Range(-135f, 45f);

            float chance_Goal = Random.Range(0f, 1f);
            if (chance_Goal < 0.5f) {
                randomGoalX = Random.Range(-4f, -3f);
                randomGoalY = Random.Range(-1.5f, -1f);
                randomGoalZ = Random.Range(3.5f, 4f);
            }
            else {
                randomGoalX = Random.Range(3f, 4f);
                randomGoalY = Random.Range(-2f, -1.5f);
                randomGoalZ = Random.Range(3.5f, 4f);
            }
            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else if (chance_Robot < 0.9f) {
            var randomPosX = Random.Range(-9f, -8f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(4.5f, 5.5f);

            float rotationAngle = Random.Range(-45f, 45f);

            randomGoalX = Random.Range(8f, 9f);
            randomGoalY = Random.Range(-2f, -1f);
            randomGoalZ = Random.Range(8f, 9f);
            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }

        else {
            var randomPosX = Random.Range(8f, 9f);
            var randomPosY = Random.Range(-2f, -1f);
            var randomPosZ = Random.Range(4.5f, 5.5f);

            float rotationAngle = Random.Range(-45f, 45f);

            randomGoalX = Random.Range(-9f, -8f);
            randomGoalY = Random.Range(-2f, -1f);
            randomGoalZ = Random.Range(8f, 9f);

            randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);
            randomGoal = new Vector3(randomGoalX, randomGoalY, randomGoalZ);

            return (randomSpawnPos, rotationAngle, randomGoal);
        }


    }

    /// <summary>
    /// Called when the agent moves the block into the goal.
    /// </summary>
    public void ScoredAGoal()
    {
        // We use a reward of 5.
        AddReward(5f);

        // By marking an agent as done AgentReset() will be called automatically.
        EndEpisode();

        // Swap ground material for a bit to indicate we scored.
//        StartCoroutine(GoalScoredSwapGroundMaterial(m_PushBlockSettings.goalScoredMaterial, 0.5f));
    }

    /// <summary>
    /// Swap ground material, wait time seconds, then swap back to the regular material.
    /// </summary>
    IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
//        m_GroundRenderer.material = mat;
        yield return new WaitForSeconds(time); // Wait for 2 sec
//        m_GroundRenderer.material = m_GroundMaterial;
    }

    /// <summary>
    /// Moves the agent according to the selected action.
    /// </summary>
    public void MoveAgent(float act0, float act1)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        dirToGo = transform.forward * 0.15f + transform.up * act0;
        rotateDir = transform.up * act1;

        transform.Rotate(rotateDir, Time.fixedDeltaTime * 20f * Abs(act1));
        m_AgentRb.AddForce(dirToGo, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
    public override void OnActionReceived(ActionBuffers actionBuffers)

    {
        // Move the agent using the action.
        var continuous_actions = actionBuffers.ContinuousActions;
        MoveAgent(continuous_actions[0], continuous_actions[1]);

        // Penalty given each step to encourage agent to finish task quickly.
        var GoalInfo = GetGoalInfo(); // horizontal_distance, vertical_distance, angle_rb_2_g

        Textchanging.show_position(m_AgentRb.transform.position, m_AgentRb.transform.eulerAngles[1],
            new Vector3(randomGoalX, randomGoalY, randomGoalZ), GoalInfo.Item1, GoalInfo.Item2, GoalInfo.Item3);
        AddReward(-1f / MaxStep);

        obsSideChannel.SendObsToPython(GoalInfo.Item1, GoalInfo.Item2, GoalInfo.Item3);

    }

    /// <summary>
    /// In the editor, if "Reset On Done" is checked then AgentReset() will be
    /// called automatically anytime we mark done = true in an agent script.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        var random_robot_goal = GetRandomSpawnPos();
        m_AgentRb.transform.position = random_robot_goal.Item1;
        m_AgentRb.transform.eulerAngles = new Vector3(0f, random_robot_goal.Item2, 0f);

        m_AgentRb.velocity = Vector3.zero;
        m_AgentRb.angularVelocity = Vector3.zero;

        SetResetParameters();
    }

    /// <summary>
    /// Compute the relative goal information for reward and observation
    /// </summary>
    public static float horizontal_distance = 0f;
    public static float angle_rb_2_g = 0f;
    public (float, float, float) GetGoalInfo() {
        Vector3 Current_pos = m_AgentRb.transform.position;

        /// first compute the distance from robot to goal
        horizontal_distance = (float)Sqrt((float)Pow(Current_pos[0] - randomGoalX, 2) +
            (float)Pow(Current_pos[2] - randomGoalZ, 2));

        /// secondly compute the angle the robot needs to turn to face the goal
        Vector3 angle_goal_vector = new Vector3(randomGoalX - Current_pos[0],
            randomGoalY - Current_pos[1], randomGoalZ - Current_pos[2]);
        Vector3 angle_goal_vector_proj = angle_goal_vector - Vector3.Project(angle_goal_vector, Vector3.up);

        angle_rb_2_g = Vector3.Angle(m_AgentRb.transform.forward, angle_goal_vector_proj);
        float dir = (Vector3.Dot(Vector3.up, Vector3.Cross(m_AgentRb.transform.forward, angle_goal_vector_proj)) < 0 ? 1 : -1);
        angle_rb_2_g *= dir; // source implementation: https://blog.csdn.net/qq_14838361/article/details/79459391

        return (horizontal_distance, (Current_pos[1] - randomGoalY), angle_rb_2_g);
    }

//    set the haze, fog and attenuation here
    void SetResetParameters()
    {
//        SetWaterCondition();
    }
}
