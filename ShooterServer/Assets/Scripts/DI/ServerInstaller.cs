using UnityEngine;
using Zenject;

public class ServerInstaller : MonoInstaller
{
    [SerializeField] private ServerBehaviour _serverBehaviour;
    [SerializeField] private Map _map;
    [SerializeField] private InfoServerBahaviour _infoServerBahaviour;
    [SerializeField] private PlayerPostionUpdater _playerPostionUpdater;


    public override void InstallBindings()
    {
        BindServerBehaviour();
        BindMap();
        BindInfoServerBehaviour();
        BindPlayerPositionUpdater();
    }

    private void BindServerBehaviour()
    {      
        Container.Bind<ServerBehaviour>().FromInstance(_serverBehaviour).AsSingle();
    }

    private void BindMap()
    {
        Container.Bind<Map>().FromInstance(_map).AsSingle();
    }

    private void BindInfoServerBehaviour()
    {        
        Container.Bind<InfoServerBahaviour>().FromInstance(_infoServerBahaviour).AsSingle();
    }

    private void BindPlayerPositionUpdater()
    {      
        Container.Bind<PlayerPostionUpdater>().FromInstance(_playerPostionUpdater).AsSingle();
    }
}
