using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Maximum of 31 items and 1 Unassigned
[Flags]
public enum ECollectibles : int
{
	Unassigned = 0,
	DevTestObject1 = 0x0001, // 1
	DevTestObject2 = 0x0010, // 2
	DevTestObject3 = 0x0100, // 4
	DevTestObject4 = 0x1000  // 8
}
