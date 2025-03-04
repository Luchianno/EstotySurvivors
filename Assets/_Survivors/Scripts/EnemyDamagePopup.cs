using UnityEngine;
using Zenject;

// when enemy unit is damaged, text with damage value will appear above the enemy
public class EnemyDamagePopup : MonoBehaviour
{
    [SerializeField] Color damageColor = Color.red;
    [SerializeField] Color criticalDamageColor = Color.red;

    [Inject] TextPopup.Factory textPopupFactory;

    void Awake()
    {
        GetComponent<UnitHealth>().OnHealthChange.AddListener(OnHealthChanged);
    }

    void OnHealthChanged(HealthChange healthChange)
    {
        
    }
}
