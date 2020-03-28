using UnityEngine;

public class Letter : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LetterManager letterManager = GameObject.Find("LetterManager").GetComponent<LetterManager>();
            letterManager.addLetter(gameObject.name[0]);
            Destroy(gameObject);
        }
    }
}
