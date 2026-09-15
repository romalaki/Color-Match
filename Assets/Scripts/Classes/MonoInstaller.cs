using Scripts;
using Zenject;

public class MonoInstallerGame : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IScoreService>()
            .To<ScoreService>()
            .AsSingle();
        Container.Bind<LevelDifficulty>().AsSingle();
    }
}