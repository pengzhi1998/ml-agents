//Put this script on your blue cube.

using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class PushAgentBasic : Agent
{
//    public GameObject ground;

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

    /// randomize the initial position
    public Vector3 GetRandomSpawnPos()
    {
        var randomSpawnPos = Vector3.zero;

        var randomPosX = Random.Range(-5f, -4f);
        var randomPosZ = Random.Range(-5f, -4f);
        var randomPosY = Random.Range(-1.2f, -0.8f);
        randomSpawnPos = new Vector3(randomPosX, randomPosY, randomPosZ);

        return randomSpawnPos;
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
    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];

        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
            case 5:
                dirToGo = transform.right * -0.75f;
                break;
            case 6:
                dirToGo = transform.right * 0.75f;
                break;
            case 7:
                dirToGo = transform.up * 1.0f;
                break;
            case 8:
                dirToGo = transform.up * -1.0f;
                break;
        }
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        m_AgentRb.AddForce(dirToGo * 0.25f, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Called every step of the engine. Here the agent takes an action.
    /// </summary>
    public override void OnActionReceived(ActionBuffers actionBuffers)

    {
        // Move the agent using the action.
        MoveAgent(actionBuffers.DiscreteActions);

        // Penalty given each step to encourage agent to finish task quickly.
        AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    /// <summary>
    /// Resets the block position and velocities.
    /// </summary>
    void ResetGoal()
    {
//        // Get a random position for the block.
//        block.transform.position = GetRandomSpawnPos();
//
//        // Reset block velocity back to zero.
//        m_BlockRb.velocity = Vector3.zero;
//
//        // Reset block angularVelocity back to zero.
//        m_BlockRb.angularVelocity = Vector3.zero;
    }

    /// <summary>
    /// In the editor, if "Reset On Done" is checked then AgentReset() will be
    /// called automatically anytime we mark done = true in an agent script.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        var rotation = Random.Range(0, 4);
        var rotationAngle = rotation * 90f;

        transform.position = GetRandomSpawnPos();
        m_AgentRb.velocity = Vector3.zero;
        m_AgentRb.angularVelocity = Vector3.zero;

        SetResetParameters();
    }

//    set the haze, fog and attenuation here
    void SetResetParameters()
    {
//        SetWaterCondition();
    }
}
