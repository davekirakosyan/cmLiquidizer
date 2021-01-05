using System;
using PathCreation;
using System.Collections.Generic;

public abstract class genWorld<genLevelInfo>
{
    protected int worldNumber;
    protected string worldName;

    protected List<genLevelInfo> levelInfo;
    protected List<PathCreator> pathCreators;
}

public class World<genLevelInfo> : genWorld<genLevelInfo>
{
    public string WorldName { get => worldName; set => worldName = value; }
    public int WorldNumber { get => worldNumber; set => worldNumber = value; }
    public List<genLevelInfo> LevelInfo { get => levelInfo; set => levelInfo = value; }
    public List<PathCreator> PathCreators { get => pathCreators; set => pathCreators = value; }

    public World(string worldName, int worldNumber, List<genLevelInfo> levelInfo, List<PathCreator> pathCreators)
    {
        WorldNumber = worldNumber;
        WorldName = worldName ?? throw new ArgumentNullException(nameof(worldName));
        LevelInfo = levelInfo ?? throw new ArgumentNullException(nameof(levelInfo));
        PathCreators = pathCreators ?? throw new ArgumentNullException(nameof(pathCreators));
    }
}
