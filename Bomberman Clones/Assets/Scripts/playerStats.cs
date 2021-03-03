using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStatsTools
{
    public class playerStats : MonoBehaviour
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

        public int changeExplosionStrength(playerStats stat, int increasedExplosion)
        {
            return stat.explosionStrength + increasedExplosion;
        }

        public float changeWalkSpeed(playerStats stat, float increaseSpeed)
        {
           return stat.walkSpeed + increaseSpeed;
        }

        public void changeBombCount()
        {

        }
    }
}

