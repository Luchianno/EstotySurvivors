using UnityEngine;
using Zenject;

public class EnemyTaunt : BasePausableBehaviour
{
    [SerializeField] EnemyUnit thisUnit;
    [SerializeField] float tauntChance = 0.01f;
    [SerializeField] float tauntDistance = 10f; 

    [Inject(Id = "TauntPopup")] TextPopup.Factory popupFactory;
    [Inject(Id = "Player")] Transform playerUnit;

    void Update()
    {
        if (thisUnit.IsAlive && thisUnit.Data.Taunts == null)
        {
            return;
        }

        if (Random.value < tauntChance)
        {
            // only taunt if close to player
            if (Vector2.SqrMagnitude(thisUnit.transform.position - playerUnit.position) > tauntDistance * tauntDistance)
            {
                return;
            }

            var taunt = thisUnit.Data.Taunts.GetRandomTaunt();

            if (taunt == null)
            {
                return;
            }

            popupFactory.Create(taunt, Color.white, transform.position + Vector3.up);
        }
    }
}
