using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityADSJ0schi : MonoBehaviour {
    //Скрипт для работы с Unity3d ADS

    public static int opend = 0;
    public string id = "";

    public GameObject adMob = null;

	void Start () {
        /*
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(id);
        }
        else
        {
            Debug.Log("не работает на этом устройстве");
        }
        */
    }
    /*
    void Update() {
        if (adMob.gameObject.active)
        {
            if (Advertisement.IsReady())
            {
                adMob.SetActive(false);
            }
        }
    }
    */

    //показываем рекламу:
    public void schoowADS() {
        /*
        if (Advertisement.IsReady()) {
            Advertisement.Show();
        }
        */
    }
}
