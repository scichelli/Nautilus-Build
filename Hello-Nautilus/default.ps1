Framework '4.0'

properties {
    $project = "HelloNautilus"
    $birthYear = 2013
    $maintainers = "Sharon Cichelli"
    $description = "A project to be built by Nautilus"

    $configuration = 'Release'
    $src = resolve-path '.\src'
}

task default -depends Compile

task Compile {
  exec { msbuild /t:clean /v:q /nologo /p:Configuration=$configuration $src\$project.sln }
  exec { msbuild /t:build /v:q /nologo /p:Configuration=$configuration $src\$project.sln }
}