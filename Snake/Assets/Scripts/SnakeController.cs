using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPref, tailPrefab;
    private GameObject food;
    private float stepRate, currentAngleZ;
    private Vector2 move;
    private List<Transform> tail = new List<Transform>();
    private bool isFoodEaten;
    private Vector2 lBoarderPos, rBoarderPos, uBoarderPos, dBoarderPos;
    private GameController controller;
    private bool isGameOver, canTurn;
    private float turnTime;

    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        turnTime = Time.time;
        isGameOver = false;
        currentAngleZ = 0.0f;
        stepRate = 0.3f;
        isFoodEaten = false;
        lBoarderPos = GameObject.Find("WallL").transform.position;
        rBoarderPos = GameObject.Find("WallR").transform.position;
        uBoarderPos = GameObject.Find("WallU").transform.position;
        dBoarderPos = GameObject.Find("WallD").transform.position;
        InvokeRepeating("Movement", 0.1f, stepRate); 
        SpawnFood();
    }
    private void Restart() {
            foreach (Transform tailChunk in tail){
                Destroy(tailChunk);
            }
            tail.Clear();
            transform.position = Vector2.zero;
            //controler.score = 0;
    }

    // Update is called once per frame
    private void Update()
    {
       SnakeBehaviour(currentAngleZ);
       if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space)){
        move = Vector2.up;
        controller.startText.color = Color.clear;
       } 
       if (food = null){
        SpawnFood();
       }
    }
    private void SnakeBehaviour(float prevAngleZ){
        SetDirections();
        transform.localEulerAngles = new Vector3(0.0f,0.0f, currentAngleZ);
        if (Time.time > turnTime + 0.2f){
            canTurn = true;
        }
        if(prevAngleZ != currentAngleZ){
            turnTime = Time.time;
            canTurn = false;
        }
        
    }
    private void Movement() {
        Vector2 v = transform.position;
        transform.Translate(move);
        if (isFoodEaten) {
            GameObject g = Instantiate(tailPrefab, v, Quaternion.identity);
           g.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
           tail.Insert(0, g.transform);
           isFoodEaten = false; 
        }
        else if (tail.Count > 0) {
            tail.Last().position = v;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    private void SpawnFood() {
        float x = (int)Random.Range(lBoarderPos.x + 2f, rBoarderPos.x - 2f);
        float y = (int)Random.Range(dBoarderPos.y + 2f, uBoarderPos.y - 2f);
        food = Instantiate(foodPref, new Vector3(x, y, 5f), Quaternion.identity);
    }
    private void SetDirections(){
        if (Input.GetKeyDown(KeyCode.D) && currentAngleZ != 90.0f){
            currentAngleZ = 270.0f;
        }
        else if (Input.GetKeyDown(KeyCode.A) && currentAngleZ != 270.0f){
        
            currentAngleZ = 90.0f;
            }
        else if (Input.GetKeyDown(KeyCode.W) && currentAngleZ != 180.0f){
            currentAngleZ = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentAngleZ != 0.0f){
            currentAngleZ = 180.0f;
        }
            
        }
        private void OnTriggerEnter2D(Collider2D col){
            if (col.gameObject.tag == "Food"){
                Destroy(col.gameObject);
                isFoodEaten = true;
                SpawnFood();
                //gameController.score++;
            }
            if (col.gameObject.name == "WallR" && currentAngleZ == 270.0f){
                transform.position = new Vector2(lBoarderPos.x, transform.position.y);
            }
            else if (col.gameObject.name == "WallL" && currentAngleZ == 90.0f){
                transform.position = new Vector2(rBoarderPos.x, transform.position.y);
            }
            else if (col.gameObject.name == "WallU" && currentAngleZ == 0.0f){
                transform.position = new Vector2(transform.position.x, dBoarderPos.y);
                
            }
            else if (col.gameObject.name == "WallD" && currentAngleZ == 180.0f){
                transform.position = new Vector2(transform.position.x, uBoarderPos.y);
            }

            if (col.gameObject.tag == "Tail"){
                Restart();
            }
        }
        
    }

