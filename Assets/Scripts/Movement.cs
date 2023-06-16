using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Rigidbody2D player;
    public GlobalValues values;
    public InputActionAsset asset;
    
    private InputAction touch;
    public bool pressing;
    private int pixel;
    public int MoveDirection;
    private int middle;
    private Vector2 endVector;

    private Coroutine activeCoroutine;

    private void Update(){
        if(values.active == false){
            if(pressing)
                values.StartGame();
            
            return;
        }

        
        if(pressing){
            pixel = (int)touch.ReadValue<Vector2>().x;

            if(pixel < Screen.width / 2 && MoveDirection != -1)
                MoveLeft();
            else if(pixel >= Screen.width / 2 && MoveDirection != 1)
                MoveRight();
        }
        else if(MoveDirection != 0)
            MoveToCenter();
    }

    private void Start(){
        touch = asset.FindAction("Position");
    }

    private IEnumerator MoveTo(int value){
        
        float target = 3 * value;
        float timeToMove = 0.75f / values.current;
        float posX = player.position.x;
        float distance = 0;

        while(timeToMove > distance){
            distance += Time.deltaTime;
            player.position = new Vector2(Mathf.Lerp(posX,target,distance / timeToMove),player.position.y);
            yield return Time.deltaTime;
        }

        player.position = new Vector2(Mathf.Clamp(target,-3,3),player.position.y);
        player.velocity = Vector2.zero;
        activeCoroutine = null;
    }

    public void MoveLeft(){
        MoveDirection = -1;
        
        if(activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        
        activeCoroutine = StartCoroutine(MoveTo(MoveDirection));
    }

    public void MoveRight(){
        MoveDirection = 1;
        
        if(activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        
        activeCoroutine = StartCoroutine(MoveTo(MoveDirection));
    }

    public void MoveToCenter(){
        MoveDirection = 0;
        
        if(activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        activeCoroutine = StartCoroutine(MoveTo(MoveDirection));
    }
    
    public void GetTouch(InputAction.CallbackContext context){

        if(context.phase == InputActionPhase.Started)
            pressing = true;

        if(context.phase == InputActionPhase.Canceled)
            pressing = false;
    }

    private void OnTriggerEnter2D(){
        if(values.active)
            StartCoroutine(values.EndGame());
    }

    private void Awake(){
        middle = Screen.width / 2;
    }
}
