using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Characters
{
    public class CharacterStatus : MonoBehaviour
    {
        [SerializeField] private int hunger = 75; //max 100

        public int Hunger => hunger;

        public void DecreaseHunger(int value)
        {
            hunger -= value;
            if(hunger < 0) hunger = 0;
        }

        public void AddHunger(int value)
        {
            hunger += value;
            if(hunger > 100) hunger = 100;
        }
    }
}