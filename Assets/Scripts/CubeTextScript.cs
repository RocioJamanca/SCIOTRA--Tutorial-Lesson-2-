using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTextScript : MonoBehaviour
{
    [SerializeField]
    double latObject;
    [SerializeField]
    double lonObject;
    
    [SerializeField]  //te asegura que desde otro escript no se puede acceder
    GameObject textDistance;  //Texto donde se visualiza la distancia del objeto 

    Position userLocation = new Position(0f,0f);  
    Position infoLocation = new Position(0f,0f);
    double dist;

    // Start is called before the first frame update
    void Start()
    {
        infoLocation.Latitude = latObject;
        infoLocation.Longitude = lonObject;
        userLocation.Latitude = 0;
        userLocation.Longitude = 0;
        this.transform.GetComponent<Renderer>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Singleton.GetInstance().fakeGpsOn)
        {
            userLocation.Latitude = GpsHandlerScript.fakelat; //GPS falso está activado ponemos las variables del Falso
            userLocation.Longitude = GpsHandlerScript.fakelon;
        }
        else
        {
            if (Input.location.status == LocationServiceStatus.Running)   //está funcionando el GPS real
            {
                userLocation.Latitude = Input.location.lastData.latitude;
                userLocation.Longitude = Input.location.lastData.longitude;
            }else{
                //only for debugging
                userLocation.Latitude = Input.location.lastData.latitude;   //Aquí podemos añadir un mensaje de error para indic ar que éste no funciona
                userLocation.Longitude = Input.location.lastData.longitude;
            }
        }

        dist = CalculateDistance(userLocation, infoLocation);  //Se calcula la distancia 
        if (CalculateDistance(userLocation,infoLocation) < Singleton.GetInstance().GetActionRadius())
        {
            
            this.transform.GetComponent<Renderer>().enabled = true; //    Si cumple la condicion de la posicion de donde está el usuario se muestra en este caso, en otros casos se ocultan
            foreach (Transform child in transform)
            {
                child.transform.GetComponent<Renderer>().enabled = true;  //Todos los hijos se activan en este caso el texto 3D
            }     
        }
        else
        {
            this.transform.GetComponent<Renderer>().enabled = false;  //Coge el mesh render de unity y lo desactiva
            foreach (Transform child in transform)
            {
                child.transform.GetComponent<Renderer>().enabled = false;   //Esto aplica a todos los hijos
            }

            //this.transform.GetComponent<Renderer>().enabled = true;
            //foreach (Transform child in transform)
            //{
            //    child.transform.GetComponent<Renderer>().enabled = true;
            //}
        }
        textDistance.GetComponent<TextMesh>().text = "D: " + dist;
    }

    private class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Position(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
    private double CalculateDistance(Position pointA, Position pointB)
    {
        double earthRadius = 6371;
        double radiantsFactor = System.Math.PI / 180;

        double diffLat = (pointB.Latitude - pointA.Latitude) * radiantsFactor;
        double diffLng = (pointB.Longitude - pointA.Longitude) * radiantsFactor;
        double a = System.Math.Pow(System.Math.Sin(diffLat / 2), 2) +
                    System.Math.Cos(pointA.Latitude * radiantsFactor) *
                    System.Math.Cos(pointB.Latitude * radiantsFactor) *
                    System.Math.Pow(System.Math.Sin(diffLng / 2), 2);
        double c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
        double res = earthRadius * c;
        return res * 1000;
    }

    
}
