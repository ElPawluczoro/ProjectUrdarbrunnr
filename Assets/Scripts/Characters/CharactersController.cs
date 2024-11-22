using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Characters
{
    public class CharactersController : MonoBehaviour
    {
        public List<CharacterStatus> characters = new List<CharacterStatus>();

        [SerializeField] private int hungerPerTick = 1;
        [SerializeField] private float tickCooldawn = 0.5f;

        private float timeScienceTick = Mathf.Infinity;

        private void Start()
        {
            foreach (CharacterStatus ch in GameObject.FindObjectsOfType<CharacterStatus>())
            {
                characters.Add(ch);
            }
        }

        private void Update()
        {
            if(timeScienceTick > tickCooldawn)
            {
                Hunger();
                timeScienceTick = 0;
            }
            timeScienceTick += Time.deltaTime;
        }

        public void Hunger()
        {
            foreach (CharacterStatus ch in characters)
            {
                ch.DecreaseHunger(hungerPerTick);
            }


        }
    }
}
