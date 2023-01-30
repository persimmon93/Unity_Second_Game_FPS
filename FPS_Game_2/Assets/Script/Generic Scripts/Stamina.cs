using UnityEngine;

public class Stamina : MonoBehaviour
{
    private float _stamina;
    private bool staminaExhaust;

    // Start is called before the first frame update
    void Start()
    {
        if (_stamina <= 0)
        {
            _stamina = 100f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
