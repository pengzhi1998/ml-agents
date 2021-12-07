from mlagents_envs.environment import UnityEnvironment
from gym_unity.envs import UnityToGymWrapper
import numpy as np
import cv2
from mlagents_envs.side_channel.side_channel import (
    SideChannel,
    IncomingMessage,
    OutgoingMessage,
)
import uuid

class PosChannel(SideChannel):

    def __init__(self) -> None:
        super().__init__(uuid.UUID("621f0a70-4f87-11ea-a6bf-784f4387d1f7"))

    def on_message_received(self, msg: IncomingMessage) -> None:
        """
        Note: We must implement this method of the SideChannel interface to
        receive messages from Unity
        """
        print(msg.read_float32_list())

pos_info = PosChannel()
unity_env = UnityEnvironment("/home/pengzhi1998/Unity/ml-agents/environments/water", side_channels=[pos_info])
env = UnityToGymWrapper(unity_env, allow_multiple_obs=True)

while True:
    print("hi!")
    env.reset()
    done = False
    t = 0
    while not done:
        # first action defines its motion vertically, second defines its rotation
        if t < 10:
            obs, reward, done, _ = env.step([0.0, 0.0])
            # print("a", t)
            print(np.shape(obs), obs)
        else:
            obs, reward, done, _ = env.step([0.0, 0.0])
            # print("b", t)
        t += 1

        # cv2.imwrite("img.png", 256 * cv2.cvtColor(obs[0], cv2.COLOR_RGB2BGR))

        # print("obs", obs[0], "\n\n\n\n")

