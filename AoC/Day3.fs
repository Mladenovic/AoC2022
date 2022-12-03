module Day3
open System
open System.IO

let input = File.ReadAllLines "input\day3.txt"

//38 capital 27 lowercase

let calculatePriority ch =
    if Char.IsLower ch then int ch - 96
    else int ch - 38
    
let findCommonItemInRucksack  (itemList: string) =
    let half = itemList.Length / 2 
    let firstCompartmentList =
        itemList.[..(half - 1)]
        |> Set.ofSeq
    let secondCompartmentList =
        itemList.[half..]
        |> Set.ofSeq
    Set.intersect firstCompartmentList secondCompartmentList
    |> Set.toSeq
    |> Seq.head
    
let day3Part1Solution =
    input
    |> Array.map findCommonItemInRucksack
    |> Array.map calculatePriority
    |> Array.sum
    
let findBadge (elfGroup: string[]) =
    elfGroup
    |> Array.map (fun x -> x |> Set.ofSeq)
    |> Array.reduce Set.intersect
    |> Seq.head

let day3Part2Solution =
    input
    |> Array.chunkBySize 3
    |> Array.map findBadge
    |> Array.map calculatePriority
    |> Array.sum