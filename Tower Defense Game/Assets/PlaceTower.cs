using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool CanPlaceMonster()
    {
        return tower == null;
    }
    void OnMouseUp()
    {
        if (CanPlaceMonster())
        {
            tower = (GameObject)
              Instantiate(towerPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            //remove gold
        }
    }
}
