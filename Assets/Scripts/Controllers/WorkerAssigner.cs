using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Buildings;
using Scripts.Characters;
using UnityEngine;

namespace Scripts.Controllers
{
    public class WorkerAssigner : MonoBehaviour
    {
        private List<CharacterStatus> characters;
        private List<CharacterStatus> unasignedCharacters = new List<CharacterStatus>();

        private List<Workplace> workplaces;
        private List<Workplace> freeWorkplaces = new List<Workplace>();


        private void Start()
        {
            characters = FindObjectsOfType<CharacterStatus>().ToList();
            foreach (CharacterStatus character in characters)
            {
                if (character.work == null) unasignedCharacters.Add(character);
            }

            workplaces = FindObjectsOfType<Workplace>().ToList();
            foreach(Workplace workplace in workplaces)
            {
                if (workplace.MaxAssigne != workplace.GetWorkersCount()) freeWorkplaces.Add(workplace);
            }
        }

        public bool AssignToWorkIfFreeCharacters()
        {
            if (unasignedCharacters.Count == 0) return false;
            if (freeWorkplaces.Count == 0) return false;

            freeWorkplaces[0].AssignWorker(unasignedCharacters[0]);
            unasignedCharacters.Remove(unasignedCharacters[0]);
            if (freeWorkplaces[0].MaxAssigne == freeWorkplaces[0].GetWorkersCount()) freeWorkplaces.Remove(freeWorkplaces[0]);

            return true;
        }

        public bool AssignToWorkIfFreeCharacters(Workplace workplace)
        {
            if (unasignedCharacters.Count == 0) return false;
            if (freeWorkplaces.Count == 0) return false;

            workplace.AssignWorker(unasignedCharacters[0]);
            unasignedCharacters.Remove(unasignedCharacters[0]);
            if (workplace.MaxAssigne == workplace.GetWorkersCount()) freeWorkplaces.Remove(workplace);

            return true;
        }

       

        public void AddWorkplace(Workplace workplace)
        {
            freeWorkplaces.Add(workplace);

            for (int i = 0; i < workplace.MaxAssigne; i++)
            {
                AssignToWorkIfFreeCharacters(workplace);
            }
        }

        public void AddWorker(CharacterStatus character)
        {
            unasignedCharacters.Add(character);
            AssignToWorkIfFreeCharacters();
        }


    }
}