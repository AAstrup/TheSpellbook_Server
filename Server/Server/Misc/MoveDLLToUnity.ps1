Set-ExecutionPolicy RemoteSigned

$targetdirectory = "C:\Users\Bruger\Documents\UnityProjects\HungryHungryCar_Network\Assets\Plugins\"

$clientMMdirectory = "C:\Users\Bruger\Documents\UnityProjects\HungryHungryCar_Server\Server\Server\ClientMatchMaker\bin\Debug\"
$clientMatchdirectory = "C:\Users\Bruger\Documents\UnityProjects\HungryHungryCar_Server\Server\Server\ClientMatch\bin\Debug\"
$clientGMJExtensiondirectory = "C:\Users\Bruger\Documents\UnityProjects\HungryHungryCar_Server\Server\Server\ClientServerSharedGameObjectMessages\bin\Debug\"
$clientDBdirectory = "C:\Users\Bruger\Documents\UnityProjects\HungryHungryCar_Server\Server\Server\ClientDB\bin\Debug\"

Copy-Item -Path $clientMMdirectory\ -Filter *.dll -Destination $targetdirectory\ -Recurse -Force
Copy-Item -Path $clientMatchdirectory\ -Filter *.dll -Destination $targetdirectory\ -Recurse -Force
Copy-Item -Path $clientGMJExtensiondirectory\ -Filter *.dll -Destination $targetdirectory\ -Recurse -Force
Copy-Item -Path $clientDBdirectory\ -Filter *.dll -Destination $targetdirectory\ -Recurse -Force