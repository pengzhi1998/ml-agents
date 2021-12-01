from mlagents_envs.environment import UnityEnvironment
from gym_unity.envs import UnityToGymWrapper

print("hello0!\n\n\n")

# unity_env = UnityEnvironment("/home/pengzhi1998/Unity/AUV_Unity_Simulation/AUV_arena_2020/robot")
unity_env = UnityEnvironment("/home/pengzhi1998/Unity/ml-agents/water")

print("hello1!\n\n\n")

env = UnityToGymWrapper(unity_env)

print("hello2!\n\n\n")

while True:
    print("hi!")
    env.reset()
    done = False
    while not done:
        obs, reward, done, _ = env.step(2)

