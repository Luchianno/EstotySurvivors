using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image xpBar;

    void UpdateUI(float health, float maxHealth, float xp, float maxXp)
    {
        healthBar.fillAmount = health / maxHealth;
        xpBar.fillAmount = xp / maxXp;
    }
}
