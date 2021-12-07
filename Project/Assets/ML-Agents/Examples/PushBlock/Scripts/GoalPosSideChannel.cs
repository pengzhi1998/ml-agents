using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;

public class GoalPosSideChannel : SideChannel
{
    public GoalPosSideChannel() {
        ChannelId = new Guid("621f0a70-4f87-11ea-a6bf-784f4387d1f7");
    }

    protected override void OnMessageReceived(IncomingMessage msg)
    {
        
    }

    public void SendGoalPosToPython(float horizontalDist, float verticleDist, float Angle)
    {

        List<float> msgToSend = new List<float>() { horizontalDist, verticleDist, Angle };
        using (var msgOut = new OutgoingMessage())
        {
            msgOut.WriteFloatList(msgToSend);
            QueueMessageToSend(msgOut);
            


        }

    }
}
