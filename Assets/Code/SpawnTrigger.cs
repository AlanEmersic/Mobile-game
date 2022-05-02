using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);        

        if (parent.name == "ground-0" && !CharacterMovement.IsDead)
        {
            Destroy(parent);
            Destroy(GameObject.Find("left-0"));
            Destroy(GameObject.Find("right-0"));
        }
        else if(!CharacterMovement.IsDead)
        {
            ObjectPooler.Instance.AddToGroundPool(parent);
            ObjectPooler.Instance.AddToSidePool(GameObject.Find("left-" + parent.name.Split('-')[1]));
            ObjectPooler.Instance.AddToSidePool(GameObject.Find("right-" + parent.name.Split('-')[1]));
            Spawner.Instance.Spawn();
        }
    }
}
