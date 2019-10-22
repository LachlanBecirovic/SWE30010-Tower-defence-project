using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float targetRadius = 10;
    public float fireRate = 2;
    private float _lastShootTime = -1;

    private GameObject GetTarget()
    {
        float minDistance = float.PositiveInfinity;
        GameObject target = null;
        foreach (var monster in WaveSpawner.Instance.Enemies)
        {
            if (monster == null)
                continue;

            // Get distance from the actual player game object (_desiredPosition.parent.position)
            float dist = (monster.transform.position - transform.position).sqrMagnitude;

            if (dist < minDistance)
            {
                minDistance = dist;
                target = monster;
            }
        }

        if (target != null)
        {
            // Raycast to see if we have line of sight
            var hit = Physics2D.Raycast(transform.position, (target.transform.position - transform.position).normalized);

            if (hit.collider.gameObject.tag == "monster")
            {
                // We don't have line of sight so return no target
                target = null;
            }

            if (minDistance > Mathf.Pow(targetRadius, 2))
                target = null;
        }

        return target;
    }

    private Vector2 Solve(GameObject targetObj)
    {
        var rb = targetObj.GetComponent<Rigidbody2D>();
        var bulletSpeed = projectile.GetComponent<ProjectileController>().projectileSpeed;

        var a = Vector2.Dot(rb.velocity, rb.velocity) - Mathf.Pow(bulletSpeed, 2);
        var b = 2 * Vector2.Dot(targetObj.transform.position - transform.position, rb.velocity);
        var c = Vector2.Dot(targetObj.transform.position - transform.position, targetObj.transform.position - transform.position);

        float discrim = Mathf.Pow(b, 2) - (4 * a * c);

        if (discrim < 0)
            return new Vector2();

        float t = -1;

        if (discrim > 0)
            t = (-b + Mathf.Sqrt(discrim)) / (2 * a);


        if (t < 0)
            t = (-b - Mathf.Sqrt(discrim)) / (2 * a);
        else
            t = -b / (2 * a);

        return new Vector2(targetObj.transform.position.x, targetObj.transform.position.y) + rb.velocity * t;
    }

    private void FixedUpdate()
    {
        if (_lastShootTime + 1.0f / fireRate < Time.fixedTime)
        {
            var target = GetTarget();

            if (target != null)
            {
                var aimDirection = (Vector3)Solve(target) - transform.position;
                aimDirection.Normalize();

                // Desired aim postion
                var eulerRoation = new Vector3(0, 0, Mathf.Atan2(-aimDirection.y, -aimDirection.x) * Mathf.Rad2Deg);
                transform.rotation = Quaternion.Euler(eulerRoation);

                // Create the projectile rotated in the direction of the ship
                Instantiate(projectile, transform.position, transform.rotation);

                // Play the sound 
                //SoundManager.Instance.PlaySound("tower_shooting", 0.1f, false);

                _lastShootTime = Time.fixedTime;
            }
        }
    }
}
