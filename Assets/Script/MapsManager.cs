using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class MapsManager : MonoBehaviour
{
    public RawImage mapImage;

    public string newUrl;

    public string url = "https://goo.gl/maps/Xaf7PYNF8zz4wg2M9";

    [SerializeField]
    public new MapsLocation location; 
    
    // Start is called before the first frame update
    void Start()
    {
        Application.OpenURL(url);
        //StartCoroutine(setDestination());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator setDestination()
    {
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1f);
                maxWait--;
            }

            if (maxWait < 1)
            {
                Debug.Log("Time Out");
                yield break;
            }

            if(Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to get location");
            }
            else
            {
                location.latCoord = Input.location.lastData.latitude;
                location.longCoord = Input.location.lastData.longitude;
            }

            Input.location.Stop();
        }
        else
        {
            Debug.Log("Location Services not enabled");
        }
    }

    public IEnumerator GetLocationRoutine()
    {          
        newUrl = location.url + "center=" + location.latCoord + "," + location.longCoord + "&zoom=" + location.zoom + "&size=" + location.imgSize + "&key=" + location.apiKey;
        using (UnityWebRequest map= UnityWebRequestTexture.GetTexture(newUrl))
        {
            yield return map.SendWebRequest();
            if(map.result == UnityWebRequest.Result.ConnectionError || map.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Map Error: " + map.error);
            }
            mapImage.texture = DownloadHandlerTexture.GetContent(map);
        }
    }

}

[Serializable]
public struct MapsLocation
{
    public string apiKey;
    public float latCoord, longCoord;
    public int zoom;
    public int imgSize;
    public string url;
}
