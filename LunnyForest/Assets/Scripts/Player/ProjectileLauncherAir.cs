using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherAir : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectilePoint;

    public void FireProjectileAir()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _projectilePoint.position,
            _projectilePrefab.transform.rotation);
        Vector3 originScale = projectile.transform.localScale;
        projectile.transform.localScale =
            new Vector3(originScale.x * transform.localScale.x > 0 ? 1 : -1, originScale.y, originScale.z);
    }
}
