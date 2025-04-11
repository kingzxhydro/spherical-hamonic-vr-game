using UnityEngine;

public class Textscript : MonoBehaviour
{

    [SerializeField] private GameObject textprefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void showmessage(string message)
    {
        if (textprefab)
        {
            Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            Vector3 textposition = Camera.main.transform.position + Camera.main.transform.forward * 6;
            GameObject prefab = Instantiate(textprefab, textposition, rotation);
            prefab.GetComponentInChildren<TextMesh>().text = message;
        }
    }
}
