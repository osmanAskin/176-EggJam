using System.Collections;
using UnityEngine;

public class RotateLevel : MonoBehaviour
{
    [SerializeField] public Transform levelObject;
    [SerializeField] private Transform centerRotatePosition;

    private float rotationDuration = 0.5f;
    private bool isRotating = false;
    
    public void RotateScreen()
    {
        if (!isRotating)//rotate false ise
        {
            StartCoroutine(RotateAroundPlayer());
        }
    }

    private IEnumerator RotateAroundPlayer()
    {
        isRotating = true;
        float elapsedTime = 0f;
        float totalRotation = 0f; // Toplam döndürme açısı

        // Döndürme süresince toplam -90 derece döndürme
        while (elapsedTime < rotationDuration && totalRotation > -90f)
        {
            // Frame başına döndürme oranı
            float rotationStep = -90 * (Time.deltaTime / rotationDuration); // -90 derece için negatif değer kullanıyoruz

            // Nesneyi player pozisyonu etrafında döndür
            levelObject.RotateAround(centerRotatePosition.position, Vector3.forward, rotationStep);

            totalRotation += rotationStep;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        float remainingRotation = -90f - totalRotation;
        if (remainingRotation != 0)
        {
            levelObject.RotateAround(centerRotatePosition.position, Vector3.forward, remainingRotation);
        }

        isRotating = false;
    }
}
