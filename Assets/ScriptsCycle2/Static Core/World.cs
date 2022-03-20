using System;
using PathCreation;
using System.Collections.Generic;

public abstract class genWorld<genCardInfo>
{
    protected int worldNumber;
    protected string worldName;

    protected List<genCardInfo> cardInfo;
    protected List<PathCreator> pathCreators;
}

public class World<genCardInfo> : genWorld<genCardInfo>
{
    public string WorldName { get => worldName; set => worldName = value; }
    public int WorldNumber { get => worldNumber; set => worldNumber = value; }
    public List<genCardInfo> CardInfo { get => cardInfo; set => cardInfo = value; }
    public List<PathCreator> PathCreators { get => pathCreators; set => pathCreators = value; }

    public World(string worldName, int worldNumber, List<genCardInfo> cardInfo, List<PathCreator> pathCreators)
    {
        WorldNumber = worldNumber;
        WorldName = worldName ?? throw new ArgumentNullException(nameof(worldName));
        CardInfo = cardInfo ?? throw new ArgumentNullException(nameof(cardInfo));
        PathCreators = pathCreators ?? throw new ArgumentNullException(nameof(pathCreators));
    }
}
