from mlagents_envs.environment import UnityEnvironment
from gym_unity.envs import UnityToGymWrapper
import numpy as np
import cv2
print("hello0!\n\n\n")

# unity_env = UnityEnvironment("/home/pengzhi1998/Unity/AUV_Unity_Simulation/AUV_arena_2020/robot")
# unity_env = UnityEnvironment("/home/pengzhi1998/Unity/ml-agents/pushblock")
unity_env = UnityEnvironment("/home/pengzhi1998/Unity/ml-agents/environments/water")
# unity_env = UnityEnvironment("/home/pengzhi1998/Unity/ml-agents/environments/3DBall")

print("hello1!\n\n\n")

env = UnityToGymWrapper(unity_env, allow_multiple_obs=True)

print("hello2!\n\n\n")

while True:
    print("hi!")
    env.reset()
    done = False
    t = 0
    while not done:
        # first action defines its motion vertically, second defines its rotation
        if t < 10:
            obs, reward, done, _ = env.step([0.20, 1.0])
            print("a", t)
        else:
            obs, reward, done, _ = env.step([-0.20, -1.0])
            print("b", t)
        t += 1

        cv2.imwrite("img.png", 256 * cv2.cvtColor(obs[0], cv2.COLOR_RGB2BGR))

        # print("obs", obs[0], "\n\n\n\n")

