using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColliderTrigger : MonoBehaviour 
{
    void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.GetComponent<FloorTile>())
        {
            Coll.gameObject.GetComponent<FloorTile>().DoDamage();
        }
        if(Coll.gameObject.GetComponent<Player>())
        {
            if(!Character.Instance.GetIndestructibleArmor)
            Coll.gameObject.GetComponent<Player>().Die();
        }
        if(Coll.gameObject.GetComponent<Bomb>())
        {
            Destroy(Instantiate(GameManager.Instance.GetExplosionPrefab(), new Vector3(Coll.transform.position.x,
                                                            GameManager.Instance.GetExplosionPrefab().transform.position.y,
                                                            Coll.transform.position.z), Quaternion.identity), ExplosionLifeTime.Instance.GetLifeTime);

            Destroy(Coll.gameObject);
        }
        /*
        if(Coll.gameObject.GetComponent<Enemy>())
        {
            GameManager.Instance.NumEnemies--;

            Destroy(Coll.gameObject);

            GameManager.Instance.CheckEnemiesInStage();
        }
        */
    }
}
