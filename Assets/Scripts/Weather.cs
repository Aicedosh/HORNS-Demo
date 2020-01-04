using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public float TimeToChange;
    public float SunMultiplier;
    public BoolVariable Rains;
    public float MinExp;
    public float MaxExp;
    public float SunSize;

    private bool starts;
    private bool changes;
    private float timeElapsed;

    private RainScript rain;
    private Light sun;
    private float baseIntensity;

    // Start is called before the first frame update
    void Start()
    {
        rain = GetComponent<RainScript>();
        Rains = GetComponent<BoolVariable>();
        sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        baseIntensity = sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            starts = !starts;
            changes = true;
        }

        if(changes)
        {
            timeElapsed += Time.deltaTime * (starts ? 1 : -1);
            timeElapsed = Mathf.Clamp(timeElapsed, 0, TimeToChange);
            float frac = timeElapsed / TimeToChange;

            rain.RainIntensity = frac;
            //sun.intensity = Mathf.Lerp(baseIntensity, baseIntensity * SunMultiplier, frac);

            Camera.main.GetComponent<Skybox>().material.SetFloat("_Exposure", Mathf.Lerp(MaxExp, MinExp, frac));
            Camera.main.GetComponent<Skybox>().material.SetFloat("_SunSize", Mathf.Lerp(SunSize, 0, frac));

            if (timeElapsed == 0 || timeElapsed == TimeToChange)
            {
                Rains.Variable.Value = timeElapsed == TimeToChange;
                changes = false;
            }
        }

    }
}
