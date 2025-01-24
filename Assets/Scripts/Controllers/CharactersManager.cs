using Scripts.Characters;
using UnityEngine;

namespace Scripts.Controllers
{
    public class CharactersManager : MonoBehaviour
    {
        [SerializeField] private GameObject basicCharacter;

        private WorkerAssigner assigner;

        private void Start()
        {
            assigner = FindObjectOfType<WorkerAssigner>();
        }

        public void AddCharacter()
        {
            var newCharacter = Instantiate(basicCharacter);
            newCharacter.transform.position = new Vector3(0, 0, 0);
            assigner.AddWorker(newCharacter.GetComponent<CharacterStatus>());
        }





    }
}
