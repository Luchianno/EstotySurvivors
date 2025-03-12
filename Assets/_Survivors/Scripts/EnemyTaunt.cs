using UnityEngine;
using Zenject;

public class EnemyTaunt : MonoBehaviour
{
    [SerializeField] float tauntChance = 0.01f;
    [SerializeField] EnemyUnit enemyUnit;

    [Inject(Id = "TauntPopup")] TextPopup.Factory popupFactory;

    void Update()
    {
        if (enemyUnit.IsAlive && enemyUnit.Data.Taunts == null)
        {
            return;
        }

        if (Random.value < tauntChance)
        {
            var taunt = enemyUnit.Data.Taunts.GetRandomTaunt();

            if (taunt == null)
            {
                return;
            }

            popupFactory.Create(taunt, Color.white, transform.position + Vector3.up);
        }
    }
}
