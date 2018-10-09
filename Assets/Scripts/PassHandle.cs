using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
class PassEvent : UnityEvent { }

[System.Serializable]
class PassInt : UnityEvent<int> { }
