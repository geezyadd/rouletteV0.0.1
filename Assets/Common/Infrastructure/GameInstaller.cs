using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private SpinRewardConfig _spinRewardConfig;
    [SerializeField] private ParticlesConfig _particlesConfig;
    [SerializeField] private CardsConfig _cardsConfig;
    public override void InstallBindings()
    {
        Container.Bind<CardChooseGameModel>().AsSingle();
        Container.Bind<LevelModel>().AsSingle();
        Container.Bind<SpinRewardConfig>().FromInstance(_spinRewardConfig).AsSingle();
        Container.Bind<ParticlesConfig>().FromInstance(_particlesConfig).AsSingle();
        Container.Bind<CardsConfig>().FromInstance(_cardsConfig).AsSingle();
        Container.Bind<GameStateMachine>().AsSingle();
        Container.Bind<GameStateButtonsHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MiniGameSpin>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ParticleEffectsService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SkillsButtonsHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SlotSpinModel>().AsSingle(); 
        Container.Bind<AccountModel>().AsSingle(); 
        Container.Bind<MiniGameSlotModel>().AsSingle(); 
        Container.Bind<ISlotWinCheckerService>().To<SlotWinCheckerService>().AsSingle();
        Container.BindInterfacesAndSelfTo<RewardSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<MenuSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<SkillsSystem>().AsSingle();
    }
}
