using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{ 
    private TextMeshProUGUI healthText;
    private Player player;

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}