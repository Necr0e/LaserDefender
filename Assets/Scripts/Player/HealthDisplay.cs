using TMPro;
using UnityEngine;

namespace Player
{
    public class HealthDisplay : MonoBehaviour
    { 
        private TextMeshProUGUI healthText;
        private Player player;

        private void Start()
        {
            healthText = GetComponent<TextMeshProUGUI>();
            player = FindObjectOfType<global::Player.Player>();
        }

        private void Update()
        {
            healthText.text = player.GetHealth().ToString();
        }
    }
}