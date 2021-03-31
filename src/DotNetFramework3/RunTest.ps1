$msbuildPath = vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | select-object -first 1
if ($msbuildPath) {
  & $msbuildPath .\SignalHandlerTestManaged.sln /p:Configuration=Release /p:Platform="Any CPU"
}

$windowskill=$(Get-ChildItem -Path $env:ProgramData\chocolatey\lib\windows-kill\tools\ -Include *.exe -recurse | % { $_.FullName })
$vcpdll=$(Get-ChildItem -Path "$env:WinDir\system32\msvcp140*.dll" | % { $_.FullName}) + $(Get-ChildItem -Path "$env:WinDir\system32\vcruntime140*.dll" | % { $_.FullName})

Write-Output "Windows-Kill.exe: $windowskill"
Write-Output $vcpdll

$Drive = $(Get-Location).Path
$DriveInfo = New-Object System.IO.DriveInfo($Drive)
if ($DriveInfo.DriveType -eq "Network") {
  Write-Error 'Network drive is not supported. Please copy this sample directory to local drive first.'
  exit
}

mkdir .\windows-kill -Force
Copy-Item $windowskill .\windows-kill\ -Force

foreach ($eachdll in $vcpdll) {
  Copy-Item $eachdll .\windows-kill\ -Force
}

$container='signaltestmgmt'
$dockercmd=$(Get-Command 'docker.exe').Source

& $dockercmd build -t signaltestmgmt:latest .
& $dockercmd rm -f $container

Start-Process -FilePath $dockercmd -ArgumentList "logs -f $container"

& $dockercmd run -dt --name=$container -e WAIT_SECONDS=10 --mount type=bind,source="$((Get-Item '.\windows-kill\').FullName)",target="C:/windows-kill/" signaltestmgmt:latest

Start-Sleep -Seconds 1

$container=$(& $dockercmd inspect --format "{{.Id}}" $container)
$targetpid=$(& $dockercmd inspect --format "{{.State.Pid}}" $container)

Write-Output "Container: $container"
Write-Output "PID: $targetpid"

& $dockercmd exec $container C:\windows-kill\windows-kill.exe -SIGINT $targetpid
