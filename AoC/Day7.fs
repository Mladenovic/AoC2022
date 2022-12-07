module Day7
open System.IO
open Helpers

let lines = File.ReadAllLines "input/day7.txt"

let directorySizes =
    lines
    |> Array.fold
           (fun (path, directories) line ->
                match line |> splitByChar ' '  with
                | [|"$"; "cd"; ".."|] -> List.tail path, directories
                | [|"$"; "cd"; name|] -> name::path, directories
                | [| Int64 num; _ |] ->
                    let rec addFileSize rem dirs =
                        match rem with
                        | [] -> dirs
                        | _::t ->
                            let newDirs =
                                dirs
                                |> Map.change rem (fun x ->
                                    match x with
                                    | Some a -> Some(a + num)
                                    | None -> Some(num))
                            addFileSize t newDirs
                    path, addFileSize path directories
                | _ -> path, directories)
           ([],Map.empty)
    |> snd
    
let day7Part1Solution =
    directorySizes
    |> Map.values
    |> Seq.filter (fun x -> x <= 100000L)
    |> Seq.sum

let usedSpace =
    directorySizes
    |> Map.find ["/"]
let remainingSpace = 70000000L - usedSpace
let neededToClear = 30000000L - remainingSpace

let day7Part2Solution =
    directorySizes
    |> Map.values
    |> Seq.filter (fun x -> x >= neededToClear)
    |> Seq.min