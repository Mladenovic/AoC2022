module Day6
open System.IO

let input = File.ReadAllText "input/day6.txt"

let day6Part1Solution =
    (input
    |> List.ofSeq
    |> List.windowed 4
    |> List.map (fun x -> x |> Set.ofList)
    |> List.map (fun x -> x.Count = 4)
    |> List.takeWhile not
    |> List.length) + 4
    
let day6Part2Solution =
    (input
    |> List.ofSeq
    |> List.windowed 14
    |> List.map (fun x -> x |> Set.ofList)
    |> List.map (fun x -> x.Count = 14)
    |> List.takeWhile not
    |> List.length) + 14
    
