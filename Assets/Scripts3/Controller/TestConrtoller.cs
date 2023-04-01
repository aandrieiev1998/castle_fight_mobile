using Scripts3.Mobs;
using UnityEngine;

namespace Scripts3.Controller
{
    public class TestConrtoller : MonoBehaviour
    {
        public GameObject Mob;

        public void Start()
        {
            var component = this.Mob.GetComponent<Unit>();
            Debug.Log(component.name);
        }
    }
}