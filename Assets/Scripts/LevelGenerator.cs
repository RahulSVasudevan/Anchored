using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject SandTile1;
    public GameObject SandTile2;
    public GameObject SandTile3;
    public GameObject MiniSandTile;
    public GameObject Background1;
    public GameObject BGFish1;
    public GameObject BGFish2;
    public GameObject BGFish3;
    public GameObject RockFormtion1;
    public GameObject RockFormtion2;
    public GameObject RockFormtion3;

    // Use this for initialization
    void Start () {
        TerrainGenerator();

    }

    double PRNG()                                 //Pseudo Random Number Generator (Linear Congruential Generator)
    {
        
        double M = 429496729;
        double A = 166452;
        double C = 1;
        double Z = (int)(Random.value * M);
        Z = (A * Z + C) % M;
        return Z / M;
    }


    double Interpolate(double pa, double pb, double px)         //Interpolate Function
    {
        double ft = (1 - Mathf.Cos((float)px * Mathf.PI)) * 0.5;
        return (pa * (1 - ft) + pb * ft);
    }


    void TerrainGenerator()                             //Generates Terrain using the Pseudo Random Number Generator and Interpolate
    {
        // Foreground
        {
            double x = 35, y = 0, a = 0, b = 0, h = 0;//0;
            double wl = 3.5;
            double amp = 2;//3;
            int X = (int)x * 100;

            a = PRNG();
            b = PRNG();

            while (X >= 0)                                                 //Size of level
            {
                x = (double)X / 100;

                if (X % (wl*100)== 0)
                {
               
                    a = b;
                    b = PRNG();
                    //Debug.Log(b);

                    y = h / 2 + a * amp;
                }
                else
                {                
                    y = h / 2 + Interpolate(b, a, ((x % wl)  / wl)) * amp;
                }

                // Randomising tiles
                int randomInt = (int)(Random.value * 4);
                if(randomInt <= 1)
                    Instantiate(SandTile1, new Vector3((float)x, (float)y, 0), Quaternion.identity);
                else if (randomInt == 2)
                    Instantiate(SandTile2, new Vector3((float)x, (float)y, 0), Quaternion.identity);
                else if (randomInt == 3)
                    Instantiate(SandTile3, new Vector3((float)x, (float)y, 0), Quaternion.identity);


                // Background fish
                if (X % (wl * 50) == 0)
                {
                    randomInt = (int)(Random.value * 3);
                    if (randomInt == 0)
                        Instantiate(BGFish1, new Vector3((float)x, Random.Range(2f, 4f), 0), Quaternion.identity);
                    else if (randomInt == 1)
                        Instantiate(BGFish2, new Vector3((float)x, Random.Range(2f, 4f), 0), Quaternion.identity);
                    else if (randomInt == 2)
                        Instantiate(BGFish3, new Vector3((float)x, Random.Range(2f, 4f), 0), Quaternion.identity);
                }


                // Rock Platforms
                if (X % (wl * 100) == 0)
                {
                    if (randomInt == 0)
                        Instantiate(RockFormtion1, new Vector3((float)x, (float)y + 1.9f, 0), Quaternion.identity);
                    else if (randomInt == 1)
                        Instantiate(RockFormtion2, new Vector3((float)x, (float)y + 1.9f, 0), Quaternion.identity);
                    else if (randomInt == 2)
                        Instantiate(RockFormtion3, new Vector3((float)x, (float)y + 1.9f, 0), Quaternion.identity);

                }

                X -= 35;
            
            
            }
        }


        // Background terrain
        {
            double x = 30, y = 0, a = 0, b = 0, h = 2.5;
            double amp = 2, wl = 4;
            int X = (int)x * 100;

            a = PRNG();
            b = PRNG();

            while (X >= 0)                                                 //Size of level
            {
                x = (double)X / 100;

                if (X % (wl * 100) == 0)
                {

                    a = b;
                    b = PRNG();
                    //Debug.Log(b);

                    y = h / 2 + a * amp;
                }
                else
                {
                    y = h / 2 + Interpolate(b, a, ((x % wl) / wl)) * amp;
                }

                

                GameObject g = Instantiate(MiniSandTile, new Vector3((float)x, (float)y, 0), Quaternion.identity);
                g.transform.parent = Background1.transform;

                X -= 20;
            }
        }
    }
}
