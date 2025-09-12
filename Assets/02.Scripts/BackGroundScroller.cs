using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [System.Serializable]
    public class ScrollingLayer
    {
        public GameObject layerObject; 
        public float scrollSpeed; 
        public float scrollAmount;
        public Vector3 moveDirection;   
    }

    public ScrollingLayer[] layers;     

    private Vector3[] startPositions; 

    private void Start()
    {
        startPositions = new Vector3[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            startPositions[i] = layers[i].layerObject.transform.position;
        }
    }

    private void Update()
    {
        //if (StageManager.Instance.isScrolling)
        //{
            for (int i = 0; i < layers.Length; i++)
            {
                ScrollingLayer layer = layers[i];
                Vector3 newPosition = layer.layerObject.transform.position + layer.moveDirection * layer.scrollSpeed * Time.deltaTime;

                Vector3 offset = newPosition - startPositions[i];
                float distanceTravelled = Vector3.Dot(offset, layer.moveDirection);

                if (distanceTravelled >= layer.scrollAmount)
                {
                    newPosition = startPositions[i] + layer.moveDirection * (distanceTravelled - layer.scrollAmount);
                }

                layer.layerObject.transform.position = newPosition;
            //}
        }
    }
}
