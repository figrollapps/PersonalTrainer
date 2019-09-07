// How to do this without going mad.
// You need to nuget the correct module (e.g. Fake.DotNet) in the paket command below.
// Then you can Open the namespace like normal F#
// If you update the nuget list, delete build.fsx.lock and the build should run
// paket install to  get the new dependencies.
// Also look out for namespace clashes (e.g. Shell is in fake.core and fake.io.filesystem
// so you need to qualiify it when you call it)

#r "paket:
nuget Fake.Core
nuget Fake.DotNet
nuget Fake.DotNet.MsBuild
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO.Globbing.Operators
open Fake.IO.FileSystemOperators

let buildMode = Environment.environVarOrDefault "buildMode" "Release"
let setParams (defaults:MSBuildParams) =
        { defaults with
            Verbosity = Some(Quiet)
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "DebugSymbols", "True"
                    "Configuration", buildMode
                ]
         }

let projectFiles = 
    !! "src/**/bin"
    ++ "src/**/obj"

let buildFiles = 
    !! "src\PersonalTrainer\bin\x86\Release"
    ++ "content"


for pf in projectFiles do
    printf "file %s" pf

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "src/**/obj"
    ++ "build"
    |> Fake.IO.Shell.cleanDirs
)

Target.create "Build" (fun _ ->
    MSBuild.build setParams "./PersonalTrainer.sln"
)

// Target.create "Build" (fun _ ->
//     !! "src/**/*.*proj"
//     |> Seq.iter (MSBuild.build id)
// )

Target.create "Deploy" (fun _ ->
    !! "src\PersonalTrainer\bin\x86\Release"
    ++ "content"
    |> Fake.IO.Shell.copy "build"
)
// todo need a release task which copies release build to build directory and zips with standard content.

Target.create "All" ignore

"Clean"
  ==> "Build"
  ==> "Deploy"
  ==> "All"

Target.runOrDefault "All"
