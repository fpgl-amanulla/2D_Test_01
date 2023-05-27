using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        public float speed;

        [SerializeField] private Transform map;
        [SerializeField] private Collider2D currentTileCollider;

        private readonly List<Collider2D> GridList = new List<Collider2D>();

        private readonly List<Vector2> DirectionList = new()
        {
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(-1, 0),
            new Vector2(0, -1),
            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1),
        };


        private void Start()
        {
            InitGrid();
            InitMap();
        }

        private void InitGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject tile = Instantiate(currentTileCollider.gameObject, map);
                GridList.Add(tile.GetComponent<Collider2D>());
            }

            Destroy(currentTileCollider.gameObject);
            currentTileCollider = GridList[GridList.Count / 2].GetComponent<Collider2D>();
        }

        private void InitMap()
        {
            Collider2D center = currentTileCollider;
            List<Transform> neighbourList = GetNeighbourList();
            Vector3 extents = center.GetComponent<Collider2D>().bounds.extents * 2;

            for (int i = 0; i < DirectionList.Count; i++)
            {
                neighbourList[i].position = center.transform.position + (Vector3) DirectionList[i] * extents.x;
            }
        }

        private List<Transform> GetNeighbourList() =>
            (from t in GridList where currentTileCollider != t select t.transform).ToList();

        private void Update()
        {
            //Key board arrow key input
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            transform.position += new Vector3(x, y) * Time.deltaTime * speed;

            CheckMap();
        }

        private void CheckMap()
        {
            if (currentTileCollider.bounds.Contains(transform.position)) return;

            currentTileCollider = GetCurrentTile();
            InitMap();
        }

        private Collider2D GetCurrentTile() => GridList.FirstOrDefault(t => t.bounds.Contains(transform.position));
    }
}