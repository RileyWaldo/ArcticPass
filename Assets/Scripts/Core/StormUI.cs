using UnityEngine;
using UnityEngine.UI;
using ArcticPass.Core;

namespace ArcticPass.UI
{
    public class StormUI : MonoBehaviour
    {
        [SerializeField] Image playerSlider;
        [SerializeField] Image stormSlider;

        Storm storm;

        private void Start()
        {
            storm = Storm.GetStorm();
        }

        private void Update()
        {

        }
    }
}
