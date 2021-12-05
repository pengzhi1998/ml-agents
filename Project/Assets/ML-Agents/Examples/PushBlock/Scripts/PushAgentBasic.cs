//Put this script on your blue cube.
using static System.Math;
using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class PushAgentBasic : Agent
{
//    public GameObject ground;

    public textchanging Textchanging;

    [HideInInspector]

//    PushBlockSettings m_PushBlockSettings;

    /// The goal for the agent to reach.
//    public GameObject goal;

    /// <summary>
    /// Detects when the block touches the goal.
    /// </summary>
//    [HideInInspector]
//    public GoalDetect goalDetect;

    public bool useVectorObs;

    Rigidbody m_AgentRb;  //cached on initialization
//    Material m_GroundMaterial; //cached on Awake()

    /// <summary>
    /// We will be changing the ground material based on success/failue
    /// </summary>
//    Renderer m_GroundRenderer;

    void Awake()
    {
//        m_PushBlockSettings = FindObjectOfType<PushBlockSettings>();
    }

    public override void Initialize()
    {
        // Cache the agent rigidbody
        m_AgentRb = GetComponent<Rigidbody>();
        // Get the ground renderer so we can change the material when a goal is scored
//        m_GroundRenderer = ground.GetComponent<Renderer>();
        // Starting material
//        m_GroundMaterial = m_GroundRenderer.material;

        SetResetParameters();
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

            Textchanging.show_position(randomSpawnPos, m_AgentRb.transform.eulerAngles[1], randomGoal);

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

            Textchanging.show_position(randomSpawnPos, rotationAngle, randomGoal);

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

            Textchanging.show_position(randomSpawnPos, rotationAngle, randomGoal);

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

            Textchanging.show_position(randomSpawnPos, rotationAngle, randomGoal);

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

            Textchanging.show_position(randomSpawnPos, rotationAngle, randomGoal);

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

        dirToGo = transform.forward * 0.25f + transform.up * act0;
        rotateDir = transform.up * act1;

        transform.Rotate(rotateDir, Time.fixedDeltaTime * 20f * Abs(act1));
        m_AgentRb.AddForce(dirToGo, ForceMode.VelocityChange);

        Textchanging.show_position(m_AgentRb.transform.position, m_AgentRb.transform.eulerAngles[1],
            new Vector3(randomGoalX, randomGoalY, randomGoalZ));
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
        AddReward(-1f / MaxStep);
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

//    public var GetGoalInfo() {
//		euler = tf.transformations.euler_from_quaternion(self.quaternion)  # the euler orientation
//		yaw = euler[2]
//		R2G = np.array(np.array(self.goal) - np.array([self.self_position_x, self.self_position_y])) # the vector of robot towarding the goal
//		distance = np.sqrt(R2G[0]**2 + R2G[1]**2)
//		if yaw < 0:
//			yaw = yaw + 2*np.pi
//		rob_ori = np.array([np.cos(yaw), np.sin(yaw)]) # the orientation vector of the robot
//		angle = np.arccos(R2G.dot(rob_ori)/np.sqrt((R2G.dot(R2G)) * np.sqrt(rob_ori.dot(rob_ori))))
//
//		# determine whether the goal is on the right or left hand side of the robot
//		if rob_ori[0] > 0 and (rob_ori[1]/rob_ori[0]) * R2G[0] > R2G[1]:
//				angle = -angle
//		elif rob_ori[0] < 0 and (rob_ori[1]/rob_ori[0]) * R2G[0] < R2G[1]:
//				angle = -angle
//		elif rob_ori[0] == 0:
//			if rob_ori[1] > 0 and R2G[0] > 0:
//				angle = -angle
//			elif rob_ori[1] < 0 and R2G[0] < 0:
//				angle = -angle
//
//		goal = np.array([distance, angle])
//		# print("goal_position, current_position, distance, angle:{:.2f}, {:.2f}, {:.2f}, {:.2f}, {:.2f}, {:.2f}".
//		# 	  format(self.goal, (self.self_position_x, self.self_position_y), goal))
//		# print("goal_position, current_position, distance, angle: {:.2f}, {:.2f}, {:.2f}, {:.2f}, {:.2f}, {:.2f}".
//		# 	  format(np.array(self.goal)[0], np.array(self.goal)[1],
//		# 			 self.self_position_x, self.self_position_y,
//		# 			 goal[0], goal[1]))
//		return goal
//    }

//    set the haze, fog and attenuation here
    void SetResetParameters()
    {
//        SetWaterCondition();
    }
}
