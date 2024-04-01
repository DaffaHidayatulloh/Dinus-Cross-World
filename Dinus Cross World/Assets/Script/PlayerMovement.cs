using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 5f;

  void Update()
  {
    float horizontalInput = Input.GetAxis("Horizontal");

           
    transform.Translate(new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0));
  }
}
