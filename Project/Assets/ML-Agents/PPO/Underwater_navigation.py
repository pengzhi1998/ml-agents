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
        self.goal = msg.read_float32_list()

    def goal_info(self):
        return self.goal

class Underwater_navigation():
    def __init__(self):
        self.pos_info = PosChannel()
        unity_env = UnityEnvironment("/home/pengzhi1998/Unity/ml-agents/environments/water", side_channels=[self.pos_info])
        self.env = UnityToGymWrapper(unity_env, allow_multiple_obs=True)

    def reset(self):
        obs_img_ray = self.env.reset()
        obs_goal = self.pos_info.goal_info()
        return obs_img_ray[0], obs_img_ray[1], obs_goal


    def step(self, action):
        obs_img_ray, _, done, _ = self.env.step(action)
        obs_goal = self.pos_info.goal_info()

        return [obs_img_ray[0], obs_img_ray[1], obs_goal], 0, done, 0

env = Underwater_navigation()

while True:
    done = False
    obs = env.reset()
    # cv2.imwrite("img1.png", 256 * cv2.cvtColor(obs[0], cv2.COLOR_RGB2BGR))
    while not done:
        obs, reward, done, _ = env.step([0.0, 0.0])
        print(obs[1], np.shape(obs[1]))
        # cv2.imwrite("img2.png", 256 * cv2.cvtColor(obs[0], cv2.COLOR_RGB2BGR))

