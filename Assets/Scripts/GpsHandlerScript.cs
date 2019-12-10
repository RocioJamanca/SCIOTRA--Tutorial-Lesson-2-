using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//se puede copiar de la pagina de Unity directamente

//https://docs.unity3d.com/ScriptReference/LocationService.html
//https://docs.unity3d.com/ScriptReference/LocationService.Start.html
//Fake gps
//https://www.reneelab.es/comandos-bcdedit-en-windows-10.html
//https://www.softzone.es/2017/12/09/como-falsificar-tu-ubicacion-en-windows-10/

public class GpsHandlerScript : MonoBehaviour
{
    GameObject textDebugGps;
    GameObject textDebugCompass;

    public static float fakelat;
    public static float fakelon;


    // Start is called before the first frame update
    IEnumerator Start()
    {
       
        fakelat = 41.275508f; //cuando el GPS no funciones se utiliza este
        fakelon = 1.984078f;

        textDebugGps = GameObject.Find("DebugTextGps");
        textDebugCompass = GameObject.Find("DebugTextCompass");  //Aún en esta  FASE 1 no se utiliza

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            Input.compass.enabled = true;
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Singleton.GetInstance().fakeGpsOn)
        {
            textDebugGps.GetComponent<Text>().text = "Location: " + fakelat + " " + fakelon ;
        }
        else
        {
            textDebugGps.GetComponent<Text>().text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude;
        }
        //textDebugCompass.GetComponent<Text>().text = "Compass: " + Input.compass.magneticHeading + "-" + Input.compass.trueHeading;

        

    }
}
