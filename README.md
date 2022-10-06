# structARal_NSF 
#This project is supported by the National Science Foundation, and the code is available (open-source) for any educational and academic purposes.

#Project info: 

	Developed by Kexin Wang, kexinw@iastate.edu
	Unity Version: 2020.3.3
	Credit: Mathmaticale calculation formula in this project is credited to David Wehr from previous version of the app
	Source code: https://github.com/kate-Kexin-Wang/structARal_NSF.git
	Packages used in the application: Vuforia Engine AR, Native Gallery for Android & iOS

#Project Content:

	Editor - Vuforia background autogenerated files  
	Image - All image assets used in the project
	Material - All materials used in the project 
	Plugins - Native Gallery for Android & iOS plugin for taking screenshots 
	Resources - Models, prefabs, and Vuforia model target data 
	Scenes - MainMenue, Skywalk, Campanile, CattHall, TownHall 

#Each scene uses a similar scene structure: 

	Main Camera to see the static image 
	AR Camera for image target and model target 
	Directional Light in the scene 
	Canvas and EventSystem for UI elements 
	StaticImage & scene_N for pre-loaded Image and corresponding models, force arrows, etc.
	ImageTarget & scene_I for indoor model AR image target and corresponding models, force arrows, etc. 
	ModelTarget for outdoor model target and corresponding models, force arrows, etc. 

#Script: 

	ARmanager1.cs: AR functionality 
	InterfaceManager: Interface functionality 
	Campanile:
		CamInterfaceManager.cs: Interface functionality in Campanile scene 
		ForceUpdte.cs: Force calculation when inputting wind load or seismic load 
		SeismicLoad.cs & WindLoad.cs: Control input of these values from both slider and touch screen 
	Cattrall: 
		Catt_Interface.cs: Interface functionality in CattHall scene
		PointForce.cs: Force calculation when inputting wind load or Snowfall 
		CattLiveLoad & CattWindLaod.cs: Control input of these values from both slider and touch screen 
	Skywalk:
		Bazier_Curve.cs: Deformation bezier curve functionality
		Live load.cs: Control input of live load from both slider and touch screen
		ReactionForce.cs: Calculation of Reaction Force when inputting live load 
		Skywalk_Interface.cs: Interface functionality in Campanile Scene
	TownHall:
		FixJoinUI.cs: Fix join diagram
		Town_ReactionForce.cs: Calculation of reaction force when inputting live load and wind force
		TownDeflection.cs: Deformation bezier curve functionality
		TownHall_Interface.cs: Interface functionality in TownHall Scene
		TownLiveLoad.cs & TownWindForce.cs: Control input of live load and wind force from both slider and touch screen 


#Additional Notes: 

#The project is built with each module separately with a similar scene structure:

	1. UI interface
	2. Bezier curve functionality for deformation
	3. Load input from both slider and touch screen
	4. The calculation for reaction force or momentum 
#To expand the work to other buildings will need:

	1. Vuforia model target from desired buildings; detailed information can be found at: https://library.vuforia.com/objects/model-targets
	2. Create Vuforia image target from desired images; detailed information can be found at: https://library.vuforia.com/objects/image-targets
	3. Mathematical function to calculate reaction force or momentum from input values 