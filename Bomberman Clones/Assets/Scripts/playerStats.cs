using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStatsTools
{
    public class playerStats
    {
        public int explosionStrength;
        public float walkSpeed;
        public int bombCount;

        public playerStats(int explosionStrength, float walkSpeed, int bombCount)
        {
            this.explosionStrength = explosionStrength;
            this.walkSpeed = walkSpeed;
            this.bombCount = bombCount;
        }

        public int ExplosionStrength
        {
            get { return explosionStrength; }
            set { explosionStrength = value; }
        }

        public float WalkSpeed
        {
            get { return walkSpeed; }
            set { walkSpeed = value; }
        }

        public int BombCount
        {
            get { return bombCount; }
            set { bombCount = value; }
        }

        public int changeExplosionStrength(playerStats stat, int changeExplosionCount)
        {
            return stat.explosionStrength + changeExplosionCount;
        }

        public float changeWalkSpeed(playerStats stat, float changeSpeed)
        {
           return stat.walkSpeed + changeSpeed;
        }

        public int changeBombCount(playerStats stat, int changeBombCount)
        {
            return stat.bombCount + changeBombCount;
        }
    }
}

