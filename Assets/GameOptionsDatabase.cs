using UnityEngine;
using System.Collections;

public class GameOptionsDatabase : MonoBehaviour {

	public enum KnightMovementType {Freeform,Chesslike}

	public KnightMovementType KnightMovement{get;set;}
}
