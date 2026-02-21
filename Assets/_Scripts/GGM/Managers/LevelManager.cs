using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    [Header("Level Pieces")]
    public List<LevelPieceBase> levelPiecesStartPrefabs;
    public List<LevelPieceBase> levelPiecesPrefabs;
    public List<LevelPieceBase> levelPiecesEndPrefabs;
    public int piecesStartCount = 2;
    public int piecesCount = 5;
    public int piecesEndCount = 1;
    public float delayToSpawnPieces = 0.1f;


    private int _index;
    private LevelPieceBase _nextLevelPiece;
    private List<LevelPieceBase> _spawnedPiecesPrefabs;



    // Start is called before the first frame update
   // void Start() {}

    // Update is called once per frame
    void Awake()
    {
        CreateLevel();
        //SpawnNextLevel();
    }

    private void SpawnNextLevel()
    {
        if(_nextLevelPiece != null) 
        {
            Destroy(_nextLevelPiece);
            _index++;

            if(_index >= levelPiecesPrefabs.Count) resetLevelIndex();
        }

        _nextLevelPiece = Instantiate(levelPiecesPrefabs[_index], container);
        _nextLevelPiece.transform.localPosition = Vector3.zero;
    }

    public void resetLevelIndex () 
    {
        _index = 0;
    }

    #region Generete Porcedural Level

    private void CreateLevel () 
    {
        StartCoroutine(CreateLevelCoroutine());   
    }

    IEnumerator CreateLevelCoroutine () 
    {
        _spawnedPiecesPrefabs = new List<LevelPieceBase>();
        
        for (int i = 0; i < piecesStartCount; i++)
        {
            GenerateLevel(levelPiecesStartPrefabs);
            yield return new WaitForSeconds(delayToSpawnPieces);
        } 
        
        for (int i = 0; i < piecesCount; i++)
        {
            GenerateLevel(levelPiecesPrefabs);
            yield return new WaitForSeconds(delayToSpawnPieces);
        } 
        
        for (int i = 0; i < piecesEndCount; i++)
        {
            GenerateLevel(levelPiecesEndPrefabs);
            yield return new WaitForSeconds(delayToSpawnPieces);
        }    
    }
    
    private void GenerateLevel(List<LevelPieceBase> list = null)
    {
        var piece = list[Random.Range(0, list.Count)];
        var newPiece = Instantiate(piece, container);

        if(_spawnedPiecesPrefabs.Count > 0) 
        {
            var lastPiece = _spawnedPiecesPrefabs[_spawnedPiecesPrefabs.Count - 1];
            newPiece.transform.position = lastPiece.endPiecePoint.position;
        }


        //newPiece.transform.localPosition = Vector3.zero;
        _spawnedPiecesPrefabs.Add(newPiece);
    }

    #endregion
}
