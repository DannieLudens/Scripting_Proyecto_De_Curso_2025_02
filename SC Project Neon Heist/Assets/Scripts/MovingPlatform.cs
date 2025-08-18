using UnityEngine;

public class movimiento : MonoBehaviour
{
    public Transform[] puntos;
    public float tiempoRecorrido = 5f;

    private int indiceActual = 0;
    private float t = 0f;

    void Start()
    {
        if (puntos != null && puntos.Length > 0)
        {
            transform.position = puntos[0].position;
        }
    }

    void Update()
    {
        MoverPlataforma();
    }

    private void MoverPlataforma()
    {
        if (puntos == null || puntos.Length < 2) return;

        int siguienteIndice = (indiceActual + 1) % puntos.Length;
        float duracion = tiempoRecorrido / puntos.Length;
        t += Time.deltaTime / duracion;
        transform.position = Vector3.Lerp(puntos[indiceActual].position, puntos[siguienteIndice].position, t);

        if (t >= 1f)
        {
            t = 0f;
            indiceActual = siguienteIndice;
        }
    }
}

