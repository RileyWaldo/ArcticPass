using System;
using UnityEngine;
using UnityEngine.UI;
using ArcticPass.Control;

namespace ArcticPass.UI
{
    public class HudController : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] Image healthGUI = null;
        [Header("Sprites")]
        [SerializeField] Sprite[] healthSprite = null;

        GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            UpdateStatusBars();
        }

        private void UpdateStatusBars()
        {
            
        }
    }
}
