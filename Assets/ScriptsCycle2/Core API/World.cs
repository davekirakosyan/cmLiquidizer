using PathCreation;
using System.Collections.Generic;

public static class World<genLevelInfo>
{
    private static int worldNumber;
    private static string worldName;
    private static List<genLevelInfo> levelInfo;
    private static List<PathCreator> pathCreators;

    public static string WorldName { get => worldName; set => worldName = value; }
    public static int WorldNumber { get => worldNumber; set => worldNumber = value; }
    public static List<genLevelInfo> LevelInfo { get => levelInfo; set => levelInfo = value; }
    public static List<PathCreator> PathCreators { get => pathCreators; set => pathCreators = value; }
}
