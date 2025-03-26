// using UnityEngine;
// using Zenject;

// // Implements MonoPoolableMemoryPool, but also IFactory<TParam1, TParam2, TPrefab>
// public class PausableFactory<TParam1, TParam2, TValue> : MonoPoolableMemoryPool<TParam1, TParam2, TValue>, IPausable
//     where TValue : Component, IPoolable<TParam1, TParam2>, IPausable
// {
//     // This is the method that will be called when the object is created
//     protected override void OnCreated(TValue item)
//     {
//         base.OnCreated(item);
//         item.SetPaused(true);
//     }

//     // This is the method that will be called when the object is destroyed
//     protected override void OnDespawned(TValue item)
//     {
//         base.OnDespawned(item);
//         item.SetPaused(false);
//     }

//     public void SetPaused(bool isPaused)
//     {
//         foreach (var item in ActiveItems)
//         {
//             item.SetPaused(isPaused);
//         }
//     }

// }