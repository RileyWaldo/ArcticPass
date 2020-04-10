﻿using System;
using UnityEngine;
using UnityEngine.UI;
using ArcticPass.Control;

namespace ArcticPass.UI
{
    public class HudController : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] Image healthGUI;
        [Header("Sprites")]
        [SerializeField] Sprite[] healthSprite;

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
            float health = player.GetComponent<Health>().GetHealth();
            int spriteIndex = Mathf.CeilToInt(health * healthSprite.Length - 1f);
            spriteIndex = Mathf.Clamp(spriteIndex, 0, healthSprite.Length);
            healthGUI.sprite = healthSprite[spriteIndex];
        }
    }
}
