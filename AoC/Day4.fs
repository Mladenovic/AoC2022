module Day4
open System.IO

type Range = {
    Low: int
    High: int
}

type Assignment = {
    FirstElf: Range
    SecondElf: Range
}

let parseRange (range: string) =
    let ar = range.Split "-"
    { Low = int ar.[0]; High = int ar.[1] }
    
let parseAssignment (assignment: string) = 
    let ar = assignment.Split ","
    { FirstElf = parseRange ar.[0]; SecondElf = parseRange ar.[1] }
    
let input =
    File.ReadAllLines "input/day4.txt"
    |> Array.map parseAssignment
    
let isOneAssignmentCoveredByOther (assignment: Assignment) =
    assignment.FirstElf.Low <= assignment.SecondElf.Low
    && assignment.FirstElf.High >= assignment.SecondElf.High
    || assignment.FirstElf.Low >= assignment.SecondElf.Low
    && assignment.FirstElf.High <= assignment.SecondElf.High
       
let day4Part1Solution =
    input
    |> Array.map isOneAssignmentCoveredByOther
    |> Array.map (fun x -> if x then 1 else 0)
    |> Array.sum
    
let areAssignmentsOverlapping (assignment: Assignment) =
    assignment.FirstElf.Low >= assignment.SecondElf.Low && assignment.FirstElf.Low <= assignment.SecondElf.High
    || assignment.SecondElf.Low >= assignment.FirstElf.Low && assignment.SecondElf.Low <= assignment.FirstElf.High
    || assignment.FirstElf.High >= assignment.SecondElf.Low && assignment.FirstElf.High <= assignment.SecondElf.High
    || assignment.SecondElf.High >= assignment.FirstElf.Low && assignment.SecondElf.High <= assignment.FirstElf.High
    
let day4Part2Solution =
    input
    |> Array.map areAssignmentsOverlapping
    |> Array.map (fun x -> if x then 1 else 0)
    |> Array.sum