using UnityEngine;

public class ReproductorSonido : MonoBehaviour
{
    public static void Reproducir(AudioClip clip, Vector3 posicion)
    {
        if (clip == null) return;

        // Crea un objeto temporal para el sonido
        GameObject tempAudio = new GameObject("SonidoLlaveTemp");
        tempAudio.transform.position = posicion;

        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        audioSource.Play();

        // Evita que la carga del nuevo nivel corte el audio
        DontDestroyOnLoad(tempAudio);

        // Destruye el objeto automáticamente cuando termine de sonar
        Destroy(tempAudio, clip.length);
    }
}