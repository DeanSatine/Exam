using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectSpawning : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject spawnableObjectPrefab; 

    private bool isDragging = false;
    private GameObject spawnedCircle;
    private Rigidbody circleRigidbody; 

    private void Start()
    {
        circleRigidbody =  circlePrefab.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDragging && spawnedCircle != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; 
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            spawnedCircle.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == spawnedCircle)
            {
                isDragging = true;
                circleRigidbody.isKinematic = true; 
            }
            else
            {
                isDragging = false;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            circleRigidbody.isKinematic = false; 
            StartCoroutine(SpawnObjectsCoroutine());
        }
    }

    public void OnButtonClick()
    {
        if (spawnedCircle != null)
        {

            Debug.Log("Game has begun!");
        }
        else
        {
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 5f;
            spawnedCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circleRigidbody = spawnedCircle.GetComponent<Rigidbody>();
        }
    }

    private IEnumerator SpawnObjectsCoroutine()
    {
        while (true)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

            Instantiate(spawnableObjectPrefab, spawnedCircle.transform.position + randomOffset, Quaternion.identity);

            yield return new WaitForSeconds(5);
        }
    }
}
