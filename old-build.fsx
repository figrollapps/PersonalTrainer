#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile
open Fake.Git
open Fake.FileHelper

let version = "0.9.0"

let buildDir = "./build/"
let contentSourceDir = "./content/"
let contentTargetDir = "./build/content/"

let commitHash = Information.getCurrentSHA1 ".git"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "SetVersions" (fun _ ->
    CreateCSharpAssemblyInfo "./src/CommonAssemblyInfo.cs"
        [Attribute.Product "PERSONAL TRAINER"
         Attribute.Copyright "Copyright (c) 2015, 2016, 2017, 2018 Figroll"
         Attribute.Version version
         Attribute.FileVersion version
         Attribute.Trademark commitHash
         Attribute.ComVisible false]
)

Target "BuildApp" (fun _ ->
    !! "src/**/*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "BuildApp-Output: "
)

Target "PackageContent" (fun _ ->
    CopyRecursive contentSourceDir contentTargetDir true |> Log "Copying content: "
)

Target "Release" (fun _ ->
    trace "App built and packaged for release"
)

"Clean"
  ==> "SetVersions"
  ==> "BuildApp"

"PackageContent"
  ==> "BuildApp"
  ==> "Release"

RunTargetOrDefault "BuildApp"
