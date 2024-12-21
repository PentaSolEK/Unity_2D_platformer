using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("SampleScene");
            
        }
    }
}
