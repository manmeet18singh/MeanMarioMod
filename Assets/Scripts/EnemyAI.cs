using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour {

  public float gravity;
  public Vector2 velocity;
  public bool isWalkingLeft = true;

  public LayerMask floorMask;
  public LayerMask wallMask;

  private bool grounded = false;

  private enum EnemyState {
    walking,
    falling,
    dead
  }

  private EnemyState state = EnemyState.falling;

  // Initialization
  void Start() {

      enabled = false;
    //   Fall();
  }

  // update is called once per frame
  void Update() {
      UpdateEnemyPosition();
  }

  void UpdateEnemyPosition() {
      
      Vector3 pos = transform.localPosition;
      Vector3 scale = transform.localScale;

    //   if (state == EnemyState.falling) {

    //       pos.y += velocity.y * Time.deltaTime;
    //       velocity.y -= gravity * Time.deltaTime;
    //   }

      state = EnemyState.walking;

      if (state == EnemyState.walking) {
          
          if (isWalkingLeft) {
            pos.x -= velocity.x * Time.deltaTime;
            scale.x = -1;
          }
          else {
            pos.x += velocity.x * Time.deltaTime;
            scale.x = 1;
          }
      }

    //   if (velocity.y <= 0) 
        // pos = CheckGround(pos);

      checkWalls(pos, scale.x);

      transform.localPosition = pos;
      transform.localScale = scale;

  }

  Vector3 CheckGround(Vector3 pos) {

      Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - .5f);
      Vector2 originMiddle = new Vector2(pos.x, pos.y - .5f);
      Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - .5f);

      RaycastHit2D groundLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
      RaycastHit2D groundMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
      RaycastHit2D groundRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

      if (groundLeft.collider != null || groundMiddle.collider != null || groundRight.collider != null) {

            RaycastHit2D hitRay = groundLeft;

            if (groundLeft) {
                hitRay = groundLeft;
            } else if (groundMiddle) {
                hitRay = groundMiddle;
            } else if (groundRight) {
                hitRay = groundRight;
            } 

            Debug.Log("Collider center: " + hitRay.collider.bounds.center);
            Debug.Log("Collider size: " + hitRay.collider.bounds.size);

            // pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 0.5f;
            // pos.y = -3;

            state = EnemyState.walking;
            grounded = true;
            velocity.y = 0;

        } else {

            // if (state != EnemyState.falling) {
            //     Fall();
            // }
        }

      return pos;

  }

  void checkWalls(Vector3 pos, float direction) {
      
      Vector2 originTop = new Vector2(pos.x + direction * 0.4f, pos.y + .5f - 0.2f);
      Vector2 originMiddle = new Vector2(pos.x + direction * 0.4f, pos.y);
      Vector2 originBottom = new Vector2(pos.x + direction * 0.4f, pos.y - .5f + 0.2f);

      RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
      RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
      RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

      if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null) {

            RaycastHit2D hitRay = wallTop;

            if (wallTop) {
                hitRay = wallTop;
            } else if (wallMiddle) {
                hitRay = wallMiddle;
            } else if (wallBottom) {
                hitRay = wallBottom;
            } 

            Debug.Log(hitRay.collider.tag);

            if (hitRay.collider.tag == "Player") {
                Debug.Log("HIT MF");
                SceneManager.LoadScene("GameOver");
            }

            isWalkingLeft = !isWalkingLeft;

      }
  }

  void OnBecameVisible() {
      enabled = true;
  }

  void Fall() {
      velocity.y = 0;
      state = EnemyState.falling;
      grounded = false;
  }

}