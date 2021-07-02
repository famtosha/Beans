using System;

[Serializable]
public class SynchronizedObjectDictionary : SerializableDictionary<int, ISynchronizedObject>
{

}
