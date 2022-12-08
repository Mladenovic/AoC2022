module Day8

open System.IO

let grid =
    File.ReadAllLines "input/day8.txt"
    |> Array.map (fun x -> x |> Seq.map int |> Array.ofSeq)

let visibleFromLeft (arr: int[]) =
    arr
    |> Array.scan
           (fun (_, visibleHeight) height ->
                if height > visibleHeight then (true, height)
                else (false, visibleHeight))
            (false, 0)
    |> Array.tail
    |> Array.map fst

let visibleFromRight (arr: int[]) =
    arr
    |> Array.rev
    |> Array.scan
           (fun (_, visibleHeight) height ->
                if height > visibleHeight then (true, height)
                else (false, visibleHeight))
            (false, 0)
    |> Array.tail
    |> Array.map fst
    |> Array.rev

let invertMatrix (matrix:'a[][])=
    seq {
        for j in 0..matrix.Length - 1 do
            yield seq {
                for i in 0..matrix.[0].Length - 1 do
                    yield matrix.[i].[j]
            } 
    }
    |> Seq.map Array.ofSeq
    |> Array.ofSeq

let visibleFromLeftMatrix =
    grid
    |> Array.map visibleFromLeft
    
let visibleFromRightMatrix =
    grid
    |> Array.map visibleFromRight
    
let visibleFromBelowMatrix =
    grid
    |> invertMatrix
    |> Array.map visibleFromLeft
    |> invertMatrix
    
let visibleFromAboveMatrix =
    grid
    |> invertMatrix
    |> Array.map visibleFromRight
    |> invertMatrix
    
let day8Part1Solution =
    seq {
        for i in 0..(grid.Length - 1) do
            for j in 0..(grid.[0].Length - 1) do
                yield visibleFromLeftMatrix.[i].[j] || visibleFromAboveMatrix.[i].[j] || visibleFromBelowMatrix.[i].[j] || visibleFromRightMatrix.[i].[j]
    }
    |> Seq.filter id
    |> Seq.length
    
let addOneIfLessThan what num =
    if num = what then num
    else num + 1
    
let calculateScenicScore (i:int) (j:int) (grid:int[][]) =
    let leftScore = 
        grid.[i]
        |> Array.take j
        |> Seq.rev
        |> Seq.takeWhile (fun x-> x < grid.[i].[j])
        |> Seq.length
        |> addOneIfLessThan j
    let rightScore = 
        grid.[i]
        |> Array.skip (j + 1)
        |> Seq.takeWhile (fun x-> x < grid.[i].[j])
        |> Seq.length
        |> addOneIfLessThan (grid.[i].Length - j - 1)
    let aboveScore = 
        (grid |> invertMatrix).[j]
        |> Array.take i
        |> Seq.rev
        |> Seq.takeWhile (fun x-> x < grid.[i].[j])
        |> Seq.length
        |> addOneIfLessThan i
    let belowScore = 
        (grid |> invertMatrix).[j]
        |> Array.skip (i + 1)
        |> Seq.takeWhile (fun x-> x < grid.[i].[j])
        |> Seq.length
        |> addOneIfLessThan (grid.Length - i - 1)
    
        
    leftScore * rightScore * aboveScore * belowScore
       
let day8Part2Solution =
    grid
    |> Array.mapi (fun idx x ->
        x |> Array.mapi (fun jdx _ -> calculateScenicScore idx jdx grid))
    |> Array.collect id
    |> Array.max



      