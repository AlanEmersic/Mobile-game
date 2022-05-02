using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject sidePrefab;
    public static Spawner Instance { get; private set; }

    float groundLength;
    float groundWidth;
    float lastPositionZ;
    int id;
    bool isFirstGround;

    private void Awake()
    {
        Instance = this;
        groundLength = groundPrefab.transform.Find("Ground").GetComponent<Renderer>().bounds.size.z;
        groundWidth = groundPrefab.transform.Find("Ground").GetComponent<Renderer>().bounds.size.x;
    }

    private void Start()
    {
        isFirstGround = false;        
        ObjectPooler.Instance.GrowPool();
        for (int i = 0; i < 7; i++)
            Spawn();
    }

    public void Spawn()
    {
        float positionZ = lastPositionZ;
        float offset = sidePrefab.transform.Find("Side").GetComponent<Renderer>().bounds.size.x / 2;

        if (!isFirstGround)
        {
            isFirstGround = true;
            GameObject groundObj = Instantiate(groundPrefab);
            groundObj.name = "ground-" + id;

            GameObject leftObj = ObjectPooler.Instance.GetFromSidePool();
            leftObj.transform.position = new Vector3(groundObj.transform.position.x - groundWidth / 2 - offset, groundObj.transform.position.y, positionZ);
            leftObj.name = "left-" + id;

            GameObject rightObj = ObjectPooler.Instance.GetFromSidePool();
            rightObj.transform.position = new Vector3(groundObj.transform.position.x + groundWidth / 2 + offset, groundObj.transform.position.y, positionZ);
            rightObj.transform.rotation = new Quaternion(0, 180, 0, 0);
            rightObj.name = "right-" + id;
        }
        else
        {
            GameObject groundObj = ObjectPooler.Instance.GetFromGroundPool();
            groundObj.transform.position = new Vector3(groundObj.transform.position.x, groundObj.transform.position.y, positionZ);
            groundObj.name = "ground-" + id;

            GameObject leftObj = ObjectPooler.Instance.GetFromSidePool();
            leftObj.transform.position = new Vector3(groundObj.transform.position.x - groundWidth / 2 - offset, groundObj.transform.position.y, positionZ);
            leftObj.name = "left-" + id;

            GameObject rightObj = ObjectPooler.Instance.GetFromSidePool();
            rightObj.transform.position = new Vector3(groundObj.transform.position.x + groundWidth / 2 + offset, groundObj.transform.position.y, positionZ);
            rightObj.transform.rotation = new Quaternion(0, 180, 0, 0);
            rightObj.name = "right-" + id;
        }

        id++;
        lastPositionZ += groundLength;
    }

}
