using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicService
{
    private TankComponent tankComponent;
    private List<TankComponent> npcTankComponents;
    private MenuComponent menuComponent;
    private VrPlayerComponent vrPlayerComponent;
    private TankSeatingName initialTankSeatingName;
    private PlayerDeviceControllerActionType playerActionController;
    private ObstacleComponent obstacleComponent;
    private List<JetComponent> jetComponents;
    private EnvironmentComponent environmentComponent;
    private bool isPlayerAlreadyInsideTank = false;

    public GameLogicService(
        MenuComponent menuComponent,
        TankComponent tankComponent,
        VrPlayerComponent vrPlayerComponent,
        List<TankComponent> npcTankComponents,
        ObstacleComponent obstacleComponent,
        List<JetComponent> jetComponents,
        EnvironmentComponent environmentComponent
    )
    {
        this.menuComponent = menuComponent;
        this.tankComponent = tankComponent;
        this.vrPlayerComponent = vrPlayerComponent;
        this.npcTankComponents = npcTankComponents;
        this.jetComponents = jetComponents;
        this.obstacleComponent = obstacleComponent;
        this.environmentComponent = environmentComponent;
    }

    public async void Start(TankSeatingName defaultTankSeatingName)
    {
        this.initialTankSeatingName = defaultTankSeatingName;

        this.vrPlayerComponent.ChangePlayerScale(0.1f);

        // Give slight delay to ensure everything is set, prepared and loaded.
        await Task.Delay(1000);
        this.ListenToVrPlayerComponentEvents();
        this.ListenToObstacleComponentEvents();
        this.ListenToMenuComponentEvents();
        this.ListenToTankComponentEvents();
    }

    private void ListenToVrPlayerComponentEvents()
    {
        this.vrPlayerComponent.PlayerDeviceControllerActionObservable.Subscribe((PlayerDeviceControllerActionType actionType) =>
        {
            if (!this.isPlayerAlreadyInsideTank)
            {
                return;
            }

            if (actionType == PlayerDeviceControllerActionType.Trigger)
            {
                this.tankComponent.TurretShoot();
            }

            if (actionType == PlayerDeviceControllerActionType.AnalogAxisLeft)
            {
                this.tankComponent.RotateTurretLeft();
            }

            if (actionType == PlayerDeviceControllerActionType.AnalogAxisRight)
            {
                this.tankComponent.RotateTurretRight();
            }

            if (actionType == PlayerDeviceControllerActionType.AnalogAxisReset)
            {
                this.tankComponent.StopTurretRotation();
            }
        });
    }

    private void ListenToObstacleComponentEvents()
    {
        this.obstacleComponent.ObstacleCheckpointReachedObservable.Subscribe(async (ObstacleName obstacleName) =>
        {
            if (obstacleName == ObstacleName.StopTank)
            {
                Debug.Log("Obstacle checkpoint reached. Stop tank movement");
                this.tankComponent.DisableTankMovement();
            }

            if (obstacleName == ObstacleName.OpenGate)
            {
                Debug.Log("Obstacle checkpoint reached. Stop tank movement");
                this.tankComponent.DisableTankMovement();
                await Task.Delay(3000);

                Debug.Log("Jet time!!");
                this.jetComponents.ForEach((jetComponent) =>
                {
                    jetComponent.Show();
                    jetComponent.StartFlying(1.4f);
                });

                await Task.Delay(15000);
                Debug.Log("Opening gate and continue tank movement");
                this.environmentComponent.OpenGate();
                await Task.Delay(3000);
                this.tankComponent.EnableTankMovement();
                Debug.Log("Restarting in 25 seconds");
                await Task.Delay(25000);
                SceneManager.LoadScene("Main");
            }
        });

        this.obstacleComponent.ObstacleShootingIndicatorCompletionObservable.Subscribe(_ =>
        {
            Debug.Log("Shooting obstacle passed. Continue tank movement");
            this.tankComponent.EnableTankMovement();
        });
    }

    private void ListenToMenuComponentEvents()
    {
        // Disable video feature. Only show the image area right now.
        // this.menuComponent.PlayAndWaitVideo().Subscribe(_ =>
        // {
        // Debug.Log("Video in menu has ended");
        this.menuComponent.ShowAndWaitStartGameConfirmation().Subscribe(_ =>
        {
            Debug.Log("Start game is confirmed");

            this.menuComponent.HideMenu();

            Debug.Log("Seat player to tank");

            this.tankComponent.EnableTurretPeriscopeView();
            this.vrPlayerComponent.ChangePlayerScale(0.1f);
            this.vrPlayerComponent.ChangeParentTransform(this.tankComponent.GetSeatingTransformForSeatingName(this.initialTankSeatingName));
            this.isPlayerAlreadyInsideTank = true;
        });
        // });
    }

    private void ListenToTankComponentEvents()
    {
        this.tankComponent.TankSeatingTeleportationObservable.Subscribe(tankSeatingName =>
        {
            Debug.Log($"Change player seating position to {tankSeatingName}");
            this.vrPlayerComponent.ChangeParentTransform(this.tankComponent.GetSeatingTransformForSeatingName(tankSeatingName));
        });

        this.tankComponent.TankStartMovementTriggerObservable.Subscribe(_ =>
        {
            Debug.Log("Start player tank movement");
            this.tankComponent.EnableTankMovement();

            foreach (var component in this.npcTankComponents)
            {
                Debug.Log("Start npc tanks movement");
                component.EnableTankMovement();
            }
        });
    }
}
