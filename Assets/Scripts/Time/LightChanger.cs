using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    public TimeManager time;
   
    [Header("Internal Clock")]
    [SerializeField]
    GameTimestamp timestamp;
    // Start is called before the first frame update
    private Light lightChange;
    void Start()
    {
        
        lightChange = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timestamp = time.TimeGiver();
        if (timestamp.hour >= 8.00 && timestamp.hour < 9.00)
        {
            lightChange.intensity += 0.00015f;
        }
        if(timestamp.hour >= 14 && timestamp.hour <16)
        {
            lightChange.intensity -= 0.0000002f;
        }
        if(timestamp.hour >= 18 && timestamp.hour < 21)
        {
            lightChange.intensity -= 0.0000002f;
        }
    }
}
