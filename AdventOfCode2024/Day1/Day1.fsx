// https://adventofcode.com/2024/day/1

module Common =
    open System.IO

    let inputLists fileName =
        let dir = __SOURCE_DIRECTORY__
        let inFile = $"{dir}/{fileName}"
        let fileContents = File.ReadAllLines inFile

        let parseLine (line: string) =
            line.Split("   ") |> Array.map int |> (fun a -> (a.[0], a.[1]))

        fileContents |> Array.map parseLine |> Array.unzip

module Part1 =
    let solve fileName =
        let distance pair = fst pair - snd pair |> abs
        let left, right = Common.inputLists fileName

        Array.zip (Array.sort left) (Array.sort right)
        |> Array.map distance
        |> Array.sum

    let exampleResult = solve "example_input.txt"
    let fullResult = solve "input.txt"

module Part2 =
    let solve fileName =
        let left, right = Common.inputLists fileName

        let counts = right |> Array.countBy id |> Map

        let countOrZero n =
            match counts.TryFind n with
            | Some n -> n
            | None -> 0

        left |> Array.map (fun n -> n * countOrZero n) |> Array.sum

    let exampleResult = solve "example_input.txt"
    let fullResult = solve "input.txt"
