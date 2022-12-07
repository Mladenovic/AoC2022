module Helpers

open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)

    if m.Success then
        Some(List.tail [ for g in m.Groups -> g.Value ])
    else
        None
        
let (|Int64|_|) str =
   match System.Int64.TryParse(str:string) with
   | (true,int) -> Some(int)
   | _ -> None
   
let splitByChar (ch: char) (str: string) = str.Split ch