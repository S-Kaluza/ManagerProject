namespace ProjDependencyInjection.Mapster;

public interface IMapsterConfiguration
{
    MapsterConfiguration Scan();
    MapsterConfiguration Compile();
}