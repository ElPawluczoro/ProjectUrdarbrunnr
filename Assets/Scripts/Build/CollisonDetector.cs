using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Build
{
    public class CollisonDetector : MonoBehaviour
    {
        private bool canBeBuilt = true;
        public bool CanBeBuilt => canBeBuilt;

        private List<Collider2D> colliders = new List<Collider2D>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            colliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            colliders.Remove(collision);
        }

        private void Update()
        {
            if(colliders.Count > 0)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                canBeBuilt = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.green;
                canBeBuilt = true;
            }
        }




    }
}
