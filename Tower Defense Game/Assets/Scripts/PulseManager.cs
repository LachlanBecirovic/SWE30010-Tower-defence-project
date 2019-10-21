using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseManager : MonoBehaviour
{
    public float coolDownTime = 0.0f;
    public GameObject pulseEffect;
    public float pulseRadius = 300.0f;
    public float pulseStrength = 100000.0f;
    public List<GameObject> ammoBalls;

    private int _ammoLeft = 3;
    private float _lastPulseTime = 0.0f;
    private static PulseManager _instance;

	private void Start()
    {
        _instance = this;
	}

    public void ApplyPulse(Vector2 position)
    {
        if (_lastPulseTime + coolDownTime > Time.time || AmmoLeft == 0)
            return;

        Instantiate(pulseEffect, position, Quaternion.identity);

        var meteors = FindObjectsOfType<MeteorController>();
        var pickupItems = FindObjectsOfType<PickupItemController>();

        foreach (var meteor in meteors)
        {
            var toMeteor = meteor.transform.position - (Vector3)position;

            if (toMeteor.sqrMagnitude > pulseRadius * pulseRadius)
                continue;

            toMeteor.Normalize();

            var rb = meteor.GetComponent<Rigidbody2D>();
            rb.AddForce(toMeteor * pulseStrength);
        }

        foreach (var pickup in pickupItems)
        {
            var toPickup = pickup.transform.parent.position - (Vector3)position;

            if (toPickup.sqrMagnitude > pulseRadius * pulseRadius)
                continue;

            toPickup.Normalize();

            var rb = pickup.GetComponentInParent<Rigidbody2D>();
            rb.AddForce(toPickup * pulseStrength);
        }

        AmmoLeft--;
        _lastPulseTime = Time.time;

        SoundManager.Instance.PlaySound("pulse", 0.5f, false);
    }

    public int AmmoLeft
    {
        get
        {
            return _ammoLeft;
        }
        set
        {
            _ammoLeft = Mathf.Clamp(value, 0, 3);

            foreach (var ammoBall in ammoBalls)
                ammoBall.SetActive(false);

            for (int i = 0; i < _ammoLeft; i++)
            {
                ammoBalls[i].SetActive(true);
            }
        }
    }

    public static PulseManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PulseManager>();

            return _instance;
        }
    }
}
