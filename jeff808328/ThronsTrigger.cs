using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronsTrigger : MonoBehaviour
{
    private GameObject Thron;

    public float LaunchDelay;

    public float LiveTime;

    public float RaiseTime;

    void Start()
    {
        Thron = this.transform.GetChild(0).gameObject;     

        StartCoroutine(Launch(LaunchDelay));

        StartCoroutine(Close(RaiseTime + LaunchDelay));

        StartCoroutine(Clear(LiveTime + RaiseTime + LaunchDelay));
    }

    private IEnumerator Launch(float Delay)
    {
       // Debug.Log("ready");


        yield return new WaitForSecondsRealtime(Delay);

      //  Debug.Log("launch");

        Thron.GetComponent<ThronMove>().LaunchThron();

        
    }

    private IEnumerator Close(float Raise)
    {
        yield return new WaitForSecondsRealtime(3);

     //   Debug.Log("stop");

        Thron.GetComponent<ThronMove>().StopThron();
    }

    private IEnumerator Clear(float Live)
    {
        yield return new WaitForSecondsRealtime(Live);

        Destroy(this.gameObject);
    }
}
