using UnityEngine;
using PathCreation;
using System.Collections;
using System.Collections.Generic;

public class WorldAPI<genCardInfo, genInventoryManger, genColor, genElixirAbility> : MonoBehaviour
{
    public World<genCardInfo> CreateWorld(string worldName, int worldNumber, List<genCardInfo> cardInfo, List<PathCreator> pathCreators)
    {
        return new World<genCardInfo>(worldName, worldNumber, cardInfo, pathCreators);
    }

    public void LoadWorld(int worldNumber)
    {
        // Need to instaniate path.
    }
}
