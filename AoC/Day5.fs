module Day5
open System.Collections
open Microsoft.FSharp.Core
open Parsing 
open System.IO
//I hate this solution

type Command = {
    Number: int
    From: int
    To: int
}

let lines = File.ReadAllLines "input/day5.txt"

let stateString =
    lines
    |> Array.takeWhile (fun x -> not (x.StartsWith ' '))
    

    
let stacks() =
    seq {
        for i in 1..9 ->
            stateString
            |> Array.map (fun x -> x.[1 + (i - 1) * 4])
            |> Array.filter (fun x -> x <> ' ')
            |> Array.rev
    } |> Array.ofSeq
    |> Array.map Stack
    
let commands =
    lines
    |> Array.skipWhile (fun x -> not (x.StartsWith "move"))
    |> Array.map (fun x ->
        match x with
        | Regex "move ([0-9]+) from ([0-9]+) to ([0-9]+)" [ number; from; o ] ->
            {
                Number = int number
                From = int from
                To = int o
            }
        | _ -> failwith "wrong command format")
    
let executeCommand (stacks: Stack[]) (command:Command): Unit =
    for i in 1 .. command.Number do
        let x = stacks.[command.From - 1].Pop()
        stacks.[command.To - 1].Push(x)
        
let day5Part1Solution =
    let stacks = stacks()
    for i in 0..commands.Length - 1 do
        executeCommand stacks commands.[i]
    
    let charAr =
        stacks
        |> Array.map (fun x -> System.Convert.ToChar(x.Peek()))
    
    new string(charAr)
    
let executeCommand9001 (stacks: Stack[]) (command:Command): Unit =
    let tempStack = new Stack()
    for i in 1 .. command.Number do
        let x = stacks.[command.From - 1].Pop()
        tempStack.Push(x)
    for i in 1 .. command.Number do
        let y = tempStack.Pop()
        stacks.[command.To - 1].Push(y)
        
let day5Part2Solution =
    let stacks = stacks()
    for i in 0..commands.Length - 1 do
        executeCommand9001 stacks commands.[i]
    
    let charAr =
        stacks
        |> Array.map (fun x -> System.Convert.ToChar(x.Peek()))
    
    new string(charAr)