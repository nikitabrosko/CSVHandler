Before run any of application's you need to:
1. Find file along the path: DbWorks - ConsoleClient - appsettings.json and change ConnectionString to your's, maybe you need to change path's to directories;
2. Find file along the path: DbWorks - ServiceClient - appsettings.json and change ConnectionString and path's to files to your's;
3. Build solution;

Work with Console application:
1. Add files to source folder;
2. Run ConsoleClient application;
3. Add more files to source folder;
4. Stop ConsoleClient application;

Work with Windows Service:
1. Publish ServiceClient project;
2. Find "bats" folder, "createService" file, then change path to your's published application;
3. Execute "createService" file as administrator to create a service;
4. Add files to source folder;
5. Execute "startService" file as administrator to run a service;
6. Add more files to source folder;
7. Execute "stopService" file as administrator to stop a service;
8. Execute "removeService" file as administrator to delete a service;

if you have problems with run Windows Service, then this might be help you: https://docs.druva.com/Knowledge_Base/Phoenix/How_to/How_to_give_permissions_to_the_system_user_in_SQL_Management_Studio_when_Phoenix_SQL_backup_fails_with_permission_issue
