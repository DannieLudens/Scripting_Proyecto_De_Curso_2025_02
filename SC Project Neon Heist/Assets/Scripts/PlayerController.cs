using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
       
    }

    void Update()
    {
        float horizontal = 0.0f;
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1.0f;
            horizontal = 1.0f;
        }

        Debug.Log(horizontal);//para observar la variable en la consola de unity
        Vector2 posicion = transform.position;
        posicion.x = posicion.x + 0.1f * horizontal;
        transform.position = posicion;
    }
}
