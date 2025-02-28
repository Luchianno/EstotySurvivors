using UnityEngine;
using Zenject;

public class EnemyUnit : MonoBehaviour
{
    [field: SerializeField] 
    public EnemyData Data { get; protected set; }

    [SerializeField] UnitHealth unitHealth;

    public void ResetState(Vector3 position, EnemyData data)
    {
        this.Data = data;

        transform.position = position;
        this.transform.localScale = Vector3.one* Data.EnemySize;

        unitHealth.ResetState(Data.MaxHealth);
    }

    public class Pool : MonoMemoryPool<Vector3, EnemyData, EnemyUnit>
    {
        protected override void Reinitialize(Vector3 position,  EnemyData data, EnemyUnit unit)
        {
            unit.Data = data;
            unit.ResetState(position, data);
        }
    }

}
