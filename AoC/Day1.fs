module Day1

open System.IO

let input =
    File.ReadAllLines "input/day1.txt"
    |> Array.toList

let splitOnEmpty list =
    let rec inner (rest: string list) (sum: int) (result: int list) =
        match rest with
        | [] -> sum :: result
        | x :: xs -> if x = "" then inner xs 0 (sum :: result)  else inner xs (sum + int x) result
    
    inner list 0 []
    
let day1SolutionPart1 =
    splitOnEmpty input
    |> List.max
    
let day1SolutionPart2 =
    splitOnEmpty input
    |> List.sortDescending
    |> List.take 3
    |> List.sum