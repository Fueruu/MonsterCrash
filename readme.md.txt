###**GD50PG12 Project Development**
----------

[REPLACE the <data> items with apropriate info and delete [comment items] like this line]

<Description of the app/game - edit and replace this line >

/App       the full game or app, leave empty until day one of Project Dev T5.
/Prototype the prototype of the App, 
           or multiple prototypes if applicable, create prototypes under here in pre-production


####**Download/Install**
---------
 - Path to the Source code in Perforce...
 - \\VFS_Depot\Projects\GD52Pg14\<myapp-folder>\App

 - Perforce config --> READ CAREFULLY
	- Open Perforce visual client [P4V], and choose new workspace.  
          P4 workspaces map a source repo (server) to a folder and user on the local (client) PC.   
          This default is temporary and we will delete it as soon as an apropriate one is created.
	- From the menu, View | Workspaces
	- In the workspaces tab, 
          clear all filters (the filter icon in the upper right of the tab with an x in it).
	- Sort by Workspace name (should be already) A -> Z
	- Select "_CREATE_FROM_ME_FOR_GD48_PROJECTDEV_"
	- Right click and select "Create/Update workspace from _CREATE_FROM_ME_FOR_GD48_PROJECTDEV_"
		- edit the workspace details in the dialog:
		- Create Workspace, give it a name <username>_<machinename>  PRESS OK!
		- In the Workspace root field, enter "C:\Users\<your_username>\Perforce"
		- In the Workspace mappings field (make sure the text icon is selected), 
                  change "projectname" to the name of your project
                  no spaces!

		- press OK!

	- Go get stuff...  Checkout --> edit --> submit.


####**How to run**
--------
< Instructions on how to use the app - edit and replace this line >


####**How to build**
--------
< Instructions on how to use the app - edit and replace this line >
