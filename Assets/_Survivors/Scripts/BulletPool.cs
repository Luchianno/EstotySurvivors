using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class BulletPool : PausableMonoPoolableMemoryPool<Vector2, Vector2, BulletData, SimpleBulletBehaviour> { }
