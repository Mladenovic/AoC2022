module Day2
open System.IO

type Rpc =
    | Rock
    | Paper
    | Scissors
    
let playRpc (play1, play2) =
    match play1 with
    | Rock ->
        match play2 with
        | Rock -> 4
        | Paper -> 8
        | Scissors -> 3
    | Paper ->
        match play2 with
        | Rock -> 1
        | Paper -> 5
        | Scissors -> 9
    | Scissors ->
        match play2 with
        | Rock -> 7
        | Paper -> 2
        | Scissors -> 6

let parseLine (line: string) =
    let ar = line.Split " "
    let first =
        match ar.[0] with
        | "A" -> Rock
        | "B" -> Paper
        | "C" -> Scissors
        | _ -> failwith "Wrong input"
    let second =
        match ar.[1] with
        | "X" -> Rock
        | "Y" -> Paper
        | "Z" -> Scissors
        | _ -> failwith "Wrong input"
    first,second
    
let parseLineStrategy (line: string) =
    let ar = line.Split " "
    match ar.[0] with
        | "A" ->
            match ar.[1] with
            | "X" -> Rock,Scissors
            | "Y" -> Rock,Rock
            | "Z" -> Rock,Paper
            | _ -> failwith "Wrong input"
        | "B" ->
            match ar.[1] with
            | "X" -> Paper,Rock
            | "Y" -> Paper,Paper
            | "Z" -> Paper,Scissors
            | _ -> failwith "Wrong input"
        | "C" ->
            match ar.[1] with
            | "X" -> Scissors,Paper
            | "Y" -> Scissors,Scissors
            | "Z" -> Scissors,Rock
            | _ -> failwith "Wrong input"
        | _ -> failwith "Wrong input"
    
let input =
    File.ReadAllLines "input/day2.txt"
    
    
let day2Part1Solution =
    input
    |> Array.map parseLine
    |> Array.map playRpc
    |> Array.sum
    
let day2Part2Solution =
    input
    |> Array.map parseLineStrategy
    |> Array.map playRpc
    |> Array.sum