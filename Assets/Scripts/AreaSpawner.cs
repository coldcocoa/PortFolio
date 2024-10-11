using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] areaPrefabs;
    [SerializeField]
    private int spawnAreaCountAtStart = 3; // 게임시작시 생성되는 맵의 수
    [SerializeField]
    private float zDistance = 20; // 맵사이의 거리
    private int areaIndex = 0;

    [SerializeField]
    private Transform playerTransform;
    private void Awake()
    {
        for(int i = 0; i < spawnAreaCountAtStart; ++i)
        {
            if(i==0)
            {
                SpawnArea(false);
            }
            else
            {
                SpawnArea();
            }
        }
    }

    public void SpawnArea(bool isRandom = true)
    {
        GameObject clone  = null; // 오브젝트를 저장하는데 사용

        if(isRandom == false)
        {
            clone = Instantiate(areaPrefabs[0]);
        }
        else
        {
            int index = Random.Range(0,areaPrefabs.Length);
            clone = Instantiate(areaPrefabs[index]);
        }

        clone.transform.position = new Vector3(0, 0, areaIndex * zDistance);
        clone.GetComponent<Area>().SetUp(this,playerTransform);
        areaIndex++;
    }
}
