using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartupService : MonoBehaviour
{
    [SerializeField]
    private TankSeatingName defaultTankSeatingName = TankSeatingName.RearRight;
    private JetFactory jetFactory;
    private TankFactory tankFactory;
    private ObstacleFactory obstacleFactory;
    private EnvironmentFactory environmentFactory;
    private TankComponent playerTankComponent;

    StartupService()
    {
        this.tankFactory = new TankFactory();
        this.jetFactory = new JetFactory();
        this.obstacleFactory = new ObstacleFactory();
        this.environmentFactory = new EnvironmentFactory();
    }

    void Start()
    {

#if SHORT_PATH_MOVEMENT
            this.playerTankComponent = this.CreateTankComponent(new Vector3(-8f, 3f, 41f), new Vector3(0f, 90f, 0f), "player-short-path");
#else
        this.playerTankComponent = this.CreateTankComponent(new Vector3(11f, 2.5f, -2f), new Vector3(0f, 90f, 0f), "player-full-path");
#endif

        var environmentComponent = this.CreateEnvironmentComponent();
        var menuComponent = this.CreateMenuComponent();
        var npcTankComponents = this.CreateNpcTankComponents();
        var vrPlayerComponent = this.CreatePlayerComponent(menuComponent.getStandingTransform());
        var jetComponents = this.CreateJetComponents();
        var obstacleComponent = this.CreateObstacleComponent();
        var gameLogicService = new GameLogicService(
            menuComponent,
            playerTankComponent,
            vrPlayerComponent,
            npcTankComponents,
            obstacleComponent,
            jetComponents,
            environmentComponent
        );

#if PATH_EDITING
        this.defaultTankSeatingName = TankSeatingName.EditorMode;
#endif

        gameLogicService.Start(defaultTankSeatingName);
    }

    private EnvironmentComponent CreateEnvironmentComponent()
    {
        return this.environmentFactory.CreateEnvironment();
    }

    private MenuComponent CreateMenuComponent()
    {
        var menuGameObject = Instantiate(PrefabProviderUtility.getMenuPrefab());
        return menuGameObject.AddComponent<MenuComponent>();
    }

    private TankComponent CreateTankComponent(Vector3 tankPosition, Vector3 tankRotation, string tankMovementPathSource)
    {
        var tankScaleFactor = 0.1f;

#if PATH_EDITING
        return this.tankFactory.CreateTankWithEditorMovement(new Vector3(74f, 2.5f, 40f), new Vector3(0f, -90f, 0f), tankScaleFactor, "new-tank-movement-data");
#else
        return this.tankFactory.CreateTankWithPathMovement(tankPosition, tankRotation, tankScaleFactor, tankMovementPathSource);
#endif
    }

    private List<TankComponent> CreateNpcTankComponents()
    {
        var npcTankComponents = new List<TankComponent>();
#if !PATH_EDITING
#if SHORT_PATH_MOVEMENT
        {
            npcTankComponents.Add(this.CreateTankComponent(new Vector3(74f, 2.5f, 40f), new Vector3(0f, -90f, 0f), "short-npc-path"));
        }
#else
        {
            npcTankComponents.Add(this.CreateTankComponent(new Vector3(74f, 2.5f, 40f), new Vector3(0f, -90f, 0f), "full-npc-path"));
        }
#endif
#endif
        return npcTankComponents;
    }

    private VrPlayerComponent CreatePlayerComponent(Transform parentTransform)
    {
        var vrplayerFactory = new VrPlayerFactory();

        return vrplayerFactory.CreateWithParentTransform(parentTransform);
    }

    private List<JetComponent> CreateJetComponents()
    {
        return new List<JetComponent>{
            this.CreateJetComponent(new Vector3(147.4f, 42.1f, -453.3f), new Vector3(0f, 0f, 0f)),
            this.CreateJetComponent(new Vector3(153.4f, 36.1f, -469.3f), new Vector3(0f, 0f, 0f)),
            this.CreateJetComponent(new Vector3(159.4f, 49.1f, -487.3f), new Vector3(0f, 0f, 0f)),
            this.CreateJetComponent(new Vector3(130.4f, 43.1f, -600.3f), new Vector3(0f, 0f, 0f)),
            this.CreateJetComponent(new Vector3(110.4f, 49.1f, -620.3f), new Vector3(0f, 0f, 0f)),
            this.CreateJetComponent(new Vector3(160.4f, 58.1f, -635.3f), new Vector3(0f, 0f, 0f)),
        };
    }

    private JetComponent CreateJetComponent(Vector3 jetPosition, Vector3 jetRotation)
    {
        return this.jetFactory.CreateJet(jetPosition, jetRotation, 1, true);
    }

    private ObstacleComponent CreateObstacleComponent()
    {
        return this.obstacleFactory.CreateObstacle();
    }
}