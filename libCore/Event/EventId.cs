namespace LibCore.Event
{

    public static class EventId
    {
        public const string LoginSuccess = "LoginSuccess";
        public const string ChooseRoleSuccess = "ChooseRoleSuccess";
        public const string EnterScene = "EnterScene";
        public const string ComingNewSceneInfo = "ComingNewSceneInfo";
        public const string PlayerSpawned = "PlayerSpawned";
        public const string PlayerDead = "PlayerDead";
        public const string PreLoadSceneDone = "PreLoadSceneDone";
        public const string ConnectionLost = "ConnectionLost";

        #region From GameEvent eEventType

        public const string EUploadInfo = "UploadInfo";
        public const string EWeaponBeReady = "WeaponBeReady";
        public const string EStartFire = "StartFire";
        public const string EStopFire = "StopFire";
        public const string EDead = "Dead";
        public const string EPopTips = "PopTips";
        public const string ERefreshBagItemById = "RefreshBagItemById";
        public const string EAllHitInfos = "AllHitInfos";
        public const string EEnterGame = "EnterGame";
        public const string EOnSpawnLocalPlayer = "OnSpawnLocalPlayer";
        public const string EItemPickUpOk = "ItemPickUpOk";
        public const string ECameraReset = "CameraReset";
        public const string EInteractUI = "InteractUI";
        public const string EInteract = "Interact";
        public const string ECloseInteractUI = "CloseInteractUI";
        public const string EShowNpcInteractUI = "ShowNpcInteractUI";
        public const string EUpdateNpcInteractUI = "EUpdateNpcInteractUI";
        public const string EHideNpcInteractUI = "HideNpcInteractUI";
        public const string EReleaseNpc = "ReleaseNpc";
        public const string ERequestNpcInteractRes = "RequestNpcInteractRes";
        public const string EReleaseNpcRes = "ReleaseNpcRes";
        public const string EAimCross = "AimCross";
        public const string EInteractStateChange = "InteractStateChange";
        public const string EInteractEnd = "InteractEnd";
        public const string ESubTitle = "SubTitle";
        public const string EGetEnterCarInfo = "GetEnterCarInfo";
        public const string EHideInteractBtn = "HideInteractBtn";
        public const string ESetEnterCarInfo = "SetEnterCarInfo";
        public const string EStartTimer = "StartTimer";
        public const string EAddCountdown = "AddCountdown";
        public const string EStartCountdown = "StartCountdown";
        public const string ESetTargetPed = "SetTargetPed";
        public const string ERefreshUiByNpcStatusOnInteractUpdateInfoChange = "RefreshUiByNpcStatusOnInteractUpdateInfoChange";
        public const string EPlayerExitNpcInteractEvent = "PlayerExitNpcInteractEvent";
        public const string EOnCarryNpcEvent = "OnCarryNpcEvent";
        public const string EPhoneCallNotify = "PhoneCallNotify";
        public const string EShowInteractButton = "ShowInteractButton";
        public const string EChangeNpcInteractState = "ChangeNpcInteractState";
        public const string ENpcInteractStatusRet = "ENpcInteractStatusRet";
        public const string EOnNpcInteractCarry = "EOnNpcInteractCarry";
        public const string EOnNpcStealSuccess = "EOnNpcStealSuccess";
        public const string ENpcStealRiskValueChange = "ENpcStealRiskValueChange";
        public const string EExitInteraction = "ExitInteraction";
        public const string ECIDPhoneEnd = "CIDPhoneEnd";
        public const string EEnterSwim = "EnterSwim";
        public const string EEnterCar = "EnterCar";
        public const string EMovePlayer = "MovePlayer";
        public const string EUpdateTargetID = "UpdateTargetID";
        public const string ERefrshWeapon = "RefrshWeapon";
        public const string ERefreshTeam = "RefreshTeam";
        public const string ERefreshInvitation = "RefreshInvitation";
        public const string EShowJob = "ShowJob";
        public const string ERefreshWeaponList = "RefreshWeaponList";
        public const string EShowWeapon = "ShowWeapon";
        public const string ERefreshBullet = "RefreshBullet";
        public const string ECanSwitchWeapon = "CanSwitchWeapon";
        public const string ESendFeedBack = "SendFeedBack";
        public const string EResetWeaponId = "ResetWeaponId";
        public const string ESelectTask = "SelectTask";
        public const string EPressEsc = "PressEsc";
        public const string ERefreshBulletNum = "RefreshBulletNum";
        public const string EExitChat = "ExitChat";
        public const string ESelectTeam = "SelectTeam";
        public const string ENewInvitations = "NewInvitations";
        public const string EUpdateStreamingDialogue = "UpdateStreamingDialogue";
        public const string EShortMessageNotify = "ShortMessageNotify";
        public const string EChangeWeapon = "ChangeWeapon";
        public const string EExitSwim = "ExitSwim";
        public const string EKilled = "Killed";
        public const string EAnyoneKillAnyone = "AnyoneKillAnyone";
        public const string EAllAreaEventStates = "AllAreaEventStates";
        public const string ERefreshHp = "RefreshHp";
        public const string EInitHp = "InitHp";
        public const string EAllCrashData = "AllCrashData";
        public const string ERotateDis = "RotateDis";
        public const string EInitWeather = "InitWeather";
        public const string EChangeWeather = "ChangeWeather";
        public const string ESetTaskTarget = "SetTaskTarget";
        public const string EHidePhone = "HidePhone";
        public const string EHideTarget = "HideTarget";
        public const string EHitOthers = "HitOthers";
        public const string EUpSurface = "UpSurface";
        public const string EDownSurface = "DownSurface";
        public const string ERefreshDialogue = "RefreshDialogue";
        public const string EDialogueNPC = "DialogueNPC";
        public const string EExitCar = "ExitCar";
        public const string ECarCanUse = "CarCanUse";
        public const string ECanLeaveCar = "CanLeaveCar";
        public const string EScrollRadio = "ScrollRadio";
        public const string EGM_Init = "GM_Init";
        public const string ERemoveButton = "RemoveButton";
        public const string EWantGetOnCar = "WantGetOnCar";
        public const string EHideQuestText = "HideQuestText";
        public const string ELeaveCar = "LeaveCar";
        public const string ESetNPC = "SetNPC";
        public const string ERemoveNPC = "RemoveNPC";
        public const string EShowInputField = "ShowInputField";
        public const string EExitDialogueState = "ExitDialogueState";
        public const string EEnterDialogueState = "EnterDialogueState";
        public const string EShowQuestText = "ShowQuestText";
        public const string EVehicleDamageStageChange = "VehicleDamageStageChange";
        public const string ESendCarDoorPosition = "SendCarDoorPosition";
        public const string EBeAttackedPos = "BeAttackedPos";
        public const string EHitInfo = "HitInfo";
        public const string EClearHitTip = "ClearHitTip";
        public const string EChangeToFpsOrTps = "ChangeToFpsOrTps";
        public const string ENpcDraggedOutVehicleFinish = "NpcDraggedOutVehicleFinish";

        #endregion From GameEvent eEventType

        public const string PlayerEnterState = "PlayerEnterState";
        public const string PlayerExitState = "PlayerExitState";

        #region Input

        public const string InputEnterMode = "InputEnterMode";
        public const string InputExitMode = "InputExitMode";

        #endregion

        #region Panel

        public const string PanelOpen = "PanelOpen";
        public const string PanelClosed = "PanelClosed";

        #endregion

        #region Director Object Management

        public const string DirectorObjectsChanged = "DirectorObjectsChanged";
        public const string DirectorOwnedObjectListUpdated = "DirectorOwnedObjectListUpdated";
        public const string DirectorOwnedObjectListAdded = "DirectorOwnedObjectListAdded";
        public const string DirectorObjectsRemoved = "DirectorObjectsRemoved";

        // Object List
        public const string DirectorTrySwitchObjectList = "DirectorTrySwitchObjectList";

        // Object Add/Remove
        public const string DirectorTryAddObjectToScene = "DirectorTryAddObjectToScene";
        public const string DirectorTryRemoveObjectsFromScene = "DirectorTryRemoveObjectsFromScene";

        // Object Transform
        public const string DirectorTryMoveToward = "DirectorTryMoveToward";
        public const string DirectorTryRotate = "DirectorTryRotate";
        public const string DirectorTryScale = "DirectorTryScale";

        public const string DirectorSetMoveStepsize = "DirectorSetMoveStepsize";
        public const string DirectorSetRotateStepsize = "DirectorSetRotateStepsize";
        public const string DirectorSetScaleStepsize = "DirectorSetScaleStepsize";

        public const string DirectorSetMoveMode = "DirectorSetMoveMode";
        public const string DirectorSetRotateMode = "DirectorSetRotateMode";

        // Copy
        public const string DirectorTryCopy = "DirectorTryCopy";
        public const string DirectorTryAddObjectGroup = "DirectorTryAddObjectGroup";
        public const string DirectorTrySetObjectGroupAddMode = "DirectorTrySetObjectGroupAddMode";

        public const string DirectorPlacedItemButtonClicked = "DirectorPlacedItemButtonClicked";
        public const string DirectorUpdateSelectedObjects = "DirectorUpdateSelectedObjects";

        public const string DirectorTryGetNearestItem = "DirectorTryGetNearestItem";
        public const string DirectorNearestItemFound = "DirectorNearestItemFound";

        // Misc
        public const string DirectorSwitchSelectEffect = "DirectorSwitchSelectEffect";
        public const string DirectorSwitchCollider = "DirectorSwitchCollider";
        public const string DirectorSwitchPhysics = "DirectorSwitchPhysics";

        #endregion

        #region Director Pose Management

        // Action
        public const string DirectorSwitchAction = "DirectorSwitchAction";
        public const string DirectorTrySetAction = "DirectorTrySetAction";

        // Action List
        public const string DirectorSwitchActionList = "DirectorSwitchActionList";

        // Animation Speed
        public const string DirectorSetAnimationSpeed = "DirectorSetAnimationSpeed";

        // Misc
        public const string DirectorSetPlayerView = "DirectorSetPlayerView";
        public const string DirectorSwitchPlayerCollisionWithCharacters = "DirectorSwitchPlayerCollisionWithCharacters";
        public const string DirectorSwitchPlayerPhysics = "DirectorSwitchPlayerPhysics";

        // NPC Control
        public const string DirectorMakeNpcDoSameAction = "DirectorMakeNpcDoSameAction";

        public const string DirectorSetNpcView = "DirectorSetNpcView";
        public const string DirectorResetNpcView = "DirectorResetNpcView";

        public const string DirectorSetNpcOrientation = "DirectorSetNpcOrientation";
        public const string DirectorResetNpcOrientation = "DirectorResetNpcOrientation";

        public const string DirectorMakeNpcFollowPlayer = "DirectorMakeNpcFollowPlayer";
        public const string DirectorMakeNpcStandBy = "DirectorMakeNpcStandBy";
        public const string DirectorOpenNpcAi = "DirectorOpenNpcAi";

        // Player Transform
        public const string DirectorTryMovePlayerToward = "DirectorTryMovePlayerToward";
        public const string DirectorTryRotatePlayer = "DirectorTryRotatePlayer";
        public const string DirectorTryScalePlayer = "DirectorTryScalePlayer";

        public const string DirectorSetPlayerMoveStepsize = "DirectorSetPlayerMoveStepsize";
        public const string DirectorSetPlayerRotateStepsize = "DirectorSetPlayerRotateStepsize";
        public const string DirectorSetPlayerScaleStepsize = "DirectorSetPlayerScaleStepsize";

        public const string DirectorSetPlayerMoveMode = "DirectorSetPlayerMoveMode";

        #endregion

        #region Director NPC Management

        public const string DirectorTrySetNpcControlRange = "DirectorTrySetNpcControlRange";

        #endregion

        #region Director Camera Management

        public const string DirectorSwitchCameraView = "DirectorSwitchCameraView";

        public const string DirectorTryModeCameraToward = "DirectorTryModeCameraToward";
        public const string DirectorTrySetCameraMoveMagnitude = "DirectorTrySetCameraMoveMagnitude";

        public const string DirectorTrySetFov = "DirectorTrySetFov";
        public const string DirectorTrySetCameraMoveMode = "DirectorTrySetCameraMoveMode";
        public const string DirectorTrySetSmoothDampTime = "DirectorTrySetSmoothDampTime";

        public const string DirectorTrySetCameraFilter = "DirectorTrySetCameraFilter";

        #endregion

        #region Director Environment Management

        // Time Control
        public const string DirectorTrySyncTime = "DirectorTrySyncTime";
        public const string DirectorTrySetTime = "DirectorTrySetTime";
        public const string DirectorSetTimeSliderValue = "DirectorSetTimeSliderValue";
        public const string DirectorSuspendSetTimeSliderValue = "DirectorSuspendSetTimeSliderValue";

        // Weather Control
        public const string DirectorSetWeather = "DirectorSetWeather";

        public const string DirectorTrySetObjectMaterial = "DirectorTrySetObjectMaterial";

        #endregion

        #region Director Import Panel

        public const string DirectorShowImportPanel = "DirectorShowImportPanel";
        public const string DirectorHideImportPanel = "DirectorHideImportPanel";
        public const string DirectorSetCurrentPanel = "DirectorSetCurrentPanel";

        #endregion

        #region Phone

        public const string PhoneMusicOver = "PhoneMusicOver";
        public const string PhoneMusicSwitch = "PhoneMusicSwitch";
        public const string ChooseNewResolution = "ChooseNewResolution";
        public const string PhoneEsc = "PhoneEsc";
        public const string PhoneIncomingNpcCall = "PhoneIncomingNpcCall";
        public const string PhoneAnswerNpcCallButtonDown = "PhoneAnswerNpcCallButtonDown";
        public const string PhoneHangUpNpcCallButtonDown = "PhoneHangUpNpcCallButtonDown";
        public const string PhoneOpenEvent = "PhoneOpenEvent";

        #endregion

        #region Weather

        public const string HandleWeatherInfo = "HandleWeatherInfo";

        #endregion

        #region Camera

        public const string SetFPPCameraFov = "SetFPPCameraFov";
        public const string SwitchAimingSight = "SwitchAimingSight";
        public const string ToggleBaseAim = "ToggleBaseAim";
        public const string SwitchAdsAim = "SwitchAdsAim";
        public const string SwitchAimAssist = "SwitchAimAssist";
        public const string SwitchCameraDistance = "SwitchCameraDistance";

        public const string CameraTargetSwitched = "CameraTargetSwitched";

        public const string SetExclusionCollider = "SetExclusionCollider";

        public const string SetCameraCompActive = "SetCameraCompActive";

        public const string WeaponRecoiled = "WeaponRecoiled";

        // Death Mode
        public const string SetMurderer = "SetMurderer";

        //Dialogue Mode
        public const string SwitchCameraView = "SwitchCameraView";

        // Weapon Store Mode/Coffee Shop Mode
        public const string SwitchFocusObject = "SwitchFocusObject";
        public const string SetCameraMoveDirection = "SetCameraMoveDirection";
        public const string SwitchLens = "SwitchLens";

        // Role Create Mode
        public const string SwitchToTakePhotoLens = "SwitchToTakePhotoLens";
        public const string SwitchToSecurityCheckLens = "SwitchToSecurityCheckLens";

        // Occlusion Detection Comp
        public const string ToggleDirectOcclusionDetection = "ToggleDirectOcclusionDetection";

        // Vibrate Comp
        public const string AddVibrateSource = "AddVibrateSource";
        public const string RemoveVibrateSource = "RemoveVibrateSource";

        #endregion

        #region Weapon

        public const string AimCross = "AimCross";
        public const string ResetRecoil = "ResetRecoil";
        public const string ShowCrosshair = "ShowCrosshair";
        public const string UpdateMovementFactor = "UpdateMovementFactor";
        public const string UpdateRecoilFactor = "UpdateRecoilFactor";
        public const string EquipNewWeapon = "EquipNewWeapon";
        public const string EquipSight = "EquipSight";
        public const string UnEquipSight = "UnEquipSight";
        public const string ToggleAdsMode = "ToggleAdsMode";
        public const string ChangeWeaponInHand = "ChangeWeaponInHand";
        public const string AnyWeaponUpdate = "AnyWeaponUpdate";
        public const string ShowPedShieldIcon = "ShowPedShieldIcon";

        #endregion

        #region Explosion

        public const string CreateExplosion = "CreateExplosion";

        #endregion

        #region Drive Shooting

        public const string SetWeaponRecoil = "SetWeaponRecoil";
        public const string TrySwitchWeaponOnVehicle = "TrySwitchWeaponOnVehicle";
        public const string ShowWeaponUiOnVehicle = "ShowWeaponUiOnVehicle";

        #endregion

        #region Radio

        public const string SwitchRadio = "SwitchRadio";

        #endregion

        #region NPCCombat

        public const string NPCEnterCombatFsm = "NPCEnterCombatFsm";
        public const string NPCExitCombatFsm = "NPCExitCombatFsm";

        #endregion

        #region Mission

        public const string MissionUpdateTimerUI = "MissionTimerUI";
        public const string MissionUpdateCounterUI = "MissionCounterUI";
        public const string ShowMissionBanner = "ShowMissionBanner";

        #endregion

        #region Attribute

        public const string PlayerValueAttribChanged = "PlayerValueAttribChanged";
        public const string PlayerPropertyAttribChanged = "PlayerPropertyAttribChanged";

        public const string ShowAttributeUIByType = "ShowAttributeUIByType";
        public const string ShowAllAttributeUI = "ShowAllAttributeUI";

        public const string AttributeHpChanged = "AttributeHpChanged";
        public const string StaminaChanged = "StaminaChanged";             // 不带Net是因为Stamina是本地计算更新的
        public const string StaminaNetChanged = "StaminaNetChanged";       // 网络更新了体力最大值等
        public const string StaminaRecNetChanged = "StaminaRecNetChanged"; // 网络更新了体力最大值等
        public const string StaminaExhausted = "StaminaExhausted";
        public const string HealthNetChanged = "HealthNetChanged";
        public const string SatietyNetChanged = "SatietyNetChanged";
        public const string ThirstyNetChanged = "ThirstyNetChanged";
        public const string RadNetChanged = "RadNetChanged";
        public const string RadThresholdNetChanged = "RadThresholdNetChanged";
        public const string BodyTempChanged = "BodyTempChanged";
        public const string UpperTempChanged = "UpperTempChanged";
        public const string LowerTempChanged = "LowerTempChanged";

        public const string ShowUnShowWarning = "ShowUnShowWarning";
        public const string ShowScreenEffect = "ShowScreenEffect";
        public const string RemoveScreenEffect = "RemoveScreenEffect";

        #endregion

        #region Shop

        public const string CanEnterShop = "CanEnterShop";
        public const string EnterShop = "EnterShop";
        public const string ExitShop = "ExitWeaponShop";
        public const string SwitchSelectItem = "SwitchSelectItem";

        #endregion

        #region WeaponStore

        public const string SwitchSelectWeapon = "SwitchSelectWeapon";
        public const string SwitchSelectAppendix = "SwitchSelectAppendix";
        public const string UnEquippedAppendix = "UnEquippedAppendix";
        public const string ItemLockedTip = "ItemLockedTip";
        public const string NotEnoughMoneyTip = "NoeEnoughMoneyTip";
        public const string PurChaseSuccessTip = "PurChaseSuccessTip";
        public const string StoreConfirm = "StoreConfirm";
        public const string CanEnterWeaponShop = "CanEnterWeaponShop";
        public const string EnterWeaponShop = "EnterWeaponShop";
        public const string ExitWeaponShop = "ExitWeaponShop";
        public const string SendShopEnterPosition = "SendShopEnterPosition";

        #endregion

        #region SubTitle

        public const string SubTitleInCutscene = "SubTitleInCutscene";

        #endregion

        #region Manufacuturer

        public const string CloseManufacturerComputer = "CloseManufacturerComputer";
        public const string StartManufacturerMission = "StartManufacturerMission";

        #endregion

        #region PlayerWanted

        public const string WantedLevelChanged = "WantedLevelChanged";
        public const string OutofPoliceSight = "OutofPoliceSight";
        public const string ComeBackPoliceSight = "ComeBackPoliceSight";
        public const string PlayerWanted = "PlayerWanted";
        public const string PlayerEscaped = "PlayerEscaped";

        #endregion

        #region NPC Data Panel

        public const string EnableNpcDataPanel = "EnableNpcDataPanel";

        #endregion

        #region NPC Schedule

        public const string NpcScheduleStart = "NpcScheduleStart";
        public const string NpcScheduleEnd = "NpcScheduleEnd";

        #endregion

        #region NPC Schedule

        public const string NpcAICommandStart = "NpcAICommandStart";
        public const string NpcAICommandEnd = "NpcAICommandEnd";

        public const string NpcAIUIShow = "NpcAIUIShow";
        public const string NpcAIUIHide = "NpcAIUIHide";

        public const string NpcInteractSelfStart = "NpcInteractSelfStart";
        public const string NpcInteractSelfEnd = "NpcInteractSelfEnd";
        
        #endregion
        
        
        #region MainHud

        public const string InitHp = "InitHp";
        public const string RefreshHp = "RefreshHp";
        public const string ShowSubtitlesFromServer = "ShowSubtitlesFromServer";
        public const string ShowSubtitlesFromTimeline = "ShowSubtitlesFromTimeline";
        public const string ShowSubtitlesFromDialogue = "ShowSubtitlesFromDialogue";
        public const string ClearMainHudTips = "ClearMainHudTips";
        public const string RefreshLv = "RrefreshLv";
        public const string ToggleReticle = "ToggleReticle";
        public const string ShowHeadDamage = "ShowHeadDamage";
        public const string ShowBodyDamage = "ShowBodyDamage";

        #endregion

        #region Map

        public const string ShowMap = "ShowMap";
        public const string SetNewDestination = "SetNewDestination";
        public const string CancelDestination = "CancelDestination";
        public const string UpdateLegendsInfo = "UpdateLegendsInfo";
        public const string MapLoaded = "MapLoaded";

        #endregion

        #region Match

        public const string ShowMatchRoomInfo = "ShowMatchRoomInfo";
        public const string MatchSucceed = "MatchSucceed";
        public const string MissionEnd = "MissionEnd";

        #endregion

        #region WantedSystem

        public const string WantedSystemWantedLevelChange = "WantedSystemWantedLevelChange";
        public const string PlayerArrested = "PlayerArrested";

        #endregion

        #region Common UI

        public const string SwitchCursorState = "ShowCursor";

        #endregion

        #region PropertyStore

        public const string RefreshLandInfo = "RefreshLandInfo";
        public const string SelectPropertyItem = "SelectPropertyItem";
        public const string BoughtNewProperty = "BoughtNewProperty";

        #endregion

        #region Login/Role Choose/Role Create

        // Input Management
        public const string BackToLastPanel = "BackToLastPanel";
        public const string SwitchToNextFunction = "SwitchToNextFunction";
        public const string SwitchToLastFunction = "SwitchToLastFunction";

        public const string RoleChooseSwitchRole = "RoleChooseSwitchRole";

        public const string RoleCreateSwitchFunction = "RoleCreateSwitchFunction";
        public const string RoleCreateSwitchPart = "RoleCreateSwitchPart";
        public const string RoleCreateSwitchAppearance = "RoleCreateSwitchAppearance";

        public const string RoleCreateFaceFinished = "RoleCreateFaceFinished";
        public const string RoleCreateClothesFinished = "RoleCreateClothesFinished";
        public const string RoleCreateSkipCutScene = "RoleCreateSkipCutScene";
        public const string RoleCreateSetProfilePhoto = "RoleCreateSetProfilePhoto";
        public const string RoleCreateProfileVideoEnd = "RoleCreateProfileVideoEnd";
        public const string RoleCreateProfileUiEnd = "RoleCreateProfileUiEnd";

        public const string EnterADomikHouse = "EnterADomikHouse";
        public const string QuitADomikHouse = "QuitADomikHouse";

        #endregion

        #region GameMode Panel

        public const string ShowNotImplementedWindow = "ShowNotImplementedWindow";
        public const string PointerEnteredModeWidget = "PointerEnteredModeWidget";
        public const string ShrinkingWidgetCountChanged = "ShrinkingWidgetCountChanged";

        public const string ShowLoadingWindow = "ShowLoadingWindow";

        #endregion

        #region GM

        public const string ConfirmGM = "ConfirmGM";

        #endregion

        #region DayTimeManager

        public const string DaytimeChanged = "DaytimeChanged";

        #endregion

        #region Destructable Object

        public const string VehicleHitDestructableObject = "VehicleHitDestructableObject";

        #endregion

        #region CutScene

        public const string ShowCutScene = "ShowCutScene";

        # endregion

        #region PropertyStore

        public const string PropertyStoreBack = "PropertyStoreBack";

        #endregion

        #region Team

        // 被邀请者
        public const string ReceiveTeamInvitation = "ReceiveTeamInvitation";
        public const string AcceptTeamInvitation = "AcceptTeamInvitation";
        public const string RejectTeamInvitation = "RejectTeamInvitation";
        public const string TeamInvitationOverTime = "TeamInvitationOverTime";

        // 邀请者
        public const string TeamInviteSent = "TeamInviteSent";
        public const string TeamInviteBusy = "TeamInviteBusy";
        public const string TeamInviteAccepted = "TeamInviteAccepted";
        public const string TeamInviteRejected = "TeamInviteRejected";
        public const string TeamAlreadyFull = "TeamAlreadyFull";

        public const string PressToAccept = "PressToAccept";
        public const string UpdateTeamInfo = "UpdateTeamInfo";
        public const string ShowMainHudTeam = "ShowMainHudTeam";
        public const string EnterNewTeam = "EnterNewTeam";
        public const string LeaveTeam = "LeaveTeam";

        #endregion

        #region NetworkMessage

        public const string PlayerParkOffVehicle = "PlayerParkOffVehicle";
        public const string NpcParkOffVehicle = "NpcParkOffVehicle";

        #endregion

        #region VoiceChat

        public const string PressToTalk = "PressToTalk";
        public const string SwitchVoiceInputMode = "SwitchVoiceChatInputMode";

        #endregion

        #region Setting

        public const string ApplySettingChanges = "ApplySettingChanges";
        public const string SettingSelectNewWidget = "SettingSelectNewWidget";
        public const string SettingSelectLastOrNextOption = "SettingSelectLastOrNextOption";
        public const string SettingSliderValueChanged = "SettingSliderValueChanged";
        public const string SettingSliderDragging = "SettingSliderDragging";
        public const string SettingOnKeyEnter = "SettingOnKeyEnter";
        public const string SettingOnKeyEsc = "SettingOnKeyEsc";

        #endregion

        #region WebBar

        public const string SetCloseAction = "SetCloseAction";

        #endregion

        #region Vehicle Region

        public const string EVehicleShowRepairInfo = "EVehicleShowRepairInfo";
        public const string EPlayerMovingWhenOpenVheicle = "EPlayerMoving";
        public const string EVehicleStartEngine = "EVehicleRepairIng";

        #endregion

        #region InteractableBoxObject

        public const string EShowInteractableObjectPickTips = "EShowInteractableObjectPickTips";

        #endregion

        #region PlayerSelf

        public const string PlayerChangeWeapon = "PlayerChangeWeapon";
        public const string PlayerHandsControlled = "PlayerHandsControlled";
        public const string PlayerInteractRequest = "PlayerInteractRequest";
        public const string PlayerGetOnCarInput = "PlayerGetOnCarInput";
        public const string PlayerGetOffCarInput = "PlayerGetOffCarInput";
        public const string PlayerJumpInput = "PlayerJumpInput";
        public const string PlayerStealthStatus = "PlayerStealthStatus";

        #endregion

        #region Equipmenet

        public const string EquipEquipment = "EquipEquipment";
        public const string UnEquipEquipment = "UnEquipEquipment";

        #endregion

        #region Weapon

        public const string EquipWeapon = "EquipWeapon";
        public const string UnEquipWeapon = "UnEquipWeapon";
        public const string ActiveWeapon = "ActiveWeapon";
        public const string EquipThrowWeapon = "EquipThrowWeapon";
        public const string UnEquipThrowWeapon = "UnEquipThrowWeapon";

        #endregion

        #region Item

        public const string AddItem = "AddItem";
        public const string RemoveItem = "RemoveItem";
        public const string UpdateItem = "UpdateItem";
        public const string AnyItemUpdate = "AnyItemUpdate";
        public const string ShortcutItemUpdate = "ShortcutItemUpdate";

        #endregion

        #region Driving

        public const string PlayerEnterVehicle = "PlayerEnterVehicle";
        public const string PlayerExitVehicle = "PlayerExitVehicle";

        #endregion

        #region BuildSystem

        // UI Event
        public const string SelectBuildItemOnRoulette = "SelectBuildItemOnRoulette";
        public const string ToggleBuildItemTips = "ShowBuildItemTips";
        public const string ToggleBuildItemAlert = "ShowBuildItemAlert";
        public const string ToggleRemovableBuildItem = "ToggleRemovableBuildItem";

        // Trigger Event
        public const string SetSelectedItemBuildable = "SetSelectedItemBuildable";

        // Sticking Point
        public const string StickingTriggered = "StickingTriggered";

        public const string BuildGhostStateChange = "BuildGhostStateChange";

        #endregion

        #region NewBuild

        public const string CancelPlace = "CancelPlace";
        public const string SelectFurniture = "SelectFurniture";
        public const string AddBrickToServerSuccess = "AddBrickToServerSuccess";
        public const string AddBlockToServerSuccess = "AddBlockToServerSuccess";
        public const string RemoveBlockToServerSuccess = "RemoveBlockToServerSuccess";
        public const string RemoveBrickToServer = "RemoveBrickToServer";

        #endregion

        #region Plant

        public const string PlantMatured = "PlantMatured";
        public const string PlantHarvested = "PlantHarvested";

        public const string FocusGrowingPlant = "FocusGrowingPlant";
        public const string StopFocusGrowingPlant = "StopFocusGrowingPlant";

        #endregion

        #region Hyper3000

        public const string ShowHyperGuideTips = "ShowHyperGuideTips";
        public const string HyperSelectNewMission = "HyperSelectNewMission";
        public const string HyperSelectNewSkill = "HyperSelectNewSkill";
        public const string HyperOpenEvent = "HyperOpenEvent";

        #endregion

        #region LoneWolf

        // public const string WolfSelectNewSkill = "WolfSelectNewSkill";
        public const string WolfSelectNewMission = "WolfSelectNewMission";
        public const string WolfOpenEvent = "WolfOpenEvent";

        #endregion

        #region Evacuation

        public const string ShowEvacuationTips = "ShowEvacuationTips";

        #endregion

        #region Friendship

        public const string FriendshipUpdate = "FriendshipUpdate";

        #endregion

        #region Trade

        public const string TradeAddSelfSellItem = "TradeAddSelfSellItem";
        public const string TradeRemoveSelfSellItem = "TradeRemoveSelfSellItem";
        public const string TradeAddOtherSellItem = "TradeAddOtherSellItem";
        public const string TradeRemoveOtherSellItem = "TradeRemoveOtherSellItem";
        public const string TradeReset = "TradeReset";
        public const string TradeStart = "TradeStart";
        public const string TradeEnd = "TradeEnd";

        #endregion

        #region Skill

        public const string SkillExpChanged = "SkillExpChanged";
        public const string SkillLevelUp = "SkillLevelUp";

        #endregion

        #region Task

        public const string TaskStageFinished = "TaskStageFinished";

        #endregion

        #region CommonTips

        public const string ShowCommonTip = "ShowCommonTip";
        public const string HideCommonTip = "HideCommonTip";

        #endregion

        #region NetObject

        public const string OnSpawnNetObject = "OnSpawnNetObject";
        public const string OnDespawnNetObject = "OnDespawnNetObject";

        #endregion

        #region PopupWindow

        public const string ShowPopup = "ShowPopup";

        #endregion

        #region Vehicle Appendix

        public const string VehicleAppendixUpdate = "VehicleAppendixUpdate";
        public const string VehicleFOVDeltaUpdate = "VehicleFOVDeltaUpdate";

        #endregion

        #region WeaponModify

        public const string WeaponModifyAccessoryUpdate = "WeaponModifyAccessoryUpdate";

        #endregion

        #region Buff

        public const string BuffInfoApply = "BuffInfoApply";
        public const string BuffInfoRevoke = "BuffInfoRevoke";

        #endregion
    }

}